using Zumra.DTOs.Response.Jitsi;
using Zumra.Models;

namespace Zumra.Application.Interfaces.LessonLive;

public interface ILessonLiveCommandService
{
    Task<Models.LessonLive> AddAsync(JitsiMeetingResponse lessonLive);
    Task UpdateAsync(Models.LessonLive lessonLive);
    Task DeleteAsync(int id);
}
