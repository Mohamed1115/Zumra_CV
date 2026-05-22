using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Course;
using Zumra.Application.Interfaces.Facility;
using Zumra.DTOs.Request.Course;

namespace Zumra.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseQueryService _courseQueryService;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly ICourseCommandService _courseCommandService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<CourseController> _logger;

    public CourseController(
        ICourseQueryService courseQueryService,
        ICourseCommandService courseCommandService,
        ILogger<CourseController> logger, IFacilityQueryService facilityQueryService, IAuthorizationService authorizationService)
    {
        _courseQueryService = courseQueryService;
        _courseCommandService = courseCommandService;
        _logger = logger;
        _facilityQueryService = facilityQueryService;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        try
        {
            var courses = await _courseQueryService.GetAllAsync();
            return Ok(new
            {
                success = true,
                data = courses
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all courses");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting all courses"
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        try
        {
            var course = await _courseQueryService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Course not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = course
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting course");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting course"
            });
        }
    }

    [HttpPost("{id}")]
    [Authorize]
    public async Task<IActionResult> CreateCourse(int id,[FromForm] CourseCreateRequest request)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(id);
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
                    "User attempted to delete facility {FacilityId} without permission",
                    id
                );
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to existing format for service
            var courseDto = new CourseCreat
            {
                Name = request.Name,
                Description = request.Description,
                Cost = request.Cost,
                Type = request.Type,
                GroupId = request.GroupId,
                FacilityId = id
            };

            var createdCourse = await _courseCommandService.Create(courseDto, request.Image);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.Id }, new
            {
                success = true,
                message = "Course created successfully",
                data = createdCourse
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating course");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred creating course"
            });
        }
    }

    [HttpPut("{Fid}/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCourse(int Fid,int id, [FromForm] CourseUpdateRequest request)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(Fid);
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
                    "User attempted to delete facility {FacilityId} without permission",
                    id
                );
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Model
            var course = new Course
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Cost = request.Cost,
                Type = request.Type,
                GroupId = request.GroupId,
                FacilityId = request.FacilityId
            };

            var updatedCourse = await _courseCommandService.Update(id, course, request.Image);
            return Ok(new
            {
                success = true,
                message = "Course updated successfully",
                data = updatedCourse
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating course");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred updating course"
            });
        }
    }

    [HttpDelete("{Fid}/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCourse(int Fid,int id)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(Fid);
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
                SD.FacilityLeader
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to delete facility {FacilityId} without permission",
                    id
                );
                return Forbid();
            }
            await _courseCommandService.Delete(id);
            return Ok(new
            {
                success = true,
                message = "Course deleted successfully"
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting course");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred deleting course"
            });
        }
    }

    [HttpGet("my-enrollments")]
    [Authorize]
    public async Task<IActionResult> GetMyEnrollments()
    {
        return Ok();
    }
}
