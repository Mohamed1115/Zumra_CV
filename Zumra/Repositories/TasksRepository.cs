using Zumra.Data;

namespace Zumra.Repositories;

public class TasksRepository : Repository<Tasks>, ITasksRepository
{
    public TasksRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Add custom methods implementation here if needed in the future
}
