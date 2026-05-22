using Zumra.Application.Interfaces.CourseContent;
using Zumra.Application.Interfaces.Tasks;
using Zumra.DTOs.Request.Task;

namespace Zumra.Application.Services.Tasks;

public class TasksCommandService : ITasksCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourseContentCommandService _contentCommandService;
    private readonly ILogger<TasksCommandService> _logger;

    public TasksCommandService(IUnitOfWork unitOfWork, ICourseContentCommandService contentCommandService, ILogger<TasksCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _contentCommandService = contentCommandService;
        _logger = logger;
    }

    public async Task<Models.Tasks> Create(TaskReq task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        try
        {
            
            
            var ccontent = new Models.CourseContent();
            ccontent.ContentType = SD.ContentTypeTask;
            ccontent.CourseBatchId = task.CourseBatchId;
            ccontent.CourseId = task.CourseId;
            ccontent.SectionId = task.SectionId;
            var cc = _contentCommandService.Create(ccontent);
            
            
            var tas = new Models.Tasks();
            tas.Title = task.Title;
            tas.Description = task.Description;
            tas.Type = task.Type;
            tas.SectionId = task.SectionId;
            tas.FormUrl = task.FormUrl;
            tas.CourseContentId = cc.Id; // Link to CourseContent
            
            var created = await _unitOfWork.Tasks.CreatAsync(tas);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Task {TaskId} created successfully", created.Id);
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create task {TaskName}", task.Title);
            throw;
        }
    }

    public async Task<Models.Tasks> Update(int id, Models.Tasks task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        try
        {
            var existing = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Task with ID {id} not found");

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.Type = task.Type;
            existing.Deadline = task.Deadline;
            existing.MaxScore = task.MaxScore;
            existing.SectionId = task.SectionId;

            await _unitOfWork.Tasks.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Task {TaskId} updated successfully", id);
            return existing;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update task {TaskId}", id);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid task ID", nameof(id));

        try
        {
            await _unitOfWork.Tasks.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Task {TaskId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete task {TaskId}", id);
            throw;
        }
    }
}
