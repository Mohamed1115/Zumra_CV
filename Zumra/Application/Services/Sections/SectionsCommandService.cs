using Zumra.Application.Interfaces.Sections;

namespace Zumra.Application.Services.Sections;

public class SectionsCommandService : ISectionsCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SectionsCommandService> _logger;

    public SectionsCommandService(IUnitOfWork unitOfWork, ILogger<SectionsCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<Models.Sections> Create(Models.Sections section)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        try
        {
            var created = await _unitOfWork.Sections.CreatAsync(section);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Section {SectionId} created successfully", created.Id);
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create section {SectionName}", section.Name);
            throw;
        }
    }

    public async Task<Models.Sections> Update(int id, Models.Sections section)
    {
        if (section == null)
            throw new ArgumentNullException(nameof(section));

        try
        {
            var existing = await _unitOfWork.Sections.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Section with ID {id} not found");

            existing.Name = section.Name;
            existing.Order = section.Order;
            existing.CourseId = section.CourseId;
            existing.CourseBatchId = section.CourseBatchId;

            await _unitOfWork.Sections.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Section {SectionId} updated successfully", id);
            return existing;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update section {SectionId}", id);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid section ID", nameof(id));

        try
        {
            await _unitOfWork.Sections.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Section {SectionId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete section {SectionId}", id);
            throw;
        }
    }
}
