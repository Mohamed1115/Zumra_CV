using Zumra.Application.Interfaces.LessonLive;
using Zumra.IRepositories;

namespace Zumra.Application.Services.LessonLive;

public class LessonLiveQueryService : ILessonLiveQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LessonLiveQueryService> _logger;

    public LessonLiveQueryService(IUnitOfWork unitOfWork, ILogger<LessonLiveQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.LessonLive>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.LessonLives.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all live lessons");
            throw;
        }
    }

    public async Task<Models.LessonLive?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.LessonLives.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve live lesson {LessonId}", id);
            throw;
        }
    }
}
