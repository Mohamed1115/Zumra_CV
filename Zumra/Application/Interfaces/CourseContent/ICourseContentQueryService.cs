namespace Zumra.Application.Interfaces.CourseContent;

public interface ICourseContentQueryService
{
    Task<List<Models.CourseContent>> GetAllAsync();
    Task<Models.CourseContent?> GetByIdAsync(int id);
}
