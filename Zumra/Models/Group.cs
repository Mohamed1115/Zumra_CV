namespace Zumra.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int FacilityId { get; set; }
    public Facility Facility { get; set; }
    public List<Course> Courses { get; set; }
    
}