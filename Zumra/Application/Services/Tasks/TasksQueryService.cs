using Zumra.Application.Interfaces.Tasks;

namespace Zumra.Application.Services.Tasks;

public class TasksQueryService : ITasksQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TasksQueryService> _logger;

    public TasksQueryService(IUnitOfWork unitOfWork, ILogger<TasksQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<List<Models.Tasks>> GetAllAsync()
    {
        try
        {
            return await _unitOfWork.Tasks.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all tasks");
            throw;
        }
    }

    public async Task<Models.Tasks?> GetByIdAsync(int id)
    {
        try
        {
            return await _unitOfWork.Tasks.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve task {TaskId}", id);
            throw;
        }
    }
}
