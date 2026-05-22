namespace Zumra.DTOs.Request.Course;

public class CourseCreat
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public string Type { get; set; }
    public int GroupId { get; set; }
    public int FacilityId { get; set; }
}
