using Zumra.Application.Interfaces.Jitsi;
using Zumra.Application.Interfaces.CourseContent;
using Zumra.Application.Interfaces.Lessons;
using Zumra.DTOs.Request.Lesson;

namespace Zumra.Application.Services.Lessons;

public class LessonsCommandService : ILessonsCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJitsiService _jitsiService;
    private readonly ICourseContentCommandService _contentCommandService;
    

    public LessonsCommandService(IUnitOfWork unitOfWork, IJitsiService jitsiService, ICourseContentCommandService contentCommandService)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _jitsiService = jitsiService;
        _contentCommandService = contentCommandService;
    }

    public async Task<Models.Lessons> Create(LessonReq lesson)
    {
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson));

        try
        {
            // 1. إنشاء الـ CourseContent أولاً وحفظه
            var ccontent = new Models.CourseContent();
            ccontent.ContentType = SD.ContentTypeLesson;
            ccontent.CourseBatchId = lesson.CourseBatchId;
            ccontent.CourseId = lesson.CourseId;
            ccontent.SectionId = lesson.SectionId;
            var cc = await _contentCommandService.Create(ccontent);
            // نعمل Commit للـ CourseContent أولاً عشان نحصل على الـ Id
            await _unitOfWork.CommitAsync();

            // 2. إنشاء الـ Lesson مع الـ CourseContentId
            var less = new Models.Lessons();
            less.Name = lesson.Name;
            less.Description = lesson.Description;
            less.Type = lesson.Type;
            less.CourseBatchId = lesson.CourseBatchId;
            less.CourseId = lesson.CourseId;
            less.MeetingId = lesson.MeetingId;
            less.VideoId = lesson.VideoId;
            less.CourseContentId = cc.Id; // Link to CourseContent
            
            var created = await _unitOfWork.Lessons.CreatAsync(less);
            await _unitOfWork.CommitAsync();
            return created;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create lesson: {ex.Message}", ex);
        }
    }

    public async Task<Models.Lessons> Update(int id, Models.Lessons lesson)
    {
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson));

        try
        {
            var existing = await _unitOfWork.Lessons.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Lesson with ID {id} not found");

            existing.Name = lesson.Name;
            existing.Description = lesson.Description;
            existing.Type = lesson.Type;
            existing.CourseId = lesson.CourseId;
            existing.CourseBatchId = lesson.CourseBatchId;
            existing.MeetingId = lesson.MeetingId;
            existing.VideoId = lesson.VideoId;

            await _unitOfWork.Lessons.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update lesson: {ex.Message}", ex);
        }
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid lesson ID", nameof(id));

        try
        {
            await _unitOfWork.Lessons.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete lesson: {ex.Message}", ex);
        }
    }

    public async Task UpdateId(int lessonId, int VMId)
    {
        await _unitOfWork.Lessons.UpdateLessonContentIdAsync(lessonId, VMId);
    }
}
