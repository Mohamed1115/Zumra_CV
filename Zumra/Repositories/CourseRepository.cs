using Microsoft.EntityFrameworkCore;
using Zumra.Data;
using Zumra.IRepositories;
using Zumra.Models;

namespace Zumra.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Course?> GetAllByIdAsync(int id)
    {
        return await _context.Courses.Where(c=> c.Id == id).
            Include(c=>c.CourseBatches)
            .FirstOrDefaultAsync();
    }
    
}
