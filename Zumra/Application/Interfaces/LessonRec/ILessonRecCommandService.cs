using Zumra.Models;

namespace Zumra.Application.Interfaces.LessonRec;

public interface ILessonRecCommandService
{
    Task<Models.LessonRec> AddAsync(Models.LessonRec lessonRec);
    Task UpdateAsync(Models.LessonRec lessonRec);
    Task DeleteAsync(int id);
}
