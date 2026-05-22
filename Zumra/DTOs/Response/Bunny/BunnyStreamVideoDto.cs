namespace Zumra.DTOs.Response.Bunny;

public class BunnyStreamVideoDto
{
    public string Guid { get; set; } = string.Empty;
    public string AuthorizationSignature { get; set; } = string.Empty;
    public long AuthorizationExpire { get; set; }
    public string LibraryId { get; set; } = string.Empty;
    public string UploadUrl { get; set; } = string.Empty;
    public string EmbedUrl { get; set; } = string.Empty;
}
