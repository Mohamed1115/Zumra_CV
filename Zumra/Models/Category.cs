namespace Zumra.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageZone { get; set; }
    public string ImagePath { get; set; }
    public string ImageName { get; set; }
    public string? ImageUrl { get; set; } // الـ URL الكامل للصورة على CDN
    public List<Facility>? Facilities { get; set; }
}