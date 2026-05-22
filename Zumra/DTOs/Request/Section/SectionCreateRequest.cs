using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request.Section;

public class SectionCreateRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int Order { get; set; }
    
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public int CourseBatchId { get; set; }
}
