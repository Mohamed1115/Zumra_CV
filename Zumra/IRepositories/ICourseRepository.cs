using Zumra.Models;

namespace Zumra.IRepositories;

public interface ICourseRepository : IRepository<Course>
{
    // Add custom methods here if needed in the future
    Task<Course?> GetAllByIdAsync(int id);
}
