using Microsoft.EntityFrameworkCore;
using Zumra.Data;
using Zumra.DTOs.Request.Facility;

namespace Zumra.Repositories;

public class UserFacilityRepository:Repository<UserFacility>,IUserFacilityRepository
{
    public UserFacilityRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<int> FindByUserID(string id,int fcId)
    {
        return await _context.UserFacilities
            .Where(u => u.UserId == id && u.Role == FacilityRole.SuperAdmin && u.FacilityId == fcId)
            .Select(u => u.FacilityId)
            .FirstOrDefaultAsync();
    }
    public async Task<List<UserFacility>> GetAllFacilitiesByUser(string userId)
    {
        return await _context.UserFacilities.Where(f =>f.UserId == userId)
            .Include(f => f.Facility)
            .ToListAsync();
    }

    public async Task<List<UserFacility>> GetAllByFacilityId(int fcId)
    {
        return await _context.UserFacilities
            .Where(f => f.FacilityId == fcId)
            .Include(f => f.User)
            .ToListAsync();
    }
    public async Task<List<UserFacilityDto>> GetAllByFacilityId2(int fcId)
    {
        return await _context.UserFacilities
            .Where(f => f.FacilityId == fcId)
            .Select(uf => new UserFacilityDto
            {
                UserId       = uf.UserId,
                UserName     = uf.User.Name,
                Email        = uf.User.Email,
                FacilityId   = uf.FacilityId,
                FacilityName = uf.Facility.Name,
                Role         = uf.Role,
                CreatedAt    = uf.CreatedAt
            })
            .ToListAsync();
    }
}