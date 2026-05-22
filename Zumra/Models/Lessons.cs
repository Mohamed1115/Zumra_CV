using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zumra.Models;

public class Lessons
{
    // Primary Key
    public int Id { get; set; }
    
    // Basic Properties
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty; // "Live" | "Recorded" | "Material"
    
    public int Order { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Foreign Keys
    public int CourseId { get; set; }
    public int? CourseBatchId { get; set; }
    public int? CourseContentId { get; set; }
    
    // One-to-One Relationship with LessonLive (nullable)
    public int? MeetingId { get; set; }
    [ForeignKey(nameof(MeetingId))]
    public LessonLive? Live { get; set; }
    
    // One-to-One Relationship with LessonRec (nullable)
    public int? VideoId { get; set; }
    [ForeignKey(nameof(VideoId))]
    public LessonRec? Rec { get; set; }
    
    // Navigation Properties
    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = null!;
    
    [ForeignKey(nameof(CourseBatchId))]
    public CourseBatches? CourseBatch { get; set; }
    
    // Uncomment when CourseContent is ready
    // [ForeignKey(nameof(CourseContentId))]
    // public CourseContent CourseContent { get; set; }
}