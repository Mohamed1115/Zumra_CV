using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.Course;

namespace Zumra.Application.Services.Course;

public class CourseQueryService : ICourseQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<CourseQueryService> _logger;

    public CourseQueryService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<CourseQueryService> logger)
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

    public async Task<List<Models.Course>> GetAllAsync()
    {
        try
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            
            // بناء الـ URL الكامل للصور
            foreach (var course in courses)
            {
                course.ImageUrl = BuildImageUrl(course.ImageName, course.ImagePath, course.ImageZone);
            }
            
            return courses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all courses");
            throw;
        }
    }

    public async Task<Models.Course?> GetByIdAsync(int id)
    {
        try
        {
            var course = await _unitOfWork.Courses.GetAllByIdAsync(id);
            
            if (course == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found", id);
                return null;
            }

            // بناء الـ URL الكامل للصورة
            course.ImageUrl = BuildImageUrl(course.ImageName, course.ImagePath, course.ImageZone);
            
            return course;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve course {CourseId}", id);
            throw;
        }
    }
}
