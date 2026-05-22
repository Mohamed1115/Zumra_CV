using Zumra.Application.Interfaces.TaskSubmissions;

namespace Zumra.Application.Services.TaskSubmissions;

public class TaskSubmissionsCommandService : ITaskSubmissionsCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TaskSubmissionsCommandService> _logger;

    public TaskSubmissionsCommandService(IUnitOfWork unitOfWork, ILogger<TaskSubmissionsCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<Models.TaskSubmissions> Create(Models.TaskSubmissions taskSubmission)
    {
        if (taskSubmission == null)
            throw new ArgumentNullException(nameof(taskSubmission));

        try
        {
            var created = await _unitOfWork.TaskSubmissions.CreatAsync(taskSubmission);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("TaskSubmission {SubmissionId} created successfully", created.Id);
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create task submission for task {TaskId}", taskSubmission.TaskId);
            throw;
        }
    }

    public async Task<Models.TaskSubmissions> Update(int id, Models.TaskSubmissions taskSubmission)
    {
        if (taskSubmission == null)
            throw new ArgumentNullException(nameof(taskSubmission));

        try
        {
            var existing = await _unitOfWork.TaskSubmissions.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Task submission with ID {id} not found");

            existing.SubmissionUrl = taskSubmission.SubmissionUrl;
            existing.SubmissionAt = taskSubmission.SubmissionAt;
            existing.Status = taskSubmission.Status;
            existing.TaskId = taskSubmission.TaskId;
            existing.UserId = taskSubmission.UserId;

            await _unitOfWork.TaskSubmissions.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("TaskSubmission {SubmissionId} updated successfully", id);
            return existing;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update task submission {SubmissionId}", id);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid task submission ID", nameof(id));

        try
        {
            await _unitOfWork.TaskSubmissions.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("TaskSubmission {SubmissionId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete task submission {SubmissionId}", id);
            throw;
        }
    }
}
