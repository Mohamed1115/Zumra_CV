using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request.Category;

public class CategoryUpdateRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    public IFormFile? Image { get; set; }
}
