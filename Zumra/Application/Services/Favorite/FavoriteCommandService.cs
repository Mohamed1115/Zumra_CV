using Microsoft.EntityFrameworkCore;
using Zumra.Application.Interfaces.Favorite;
using Zumra.Data;
using Zumra.Models;

namespace Zumra.Application.Services.Favorite;

public class FavoriteCommandService : IFavoriteCommandService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<FavoriteCommandService> _logger;

    public FavoriteCommandService(ApplicationDbContext db, ILogger<FavoriteCommandService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> AddToFavorite(int courseId, string userId)
    {
        try
        {
            var alreadyExists = await _db.Favorites
                .AnyAsync(f => f.CourseId == courseId && f.UserId == userId);

            if (alreadyExists)
            {
                _logger.LogWarning("Course {CourseId} is already in favorites for user {UserId}", courseId, userId);
                return false;
            }

            var favorite = new Models.Favorite
            {
                CourseId = courseId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _db.Favorites.AddAsync(favorite);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Course {CourseId} added to favorites for user {UserId}", courseId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to add course {CourseId} to favorites for user {UserId}", courseId, userId);
            throw;
        }
    }

    public async Task<bool> RemoveFromFavorite(int courseId, string userId)
    {
        try
        {
            var favorite = await _db.Favorites
                .FirstOrDefaultAsync(f => f.CourseId == courseId && f.UserId == userId);

            if (favorite == null)
            {
                _logger.LogWarning("Favorite not found for course {CourseId} and user {UserId}", courseId, userId);
                return false;
            }

            _db.Favorites.Remove(favorite);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Course {CourseId} removed from favorites for user {UserId}", courseId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove course {CourseId} from favorites for user {UserId}", courseId, userId);
            throw;
        }
    }
}
