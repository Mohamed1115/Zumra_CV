using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.Category;
using Zumra.DTOs.Request.Category;

namespace Zumra.Application.Services.Category;

public class CategoryCommandService : ICategoryCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<CategoryCommandService> _logger;

    // الثوابت
    private const string ZONE = "zumra";
    private const string FILE_PATH = "Categories/Images/";
    private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public CategoryCommandService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<CategoryCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _bunnyService = bunnyService ?? throw new ArgumentNullException(nameof(bunnyService));
        _logger = logger;
    }

    // ==========================================
    // 🔹 دالة مساعدة للتحقق من الصورة
    // ==========================================
    private void ValidateImageFile(IFormFile image)
    {
        if (image == null || image.Length == 0)
            throw new ArgumentException("Image cannot be empty", nameof(image));

        var extension = Path.GetExtension(image.FileName).ToLower();
        
        if (!AllowedExtensions.Contains(extension))
            throw new InvalidOperationException("Invalid file type. Only jpg, jpeg, png, gif are allowed");

        if (image.Length > MAX_FILE_SIZE)
            throw new InvalidOperationException("Image size must be less than 5MB");
    }

    // ==========================================
    // 🔹 دالة مساعدة لرفع الصورة
    // ==========================================
    private async Task<(string FileName, string FilePath)> UploadImageAsync(IFormFile image)
    {
        ValidateImageFile(image);

        var extension = Path.GetExtension(image.FileName).ToLower();
        var fileName = Guid.NewGuid().ToString() + extension;
        var fullPath = FILE_PATH + fileName;

        await _bunnyService.UploadFileAsync(ZONE, fullPath, image);

        return (fileName, FILE_PATH);
    }

    // ==========================================
    // 🔹 إنشاء تصنيف جديد
    // ==========================================
    public async Task<Models.Category> Create(CategoryCreat categoryDto, IFormFile image)
    {
        if (categoryDto == null)
            throw new ArgumentNullException(nameof(categoryDto));

        string fileName = string.Empty;
        string filePath = string.Empty;

        try
        {
            // رفع الصورة إن وجدت
            if (image != null && image.Length > 0)
            {
                (fileName, filePath) = await UploadImageAsync(image);
            }

            // إنشاء كائن التصنيف
            var category = new Models.Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                ImageName = fileName,
                ImagePath = filePath,
                ImageZone = string.IsNullOrEmpty(fileName) ? string.Empty : ZONE
            };

            // حفظ في قاعدة البيانات
            var createdCategory = await _unitOfWork.Categories.CreatAsync(category);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Category {CategoryId} created successfully", createdCategory.Id);
            return createdCategory;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create category {CategoryName}", categoryDto.Name);
            throw;
        }
    }

    // ==========================================
    // 🔹 تحديث تصنيف مع صورة جديدة
    // ==========================================
    public async Task<Models.Category> Update(
        int categoryId,
        Models.Category category,
        IFormFile? newImage = null)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));

        try
        {
            // جيب التصنيف القديم
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (existingCategory == null)
                throw new InvalidOperationException($"Category with ID {categoryId} not found");

            // حدث البيانات الأساسية
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            // لو في صورة جديدة
            if (newImage != null && newImage.Length > 0)
            {
                // احذف الصورة القديمة
                if (!string.IsNullOrEmpty(existingCategory.ImageName))
                {
                    try
                    {
                        var oldPath = existingCategory.ImagePath + existingCategory.ImageName;
                        await _bunnyService.DeleteFileAsync(ZONE, oldPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete old image for category {CategoryId}", categoryId);
                    }
                }

                // رفع الصورة الجديدة
                (var fileName, var filePath) = await UploadImageAsync(newImage);
                existingCategory.ImageName = fileName;
                existingCategory.ImagePath = filePath;
                existingCategory.ImageZone = ZONE;
            }

            // حفظ التعديلات
            await _unitOfWork.Categories.UpdateAsync(existingCategory);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Category {CategoryId} updated successfully", categoryId);
            return existingCategory;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update category {CategoryId}", categoryId);
            throw;
        }
    }

    // ==========================================
    // 🔹 حذف تصنيف
    // ==========================================
    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid category ID", nameof(id));

        try
        {
            // جيب التصنيف عشان تحذف صورته
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            
            if (category != null && !string.IsNullOrEmpty(category.ImageName))
            {
                try
                {
                    var imagePath = category.ImagePath + category.ImageName;
                    await _bunnyService.DeleteFileAsync(ZONE, imagePath);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete image for category {CategoryId}", id);
                }
            }

            // احذف من قاعدة البيانات
            await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Category {CategoryId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete category {CategoryId}", id);
            throw;
        }
    }
}