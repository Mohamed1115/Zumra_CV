namespace Zumra.DTOs.Request.Jitsi;

public class JitsiMeetingRequest
{
    public string RoomName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Avatar { get; set; }
    public bool IsModerator { get; set; } = false;
    public Dictionary<string, object>? CustomProperties { get; set; }
    public int? MeetingDuration { get; set; } // in minutes
    public bool EnableRecording { get; set; } = false;
    public bool EnableChat { get; set; } = true;
    public bool EnableScreenShare { get; set; } = true;
    public bool EnableWhiteboard { get; set; } = true;
}


