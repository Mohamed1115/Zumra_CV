namespace Zumra.Models;

public class Sections
{
    //Sections
    // --------
    // Id
    // CourseId
    // CourseBatchId (nullable)
    // Title
    // Order
    // 
    public int Id { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public int CourseId { get; set; }
    public int CourseBatchId { get; set; }
    
    // Navigation Properties
    public Course Course { get; set; }
    public CourseBatches CourseBatch { get; set; }
    public ICollection<Tasks> Tasks { get; set; }
    public ICollection<CourseContent> CourseContents { get; set; }
    
    
    
}