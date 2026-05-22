namespace Zumra.DTOs.Response.Facility;

/// <summary>
/// DTO آمن لإرجاع بيانات الـ Facility - يمنع Circular Reference ويخفي البيانات الحساسة
/// </summary>
public class FacilityResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}

/// <summary>
/// DTO لإرجاع بيانات الـ UserFacility مع الـ Facility - بدون User navigation property
/// </summary>
public class UserFacilityResponse
{
    public int FacilityId { get; set; }
    public string Role { get; set; }
    public DateTime JoinedAt { get; set; }
    public FacilityResponse Facility { get; set; }
}
