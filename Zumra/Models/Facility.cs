using Zumra.Data;

namespace Zumra.Models;

public class Facility
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string UserID {get; set;}
    
    public string ImageZone { get; set; }
    public string ImagePath { get; set; }
    public string? ImageName { get; set; }
    public string? ImageUrl { get; set; } // الـ URL الكامل للصورة على CDN

    public string Status { get; set; } = SD.Pending;
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    
    public ICollection<UserFacility> UserFacilities { get; set; }
    public List<Group> Groups { get; set; }
    public ApplicationUser User { get; set; }
}