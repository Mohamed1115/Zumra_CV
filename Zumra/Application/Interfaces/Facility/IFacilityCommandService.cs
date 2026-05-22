using Zumra.DTOs.Request.Facility;

namespace Zumra.Application.Interfaces.Facility;

public interface IFacilityCommandService
{
    Task<Models.Facility> CreatFacilityAsync(FacilityCreat fc, IFormFile image);
    Task<Models.Facility> UpdateFacilityAsync(int facilityId,
        Models.Facility fc,
        IFormFile? newImage = null);
    Task DeleteFacilityAsync(int fc_id);
    Task<bool> GiveAccess(AccesReq req);
}