namespace Zumra.Models;

public class LessonRec
{
    public int Id { get; set; }
    
    // Recorded Lesson Properties
    public string VideoUrl { get; set; } = string.Empty;
    public float Duration { get; set; } // in minutes
    public long? VideoSize { get; set; } // in bytes
    public string? VideoFormat { get; set; } // mp4, mkv, etc.
    public string? VideoQuality { get; set; } // 720p, 1080p, etc.
    public DateTime? UploadedAt { get; set; }
    public bool IsProcessed { get; set; } = false;
    
    // Navigation Property
    public Lessons? Lesson { get; set; }
}