using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request.Group;

public class GroupUpdateRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public int FacilityId { get; set; }
}
