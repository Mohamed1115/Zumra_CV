namespace Zumra.Application.Interfaces.TaskSubmissions;

public interface ITaskSubmissionsQueryService
{
    Task<List<Models.TaskSubmissions>> GetAllAsync();
    Task<Models.TaskSubmissions?> GetByIdAsync(int id);
}
