using Zumra.Data;

namespace Zumra.Models;

public class Favorite
{
    public string UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ApplicationUser User { get; set; }
    public Course Course { get; set; }
}
