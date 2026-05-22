using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request.Course;

public class CourseUpdateRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int Cost { get; set; }
    
    [Required]
    public string Type { get; set; }
    
    [Required]
    public int GroupId { get; set; }
    
    [Required]
    public int FacilityId { get; set; }
    
    public IFormFile? Image { get; set; }
}
