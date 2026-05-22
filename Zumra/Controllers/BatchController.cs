using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.CourseBatches;
using Zumra.Application.Interfaces.Facility;
using Zumra.Application.Interfaces.Course;
using Zumra.DTOs.Request.Batch;

namespace Zumra.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class BatchController:ControllerBase
{
    private readonly ILogger<BatchController> _logger;
    private readonly ICourseBatchesCommandService _courseBatchesCommandService;
    private readonly ICourseBatchesQueryService _courseBatchesQueryService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly ICourseQueryService _courseQueryService;

    public BatchController(
        ILogger<BatchController> logger, 
        ICourseBatchesCommandService courseBatchesCommandService, 
        ICourseBatchesQueryService courseBatchesQueryService, 
        IAuthorizationService authorizationService, 
        IFacilityQueryService facilityQueryService,
        ICourseQueryService courseQueryService)
    {
        _logger = logger;
        _courseBatchesCommandService = courseBatchesCommandService;
        _courseBatchesQueryService = courseBatchesQueryService;
        _authorizationService = authorizationService;
        _facilityQueryService = facilityQueryService;
        _courseQueryService = courseQueryService;
    }

    [HttpGet("Course/{courseId}")]
    public async Task<IActionResult> GetAll(int courseId)
    {
        try
        {
            var batches = await _courseBatchesQueryService.GetAllAsync(courseId);
            return Ok(new
            {
                success = true,
                Data = batches
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetAll batches for course {CourseId}", courseId);
            return StatusCode(500, new { success = false, message = e.Message });
        }
    }

    [HttpGet("Batch/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var batch = await _courseBatchesQueryService.GetByIdAsync(id);
            return Ok(new
                {
                    success = true,
                    Data = batch
                }
            );

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in GetById batch {Id}", id);
            return StatusCode(500, new { success = false, message = e.Message });
        }
    }

    [HttpPost("{FId}")]
    [Authorize]
    public async Task<IActionResult> Create(int FId, [FromBody] BatchCreateRequest request)
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
            
            var course = await _courseQueryService.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                return NotFound(new { success = false, message = "Course not found" });
            }

            if (course.FacilityId != FId)
            {
                _logger.LogWarning("Mismatch: Course {CourseId} does not belong to Facility {FacilityId}", request.CourseId, FId);
                return BadRequest(new { success = false, message = "Course does not belong to this facility" });
            }
            
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to create batch in facility {FacilityId} without permission",
                    FId
                );
                return Forbid();
            }
            
            // Map DTO to Model
            var batch = new CourseBatches
            {
                CourseId = request.CourseId,
                Title = request.Title,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Capacity = request.Capacity,
                Status = request.Status
            };
            
            await _courseBatchesCommandService.Create(batch);
            return Ok(new { success = true , Message = "Created successfully" });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Create batch for facility {FacilityId}", FId);
            return StatusCode(500, new { success = false, message = e.Message });
        }
    }

    [HttpPut("{FId}/{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int FId,int id, [FromBody] BatchUpdateRequest request)
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

            var course = await _courseQueryService.GetByIdAsync(request.CourseId);
            if (course == null)
            {
                return NotFound(new { success = false, message = "Course not found" });
            }

            if (course.FacilityId != FId)
            {
                _logger.LogWarning("Mismatch: Course {CourseId} does not belong to Facility {FacilityId}", request.CourseId, FId);
                return BadRequest(new { success = false, message = "Course does not belong to this facility" });
            }
            
            
            var authResult = await _authorizationService.AuthorizeAsync(
                User, 
                facility, 
                SD.FacilitySuperAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to update batch in facility {FacilityId} without permission",
                    FId
                );
                return Forbid();
            }
            
            // Map DTO to Model
            var batch = new CourseBatches
            {
                Id = id,
                CourseId = request.CourseId,
                Title = request.Title,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Capacity = request.Capacity,
                Status = request.Status
            };
            
            await _courseBatchesCommandService.Update(id, batch);
            return Ok(new { success = true , Message = "Updated successfully" });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Update batch {Id} for facility {FacilityId}", id, FId);
            return StatusCode(500, new { success = false, message = e.Message });
        }
    }

    [HttpDelete("{FId}/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int FId,int id)
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
                "User attempted to delete batch in facility {FacilityId} without permission",
                FId
            );
            return Forbid();
        }
        try
        {
            await _courseBatchesCommandService.Delete(id);
            return Ok(new { success = true , Message = "Deleted successfully" });
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error in Delete batch {Id} for facility {FacilityId}", id, FId);
            return StatusCode(500, new { success = false, message = e.Message });
        }
    }
    
    
    
    
}