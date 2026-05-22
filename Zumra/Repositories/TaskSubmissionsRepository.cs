using Zumra.Data;

namespace Zumra.Repositories;

public class TaskSubmissionsRepository : Repository<TaskSubmissions>, ITaskSubmissionsRepository
{
    public TaskSubmissionsRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add custom methods implementation here if needed in the future
}
