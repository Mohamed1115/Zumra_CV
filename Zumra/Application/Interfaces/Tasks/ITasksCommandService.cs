using Zumra.DTOs.Request.Task;

namespace Zumra.Application.Interfaces.Tasks;

public interface ITasksCommandService
{
    Task<Models.Tasks> Create(TaskReq task);
    Task<Models.Tasks> Update(int id, Models.Tasks task);
    Task Delete(int id);
}
