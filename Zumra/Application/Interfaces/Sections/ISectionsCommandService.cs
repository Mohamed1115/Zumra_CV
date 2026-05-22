namespace Zumra.Application.Interfaces.Sections;

public interface ISectionsCommandService
{
    Task<Models.Sections> Create(Models.Sections section);
    Task<Models.Sections> Update(int id, Models.Sections section);
    Task Delete(int id);
}
