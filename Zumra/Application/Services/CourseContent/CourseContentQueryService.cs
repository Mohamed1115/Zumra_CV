using Zumra.Application.Interfaces.CourseContent;

namespace Zumra.Application.Services.CourseContent;

public class CourseContentQueryService : ICourseContentQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CourseContentQueryService> _logger;

    public CourseContentQueryService(IUnitOfWork unitOfWork, ILogger<CourseContentQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.CourseContent>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.CourseContent.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all course content");
            throw;
        }
    }

    public async Task<Models.CourseContent?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.CourseContent.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve course content {ContentId}", id);
            throw;
        }
    }
}
