namespace Zumra.Application.Interfaces.Category;

public interface ICategoryQueryService
{
    Task<List<Models.Category>> All();
    Task<Models.Category?> GetCategory(int id);
}