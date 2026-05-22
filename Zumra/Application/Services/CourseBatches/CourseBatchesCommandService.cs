using Zumra.Application.Interfaces.CourseBatches;

namespace Zumra.Application.Services.CourseBatches;

public class CourseBatchesCommandService : ICourseBatchesCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CourseBatchesCommandService> _logger;

    public CourseBatchesCommandService(IUnitOfWork unitOfWork, ILogger<CourseBatchesCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<Models.CourseBatches> Create(Models.CourseBatches courseBatch)
    {
        if (courseBatch == null)
            throw new ArgumentNullException(nameof(courseBatch));

        try
        {
            var created = await _unitOfWork.CourseBatches.CreatAsync(courseBatch);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("CourseBatch {BatchId} created successfully", created.Id);
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create course batch for course {CourseId}", courseBatch.CourseId);
            throw;
        }
    }

    public async Task<Models.CourseBatches> Update(int id, Models.CourseBatches courseBatch)
    {
        if (courseBatch == null)
            throw new ArgumentNullException(nameof(courseBatch));

        try
        {
            var existing = await _unitOfWork.CourseBatches.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Course batch with ID {id} not found");

            existing.Title = courseBatch.Title;
            existing.StartDate = courseBatch.StartDate;
            existing.EndDate = courseBatch.EndDate;
            existing.Capacity = courseBatch.Capacity;
            existing.Status = courseBatch.Status;
            existing.CourseId = courseBatch.CourseId;

            await _unitOfWork.CourseBatches.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("CourseBatch {BatchId} updated successfully", id);
            return existing;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update course batch {BatchId}", id);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid course batch ID", nameof(id));

        try
        {
            await _unitOfWork.CourseBatches.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("CourseBatch {BatchId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete course batch {BatchId}", id);
            throw;
        }
    }
}
