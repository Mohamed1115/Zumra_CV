using Zumra.DTOs.Request.Jitsi;
using Zumra.DTOs.Response.Jitsi;
using Zumra.Application.Interfaces.Jitsi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.CourseContent;
using Zumra.Application.Interfaces.Facility;
using Zumra.Application.Interfaces.LessonLive;
using Zumra.Application.Interfaces.Lessons;
using Zumra.DTOs.Request.Jitsi;
using Zumra.DTOs.Request.Lesson;
using Zumra.Application.Interfaces.LessonRec;
using Zumra.Application.Interfaces.Bunny;

namespace Zumra.Controllers;

[Route("Api/[controller]/[action]")]
[ApiController]
public class LessonController:ControllerBase
{
    private readonly ILogger<LessonController> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IFacilityQueryService _facilityQueryService;
    private readonly ILessonsCommandService _lessonsCommandService;
    private readonly ILessonsQueryService _lessonsQueryService;
    private readonly ICourseContentCommandService _courseContentCommandService;
    private readonly IJitsiService _jitsiService;
    private readonly ILessonLiveCommandService _lessonLiveCommandService;
    private readonly ILessonRecCommandService _lessonRecCommandService;
    private readonly ILessonRecQueryService _lessonRecQueryService;
    private readonly IBunnyService _bunnyService;
    


    public  LessonController(ILogger<LessonController> logger, IAuthorizationService authorizationService, IFacilityQueryService facilityQueryService, ILessonsQueryService lessonsQueryService, ILessonsCommandService lessonsCommandService, ICourseContentCommandService courseContentCommandService, IJitsiService jitsiService, ILessonLiveCommandService lessonLiveCommandService, ILessonRecCommandService lessonRecCommandService, ILessonRecQueryService lessonRecQueryService, IBunnyService bunnyService)
    {
        _logger = logger;
        _authorizationService = authorizationService;
        _facilityQueryService = facilityQueryService;
        _lessonsQueryService = lessonsQueryService;
        _lessonsCommandService = lessonsCommandService;
        _courseContentCommandService = courseContentCommandService;
        _jitsiService = jitsiService;
        _lessonLiveCommandService = lessonLiveCommandService;
        _lessonRecCommandService = lessonRecCommandService;
        _lessonRecQueryService = lessonRecQueryService;
        _bunnyService = bunnyService;
    }

    [HttpPost("{FId}")]
    [Authorize]
    public async Task<IActionResult> Add([FromRoute]int FId,[FromForm]LessonReq lessonReq)
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
            var liv = await _lessonsCommandService.Create(lessonReq);
            return Ok(new
            {
                success = true,
                message = "Successfully added lesson",
                data = liv
                
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to add lesson for facility {FId}", FId);
            return StatusCode(500, new { success = false, message = "An error occurred adding lesson" });
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
            var less = await _lessonsQueryService.GetByIdAsync(id);
            if (less == null)
                return NotFound(new
                    {
                        success = false,
                        message = "Lesson not found"
                    }
                );
            if (less.CourseContentId.HasValue)
                await _courseContentCommandService.Delete(less.CourseContentId.Value);
            await _lessonsCommandService.Delete(less.Id);
            return Ok(new
            {
                success = true,
                message = "Successfully deleted lesson"
            });


        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete lesson {LessonId} in facility {FId}", id, FId);
            return StatusCode(500, new { success = false, message = "An error occurred deleting lesson" });
        }
    }

    [HttpPost("{FId}/{id}/Live")]
    [Authorize]
    public async Task<IActionResult> CreateLive([FromRoute] int FId, [FromRoute] int id,
        [FromForm] JitsiMeetingRequest JitsiMeetingReq)
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
            
            
            var meet =await _jitsiService.CreateMeetingAsync(JitsiMeetingReq);
            var meetUrl = await _lessonLiveCommandService.AddAsync(meet);
            var lesson = await _lessonsQueryService.GetByIdAsync(id);
            if (lesson == null)
                return NotFound();
            lesson.MeetingId = meetUrl.Id;
            await _lessonsCommandService.Update(id,lesson);
            
