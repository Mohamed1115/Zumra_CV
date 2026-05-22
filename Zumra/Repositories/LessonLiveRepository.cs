using Zumra.Data;
using Zumra.IRepositories;
using Zumra.Models;

namespace Zumra.Repositories;

public class LessonLiveRepository : Repository<LessonLive>, ILessonLiveRepository
{
    public LessonLiveRepository(ApplicationDbContext context) : base(context)
    {
    }
}
