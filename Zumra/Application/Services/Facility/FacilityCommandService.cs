using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.Facility;
using Zumra.DTOs.Request.Facility;

namespace Zumra.Application.Services.Facility;

public class FacilityCommandService : IFacilityCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<FacilityCommandService> _logger;

    // الثوابت
    private const string ZONE = "zumra";
    private const string FILE_PATH = "Facilities/Images/";
    private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public FacilityCommandService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<FacilityCommandService> logger)
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
    // 🔹 إنشاء منشآة جديدة
    // ==========================================
    public async Task<Models.Facility> CreatFacilityAsync(FacilityCreat fc, IFormFile image)
    {
        if (fc == null)
            throw new ArgumentNullException(nameof(fc));

        string fileName = string.Empty;
        string filePath = string.Empty;

        try
        {
            // رفع الصورة إن وجدت
            if (image != null && image.Length > 0)
            {
                (fileName, filePath) = await UploadImageAsync(image);
            }

            // إنشاء كائن المنشآة
            var facility = new Models.Facility
            {
                Name = fc.Name,
                Description = fc.Description,
                Type = fc.Type,
                ImageName = fileName,
                ImagePath = filePath,
                ImageZone = string.IsNullOrEmpty(fileName) ? string.Empty : ZONE,
                CategoryId = fc.CategoryId,
                UserID = fc.UserId
            };

            // حفظ في قاعدة البيانات
            // حفظ في قاعدة البيانات
            var createdFacility = await _unitOfWork.Facilities.CreatAsync(facility);
            // Don't commit here - facility creation usually involves user assignment in controller which manages transaction
            
            _logger.LogInformation("Facility {FacilityId} created successfully", createdFacility.Id);
            return createdFacility;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create facility {FacilityName}", fc.Name);
            throw;
        }
    }

    // ==========================================
    // 🔹 تحديث منشآة مع صورة جديدة
    // ==========================================
    public async Task<Models.Facility> UpdateFacilityAsync(
        int facilityId,
        Models.Facility fc,
        IFormFile? newImage = null)
    {
        if (fc == null)
            throw new ArgumentNullException(nameof(fc));

        try
        {
            // جيب المنشآة القديمة
            var existingFacility = await _unitOfWork.Facilities.GetByIdAsync(facilityId);
            if (existingFacility == null)
                throw new InvalidOperationException($"Facility with ID {facilityId} not found");

            // حدث البيانات الأساسية
            existingFacility.Name = fc.Name;
            existingFacility.Description = fc.Description;
            existingFacility.Type = fc.Type;
            existingFacility.CategoryId = fc.CategoryId;

            // لو في صورة جديدة
            if (newImage != null && newImage.Length > 0)
            {
                // احذف الصورة القديمة
                if (!string.IsNullOrEmpty(existingFacility.ImageName))
                {
                    try
                    {
                        var oldPath = existingFacility.ImagePath + existingFacility.ImageName;
                        await _bunnyService.DeleteFileAsync(ZONE, oldPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete old image for facility {FacilityId}", facilityId);
                    }
                }

                // رفع الصورة الجديدة
                (var fileName, var filePath) = await UploadImageAsync(newImage);
                existingFacility.ImageName = fileName;
                existingFacility.ImagePath = filePath;
                existingFacility.ImageZone = ZONE;
            }

            // حفظ التعديلات
            await _unitOfWork.Facilities.UpdateAsync(existingFacility);
            // Controller manages transactionCommit
            
            _logger.LogInformation("Facility {FacilityId} updated successfully", facilityId);
            return existingFacility;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update facility {FacilityId}", facilityId);
            throw;
        }
    }

    // ==========================================
    // 🔹 حذف منشآة
    // ==========================================
    public async Task DeleteFacilityAsync(int facilityId)
    {
        if (facilityId <= 0)
            throw new ArgumentException("Invalid facility ID", nameof(facilityId));

        try
        {
            // جيب المنشآة عشان تحذف صورتها
            var facility = await _unitOfWork.Facilities.GetByIdAsync(facilityId);
            
            if (facility != null && !string.IsNullOrEmpty(facility.ImageName))
            {
                try
                {
                    var imagePath = facility.ImagePath + facility.ImageName;
                    await _bunnyService.DeleteFileAsync(ZONE, imagePath);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete image for facility {FacilityId}", facilityId);
                }
            }

            // احذف من قاعدة البيانات
            await _unitOfWork.Facilities.DeleteAsync(facilityId);
            // Controller manages transactionCommit
            
            _logger.LogInformation("Facility {FacilityId} deleted successfully", facilityId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete facility {FacilityId}", facilityId);
            throw;
        }
    }
    // ===========================================
    // 🔹 اعطاء اكسيس
    //============================================
    public async Task<bool> GiveAccess(AccesReq req)
    {
        var uf = new UserFacility()
        {
            UserId = req.User,
            FacilityId = req.FacilityId,
            Role = (FacilityRole)req.Level,
            CreatedAt  = DateTime.UtcNow 
        };
        var create =await _unitOfWork.UserFacility.CreatAsync(uf);
        if (create == null)
        {
            return false;
            
        }

        await _unitOfWork.CommitAsync();

        return true;
    }
}