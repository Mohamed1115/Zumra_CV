using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.Category;

namespace Zumra.Application.Services.Category;

public class CategoryQueryService : ICategoryQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<CategoryQueryService> _logger;

    public CategoryQueryService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<CategoryQueryService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _bunnyService = bunnyService ?? throw new ArgumentNullException(nameof(bunnyService));
        _logger = logger;
    }

    // ==========================================
    // 🔹 دالة مساعدة لبناء URL الصورة الكامل
    // ==========================================
    private string? BuildImageUrl(string? imageName, string? imagePath, string? imageZone)
    {
        if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(imagePath) || string.IsNullOrEmpty(imageZone))
            return null;

        var fullPath = imagePath + imageName;
        return _bunnyService.GetFileUrl(imageZone, fullPath);
    }

    public async Task<List<Models.Category>> All()
    {
        try
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            
            // بناء الـ URL الكامل للصور
            foreach (var category in categories)
            {
                category.ImageUrl = BuildImageUrl(category.ImageName, category.ImagePath, category.ImageZone);
            }
            
            return categories;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all categories");
            throw;
        }
    }

    public async Task<Models.Category?> GetCategory(int id)
    {
        try
        {
            var category = await _unitOfWork.Category.GetCategory(id);
            
            if (category == null)
            {
                _logger.LogWarning("Category with ID {CategoryId} not found", id);
                return null;
            }

            // بناء الـ URL الكامل للصورة
            category.ImageUrl = BuildImageUrl(category.ImageName, category.ImagePath, category.ImageZone);
            
            return category;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve category {CategoryId}", id);
            throw;
        }
    }
}