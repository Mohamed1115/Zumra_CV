using System.ComponentModel.DataAnnotations;

namespace Zumra.DTOs.Request;

public class UpdateProfileRequest
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string Name { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format")]
    public string? PhoneNumber { get; set; }

    [MinLength(3, ErrorMessage = "Username must be at least 3 characters")]
    public string? UserName { get; set; }
}
