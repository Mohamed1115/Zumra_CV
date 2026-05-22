namespace Zumra.IRepositories;

public interface ICategoryRepository
{
    Task<Category?> GetCategory(int id);
}