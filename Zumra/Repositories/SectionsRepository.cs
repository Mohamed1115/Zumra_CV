using Microsoft.EntityFrameworkCore;
using Zumra.Data;
using Zumra.DTOs.Response.Section;

namespace Zumra.Repositories;

public class SectionsRepository : Repository<Sections>, ISectionsRepository
{
    public SectionsRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <summary>
    /// جلب جميع الـ Sections الخاصة بـ Batch معين مع محتوياتها
    /// بتستخدم Select عشان تجيب بس البيانات المطلوبة (أسرع من Include)
    /// </summary>
    public async Task<List<SectionDto>> GetAllByBatchIdAsync(int batchId)
    {
        return await _context.Sections
            .Where(s => s.CourseBatchId == batchId)
            .OrderBy(s => s.Order) // ترتيب الـ Sections
            .AsNoTracking()
            .Select(s => new SectionDto
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order, // إضافة Order
                Contents = s.CourseContents
                    .OrderBy(cc => cc.CourseOrder) // ترتيب المحتويات
                    .Select(cc => new ContentDto
                    {
                        Type = cc.ContentType,
                        Order = cc.CourseOrder, // إضافة Order
                        
                        // إذا كان المحتوى درس
                        Lesson = cc.Lesson != null ? new LessonDto
                        {
                            Id = cc.Lesson.Id,
                            Name = cc.Lesson.Name,
                            Type = cc.Lesson.Type,
                            
                            // بيانات الدرس المباشر (إذا موجود)
                            Live = cc.Lesson.Live != null ? new LiveDto
                            {
                                MeetingUrl = cc.Lesson.Live.MeetingUrl,
                                StartTime = cc.Lesson.Live.StartTime,
                                EndTime = cc.Lesson.Live.EndTime,
                                RoomName = cc.Lesson.Live.RoomName
                            } : null,
                            
                            // بيانات الدرس المسجل (إذا موجود)
                            Rec = cc.Lesson.Rec != null ? new RecDto
                            {
                                VideoUrl = cc.Lesson.Rec.VideoUrl
                            } : null
                        } : null,
                        
                        // إذا كان المحتوى مهمة
                        Task = cc.Task != null ? new TaskDto
                        {
                            Id = cc.Task.Id,
                            Title = cc.Task.Title,
                            MaxScore = cc.Task.MaxScore
                        } : null
                    })
                    .ToList()
            })
            .ToListAsync();
    }
    
}
