using Microsoft.AspNetCore.Http; // Required for IFormFile

namespace Zumra.DTOs.Request.Facility;

public class FacilityCreateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public int CategoryId { get; set; }
    public IFormFile Image { get; set; }
}
