using Microsoft.EntityFrameworkCore;
using Zumra.Data;

namespace Zumra.Repositories;

public class GroupRepository:Repository<Group>,IGroupRepository
{
    public GroupRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Group?> GetGroupAsync(int id,int facilityId)
    {
        return await _context.Groups.Where(g=> g.FacilityId == facilityId).Include(g =>g.Courses).FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<List<Group>> GetAllGroupsAsync(int facilityid)
    {
        return await _context.Groups.Where(g => g.FacilityId == facilityid).ToListAsync();
    }
    
    
    
}