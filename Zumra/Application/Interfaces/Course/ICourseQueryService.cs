namespace Zumra.Application.Interfaces.Course;

public interface ICourseQueryService
{
    Task<List<Models.Course>> GetAllAsync();
    Task<Models.Course?> GetByIdAsync(int id);
}
