using Zumra.Data;
using Zumra.IRepositories;
using Zumra.Models;

namespace Zumra.Repositories;

public class LessonRecRepository : Repository<LessonRec>, ILessonRecRepository
{
    public LessonRecRepository(ApplicationDbContext context) : base(context)
    {
    }
}
