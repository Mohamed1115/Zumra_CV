namespace Zumra.IRepositories;

public interface IGroupRepository:IRepository<Group>
{
    Task<Group?> GetGroupAsync(int id, int facilityId);
    Task<List<Group>> GetAllGroupsAsync(int facilityid);
}