using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Facility;
using Zumra.Application.Interfaces.Group;
using Zumra.DTOs.Request.Group;

namespace Zumra.Controllers;


[Route("api/[controller]")]
[ApiController]
public class GroupController:ControllerBase
{
    
    private readonly IGroupQueryService _groupQueryService;
    private readonly IGroupCommandService _groupCommandService;
    private readonly ILogger<GroupController> _logger;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly IAuthorizationService _authorizationService;


    public GroupController(IGroupQueryService groupQueryService,
        IGroupCommandService groupCommandService,
        ILogger<GroupController> logger, IFacilityQueryService facilityQueryService, IAuthorizationService authorizationService)
    {
        _groupQueryService = groupQueryService;
        _groupCommandService = groupCommandService;
        _logger = logger;
        _facilityQueryService = facilityQueryService;
        _authorizationService = authorizationService;
    }

    [HttpGet("{facilityId}")]
    public async Task<IActionResult> GetGroups([FromRoute]int facilityId)
    {
        try
        {
            var grp = await _groupQueryService.GetAllGroups(facilityId);
            return Ok(new
            {
                success = true,
                data = grp
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all Groups");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting all Groups"
            });
        }
    }
    
    
    [HttpGet("{facilityId}/{id}")]
    public async Task<IActionResult> GetGroup([FromRoute]int facilityId,[FromRoute]int id)
    {
        try
        {
            var cat = await _groupQueryService.GetGroup(id,facilityId);
            return Ok(new
            {
                success = true,
                data = cat
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting Group");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting Group"
            });
        }
    }


    [HttpPost("{facilityId}")]
    [Authorize]
    public async Task<IActionResult> CreateGroup([FromRoute]int facilityId,[FromBody] GroupCreateRequest request)
    {
        try
        {

            var facility = await _facilityQueryService.GetByIdAsync(facilityId);
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
                    "User attempted to update facility {FacilityId} without permission",
                    facilityId
                );
                return Forbid();
            }
            
            // Map DTO to Model
            var group = new Group
            {
                Name = request.Name,
                Description = request.Description,
                FacilityId = facilityId
            };
            
            await _groupCommandService.Create(group);
            return Ok(new
            {
                success = true,
                message = "Group created successfully"
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating Group");
            return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred creating Group"
                }
            );
        }
    }

    [HttpPut("{facilityId}")]
    [Authorize]
    public async Task<IActionResult> UpdateGroup([FromRoute]int facilityId,[FromBody] GroupUpdateRequest request)
    {
        try
        { 
            var facility = await _facilityQueryService.GetByIdAsync(facilityId);
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
                    "User attempted to update facility {FacilityId} without permission",
                    facilityId
                );
                return Forbid();
            }

            // Map DTO to Model
            var group = new Group
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                FacilityId = facilityId
            };

            await _groupCommandService.Update(group);
            return Ok(new { success = true, message = "Group updated successfully" });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating Group");
            return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred updating Group"
                }
            );
        }
    }

    [HttpDelete("{facilityId}/{id}")]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public async Task<IActionResult> DeleteGroup([FromRoute]int facilityId,[FromRoute]int id)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(facilityId);
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
                    "User attempted to update facility {FacilityId} without permission",
                    facilityId
                );
                return Forbid();
            }
            await _groupCommandService.Delete(id);
            return Ok(new
            {
                success = true,
                message = "Group deleted successfully"
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting Group");
            return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred deleting Group"
                }
            );
        }
    }
    
    
}