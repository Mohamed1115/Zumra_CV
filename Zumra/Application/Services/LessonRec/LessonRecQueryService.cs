using Zumra.Application.Interfaces.LessonRec;
using Zumra.IRepositories;

namespace Zumra.Application.Services.LessonRec;

public class LessonRecQueryService : ILessonRecQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LessonRecQueryService> _logger;

    public LessonRecQueryService(IUnitOfWork unitOfWork, ILogger<LessonRecQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.LessonRec>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.LessonRecs.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all recorded lessons");
            throw;
        }
    }

    public async Task<Models.LessonRec?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.LessonRecs.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve recorded lesson {LessonId}", id);
            throw;
        }
    }
}
