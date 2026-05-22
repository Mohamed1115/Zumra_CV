using System.Collections.Specialized;

namespace Zumra.Models;

public class CourseContent
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int CourseBatchId { get; set; }
    public int SectionId { get; set; }
    public string ContentType { get; set; } // "Lesson" or "Task"
    public int? ContentId { get; set; }
    public int CourseOrder { get; set; }
    
    // Navigation Properties
    public Course Course { get; set; }
    public CourseBatches CourseBatch { get; set; }
    public Sections Section { get; set; }
    
    // Polymorphic relationship - ContentType determines which is populated
    public Lessons? Lesson { get; set; }
    public Tasks? Task { get; set; }
}