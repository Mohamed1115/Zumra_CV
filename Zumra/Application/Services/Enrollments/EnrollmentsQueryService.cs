using Zumra.Application.Interfaces.Enrollments;
using Zumra.DTOs.Response;

namespace Zumra.Application.Services.Enrollments;

public class EnrollmentsQueryService : IEnrollmentsQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EnrollmentsQueryService> _logger;
    

    public EnrollmentsQueryService(IUnitOfWork unitOfWork, ILogger<EnrollmentsQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<CartResponse> GetAllAsync(string userId)
    {
        try
        {
            var carts = await _unitOfWork.Enrollments.GetCartItemsAsync(userId);
            var total = await _unitOfWork.Enrollments.GetCartCostAsync(userId);
            return new CartResponse
            {
                Cart = carts,
                Total = total
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve cart items for user {UserId}", userId);
            throw;
        }
    }

    public async Task<Models.Enrollments?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.Enrollments.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve enrollment {EnrollmentId}", id);
            throw;
        }
    }
}
