using Zumra.Models;

namespace Zumra.Application.Interfaces.Favorite;

public interface IFavoriteQueryService
{
    Task<List<Models.Favorite>> GetUserFavorites(string userId);
    Task<bool> IsFavorite(int courseId, string userId);
}
