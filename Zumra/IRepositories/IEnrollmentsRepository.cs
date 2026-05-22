using Zumra.DTOs.Response;

namespace Zumra.IRepositories;

public interface IEnrollmentsRepository : IRepository<Enrollments>
{
    Task<List<Enrollments>> GetAllCartAsync(string id);
    Task<decimal> GetCartCostAsync(string id);
    Task<List<CartItemDto>> GetCartItemsAsync(string userId);
}
