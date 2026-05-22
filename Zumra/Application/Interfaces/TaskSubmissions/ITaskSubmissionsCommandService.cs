namespace Zumra.Application.Interfaces.TaskSubmissions;

public interface ITaskSubmissionsCommandService
{
    Task<Models.TaskSubmissions> Create(Models.TaskSubmissions taskSubmission);
    Task<Models.TaskSubmissions> Update(int id, Models.TaskSubmissions taskSubmission);
    Task Delete(int id);
}
