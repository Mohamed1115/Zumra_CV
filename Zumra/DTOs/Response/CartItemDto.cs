namespace Zumra.DTOs.Response;

public class CartItemDto
{
    public int EnrollmentId { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string? CourseImage { get; set; }
    public int BatchId { get; set; }
    public string BatchTitle { get; set; } = string.Empty;
    public decimal CourseCost { get; set; }
}
