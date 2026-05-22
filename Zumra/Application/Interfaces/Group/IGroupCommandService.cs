namespace Zumra.Application.Interfaces.Group;

public interface IGroupCommandService
{
    Task Create(Models.Group group);
    Task Update(Models.Group group);
    Task Delete(int id);
}