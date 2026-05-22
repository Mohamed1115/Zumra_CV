namespace Zumra.Application.Interfaces.CourseBatches;

public interface ICourseBatchesCommandService
{
    Task<Models.CourseBatches> Create(Models.CourseBatches courseBatch);
    Task<Models.CourseBatches> Update(int id, Models.CourseBatches courseBatch);
    Task Delete(int id);
}
