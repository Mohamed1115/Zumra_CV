using Zumra.Data;

namespace Zumra.Models;

public class Enrollments
{
    //-----------
    // Id
    // UserId
    // CourseBatchId
    // AccessType (Free | Paid | Grant)
    // Status (Active | Expired | Cancelled)
    // EnrolledAt
    // ExpiresAt (nullable)
    public int Id { get; set; }
    public string UserId { get; set; } // Changed from int to string to match ApplicationUser.Id
    public int CourseBatchId { get; set; }
    public string? AccessType {get; set;} 
    public string Status { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    // Navigation Properties
    public CourseBatches CourseBatch { get; set; }
    public ApplicationUser User { get; set; }
}