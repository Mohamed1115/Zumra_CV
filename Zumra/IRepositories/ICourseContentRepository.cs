namespace Zumra.IRepositories;

public interface ICourseContentRepository : IRepository<CourseContent>
{
    // Add custom methods here if needed in the future
    Task<int> MaxContentOrder(int BId, int SId);
}
