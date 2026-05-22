using Zumra.DTOs.Request.Jitsi;
using Zumra.DTOs.Response.Jitsi;

namespace Zumra.Application.Interfaces.Jitsi;

public interface IJitsiService
{
    Task<JitsiMeetingResponse> CreateMeetingAsync(JitsiMeetingRequest request);
    Task<string> GenerateJwtTokenAsync(JitsiMeetingRequest request);
    string GetMeetingUrl(string roomName, string? jwtToken = null);
    bool ValidateJwtToken(string token);
    Task<JitsiMeetingResponse> JoinMeetingAsync(string roomName, string displayName, bool isModerator = false);
}
