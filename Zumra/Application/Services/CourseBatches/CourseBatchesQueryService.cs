using Zumra.Application.Interfaces.CourseBatches;
using Zumra.DTOs.Response.Section;

namespace Zumra.Application.Services.CourseBatches;

public class CourseBatchesQueryService : ICourseBatchesQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CourseBatchesQueryService> _logger;

    public CourseBatchesQueryService(IUnitOfWork unitOfWork, ILogger<CourseBatchesQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.CourseBatches>> GetAllAsync(int CourseId)
    {
        try
        {
            return await _unitOfWork.CourseBatches.GetAllByCourseId(CourseId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve course batches for course {CourseId}", CourseId);
            throw;
        }
    }

    public async Task<List<SectionDto>> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.Sections.GetAllByBatchIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve sections for batch {BatchId}", id);
            throw;
        }
    }
}
