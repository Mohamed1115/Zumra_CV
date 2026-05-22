namespace Zumra.Application.Interfaces.CourseContent;

public interface ICourseContentCommandService
{
    Task<Models.CourseContent> Create(Models.CourseContent courseContent);
    Task<Models.CourseContent> Update(int id, Models.CourseContent courseContent);
    Task Delete(int id);
}
