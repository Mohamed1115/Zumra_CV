namespace Zumra.IRepositories;

public interface ILessonsRepository : IRepository<Lessons>
{
    // Add custom methods here if needed in the future
    Task<bool> UpdateLessonContentIdAsync(int lessonId, int vmId);
    Task<List<Lessons>> GetByBatchIdAsync(int batchId);
}
