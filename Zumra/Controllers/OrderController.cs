using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Zumra.Application.Interfaces.Enrollments;
using Zumra.Application.Interfaces.Facility;
using Zumra.Data;

namespace Zumra.Controllers;
[Route("Api/[controller]/[action]")]
[ApiController]
public class OrderController:ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IEnrollmentsCommandService _enrollmentsCommandService;
    private readonly IEnrollmentsQueryService _enrollmentsQueryService;
    private readonly UserManager<ApplicationUser> _userManager;
    public OrderController(ILogger<OrderController> logger, IEnrollmentsCommandService enrollmentsCommandService, IEnrollmentsQueryService enrollmentsQueryService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _enrollmentsCommandService = enrollmentsCommandService;
        _enrollmentsQueryService = enrollmentsQueryService;
        _userManager = userManager;
    }

    [HttpPost("Enroll/{batchId}")]
    [Authorize]
    public async Task<IActionResult> Enroll([FromRoute] int batchId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized(new { success = false, message = "User not authenticated" });
            var userId = user.Id;
            var enrolled = await _enrollmentsCommandService.Create(batchId, userId);
            if (!enrolled){
                return BadRequest(new
                {
                    success = false,
                    message = "Enrollment Failed"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Course Enrolled"
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error enrolling in batch {BatchId}", batchId);
            return StatusCode(500, new { success = false, message = "An error occurred during enrollment" });
        }
    }

    [HttpDelete("Enroll/{enrollId}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int enrollId)
    {
        try
        {
            var enroll = await _enrollmentsCommandService.Delete(enrollId);
            if (!enroll){
                return BadRequest(new
                {
                    success = false,
                    message = "Enrollment Failed To Delete"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Course Delete From  Cart"
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting enrollment {EnrollId}", enrollId);
            return StatusCode(500, new { success = false, message = "An error occurred deleting enrollment" });
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized(new { success = false, message = "User not authenticated" });
            var userId = user.Id;

            var cart = await _enrollmentsQueryService.GetAllAsync(userId);
            return Ok(new
            {
                success = true,
                message = "Get All Cart Successfully",
                Data = cart
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all enrollments");
            return StatusCode(500, new { success = false, message = "An error occurred getting enrollments" });
        }
    }
    
       // ====== Pay (Stripe) ======
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Pay()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized(new { success = false, message = "User not authenticated" });
        string userId = user.Id;

        var cart = await _enrollmentsQueryService.GetAllAsync(userId);
    
        if (cart.Cart.Count()==0) return BadRequest("Cart is empty");
    
        var lineItems = cart.Cart.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "egp",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = item.CourseName,
                    Description = item.BatchTitle
                },
                UnitAmount = (long)(item.CourseCost * 100),
            },
            Quantity = 1
        }).ToList();

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "http://localhost:5173/checkout?status=success",
            CancelUrl = "http://localhost:5173/checkout?status=cancel"
        };
    
        var service = new SessionService();
        var session = await service.CreateAsync(options);
    
        if (string.IsNullOrEmpty(session.Url))
            return StatusCode(500, "Unable to create payment session");
    
        return Ok(new { CheckoutUrl = session.Url });
    }
    
    // ====== success ======
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Success()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized(new { success = false, message = "User not authenticated" });
        string userId = user.Id;
        var cart = await _enrollmentsQueryService.GetAllAsync(userId);
        foreach (var item in cart.Cart)
        {
            await _enrollmentsCommandService.EnrollCourses(item.EnrollmentId);
        }
        return Ok(new { Message = "Payment success" });
    }
    
    


}