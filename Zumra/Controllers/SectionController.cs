using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Facility;
using Zumra.Application.Interfaces.Sections;
using Zumra.DTOs.Request.Section;

namespace Zumra.Controllers;
[Route("Api/[controller]")]
[ApiController]
public class SectionController:ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly ILogger<SectionController> _logger;
    private readonly ISectionsQueryService _sectionsQueryService;
    private readonly ISectionsCommandService _sectionsCommandService;

    public  SectionController(IFacilityQueryService facilityQueryService, IAuthorizationService authorizationService, ILogger<SectionController> logger, ISectionsCommandService sectionsCommandService, ISectionsQueryService sectionsQueryService)
    {
        _facilityQueryService = facilityQueryService;
        _authorizationService = authorizationService;
        _logger = logger;
        _sectionsCommandService = sectionsCommandService;
        _sectionsQueryService = sectionsQueryService;
    }


    [HttpPost("{FId}/{BId}")]
    [Authorize]
    public async Task<IActionResult> Create(int FId,int BId,[FromBody] SectionCreateRequest request)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(FId);
            if (facility == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Facility not found"
                });
            }
            
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to create section in facility {FacilityId} without permission",
                    FId
                );
                return Forbid();
            }
            
            
            
            // Map DTO to Model
            var section = new Models.Sections
            {
                Name = request.Name,
                Order = request.Order,
                CourseId = request.CourseId,
                CourseBatchId = request.CourseBatchId
            };
            
            await _sectionsCommandService.Create(section);

            var sec = await _sectionsQueryService.GetAllAsync(BId);
            
            return Ok(new{success = true, message = "Successfully create section",Data = sec});
            

        }
        catch (Exception e)
        {
            
            _logger.LogError(e, "Failed to create section");
            return StatusCode(500, new { success = false, message = "An error occurred creating section" });
        }
    }

    [HttpDelete("{FId}/{BId}/{SId}")]
    [Authorize]
    public async Task<IActionResult> Delete(int FId,int BId, int SId)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(FId);
            if (facility == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Facility not found"
                });
            }
            
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to delete section in facility {FacilityId} without permission",
                    FId
                );
                return Forbid();
            }
            await _sectionsCommandService.Delete(SId);
            
            var sec = await _sectionsQueryService.GetAllAsync(BId);
            return Ok(new{success = true, message = "Successfully delete section", Data = sec});
            

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete section {SectionId}", SId);
            return StatusCode(500, new { success = false, message = "An error occurred deleting section" });
        }
    }
    
    [HttpGet("Batch/{BId}")]
    public async Task<IActionResult> GetByBatch(int BId)
    {
        try
        {
            var sec = await _sectionsQueryService.GetAllAsync(BId);
            return Ok(new { success = true, Data = sec });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get sections for batch {BatchId}", BId);
            return StatusCode(500, new { success = false, message = "An error occurred fetching sections" });
        }
    }
}
