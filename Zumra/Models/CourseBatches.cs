using System.Collections.Specialized;
using System.Collections.Generic;

namespace Zumra.Models;

public class CourseBatches
{
    
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int? Capacity { get; set; }
    public string Status  { get; set; }
    
    // Navigation Properties
    public Course Course { get; set; }
    public ICollection<Sections> Sections { get; set; }
    public ICollection<Lessons> Lessons { get; set; }
    public ICollection<Enrollments> Enrollments { get; set; }
    public ICollection<CourseContent> CourseContents { get; set; }
}