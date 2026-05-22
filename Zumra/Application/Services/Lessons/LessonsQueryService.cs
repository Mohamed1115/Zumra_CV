using Zumra.Application.Interfaces.Lessons;

namespace Zumra.Application.Services.Lessons;

public class LessonsQueryService : ILessonsQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LessonsQueryService> _logger;

    public LessonsQueryService(IUnitOfWork unitOfWork, ILogger<LessonsQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.Lessons>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.Lessons.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all lessons");
            throw;
        }
    }

    public async Task<Models.Lessons?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.Lessons.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve lesson {LessonId}", id);
            throw;
        }
    }

    public async Task<List<Models.Lessons>> GetByBatchIdAsync(int batchId)
    {
        try
        {
            return await _unitOfWork.Lessons.GetByBatchIdAsync(batchId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve lessons for batch {BatchId}", batchId);
            throw;
        }
    }
}
