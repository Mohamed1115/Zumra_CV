namespace Zumra.DTOs.Response;

/// <summary>
/// Standard error response for API endpoints
/// </summary>
public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string? Error { get; set; } // Only populated in Development environment
}
