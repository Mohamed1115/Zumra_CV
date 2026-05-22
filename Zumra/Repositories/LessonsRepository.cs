using Microsoft.EntityFrameworkCore;
using Zumra.Data;

namespace Zumra.Repositories;

public class LessonsRepository : Repository<Lessons>, ILessonsRepository
{
    public LessonsRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add custom methods implementation here if needed in the future
    public async Task<bool> UpdateLessonContentIdAsync(int lessonId, int vmId)
    {
        var less = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);
        if (less == null)
            return false;
        if (less.Type==SD.LessonTypeLive)
        {
            less.MeetingId = vmId;
        }
        else if (less.Type == SD.LessonTypeRecorded)
        {
            less.VideoId = vmId;
        }  else
        {
            return false;
        }
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Lessons>> GetByBatchIdAsync(int batchId)
    {
        return await _context.Lessons
            .Where(l => l.CourseBatchId == batchId)
            .OrderBy(l => l.Id)
            .ToListAsync();
    }
}
