using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.UserImage;
using Zumra.Data;
using Zumra.DTOs.Request;
using Zumra.DTOs.Response;
using Zumra.IRepositories;

namespace Zumra.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageCommandService _imageCommandService;
    private readonly ILogger<UserController> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserController(
        UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork,
        IImageCommandService imageCommandService,
        ILogger<UserController> logger,
        IEmailSender emailSender,
        IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _imageCommandService = imageCommandService;
        _logger = logger;
        _emailSender = emailSender;
        _webHostEnvironment = webHostEnvironment;
    }

    // ==========================================
    // GET Api/User  —  بيانات المستخدم الحالي
    // ==========================================
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return BadRequest(new { success = false, message = "you are not logged in" });

            string? imageUrl = null;
            if (user.ImageId.HasValue && user.ImageId.Value > 0)
            {
                var image = await _unitOfWork.UserImage.GetByIdAsync(user.ImageId.Value);
                imageUrl = image?.ImageUrl;
            }

            var response = new UserRec(
                Email: user.Email,
                Name: user.Name,
                UserName: user.UserName,
                Phone: user.PhoneNumber,
                ImageUrl: imageUrl
            );

            return Ok(new { success = true, Data = response });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred User 1");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred User 1 "
            });
        }
    }

    // ==========================================
    // PUT Api/User  —  تحديث بيانات المستخدم
    // ==========================================
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { success = false, message = "You are not logged in." });

            // التحقق من أن الـ UserName مش مستخدم من حد تاني
            if (!string.IsNullOrWhiteSpace(request.UserName) && request.UserName != user.UserName)
            {
                var existingUser = await _userManager.FindByNameAsync(request.UserName);
                if (existingUser != null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Username is already taken."
                    });
                }
                user.UserName = request.UserName;
            }

            user.Name = request.Name;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Failed to update profile.",
                    errors = result.Errors.Select(e => e.Description)
                });
            }

            return Ok(new
            {
                success = true,
                message = "Profile updated successfully.",
                data = new
                {
                    name = user.Name,
                    userName = user.UserName,
                    phone = user.PhoneNumber,
                    email = user.Email
                }
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred UpdateProfile");
            return StatusCode(500, new { success = false, message = "An error occurred while updating profile." });
        }
    }

    // ==========================================
    // POST Api/User/Email  —  طلب تغيير الإيميل
    // ==========================================
    [HttpPost("Email")]
    [Authorize]
    public async Task<IActionResult> RequestEmailChange([FromBody] ChangeEmailRequest request)
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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { success = false, message = "You are not logged in." });

            // التحقق إن الإيميل الجديد مختلف عن الحالي
            if (user.Email?.ToLower() == request.NewEmail.ToLower())
                return BadRequest(new { success = false, message = "New email is the same as the current email." });

            // التحقق إن الإيميل الجديد مش مستخدم من حد تاني
            var existingUser = await _userManager.FindByEmailAsync(request.NewEmail);
            if (existingUser != null)
                return BadRequest(new { success = false, message = "This email is already in use." });

            // توليد token تغيير الإيميل
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);

            // بناء رابط التأكيد
            var confirmLink = Url.Action(
                nameof(ConfirmEmailChange),
                "User",
                new { token, newEmail = request.NewEmail, userId = user.Id },
                Request.Scheme
            );

            // إرسال الإيميل
            var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Templates", "ConfirmEmail.html");
            string htmlMessage;

            if (System.IO.File.Exists(templatePath))
            {
                var template = await System.IO.File.ReadAllTextAsync(templatePath);
                htmlMessage = template.Replace("{link}", confirmLink);
            }
            else
            {
                // fallback لو التمبليت مش موجود
                htmlMessage = $"<p>Click <a href='{confirmLink}'>here</a> to confirm your new email address.</p>";
            }

            await _emailSender.SendEmailAsync(request.NewEmail, "Confirm your new email", htmlMessage);

            return Ok(new
            {
                success = true,
                message = "A confirmation link has been sent to your new email. Please check your inbox."
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred RequestEmailChange");
            return StatusCode(500, new { success = false, message = "An error occurred while requesting email change." });
        }
    }

    // ==========================================
    // GET Api/User/ConfirmEmail  —  تأكيد تغيير الإيميل
    // ==========================================
    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmailChange(string token, string newEmail, string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Redirect("http://localhost:5000?error=invalid_user");

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);

            if (!result.Succeeded)
            {
                _logger.LogWarning("ConfirmEmailChange failed for user {UserId}: {Errors}",
                    userId, string.Join(", ", result.Errors.Select(e => e.Description)));
                return Redirect("http://localhost:5000?error=invalid_token");
            }

            // تحديث الـ UserName ليطابق الإيميل الجديد (نفس نمط التسجيل)
            user.UserName = newEmail;
            await _userManager.UpdateAsync(user);

            return Redirect("http://localhost:5000?emailChanged=true");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred ConfirmEmailChange for user {UserId}", userId);
            return Redirect("http://localhost:5000?error=email_change_failed");
        }
    }

    // ==========================================
    // POST Api/User/Image  —  رفع صورة جديدة
    // ==========================================
    [HttpPost("Image")]
    [Authorize]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { success = false, message = "you are not logged in" });

            if (image == null || image.Length == 0)
                return BadRequest(new { success = false, message = "No image provided" });

            var userImage = await _imageCommandService.UploadUserImageAsync(image, user.Id);

            // ربط الصورة بالمستخدم
            user.ImageId = userImage.Id;
            await _userManager.UpdateAsync(user);

            return Ok(new { success = true, message = "Image uploaded successfully", imageUrl = userImage.ImageUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred User 2");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred User 2 "
            });
        }
    }

    // ==========================================
    // PUT Api/User/Image  —  تحديث الصورة الحالية
    // ==========================================
    [HttpPut("Image")]
    [Authorize]
    public async Task<IActionResult> UpdateImage([FromForm] IFormFile image)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { success = false, message = "you are not logged in" });

            if (image == null || image.Length == 0)
                return BadRequest(new { success = false, message = "No image provided" });

            var userImage = await _imageCommandService.UpdateUserImageAsync(image, user.ImageId ?? 0);

            // تحديث الـ ImageId لو اتغير
            if (user.ImageId != userImage.Id)
            {
                user.ImageId = userImage.Id;
                await _userManager.UpdateAsync(user);
            }

            return Ok(new { success = true, message = "Image updated successfully", imageUrl = userImage.ImageUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred User 3");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred User 3 "
            });
        }
    }

    // ==========================================
    // DELETE Api/User/Image  —  حذف الصورة
    // ==========================================
    [HttpDelete("Image")]
    [Authorize]
    public async Task<IActionResult> DeleteImage()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized(new { success = false, message = "you are not logged in" });

            if (!user.ImageId.HasValue || user.ImageId.Value == 0)
                return BadRequest(new { success = false, message = "No image to delete" });

            var imageId = user.ImageId.Value;
            user.ImageId = null;
            await _userManager.UpdateAsync(user);

            await _imageCommandService.DeleteUserImageAsync(imageId);

            return Ok(new { success = true, message = "Image deleted successfully" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred User 4");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred User 4 "
            });
        }
    }
}