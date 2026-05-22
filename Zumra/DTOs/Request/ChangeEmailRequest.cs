using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request;

public class ChangeEmailRequest
{
    [Required(ErrorMessage = "New email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string NewEmail { get; set; }
}
