namespace Zumra.Application.Interfaces.Tasks;

public interface ITasksQueryService
{
    Task<List<Models.Tasks>> GetAllAsync();
    Task<Models.Tasks?> GetByIdAsync(int id);
}
