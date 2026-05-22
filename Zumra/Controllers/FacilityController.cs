using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Zumra.Application.Interfaces.Facility;
using Zumra.Data;
using Zumra.DTOs.Request.Facility;


namespace Zumra.Controllers;
[Route("Api/[controller]/[action]")]
[ApiController]
public class FacilityController:ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly IFacilityCommandService _facilityCommandService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<FacilityController> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IConfiguration _configuration;


    public FacilityController(IUnitOfWork unitOfWork, IFacilityCommandService facilityCommandService,
        IFacilityQueryService facilityQueryService, UserManager<ApplicationUser> userManager,
        ILogger<FacilityController> logger, IAuthorizationService authorizationService,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _facilityCommandService = facilityCommandService;
        _facilityQueryService = facilityQueryService;
        _userManager = userManager;
        _logger = logger;
        _authorizationService = authorizationService;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var facilities = await _facilityQueryService.GetAllAsync();
            return Ok(new
            {
                success = true,
                data = facilities
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all facilities");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting all facilities"
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(id);
            if (facility == null)
            {
                _logger.LogWarning("Facility not found with id {FacilityId}", id);
                return NotFound(new
                {
                    success = false,
                    message = "Facility Not Found "

                });
            }

            return Ok(new
            {
                success = true,
                message = "Facility retrieved successfully",
                data = facility
            });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving facility with id {FacilityId}", id);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred while retrieving the facility"
            });
        }
        
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromForm] FacilityCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Facility facility=null;

            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                // Get authenticated user first
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _logger.LogWarning("Authenticated user not found for {UserName}", User.Identity?.Name);
                    throw new Exception("User authentication failed");
                }

                // Map request to existing DTO for service
                var facilityDto = new FacilityCreat
                {
                    Name = request.Name,
                    Description = request.Description,
                    Type = request.Type,
                    CategoryId = request.CategoryId,
                    UserId = user.Id
                };

                // Create facility
                facility = await _facilityCommandService.CreatFacilityAsync(facilityDto, request.Image);
                await _unitOfWork.CommitAsync(); // Save to generate ID

                // Create user-facility relationship
                var userFacility = new UserFacility
                {
                    UserId = user.Id,
                    FacilityId = facility.Id,
                    Role = FacilityRole.SuperAdmin,
                    CreatedAt = DateTime.UtcNow,
                };
                
                await _unitOfWork.FacilityUser.CreatAsync(userFacility);
                await _unitOfWork.CommitAsync();
                
                _logger.LogInformation(
                    "Facility {FacilityId} created successfully by user {UserId}", 
                    facility.Id, 
                    user.Id);
            });

            return CreatedAtAction(
                nameof(GetById), 
                new { id = facility!.Id }, 
                new
                {
                    success = true,
                    message = "Facility created successfully",
                    data = new 
                    {
                        id = facility.Id,
                        name = facility.Name,
                        category = facility.CategoryId
                    }
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating facility for user {UserName}", User.Identity?.Name);
            return BadRequest(new
            {
                success = false,
                message = "Failed to creat facility"
            });
        }
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByUserID()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
                return Unauthorized(new { success = false, message = "User not authenticated" });

            // ✅ await مضاف - بيجيب البيانات الفعلية مش الـ Task object
            var facilities = await _facilityQueryService.GetByUserIdAsync(user.Id);
            
            if (!facilities.Any())
                return NotFound(new { success = false, message = "No facilities found for this user" });

            return Ok(new
            {
                success = true,
                data = facilities  // ✅ DTO آمن - بدون circular reference أو بيانات حساسة
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting facilities for user {UserId}", User.Identity?.Name);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting your facilities"
            });
        }
    }
    
    
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromForm] FacilityCreateRequest request)
    {
        try
        {
            // Get the facility from database
            var facility = await _facilityQueryService.GetByIdAsync(id);
            if (facility == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Facility not found"
                });
            }
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin  
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to update facility {FacilityId} without permission",
                    id
                );
                return Forbid();
            }
            
            // Map request to Facility object
            facility.Name = request.Name;
            facility.Description = request.Description;
            facility.Type = request.Type;
            facility.CategoryId = request.CategoryId;
            // Image is handled by service

            await _facilityCommandService.UpdateFacilityAsync(id, facility, request.Image);
            await _unitOfWork.CommitAsync();
            return Ok(new
            {
                success = true,
                message = "Facility updated successfully"
            } ) ;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating facility for user {UserName}", User.Identity?.Name);
            return BadRequest(new
            {
                success = false,
                message = "Failed to update facility"
            });
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            // Get the facility from database
            var facility = await _facilityQueryService.GetByIdAsync(id);
            if (facility == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Facility not found"
                });
            }
            
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to delete facility {FacilityId} without permission",
                    id
                );
                return Forbid();
            }
            
            
            await _facilityCommandService.DeleteFacilityAsync(id);
            await _unitOfWork.CommitAsync();
            return Ok(new
            {
                success = true,
                message = "Facility deleted successfully"
            } ) ;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting facility for user {UserName}", User.Identity?.Name);
            return StatusCode(500, new
            {
                success = false,
                message = "Failed to delete facility"
            });
        }
    }

    // [HttpGet("{id}")]
    private async Task<bool> Check(int id)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(id);
            if (facility == null)
            {
                // return NotFound(new
                // {
                //     success = false,
                //     message = "Facility not found"
                // });
                return false;
            }
            
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to delete facility {FacilityId} without permission",
                    id
                );
                // return Forbid();
                return false;
            }
            // return Ok(new { success = true });
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "This {UserName} is not allowed", User.Identity?.Name);
            return false;
            // return StatusCode(500, new
            // {
            //     success = false,
            //     message = "not allowed"
            // });
        }
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> GiveAccess(AccesReq req)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
                return Unauthorized(new { success = false, message = "User not authenticated" });
            bool ch = await Check(req.FacilityId);
            if (ch)
            {
                var fUser=await _userManager.FindByEmailAsync(req.User);
                req = req with { User = fUser.Id };
                bool isAdded = await _facilityCommandService.GiveAccess(req);
        
                if (isAdded)
                {
                    return Ok(new { success = true, message = "Access granted successfully" });
                }
                else
                {
                    // لو العملية فشلت لأي سبب في الـ Service
                    return BadRequest(new { success = false, message = "Failed to give access" });
                }
            }
            else
            {
                return BadRequest(new { success = false, message = "Facility Error" });
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Add to facility for user {UserId}", User.Identity?.Name);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting your facilities"
            });
        }
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> whoAccess(int id)
    {
        try
        {
            bool ch = await Check(id);
            if (!ch)
            {
                return BadRequest(new { success = false, message = "you are not the superadmin" });
                
            }
            
            var list = await _facilityQueryService.GetByFacilityAsync(id);
            return Ok(new
            {
                success = true,
                data = list
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Add to facility for user {UserId}", User.Identity?.Name);
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting your facilities"
            });
        }
    }
    
    
    
        // // ====== Pay (Stripe) ======
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Pay(int fcId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
                return Unauthorized(new { success = false, message = "User not authenticated" });

            var facilityId = await _unitOfWork.UserFacility.FindByUserID(user.Id,fcId);
            if (facilityId == 0)
                return BadRequest(new { success = false, message = "No facility found for user" });

            var facility = await _facilityQueryService.GetByIdAsync(facilityId);
            if (facility == null)
                return NotFound(new { success = false, message = "Facility not found" });
            
            if (facility.Status == SD.Active)
                return BadRequest(new { success = false, message = "Facility is already active" });

            var lineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Expenses of opening a facility"
                        },
                        UnitAmount = long.Parse(_configuration["Stripe:FacilityPrice"] ?? "25000")
                    },
                    Quantity = 1
                }
            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/Api/Facility/Success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/Api/Facility/Cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "userId", user.Id },
                    { "facilityId", facilityId.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            if (string.IsNullOrEmpty(session.Url))
                return StatusCode(500, new { success = false, message = "Unable to create payment session" });

            return Ok(new { CheckoutUrl = session.Url });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating payment session for user {UserName}", User.Identity?.Name);
            return StatusCode(500, new { success = false, message = "An error occurred creating payment session" });
        }
    }

    // ====== Success ======
    // ====== Success ======
    [HttpGet]
    public async Task<IActionResult> Success([FromQuery] string session_id)
    {
        if (string.IsNullOrEmpty(session_id))
            return BadRequest("Invalid session");

        try
        {
            var service = new SessionService();
            var session = await service.GetAsync(session_id);

            if (session.PaymentStatus != "paid")
                return BadRequest("Payment not completed");

            // Extract userId and facilityId from metadata
            if (session.Metadata == null || 
                !session.Metadata.TryGetValue("userId", out var userId) ||
                !session.Metadata.TryGetValue("facilityId", out var facilityIdStr) ||
                !int.TryParse(facilityIdStr, out var facilityId))
            {
                _logger.LogError("Stripe session {SessionId} missing required metadata", session.Id);
                return BadRequest("Invalid session metadata");
            }

            // Use shared activation logic (Idempotent)
            var activated = await ActivateFacilityAfterPayment(
                userId, 
                facilityId, 
                session.Id, 
                session.AmountTotal ?? 0
            );

            if (!activated)
            {
                return StatusCode(500, "Error activating facility. Please contact support.");
            }

            return Redirect("https://zumra.site/profile");
        }
        catch (StripeException ex)
        {
            _logger.LogWarning(ex, "Invalid Stripe session {SessionId}", session_id);

            return BadRequest(new
            {
                success = false,
                message = "Invalid Stripe session"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing payment success");
            return StatusCode(500, $"Error processing payment: {ex.Message}");
        }
    }

    // ====== Cancel ======
    [HttpGet]
    public IActionResult Cancel()
    {
        return Redirect("https://zumra.site/profile");
    }
    

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var webhookSecret = _configuration["Stripe:WebhookSecret"];
            var signature = Request.Headers["Stripe-Signature"];

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                signature,
                webhookSecret
            );

            // نهمنا بس الـ checkout.session.completed
            if (stripeEvent.Type != "checkout.session.completed")
            {
                return Ok(); // Ignore other events
            }

            var session = stripeEvent.Data.Object as Session;
            if (session == null)
            {
                _logger.LogWarning("Stripe webhook received null session");
                return BadRequest();
            }

            // تأكيد الدفع
            if (session.PaymentStatus != "paid")
            {
                _logger.LogWarning(
                    "Stripe session {SessionId} not paid. Status: {Status}",
                    session.Id,
                    session.PaymentStatus
                );
                return Ok();
            }

            // التحقق من الـ Metadata
            if (session.Metadata == null ||
                !session.Metadata.TryGetValue("userId", out var userId) ||
                !session.Metadata.TryGetValue("facilityId", out var facilityIdStr))
            {
                _logger.LogError(
                    "Stripe session {SessionId} missing metadata",
                    session.Id
                );
                return BadRequest();
            }

            if (!int.TryParse(facilityIdStr, out var facilityId))
            {
                _logger.LogError(
                    "Invalid facilityId in metadata: {FacilityId}",
                    facilityIdStr
                );
                return BadRequest();
            }

            // السعر المتوقع (بالـ cents)
            var expectedAmount = long.Parse(
                _configuration["Stripe:FacilityPrice"] ?? "25000"
            );

            if (session.AmountTotal != expectedAmount)
            {
                _logger.LogError(
                    "Amount mismatch for session {SessionId}. Expected {Expected}, Got {Actual}",
                    session.Id,
                    expectedAmount,
                    session.AmountTotal
                );
                return BadRequest();
            }

            // التفعيل (Idempotent)
            var activated = await ActivateFacilityAfterPayment(
                userId,
                facilityId,
                session.Id,
                session.AmountTotal.Value
            );

            if (!activated)
            {
                _logger.LogError(
                    "Failed to activate facility {FacilityId} for user {UserId}",
                    facilityId,
                    userId
                );
                return StatusCode(500);
            }

            return Ok();
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe webhook error");
            return BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled webhook error");
            return StatusCode(500);
        }
    }


    private async Task<bool> ActivateFacilityAfterPayment(
        string userId,
        int facilityId,
        string sessionId,
        long amount)
    {
        try
        {
            var existingPayment = await _unitOfWork.PayFac.FindBySessionIdAsync(sessionId);
            if (existingPayment != null)
                return true;

            var facility = await _facilityQueryService.GetByIdAsync(facilityId);
            if (facility == null)
                return false;

            // if (facility.UserFacilities.UserId != userId)
            //     return false;

            if (facility.Status == SD.Active)
                return true;

            var expectedAmount = long.Parse(
                _configuration["Stripe:FacilityPrice"] ?? "25000"
            );

            if (amount != expectedAmount)
                return false;

            var pay = new PayFac
            {
                UserId = userId,
                FacilityId = facilityId,
                status = SD.Active,
                PaymentDate = DateTime.UtcNow,
                StripeSessionId = sessionId,
                Amount = amount / 100m
            };

            facility.Status = SD.Active;

            await _unitOfWork.PayFac.CreatAsync(pay);
            await _unitOfWork.Facilities.UpdateAsync(facility);
            await _unitOfWork.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error activating facility {FacilityId} for user {UserId}", facilityId, userId);
            return false;
        }
    }

}