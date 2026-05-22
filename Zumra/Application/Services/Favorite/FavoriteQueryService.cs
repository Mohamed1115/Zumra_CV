using Microsoft.EntityFrameworkCore;
using Zumra.Application.Interfaces.Favorite;
using Zumra.Data;

namespace Zumra.Application.Services.Favorite;

public class FavoriteQueryService : IFavoriteQueryService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<FavoriteQueryService> _logger;

    public FavoriteQueryService(ApplicationDbContext db, ILogger<FavoriteQueryService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<List<Models.Favorite>> GetUserFavorites(string userId)
    {
        try
        {
            return await _db.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Course)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get favorites for user {UserId}", userId);
            throw;
        }
    }

    public async Task<bool> IsFavorite(int courseId, string userId)
    {
        return await _db.Favorites
            .AnyAsync(f => f.CourseId == courseId && f.UserId == userId);
    }
}
