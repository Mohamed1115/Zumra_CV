using Zumra.Models;

namespace Zumra.Application.Interfaces.LessonLive;

public interface ILessonLiveQueryService
{
    Task<List<Models.LessonLive>> GetAllAsync();
    Task<Models.LessonLive?> GetByIdAsync(int id);
}
