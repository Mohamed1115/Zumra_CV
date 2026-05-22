using Zumra.DTOs.Response;

namespace Zumra.Application.Interfaces.Enrollments;

public interface IEnrollmentsQueryService
{
    Task<CartResponse> GetAllAsync(string userId);
    Task<Models.Enrollments?> GetByIdAsync(int id);
}
