namespace Zumra.IRepositories;

public interface ICourseBatchesRepository : IRepository<CourseBatches>
{
    // Add custom methods here if needed in the future
    Task<List<CourseBatches>> GetAllByCourseId(int courseId);
}
