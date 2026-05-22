using Microsoft.EntityFrameworkCore;
using Zumra.Data;

namespace Zumra.Repositories;

public class CourseBatchesRepository : Repository<CourseBatches>, ICourseBatchesRepository
{
    public CourseBatchesRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add custom methods implementation here if needed in the future
    public async Task<List<CourseBatches>> GetAllByCourseId(int courseId)
    {
        return await _context.CourseBatches.Where(cb => cb.CourseId == courseId).ToListAsync();
    }

    public async Task<CourseBatches?> GetByBatchId(int batchId)
    {
        return await _context.CourseBatches
            .Include(cb => cb.Sections)
            .FirstOrDefaultAsync(cb => cb.Id == batchId);
    }

}
