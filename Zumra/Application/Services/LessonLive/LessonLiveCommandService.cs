using Zumra.Application.Interfaces.LessonLive;
using Zumra.DTOs.Response.Jitsi;
using Zumra.IRepositories;

namespace Zumra.Application.Services.LessonLive;

public class LessonLiveCommandService : ILessonLiveCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LessonLiveCommandService> _logger;

    public LessonLiveCommandService(IUnitOfWork unitOfWork, ILogger<LessonLiveCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<Models.LessonLive> AddAsync(JitsiMeetingResponse lessonLive)
    {
        try
        {
            var meet = new Models.LessonLive();
            meet.MeetingUrl = lessonLive.MeetingUrl;
            meet.RoomName = lessonLive.RoomName;
            meet.StartTime = lessonLive.CreatedAt;
            meet.EndTime = lessonLive.ExpiresAt;
            await _unitOfWork.LessonLives.CreatAsync(meet);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("LessonLive {LessonId} created successfully", meet.Id);
            return meet;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create live lesson {RoomName}", lessonLive.RoomName);
            throw;
        }
    }

    public async Task UpdateAsync(Models.LessonLive lessonLive)
    {
        try
        {
            await _unitOfWork.LessonLives.UpdateAsync(lessonLive);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("LessonLive {LessonId} updated successfully", lessonLive.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update live lesson {LessonId}", lessonLive.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _unitOfWork.LessonLives.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("LessonLive {LessonId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete live lesson {LessonId}", id);
            throw;
        }
    }
}
