using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.Course;
using Zumra.DTOs.Request.Course;

namespace Zumra.Application.Services.Course;

public class CourseCommandService : ICourseCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<CourseCommandService> _logger;

    // الثوابت
    private const string ZONE = "zumra";
    private const string FILE_PATH = "Courses/Images/";
    private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public CourseCommandService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<CourseCommandService> logger)
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
    // 🔹 إنشاء كورس جديد
    // ==========================================
    public async Task<Models.Course> Create(CourseCreat courseDto, IFormFile image)
    {
        if (courseDto == null)
            throw new ArgumentNullException(nameof(courseDto));

        string fileName = string.Empty;
        string filePath = string.Empty;

        try
        {
            // رفع الصورة إن وجدت
            if (image != null && image.Length > 0)
            {
                (fileName, filePath) = await UploadImageAsync(image);
            }

            // إنشاء كائن الكورس
            var course = new Models.Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                Cost = courseDto.Cost,
                Type = courseDto.Type,
                GroupId = courseDto.GroupId,
                FacilityId = courseDto.FacilityId,
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                ImageName = fileName,
                ImagePath = filePath,
                ImageZone = string.IsNullOrEmpty(fileName) ? string.Empty : ZONE
            };

            // حفظ في قاعدة البيانات
            var createdCourse = await _unitOfWork.Courses.CreatAsync(course);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Course {CourseId} created successfully", createdCourse.Id);
            return createdCourse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create course {CourseName}", courseDto.Name);
            throw;
        }
    }

    // ==========================================
    // 🔹 تحديث كورس مع صورة جديدة
    // ==========================================
    public async Task<Models.Course> Update(
        int courseId,
        Models.Course course,
        IFormFile? newImage = null)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course));

        try
        {
            // جيب الكورس القديم
            var existingCourse = await _unitOfWork.Courses.GetByIdAsync(courseId);
            if (existingCourse == null)
                throw new InvalidOperationException($"Course with ID {courseId} not found");

            // حدث البيانات الأساسية
            existingCourse.Name = course.Name;
            existingCourse.Description = course.Description;
            existingCourse.GroupId = course.GroupId;

            // لو في صورة جديدة
            if (newImage != null && newImage.Length > 0)
            {
                // احذف الصورة القديمة
                if (!string.IsNullOrEmpty(existingCourse.ImageName))
                {
                    try
                    {
                        var oldPath = existingCourse.ImagePath + existingCourse.ImageName;
                        await _bunnyService.DeleteFileAsync(ZONE, oldPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete old image for course {CourseId}", courseId);
                    }
                }

                // رفع الصورة الجديدة
                (var fileName, var filePath) = await UploadImageAsync(newImage);
                existingCourse.ImageName = fileName;
                existingCourse.ImagePath = filePath;
                existingCourse.ImageZone = ZONE;
            }

            // حفظ التعديلات
            await _unitOfWork.Courses.UpdateAsync(existingCourse);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Course {CourseId} updated successfully", courseId);
            return existingCourse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update course {CourseId}", courseId);
            throw;
        }
    }

    // ==========================================
    // 🔹 حذف كورس
    // ==========================================
    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid course ID", nameof(id));

        try
        {
            // جيب الكورس عشان تحذف صورته
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            
            if (course != null && !string.IsNullOrEmpty(course.ImageName))
            {
                try
                {
                    var imagePath = course.ImagePath + course.ImageName;
                    await _bunnyService.DeleteFileAsync(ZONE, imagePath);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to delete image for course {CourseId}", id);
                }
            }

            // احذف من قاعدة البيانات
            await _unitOfWork.Courses.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Course {CourseId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete course {CourseId}", id);
            throw;
        }
    }
}
