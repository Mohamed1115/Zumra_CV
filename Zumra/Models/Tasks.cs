namespace Zumra.Models;

public class Tasks
{
    //LessonTasks
    // -----------
    // Id
    // SectionId
    // Title
    // Description
    // Type (Assignment | Quiz | Practice)
    // MaxScore
    // DeadlineAt
    // Order
    // 
    public int Id { get; set; }
    public int SectionId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string? FormUrl { get; set; }
    public int MaxScore { get; set; }
    public DateTime Deadline { get; set; }
    
    // Navigation Properties
    public Sections Section { get; set; }
    public ICollection<TaskSubmissions> TaskSubmissions { get; set; }
    // public ICollection<CourseContent> CourseContents { get; set; }
    
    public int CourseContentId { get; set; }
    // public CourseContent? CourseContent { get; set; }
    
}