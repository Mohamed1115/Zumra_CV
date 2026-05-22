using Zumra.DTOs.Request.Facility;

namespace Zumra.IRepositories;

public interface IUserFacilityRepository: IRepository<UserFacility>
{
    Task<int> FindByUserID(string id,int fcId);
    Task<List<UserFacility>> GetAllFacilitiesByUser(string userId);
    Task<List<UserFacility>> GetAllByFacilityId(int fcId);
    Task<List<UserFacilityDto>> GetAllByFacilityId2(int fcId);
}