namespace Zumra.IRepositories;

public interface IFacilityRepository
{
    Task<Facility?> GetFacilityWithAllById(int id);
}