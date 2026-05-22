using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Facility;

namespace Zumra.Controllers;
[Route("Api/[controller]")]
[ApiController]
public class CourseContentController:ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly ILogger<CourseContentController> _logger;

    public CourseContentController(IAuthorizationService authorizationService, IFacilityQueryService facilityQueryService, ILogger<CourseContentController> logger)
    {
        _authorizationService = authorizationService;
        _facilityQueryService = facilityQueryService;
        _logger = logger;
    }
    
    
}