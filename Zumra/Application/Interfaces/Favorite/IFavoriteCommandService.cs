namespace Zumra.Application.Interfaces.Favorite;

public interface IFavoriteCommandService
{
    Task<bool> AddToFavorite(int courseId, string userId);
    Task<bool> RemoveFromFavorite(int courseId, string userId);
}
