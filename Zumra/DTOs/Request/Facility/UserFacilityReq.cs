namespace Zumra.DTOs.Request.Facility;

public class UserFacilityReq
{
    public string UserId { get; set; }
    // public ApplicationUser User { get; set; }
    public int FacilityId { get; set; }
    // public Facility Facility { get; set; }
    public FacilityRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}