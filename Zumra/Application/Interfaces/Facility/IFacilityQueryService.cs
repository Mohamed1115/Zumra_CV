using Zumra.DTOs.Request.Facility;
using Zumra.DTOs.Response.Facility;

namespace Zumra.Application.Interfaces.Facility;

public interface IFacilityQueryService
{
    Task<List<Models.Facility>> GetAllAsync();
    Task<Models.Facility> GetByIdAsync(int id);
    Task<List<UserFacilityResponse>> GetByUserIdAsync(string id);
    Task<List<UserFacilityDto>> GetByFacilityAsync(int Id);
}