using Zumra.Application.Interfaces.LessonRec;
using Zumra.IRepositories;

namespace Zumra.Application.Services.LessonRec;

public class LessonRecCommandService : ILessonRecCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LessonRecCommandService> _logger;

    public LessonRecCommandService(IUnitOfWork unitOfWork, ILogger<LessonRecCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task<Models.LessonRec> AddAsync(Models.LessonRec lessonRec)
    {
        try
        {
            var created = await _unitOfWork.LessonRecs.CreatAsync(lessonRec);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("LessonRec {LessonId} created successfully", created.Id);
            return created;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create recorded lesson");
            throw;
        }
    }

    public async Task UpdateAsync(Models.LessonRec lessonRec)
    {
        try
        {
            await _unitOfWork.LessonRecs.UpdateAsync(lessonRec);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("LessonRec {LessonId} updated successfully", lessonRec.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update recorded lesson {LessonId}", lessonRec.Id);
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            await _unitOfWork.LessonRecs.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("LessonRec {LessonId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete recorded lesson {LessonId}", id);
            throw;
        }
    }
}
