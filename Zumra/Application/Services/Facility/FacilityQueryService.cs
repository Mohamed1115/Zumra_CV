using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Interfaces.Facility;
using Zumra.DTOs.Request.Facility;
using Zumra.DTOs.Response.Facility;

namespace Zumra.Application.Services.Facility;

public class FacilityQueryService : IFacilityQueryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBunnyService _bunnyService;
    private readonly ILogger<FacilityQueryService> _logger;

    public FacilityQueryService(IUnitOfWork unitOfWork, IBunnyService bunnyService, ILogger<FacilityQueryService> logger)
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
    
    public async Task<List<Models.Facility>> GetAllAsync()
    {
        try
        {
            var facilities = await _unitOfWork.Facilities.GetAllAsync();
            
            // بناء الـ URL الكامل للصور
            foreach (var facility in facilities)
            {
                facility.ImageUrl = BuildImageUrl(facility.ImageName, facility.ImagePath, facility.ImageZone);
            }
            
            return facilities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all facilities");
            throw;
        }
    }

    public async Task<Models.Facility> GetByIdAsync(int id)
    {
        try
        {
            var facility = await _unitOfWork.Facilities.GetByIdAsync(id);
            
            if (facility == null)
            {
                _logger.LogWarning("Facility with ID {FacilityId} not found", id);
                return null;
            }

            // بناء الـ URL الكامل للصورة
            facility.ImageUrl = BuildImageUrl(facility.ImageName, facility.ImagePath, facility.ImageZone);
            
            return facility;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve facility {FacilityId}", id);
            throw;
        }
    }
    public async Task<List<UserFacilityResponse>> GetByUserIdAsync(string id)
    {
        try
        {
            var userFacilities = await _unitOfWork.UserFacility.GetAllFacilitiesByUser(id);
            
            if (userFacilities == null || !userFacilities.Any())
            {
                _logger.LogWarning("No facilities found for user {UserId}", id);
                return new List<UserFacilityResponse>();
            }

            // Map إلى DTO - يمنع Circular Reference ويحمي البيانات الحساسة
            var result = userFacilities.Select(uf => new UserFacilityResponse
            {
                FacilityId = uf.FacilityId,
                Role = uf.Role.ToString(),
                JoinedAt = uf.CreatedAt,
                Facility = new FacilityResponse
                {
                    Id = uf.Facility.Id,
                    Name = uf.Facility.Name,
                    Description = uf.Facility.Description,
                    Type = uf.Facility.Type,
                    Status = uf.Facility.Status,
                    ImageUrl = BuildImageUrl(uf.Facility.ImageName, uf.Facility.ImagePath, uf.Facility.ImageZone),
                    CategoryId = uf.Facility.CategoryId,
                    CategoryName = uf.Facility.Category?.Name
                }
            }).ToList();
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve facilities for user {UserId}", id);
            throw;
        }
    }

    public async Task<List<UserFacilityDto>> GetByFacilityAsync(int Id)
    {
        var list =await _unitOfWork.UserFacility.GetAllByFacilityId2(Id);
        return list;
    }
    
}