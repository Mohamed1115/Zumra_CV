namespace Zumra.DTOs.Request.Lesson;

public class LessonReq
{
    // public int Id { get; set; }
    public int CourseId  { get; set; }
    public int CourseBatchId  { get; set; }
    public int SectionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? MeetingId { get; set; }
    public int? VideoId { get; set; }
    public float Duration { get; set; }
    // public DateTime CreatedAt { get; set; }
    // public int CourseContentId { get; set; }
    
}