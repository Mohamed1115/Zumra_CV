using Zumra.DTOs.Request.Lesson;

namespace Zumra.Application.Interfaces.Lessons;

public interface ILessonsCommandService
{
    Task<Models.Lessons> Create(LessonReq lesson);
    Task<Models.Lessons> Update(int id, Models.Lessons lesson);
    Task Delete(int id);
    Task UpdateId(int lessonId, int VMId);
}
