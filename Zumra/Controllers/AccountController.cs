using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zumra.Data;
using Zumra.IRepositories;
using Zumra.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Zumra.DTOs.Request;

namespace Zumra.Controllers;

[Route("Auth/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IRepository<Otp> _otpRepository;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        IRepository<Otp> otpRepository, 
        SignInManager<ApplicationUser> signInManager, 
        IEmailSender emailSender, 
        UserManager<ApplicationUser> userManager, 
        IConfiguration configuration, 
        IWebHostEnvironment webHostEnvironment, 
        IUnitOfWork unitOfWork,
        ILogger<AccountController> logger)
    {
        _otpRepository = otpRepository;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _userManager = userManager;
        _configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data.",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }
            
            var user = await _userManager.FindByNameAsync(request.UserName)
                       ?? await _userManager.FindByEmailAsync(request.UserName);
                       
            if (user == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid username or password."
                });
            }

            var userName = user.UserName ?? user.Email ?? request.UserName;
            var result = await _signInManager.PasswordSignInAsync(
                userName!, 
                request.Password, 
                isPersistent: request.RememberMe, 
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                var token = await GenerateJwtTokenAsync(user);
                return Ok(new
                {
                    success = true,
                    message = "Login successful.",
                    token = token
                });
            }

            if (result.IsLockedOut)
            {
                var lockoutEnd = user.LockoutEnd?.LocalDateTime;
                if (lockoutEnd.HasValue && lockoutEnd.Value > DateTime.Now)
                {
                    var remainingTime = lockoutEnd.Value - DateTime.Now;
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Your account is locked. Please try again after {remainingTime.Minutes} minutes and {remainingTime.Seconds} seconds.",
                        isLockedOut = true,
                        lockoutEnd = lockoutEnd.Value
                    });
                }
                
                return BadRequest(new
                {
                    success = false,
                    message = "Your account is locked. Please try again later.",
                    isLockedOut = true
                });
            }
        
            if (result.IsNotAllowed)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Please confirm your email first.",
                    requiresEmailConfirmation = true
                });
            }

            return BadRequest(new
            {
                success = false,
                message = "Invalid username or password."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for user {UserName}", request.UserName);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred during login.",
                error = ex.Message
            });
        }
    }

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var userRoles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, string.Join(",", userRoles)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest vm)
    {
        try 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data.",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }
            
            var user = vm.Adapt<ApplicationUser>();
            user.UserName = vm.Email;
            var result = await _userManager.CreateAsync(user, vm.Password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user!, SD.UserRole);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(Confirm), "Account", new { token = token, id = user.Id }, Request.Scheme);
                
                var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "ConfirmEmail.html");
                var template = await System.IO.File.ReadAllTextAsync(templatePath);
                var htmlMessage = template.Replace("{link}", link);

                await _emailSender.SendEmailAsync(vm.Email, "Confirm your email", htmlMessage);
                return Ok(new
                {
                    success = true,
                    message = "Registration successful. Please check your email to confirm your account."
                });
            }
            
            return BadRequest(new
            {
                success = false,
                message = "User registration failed.",
                errors = result.Errors.Select(e => e.Description)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for email {Email}", vm.Email);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred during registration.",
                error = ex.Message
            });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Confirm(string token, string id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                // Redirect to a frontend error page or login with error param
                return Redirect("https://zumra.site?error=invalid_user");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return Redirect("https://zumra.site?error=invalid_token");
            }

            // Redirect to frontend login page with success param
            return Redirect("https://zumra.site?confirmed=true");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error confirming email for user ID {UserId}", id);
            return Redirect("https://zumra.site?error=confirmation_failed");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        try
        {
            var email = request.Email;
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user != null)
            {
                string otpCode = new Random().Next(100000, 999999).ToString();
                var otp = new Otp(email, otpCode);
                await _otpRepository.CreatAsync(otp);
                await _unitOfWork.CommitAsync();
                
                var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "ResetPassword.html");
                var template = await System.IO.File.ReadAllTextAsync(templatePath);
                var htmlMessage = template
                    .Replace("{0}", otpCode[0].ToString())
                    .Replace("{1}", otpCode[1].ToString())
                    .Replace("{2}", otpCode[2].ToString())
                    .Replace("{3}", otpCode[3].ToString())
                    .Replace("{4}", otpCode[4].ToString())
                    .Replace("{5}", otpCode[5].ToString());

                await _emailSender.SendEmailAsync(email, "Reset Password", htmlMessage);
                return Ok(new
                {
                    success = true,
                    message = "If the email exists, a password reset code has been sent.",
                    email = email
                });
            }
            
            return Ok(new
            {
                success = true,
                message = "If the email exists, a password reset code has been sent."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ForgotPassword for email");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred processing your request.",
                error = ex.Message
            });
        }
    }
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPRequest request)
    {
        try
        {
            var email = request.Email;
            var otpCode = request.Otp;
            var otp = await _otpRepository.GetOtpAsync(email, otpCode);
            if (otp == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid OTP code."
                });
            }
            
            bool isValid = await _otpRepository.IsOtpExpiredAsync(email, otpCode);
            if (!isValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "OTP code has expired or is invalid."
                });
            }
            
            otp.IsUsed = true;
            await _otpRepository.UpdateAsync(otp);
            await _unitOfWork.CommitAsync();
            
            return Ok(new
            {
                success = true,
                message = "OTP verified successfully. You can now reset your password.",
                email = email
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying OTP for email ");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred during OTP verification.",
                error = ex.Message
            });
        }
    }
    public class VerifyOTPRequest
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid input data.",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }
            
            // Verify OTP was validated before allowing reset
            var otp = await _otpRepository.GetOtpAsync(vm.Email, vm.OtpCode);
            if (otp == null || !otp.IsUsed)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid or unverified OTP. Please verify your OTP first."
                });
            }
            
            var user = await _userManager.FindByEmailAsync(vm.Email);
            if (user == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "User not found."
                });
            }
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, vm.Password);
            
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Password reset failed.",
                    errors = result.Errors.Select(e => e.Description)
                });
            }
            
            return Ok(new
            {
                success = true,
                message = "Password has been reset successfully. You can now log in with your new password."
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resetting password for email {Email}", vm.Email);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred during password reset."
            });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> ResendconfirmEmail(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { success = false, message = "User not found" });
            
            if (!user.EmailConfirmed)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(Confirm), "Account", new { token = token, id = user.Id }, Request.Scheme);
                var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "ConfirmEmail.html");
                var template = await System.IO.File.ReadAllTextAsync(templatePath);
                var htmlMessage = template.Replace("{link}", link);

                await _emailSender.SendEmailAsync(email, "Confirm your email", htmlMessage);
                return Ok(new
                {
                    success = true,
                    message = "Confirmation email sent successfully."
                });
            }
            
            return BadRequest(new
            {
                success = false,
                message = "Email Already Confirmed"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resending confirmation email for {Email}", email);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred while resending confirmation email.",
                error = ex.Message
            });
        }
    }

    [HttpGet]
    public IActionResult ExternalLogin([FromQuery] string provider, [FromQuery] string returnUrl = null)
    {
        try
        {
            // Do NOT pass explicit protocol/host here.
            // ForwardedHeaders middleware rewrites Request.Scheme and Request.Host
            // to the public-facing values (https / zumra.site) before this runs.
            // Passing them explicitly would capture the raw Docker-internal values.
            var redirectUrl = Url.Action(
                nameof(ExternalLoginCallback), 
                "Account", 
                new { returnUrl }
            );
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initiating external login for provider {Provider}", provider);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred initiating external login.",
                error = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
    {
        try
        {
            _logger.LogInformation("ExternalLoginCallback called. ReturnUrl: {ReturnUrl}, RemoteError: {RemoteError}, QueryString: {QueryString}", 
                returnUrl, remoteError, Request.QueryString);

            returnUrl = returnUrl ?? "/";

            if (remoteError != null)
            {
                return BadRequest(new { success = false, message = $"Error from external provider: {remoteError}" });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest(new { success = false, message = "Error loading external login information." });
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return BadRequest(new { success = false, message = "Email not found from external provider." });
            }

            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null)
            {
                var username = info.Principal.FindFirstValue(ClaimTypes.Name) ?? "User";
                user = new ApplicationUser
                {
                    UserName = $"{username.Replace(" ", "").ToLower()}_{Guid.NewGuid().ToString().Substring(0, 8)}",
                    Name = username,
                    Email = email,
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return BadRequest(new { success = false, message = "Error creating user.", errors = createResult.Errors });
                }

                await _userManager.AddToRoleAsync(user, SD.UserRole);
            }

            var logins = await _userManager.GetLoginsAsync(user);
            if (!logins.Any(l => l.LoginProvider == info.LoginProvider))
            {
                var addLoginResult = await _userManager.AddLoginAsync(user, info);
                if (!addLoginResult.Succeeded)
                {
                    return BadRequest(new { success = false, message = "Error linking external login.", errors = addLoginResult.Errors });
                }
            }

            var token = await GenerateJwtTokenAsync(user);

            // If returnUrl is provided and not root, redirect with token
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/")
            {
                var separator = returnUrl.Contains("?") ? "&" : "?";
                var redirectUri = $"{returnUrl}{separator}token={token}&email={user.Email}&username={user.UserName}";
                return Redirect(redirectUri);
            }

            return Ok(new 
            { 
                success = true, 
                message = "Google Login successful", 
                token = token,
                email = user.Email,
                username = user.UserName
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during external login callback");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred during external login.",
                error = ex.Message
            });
        }
    }
}