            return Ok(new
            {
                success = true,
                message = "Successfully added Meeting",
                data = new
                {
                    meetingId   = meetUrl.Id,
                    meetingUrl  = meetUrl.MeetingUrl,
                    roomName    = meetUrl.RoomName,
                    startTime   = meetUrl.StartTime,
                    endTime     = meetUrl.EndTime
                }
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create live meeting for lesson {LessonId} in facility {FId}", id, FId);
            return StatusCode(500, new { success = false, message = "An error occurred creating live meeting" });
        }
    }

    [HttpPost("{FId}/{id}/Video")]
    [Authorize]
    public async Task<IActionResult> CreateVideo([FromRoute] int FId, [FromRoute] int id, [FromForm] CreateVideoReq req)
    {
        try
        {
            var facility = await _facilityQueryService.GetByIdAsync(FId);
            if (facility == null)
            {
                return NotFound(new { success = false, message = "Facility not found" });
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, facility, SD.FacilityAdmin);
            if (!authResult.Succeeded)
            {
                _logger.LogWarning("User attempted to change in facility {FacilityId} without permission", FId);
                return Forbid();
            }

            var lesson = await _lessonsQueryService.GetByIdAsync(id);
            if (lesson == null)
                return NotFound(new { success = false, message = "Lesson not found" });

            // 1. Create Video Placeholder in Bunny Stream
            var bunnyVideo = await _bunnyService.CreateStreamVideoAsync(lesson.Name);

            // 2. Create LessonRec with Bunny URL
            var lessonRec = new Zumra.Models.LessonRec
            {
                VideoUrl = bunnyVideo.EmbedUrl, // Link the DB record to the Bunny Video
                Duration = req.Duration,
                VideoSize = req.VideoSize,
                VideoFormat = req.VideoFormat,
                VideoQuality = req.VideoQuality,
                UploadedAt = DateTime.UtcNow,
                IsProcessed = false // Set to false until upload is confirmed/processed? Or true given we have the ID?
            };

            var createdRec = await _lessonRecCommandService.AddAsync(lessonRec);
            
            lesson.VideoId = createdRec.Id;
            await _lessonsCommandService.Update(id, lesson);

            return Ok(new
            {
                success = true,
                message = "Successfully added Video",
                data = createdRec,
                link = bunnyVideo // Contains UploadUrl and AuthorizationSignature
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create video for lesson {LessonId} in facility {FId}", id, FId);
            return StatusCode(500, new { success = false, message = "An error occurred creating video" });
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetByBatchId([FromQuery] int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = "Invalid batch ID" });

            var lessons = await _lessonsQueryService.GetByBatchIdAsync(id);

            return Ok(new
            {
                success = true,
                data = lessons
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get lessons for batch {BatchId}", id);
            return StatusCode(500, new { success = false, message = "An error occurred getting lessons" });
        }
    }
    // [HttpPost("{FId}")]
    // public async Task<IActionResult> PrepareVideoUpload([FromQuery] int FId, [FromQuery] string title)
    // {
    //     try
    //     {
    //         var facility = await _facilityQueryService.GetByIdAsync(FId);
    //         if (facility == null)
    //             return NotFound(new { success = false, message = "Facility not found" });
    //
    //         var authResult = await _authorizationService.AuthorizeAsync(User, facility, SD.FacilityAdmin);
    //         if (!authResult.Succeeded)
    //             return Forbid();
    //
    //         var result = await _bunnyService.CreateStreamVideoAsync(title);
    //         
    //         return Ok(new { success = true, data = result });
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e.Message);
    //         return StatusCode(500, new { success = false, message = e.Message });
    //     }
    // }
}