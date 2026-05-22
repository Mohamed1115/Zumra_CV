namespace Zumra.Application.Interfaces.Lessons;

public interface ILessonsQueryService
{
    Task<List<Models.Lessons>> GetAllAsync();
    Task<Models.Lessons?> GetByIdAsync(int id);
    Task<List<Models.Lessons>> GetByBatchIdAsync(int batchId);
}
