using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Zumra.DTOs.Request.Jitsi;
using Zumra.DTOs.Response.Jitsi;
using Zumra.Application.Interfaces.Jitsi;

namespace Zumra.Application.Services.Jitsi;

public class JitsiService : IJitsiService
{
    private readonly JitsiConfiguration _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<JitsiService> _logger;
    private readonly IConfiguration _configuration;


    public JitsiService(
        IOptions<JitsiConfiguration> config,
        IHttpClientFactory httpClientFactory,
        ILogger<JitsiService> logger, IConfiguration configuration)
    {
        _config = config.Value;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<JitsiMeetingResponse> CreateMeetingAsync(JitsiMeetingRequest request)
    {
        try
        {
            var jwtToken = await GenerateJwtTokenAsync(request);
            var meetingUrl = GetMeetingUrl(request.RoomName, jwtToken);

            var response = new JitsiMeetingResponse
            {
                MeetingUrl = meetingUrl,
                RoomName = request.RoomName,
                JwtToken = jwtToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                IsModerator = request.IsModerator,
                CustomProperties = request.CustomProperties,
                Settings = new JitsiMeetingSettings
                {
                    EnableRecording = request.EnableRecording,
                    EnableChat = request.EnableChat,
                    EnableScreenShare = request.EnableScreenShare,
                    EnableWhiteboard = request.EnableWhiteboard,
                    MeetingDuration = request.MeetingDuration
                }
            };

            _logger.LogInformation("Created Jitsi meeting for room: {RoomName}", request.RoomName);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create Jitsi meeting for room: {RoomName}", request.RoomName);
            throw;
        }
    }

    public Task<string> GenerateJwtTokenAsync(JitsiMeetingRequest request)
    {
        var privateKeyPem = _configuration["Jitsi:PrivateKey"];
        var appId         = _configuration["Jitsi:AppId"]!;   // e.g. vpaas-magic-cookie-xxxx
        var keyId         = _configuration["Jitsi:KeyId"]!;   // Key ID from 8x8 developer console

        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem);

        // kid MUST be "AppId/KeyId" — 8x8 uses it to look up the public key
        var key = new RsaSecurityKey(rsa)
        {
            KeyId = $"{appId}/{keyId}",
            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
        };
        var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);

        var now = DateTimeOffset.UtcNow;

        var payload = new JwtPayload
        {
            // aud  → hardcoded "jitsi"
            { "aud", "jitsi" },
            // iss  → hardcoded "chat"
            { "iss", "chat" },
            // sub  → your AppId (tenant)
            { "sub", appId },
            { "room", request.RoomName },
            { "nbf", now.ToUnixTimeSeconds() },
            { "exp", now.AddHours(24).ToUnixTimeSeconds() },
            { "context", new Dictionary<string, object>
                {
                    { "user", new Dictionary<string, object>
                        {
                            { "name", string.IsNullOrWhiteSpace(request.DisplayName)
                                ? _configuration["Jitsi:DefaultDisplayName"] ?? "Participant"
                                : request.DisplayName },
                            { "email",     request.Email  ?? "" },
                            { "avatar",    request.Avatar ?? "" },
                            // moderator lives inside context.user, as string "true"/"false"
                            { "moderator", request.IsModerator ? "true" : "false" }
                        }
                    },
                    { "features", new Dictionary<string, object>
                        {
                            { "livestreaming", request.EnableRecording ? "true" : "false" },
                            { "recording",     request.EnableRecording ? "true" : "false" }
                        }
                    }
                }
            }
        };

        var token = new JwtSecurityToken(
            new JwtHeader(creds),
            payload
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(tokenString);
    }

    public string GetMeetingUrl(string roomName, string? jwtToken = null)
    {
        try
        {
            var baseUrl = _config.BaseUrl.TrimEnd('/');
            var appId   = _config.AppId;
            var encodedRoomName = Uri.EscapeDataString(roomName);
            
            // 8x8 JaaS URL format: https://8x8.vc/{AppId}/{roomName}
            var meetingUrl = jwtToken != null 
                ? $"{baseUrl}/{appId}/{encodedRoomName}?jwt={jwtToken}"
                : $"{baseUrl}/{appId}/{encodedRoomName}";

            _logger.LogInformation("Generated meeting URL for room: {RoomName}", roomName);
            return meetingUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to generate meeting URL for room: {RoomName}", roomName);
            throw;
        }
    }

    public bool ValidateJwtToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var privateKeyPem = _configuration["Jitsi:PrivateKey"];
            using var rsa = RSA.Create();
            rsa.ImportFromPem(privateKeyPem);
            var key = new RsaSecurityKey(rsa);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jitsi:AppId"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jitsi:AppId"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, validationParameters, out _);
            
            _logger.LogInformation("JWT token validation successful");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "JWT token validation failed");
            return false;
        }
    }

    public async Task<JitsiMeetingResponse> JoinMeetingAsync(string roomName, string displayName, bool isModerator = false)
    {
        try
        {
            var request = new JitsiMeetingRequest
            {
                RoomName = roomName,
                DisplayName = displayName,
                IsModerator = isModerator
            };

            return await CreateMeetingAsync(request);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to join meeting for room: {RoomName}", roomName);
            throw;
        }
    }
}
