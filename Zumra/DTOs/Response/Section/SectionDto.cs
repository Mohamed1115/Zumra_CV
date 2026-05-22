namespace Zumra.DTOs.Response.Section;

/// <summary>
/// DTO للـ Section - بيمثل قسم من أقسام الكورس
/// </summary>
public class SectionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Order { get; set; } // ترتيب القسم
    
    // قائمة المحتويات (دروس أو مهام)
    public List<ContentDto> Contents { get; set; } = new();
}

/// <summary>
/// DTO للمحتوى - ممكن يكون Lesson أو Task
/// </summary>
public class ContentDto
{
    // نوع المحتوى: "Lesson" أو "Task"
    public string Type { get; set; } = null!;
    public int Order { get; set; } // ترتيب المحتوى
    
    // إذا كان المحتوى درس
    public LessonDto? Lesson { get; set; }
    
    // إذا كان المحتوى مهمة
    public TaskDto? Task { get; set; }
}

/// <summary>
/// DTO للدرس - ممكن يكون Live أو Recorded
/// </summary>
public class LessonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; }
    
    // بيانات الدرس المباشر (إذا كان live)
    public LiveDto? Live { get; set; }
    
    // بيانات الدرس المسجل (إذا كان recorded)
    public RecDto? Rec { get; set; }
}

/// <summary>
/// DTO للمهمة
/// </summary>
public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public float? MaxScore { get; set; }
}

/// <summary>
/// DTO للدرس المباشر (Live Lesson)
/// </summary>
public class LiveDto
{
    public string MeetingUrl { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? RoomName { get; set; }
}

/// <summary>
/// DTO للدرس المسجل (Recorded Lesson)
/// </summary>
public class RecDto
{
    public string VideoUrl { get; set; } = string.Empty;
    public float Duration { get; set; }
    public string? VideoQuality { get; set; }
}
