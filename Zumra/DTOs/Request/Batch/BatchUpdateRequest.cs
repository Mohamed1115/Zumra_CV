using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request.Batch;

public class BatchUpdateRequest
{
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string StartDate { get; set; }
    
    [Required]
    public string EndDate { get; set; }
    
    public int? Capacity { get; set; }
    
    [Required]
    public string Status { get; set; }
}
