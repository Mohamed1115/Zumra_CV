using Zumra.Application.Interfaces.Sections;
using Zumra.DTOs.Response.Section;

namespace Zumra.Application.Services.Sections;

public class SectionsQueryService : ISectionsQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SectionsQueryService> _logger;

    public SectionsQueryService(IUnitOfWork unitOfWork, ILogger<SectionsQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<SectionDto>> GetAllAsync(int BatchId)
    {
        try
        {
            return await _unitOfWork.Sections.GetAllByBatchIdAsync(BatchId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve sections for batch {BatchId}", BatchId);
            throw;
        }
    }

    public async Task<Models.Sections?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.Sections.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve section {SectionId}", id);
            throw;
        }
    }
}
