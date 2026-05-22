namespace Zumra.DTOs.Response.Jitsi;

public class JitsiMeetingResponse
{
    public string MeetingUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } = string.Empty;
    public string JwtToken { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsModerator { get; set; }
    public Dictionary<string, object>? CustomProperties { get; set; }
    public JitsiMeetingSettings Settings { get; set; } = new();
}

public class JitsiMeetingSettings
{
    public bool EnableRecording { get; set; }
    public bool EnableChat { get; set; }
    public bool EnableScreenShare { get; set; }
    public bool EnableWhiteboard { get; set; }
    public int? MeetingDuration { get; set; }
}


