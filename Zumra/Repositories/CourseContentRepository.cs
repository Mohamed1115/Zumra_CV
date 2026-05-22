using Microsoft.EntityFrameworkCore;
using Zumra.Data;

namespace Zumra.Repositories;

public class CourseContentRepository : Repository<CourseContent>, ICourseContentRepository
{
    public CourseContentRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add custom methods implementation here if needed in the future
    public async Task<int> MaxContentOrder(int BId, int SId)
    {
        var maxOrder = await _context.CourseContents
            .Where(cc => cc.CourseBatchId == BId && cc.SectionId == SId)
            .Select(cc => (int?)cc.CourseOrder)
            .MaxAsync();

        return (maxOrder ?? 0) + 1;
    }
}
