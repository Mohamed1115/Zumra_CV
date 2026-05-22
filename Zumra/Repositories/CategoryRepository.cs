using Microsoft.EntityFrameworkCore;
using Zumra.Data;

namespace Zumra.Repositories;

public class CategoryRepository:Repository<Category>,ICategoryRepository
{
public CategoryRepository(ApplicationDbContext context) : base(context)
{
}

public async Task<Category?> GetCategory(int id)
{
    return await _context.Categories.Include(c =>c.Facilities).Where(c => c.Id == id).FirstOrDefaultAsync();
}
}