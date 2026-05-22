namespace Zumra.Application.Interfaces.UserImage;

public interface IImageCommandService
{
    /// <summary>
    /// رفع صورة جديدة للمستخدم وحفظها في DB
    /// </summary>
    Task<Models.UserImage> UploadUserImageAsync(IFormFile image, string userId);

    /// <summary>
    /// تحديث صورة المستخدم الحالية
    /// </summary>
    Task<Models.UserImage> UpdateUserImageAsync(IFormFile image, int existingImageId);

    /// <summary>
    /// حذف صورة المستخدم
    /// </summary>
    Task DeleteUserImageAsync(int imageId);
}
