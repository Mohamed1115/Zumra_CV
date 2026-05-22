using System.Text.Json.Serialization;

namespace Zumra.DTOs.Request.Jitsi;

public class JitsiJwtPayload
{
    [JsonPropertyName("iss")]
    public string Issuer { get; set; } = string.Empty;

    [JsonPropertyName("aud")]
    public string Audience { get; set; } = string.Empty;

    [JsonPropertyName("exp")]
    public long ExpirationTime { get; set; }

    [JsonPropertyName("nbf")]
    public long NotBefore { get; set; }

    [JsonPropertyName("iat")]
    public long IssuedAt { get; set; }

    [JsonPropertyName("room")]
    public string Room { get; set; } = string.Empty;

    [JsonPropertyName("sub")]
    public string Subject { get; set; } = string.Empty;

    [JsonPropertyName("context")]
    public JitsiJwtContext? Context { get; set; }

    [JsonPropertyName("moderator")]
    public bool Moderator { get; set; } = false;
}

public class JitsiJwtContext
{
    [JsonPropertyName("user")]
    public JitsiJwtUser? User { get; set; }

    [JsonPropertyName("features")]
    public JitsiJwtFeatures? Features { get; set; }
}

public class JitsiJwtUser
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }
}

public class JitsiJwtFeatures
{
    [JsonPropertyName("livestreaming")]
    public bool? Livestreaming { get; set; }

    [JsonPropertyName("recording")]
    public bool? Recording { get; set; }

    [JsonPropertyName("transcription")]
    public bool? Transcription { get; set; }

    [JsonPropertyName("outbound-call")]
    public bool? OutboundCall { get; set; }
}


