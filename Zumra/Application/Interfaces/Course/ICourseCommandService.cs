using Zumra.DTOs.Request.Course;

namespace Zumra.Application.Interfaces.Course;

public interface ICourseCommandService
{
    Task<Models.Course> Create(CourseCreat course, IFormFile image);
    Task<Models.Course> Update(int courseId, Models.Course course, IFormFile? newImage = null);
    Task Delete(int id);
}
