using Zumra.Models;

namespace Zumra.Application.Interfaces.LessonRec;

public interface ILessonRecQueryService
{
    Task<List<Models.LessonRec>> GetAllAsync();
    Task<Models.LessonRec?> GetByIdAsync(int id);
}
