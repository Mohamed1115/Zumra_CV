using Zumra.Data;
namespace Zumra.Models;

public class TaskSubmissions
{
    //Id
    // TaskId
    // UserId
    // SubmissionUrl   ← اللينك اللي الطالب دخّله
    // SubmittedAt
    // Status (Submitted | Late)
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string UserId { get; set; }
    public string SubmissionUrl  { get; set; }
    public DateTime SubmissionAt { get; set; }
    public string Status { get; set; }
    
    // Navigation Properties
    public Tasks Task { get; set; }
    public ApplicationUser User { get; set; }
}