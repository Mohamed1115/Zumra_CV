using Zumra.Application.Interfaces.Group;

namespace Zumra.Application.Services.Group;

public class GroupQueryService:IGroupQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GroupQueryService> _logger;

    public GroupQueryService(IUnitOfWork unitOfWork, ILogger<GroupQueryService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Models.Group>> GetAllGroups(int facilityId)
    {
        try
        {
            var Grps = await _unitOfWork.Groups.GetAllGroupsAsync(facilityId);
            return Grps;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve groups for facility {FacilityId}", facilityId);
            throw;
        }
    }

    public async Task<Models.Group> GetGroup(int id, int facilityId)
    {
        try
        {
            var grp = await _unitOfWork.Groups.GetGroupAsync(id,facilityId);
             if (grp == null)
            {
                _logger.LogWarning("Group with ID {GroupId} not found in facility {FacilityId}", id, facilityId);
                return null;
            }
            return grp;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve group {GroupId} for facility {FacilityId}", id, facilityId);
            throw;
        }
    }
}