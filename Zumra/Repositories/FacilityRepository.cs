using Microsoft.EntityFrameworkCore;
using Zumra.Data;

namespace Zumra.Repositories;

public class FacilityRepository:Repository<UserFacility>,IFacilityRepository
{
    public FacilityRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Facility?> GetFacilityWithAllById(int id)
    {
        return await _context.Facilities.Include(f =>f.UserFacilities).Where(f => f.Id == id).FirstOrDefaultAsync();
    }

  
    

}