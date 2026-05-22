namespace Zumra.Models;

public class LessonLive
{
    public int Id { get; set; }
    
    // Live Lesson Properties
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string MeetingUrl { get; set; } = string.Empty;
    public string RoomName { get; set; } // Zoom, Teams, Google Meet, etc.
    // public string? MeetingPassword { get; set; }
    // public int? MaxParticipants { get; set; }
    
    // Navigation Property
    public Lessons? Lesson { get; set; }
    
}