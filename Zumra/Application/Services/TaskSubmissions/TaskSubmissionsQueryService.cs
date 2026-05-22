using Zumra.Application.Interfaces.TaskSubmissions;

namespace Zumra.Application.Services.TaskSubmissions;

public class TaskSubmissionsQueryService : ITaskSubmissionsQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TaskSubmissionsQueryService> _logger;

    public TaskSubmissionsQueryService(IUnitOfWork unitOfWork, ILogger<TaskSubmissionsQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.TaskSubmissions>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.TaskSubmissions.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all task submissions");
            throw;
        }
    }

    public async Task<Models.TaskSubmissions?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.TaskSubmissions.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve task submission {SubmissionId}", id);
            throw;
        }
    }
}
