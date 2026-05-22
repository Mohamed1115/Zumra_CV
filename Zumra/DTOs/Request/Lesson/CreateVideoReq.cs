namespace Zumra.DTOs.Request.Lesson;

public class CreateVideoReq
{
    public string VideoUrl { get; set; } = string.Empty;
    public float Duration { get; set; } // in minutes
    public long? VideoSize { get; set; } // in bytes
    public string? VideoFormat { get; set; } // mp4, mkv, etc.
    public string? VideoQuality { get; set; } // 720p, 1080p, etc.
}
