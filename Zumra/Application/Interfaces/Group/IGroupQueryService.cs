namespace Zumra.Application.Interfaces.Group;

public interface IGroupQueryService
{
    Task<List<Models.Group>> GetAllGroups(int facilityId);
    Task<Models.Group> GetGroup(int id, int facilityId);
}