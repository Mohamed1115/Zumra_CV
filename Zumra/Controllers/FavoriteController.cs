using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Zumra.Application.Interfaces.Favorite;

namespace Zumra.Controllers;

[Route("Api/[controller]")]
[ApiController]
[Authorize]
public class FavoriteController : ControllerBase
{
    private readonly IFavoriteCommandService _favoriteCommandService;
    private readonly IFavoriteQueryService _favoriteQueryService;
    private readonly ILogger<FavoriteController> _logger;

    public FavoriteController(
        IFavoriteCommandService favoriteCommandService,
        IFavoriteQueryService favoriteQueryService,
        ILogger<FavoriteController> logger)
    {
        _favoriteCommandService = favoriteCommandService;
        _favoriteQueryService = favoriteQueryService;
        _logger = logger;
    }

    // GET Api/Favorite — Get all favorites for the current user
    [HttpGet]
    public async Task<IActionResult> GetMyFavorites()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { success = false, message = "User not found" });

            var favorites = await _favoriteQueryService.GetUserFavorites(userId);
            return Ok(new
            {
                success = true,
                data = favorites
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting favorites");
            return StatusCode(500, new { success = false, message = "An error occurred getting favorites" });
        }
    }

    // POST Api/Favorite/{courseId} — Add course to favorites
    [HttpPost("{courseId}")]
    public async Task<IActionResult> AddToFav(int courseId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { success = false, message = "User not found" });

            var result = await _favoriteCommandService.AddToFavorite(courseId, userId);
            if (!result)
                return Conflict(new { success = false, message = "Course is already in favorites" });

            return Ok(new { success = true, message = "Course added to favorites" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding course {CourseId} to favorites", courseId);
            return StatusCode(500, new { success = false, message = "An error occurred adding to favorites" });
        }
    }

    // DELETE Api/Favorite/{courseId} — Remove course from favorites
    [HttpDelete("{courseId}")]
    public async Task<IActionResult> DeleteFromFav(int courseId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { success = false, message = "User not found" });

            var result = await _favoriteCommandService.RemoveFromFavorite(courseId, userId);
            if (!result)
                return NotFound(new { success = false, message = "Favorite not found" });

            return Ok(new { success = true, message = "Course removed from favorites" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error removing course {CourseId} from favorites", courseId);
            return StatusCode(500, new { success = false, message = "An error occurred removing from favorites" });
        }
    }

    // GET Api/Favorite/{courseId}/check — Check if a course is in favorites
    [HttpGet("{courseId}/check")]
    public async Task<IActionResult> IsFavorite(int courseId)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { success = false, message = "User not found" });

            var isFav = await _favoriteQueryService.IsFavorite(courseId, userId);
            return Ok(new { success = true, isFavorite = isFav });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking favorite status for course {CourseId}", courseId);
            return StatusCode(500, new { success = false, message = "An error occurred checking favorite status" });
        }
    }
}
