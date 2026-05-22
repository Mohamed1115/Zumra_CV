using Zumra.DTOs.Request.Category;

namespace Zumra.Application.Interfaces.Category;

public interface ICategoryCommandService
{
    Task<Models.Category> Create(CategoryCreat category, IFormFile image);
    Task<Models.Category> Update(int categoryId, Models.Category category, IFormFile? newImage = null);
    Task Delete(int id);
}