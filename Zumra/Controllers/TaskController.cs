using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.CourseContent;
using Zumra.Application.Interfaces.Facility;
using Zumra.Application.Interfaces.Tasks;
using Zumra.DTOs.Request.Lesson;
using Zumra.DTOs.Request.Task;

namespace Zumra.Controllers;
[Route("Api/[controller]/[action]")]
[ApiController]
public class TaskController:ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly ICourseContentCommandService _courseContentCommandService;
    private readonly ITasksQueryService _tasksQueryService;
    private readonly ITasksCommandService _tasksCommandService;

    public  TaskController(ILogger<TaskController> logger, IAuthorizationService authorizationService, IFacilityQueryService facilityQueryService, ICourseContentCommandService courseContentCommandService, ITasksQueryService tasksQueryService, ITasksCommandService tasksCommandService)
    {
        _logger = logger;
        _authorizationService = authorizationService;
        _facilityQueryService = facilityQueryService;
        _courseContentCommandService = courseContentCommandService;
        _tasksQueryService = tasksQueryService;
        _tasksCommandService = tasksCommandService;
    }

    [HttpPost("{FId}")]
    [Authorize]
    public async Task<IActionResult> Add([FromRoute] int FId, [FromForm] TaskReq taskReq)
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
                SD.FacilityAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to change in facility {FacilityId} without permission",
                    FId
                );
                return Forbid();
            }

            await _tasksCommandService.Create(taskReq);
            return Ok(new
            {
                success = true,
                message = "Successfully added task"
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add task for facility {FId}", FId);
            return StatusCode(500, new { success = false, message = "An error occurred adding task" });
        }
        
    }
    [HttpDelete("{FId}/{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute]int FId,[FromRoute] int id)
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
                SD.FacilityAdmin
            );
    
            if (!authResult.Succeeded)
            {
                _logger.LogWarning(
                    "User attempted to change in facility {FacilityId} without permission",
                    FId
                );
                return Forbid();
            }
            var tes = await _tasksQueryService.GetByIdAsync(id);
            if (tes == null)
                return NotFound(new
                    {
                        success = false,
                        message = "Task not found"
                    }
                );
            await _courseContentCommandService.Delete(tes.CourseContentId);
            await _tasksCommandService.Delete(tes.Id);
            return Ok(new
            {
                success = true,
                message = "Successfully deleted Task"
            });


        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete task {TaskId} in facility {FId}", id, FId);
            return StatusCode(500, new { success = false, message = "An error occurred deleting task" });
        }
    }
}