namespace Zumra.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    
    public string Type { get; set; }
    public string CreatedAt { get; set; }
    
    public string ImageZone { get; set; }
    public string ImagePath { get; set; }
    public string ImageName { get; set; }
    public string? ImageUrl { get; set; } // الـ URL الكامل للصورة على CDN
    public int GroupId { get; set; }
    public int FacilityId { get; set; }
    
    // Navigation Properties
    public Group Group { get; set; }
    public Facility Facility { get; set; }
    public ICollection<CourseBatches> CourseBatches { get; set; }
    public ICollection<Sections> Sections { get; set; }
    public ICollection<Lessons> Lessons { get; set; }
    public ICollection<Favorite> Favorites { get; set; }
}