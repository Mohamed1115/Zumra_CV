namespace Zumra.DTOs.Request.Task;

public class TaskReq
{
    
    public int CourseId  { get; set; }
    public int CourseBatchId  { get; set; }
    public int SectionId { get; set; }
    
    
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string? FormUrl { get; set; }
    public int MaxScore { get; set; }
    public DateTime Deadline { get; set; }
    
    
    
}