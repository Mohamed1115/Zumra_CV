using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.UserImage;

namespace Zumra.Application.Services.UserImage;

public class ImageCommandService : IImageCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<ImageCommandService> _logger;

    // الثوابت
    private const string ZONE = "zumra";
    private const string FILE_PATH = "Users/Images/";
    private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public ImageCommandService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<ImageCommandService> logger)
    {
        _unitOfWork = unitOfWork;
        _bunnyService = bunnyService;
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
    // 🔹 دالة مساعدة لرفع الصورة على Bunny
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
    // 🔹 رفع صورة جديدة للمستخدم
    // ==========================================
    public async Task<Models.UserImage> UploadUserImageAsync(IFormFile image, string userId)
    {
        var (fileName, filePath) = await UploadImageAsync(image);

        var imageUrl = _bunnyService.GetFileUrl(ZONE, filePath + fileName);

        var userImage = new Models.UserImage
        {
            ImageZone = ZONE,
            ImagePath = filePath,
            ImageName = fileName,
            ImageUrl = imageUrl
        };

        await _unitOfWork.UserImage.CreatAsync(userImage);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation("Uploaded image for user {UserId}: {FileName}", userId, fileName);

        return userImage;
    }

    // ==========================================
    // 🔹 تحديث صورة المستخدم الحالية
    // ==========================================
    public async Task<Models.UserImage> UpdateUserImageAsync(IFormFile image, int existingImageId)
    {
        var existing = await _unitOfWork.UserImage.GetByIdAsync(existingImageId);

        if (existing != null)
        {
            // حذف الصورة القديمة من Bunny
            try
            {
                await _bunnyService.DeleteFileAsync(existing.ImageZone, existing.ImagePath + existing.ImageName);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to delete old image {ImageName} from Bunny, continuing with upload.", existing.ImageName);
            }
        }

        var (fileName, filePath) = await UploadImageAsync(image);
        var imageUrl = _bunnyService.GetFileUrl(ZONE, filePath + fileName);

        if (existing != null)
        {
            // تحديث السجل الموجود
            existing.ImageZone = ZONE;
            existing.ImagePath = filePath;
            existing.ImageName = fileName;
            existing.ImageUrl = imageUrl;

            await _unitOfWork.UserImage.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Updated image record {ImageId}: {FileName}", existingImageId, fileName);
            return existing;
        }
        else
        {
            // إنشاء سجل جديد لو القديم مش موجود
            var userImage = new Models.UserImage
            {
                ImageZone = ZONE,
                ImagePath = filePath,
                ImageName = fileName,
                ImageUrl = imageUrl
            };

            await _unitOfWork.UserImage.CreatAsync(userImage);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Created new image record {ImageId}: {FileName}", userImage.Id, fileName);
            return userImage;
        }
    }

    // ==========================================
    // 🔹 حذف صورة المستخدم
    // ==========================================
    public async Task DeleteUserImageAsync(int imageId)
    {
        var image = await _unitOfWork.UserImage.GetByIdAsync(imageId);
        if (image == null)
        {
            _logger.LogWarning("DeleteUserImageAsync: Image {ImageId} not found.", imageId);
            return;
        }

        try
        {
            await _bunnyService.DeleteFileAsync(image.ImageZone, image.ImagePath + image.ImageName);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete image file from Bunny for image {ImageId}.", imageId);
        }

        await _unitOfWork.UserImage.DeleteAsync(image.Id);
        await _unitOfWork.CommitAsync();

        _logger.LogInformation("Deleted image {ImageId}.", imageId);
    }
}