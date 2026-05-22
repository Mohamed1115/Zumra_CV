namespace Zumra.DTOs.Request.Facility;

public class UserFacilityDto
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public int FacilityId { get; set; }
    public string FacilityName { get; set; }
    public FacilityRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}