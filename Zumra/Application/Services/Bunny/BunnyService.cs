using RestSharp;
using Zumra.Application.Interfaces.Bunny;

using System.Security.Cryptography;
using System.Text;
// using Zumra.Application.Interfaces.Bunny;
using Zumra.DTOs.Response.Bunny;

namespace Zumra.Application.Services.Bunny;

public class BunnyService : IDisposable,IBunnyService
{
    private readonly IConfiguration _config;
    private readonly RestClient _managementClient;
    private readonly string _managementApiKey;
    private readonly string _storageApiKey;
    private readonly string? _streamApiKey;
    private readonly string? _streamLibraryId;

    public BunnyService(IConfiguration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        
        _managementApiKey = _config["Bunny:ManagementApiKey"] 
            ?? throw new InvalidOperationException("Missing Bunny:ManagementApiKey");
        _storageApiKey = _config["Bunny:StorageApiKey"] 
            ?? throw new InvalidOperationException("Missing Bunny:StorageApiKey");
            
        _streamApiKey = _config["Bunny:StreamApiKey"];
        _streamLibraryId = _config["Bunny:StreamLibraryId"];
        
        _managementClient = new RestClient(
            new RestClientOptions("https://api.bunny.net"));
    }

    // =========================
    // 🔹 Helper Methods
    // =========================
    private RestClient CreateStorageClient(string zoneName) =>
        new($"https://storage.bunnycdn.com/{zoneName}");

    private void AddDefaultHeaders(RestRequest request, string apiKey)
    {
        request.AddHeader("Accept", "application/json");
        request.AddHeader("AccessKey", apiKey);
    }

    private async Task<T> ExecuteRequestAsync<T>(
        RestClient client,
        RestRequest request,
        string operationName)
    {
        var response = await client.ExecuteAsync(request);
        
        if (!response.IsSuccessful)
        {
            throw new BunnyApiException(
                $"{operationName} failed",
                response.StatusCode,
                response.ErrorMessage ?? response.Content);
        }

        return response.Content is null 
            ? throw new BunnyApiException($"{operationName} returned empty response", null, null)
            : (T)Convert.ChangeType(response.Content, typeof(T));
    }

    // =========================
    // 🔹 Management API
    // =========================
    public async Task<string> GetStorageZonesAsync()
    {
        var request = new RestRequest("/storagezone", Method.Get);
        AddDefaultHeaders(request, _managementApiKey);

        return await ExecuteRequestAsync<string>(
            _managementClient,
            request,
            "Get storage zones");
    }

    // =========================
    // 🔹 Storage API
    // =========================
    public async Task UploadFileAsync(
        string zoneName,
        string path,
        IFormFile file)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be empty", nameof(file));

        using var client = CreateStorageClient(zoneName);
        var request = new RestRequest(path, Method.Put);
        
        AddDefaultHeaders(request, _storageApiKey);
        request.AddHeader("Content-Type", "application/octet-stream");
        
        using var stream = file.OpenReadStream();
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        
        // Use AddParameter for raw binary data. AddBody can accidentally serialize byte[] to a base64 JSON string.
        request.AddParameter("application/octet-stream", ms.ToArray(), ParameterType.RequestBody);

        await ExecuteRequestAsync<string>(client, request, "Upload file");
    }

    public async Task<string> ListFilesAsync(string zoneName, string path)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));

        using var client = CreateStorageClient(zoneName);
        var request = new RestRequest(path, Method.Get);
        
        AddDefaultHeaders(request, _storageApiKey);

        return await ExecuteRequestAsync<string>(client, request, "List files");
    }

    public async Task DeleteFileAsync(string zoneName, string path)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));

        using var client = CreateStorageClient(zoneName);
        var request = new RestRequest(path, Method.Delete);
        
        AddDefaultHeaders(request, _storageApiKey);

        await ExecuteRequestAsync<string>(client, request, "Delete file");
    }

    public async Task<Stream> GetFileAsync(string zoneName, string path)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));

        using var client = CreateStorageClient(zoneName);
        var request = new RestRequest(path, Method.Get);
        request.AddHeader("AccessKey", _storageApiKey);

        var response = await client.ExecuteAsync(request);
        
        if (!response.IsSuccessful)
        {
            throw new BunnyApiException(
                "Get file failed",
                response.StatusCode,
                response.ErrorMessage ?? response.Content);
        }

        var ms = new MemoryStream(response.RawBytes ?? Array.Empty<byte>());
        return ms;
    }

    public async Task<(Stream FileStream, string FileName)> GetFileWithNameAsync(
        string zoneName,
        string path,
        string? fileName = null)
    {
        var stream = await GetFileAsync(zoneName, path);
        var name = fileName ?? Path.GetFileName(path);
        return (stream, name);
    }
    public async Task UpdateFileAsync(
        string zoneName,
        string path,
        IFormFile newFile)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));
        if (newFile == null || newFile.Length == 0)
            throw new ArgumentException("File cannot be empty", nameof(newFile));

        // حذف الملف القديم
        try
        {
            await DeleteFileAsync(zoneName, path);
        }
        catch
        {
            // لو الملف القديم مش موجود، متقلقش
        }

        // رفع الملف الجديد
        await UploadFileAsync(zoneName, path, newFile);
    }
    //
    
    public string GetSignedFileUrl(string zoneName, string path, TimeSpan validFor)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));

        long expiration = DateTimeOffset.UtcNow.Add(validFor).ToUnixTimeSeconds();
        
        // BunnyCDN Signed URL: token = Base64(SHA256(securityKey + normalizedPath + expiration))
        var normalizedPath = "/" + path.TrimStart('/');
        var hashableBase = $"{_storageApiKey}{normalizedPath}{expiration}";
        
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashableBase));
        var token = Convert.ToBase64String(hash)
            .Replace("\n", "").Replace("+", "-")
            .Replace("/", "_").Replace("=", "");

        return $"https://{zoneName}-cdn.b-cdn.net{normalizedPath}?token={token}&expires={expiration}";
    }

    // =========================
    // 🔹 Get File URL
    // =========================
    public string GetFileUrl(string zoneName, string path)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be empty", nameof(path));

        // بناء الـ URL الكامل للملف على CDN
        // Format: https://zonename-cdn.b-cdn.net/path
        return $"https://{zoneName}-cdn.b-cdn.net/{path.TrimStart('/')}";
    }

    public void Dispose()
    {
        _managementClient?.Dispose();
    }

    // =========================
    // 🔹 Stream API (Video)
    // =========================
    public async Task<BunnyStreamVideoDto> CreateStreamVideoAsync(string title)
    {
        if (string.IsNullOrEmpty(_streamApiKey) || string.IsNullOrEmpty(_streamLibraryId))
            throw new InvalidOperationException("Bunny Stream configuration is missing (StreamApiKey or StreamLibraryId).");

        // 1. Create Video Object via API
        var request = new RestRequest($"/library/{_streamLibraryId}/videos", Method.Post);
        request.AddHeader("AccessKey", _streamApiKey);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Content-Type", "application/json");
        request.AddBody(new { title = title });
        
        // Use a separate client for Video API if base URL differs, but management client base is api.bunny.net
        // Stream API is usually video.bunnycdn.com for Management? 
        // Docs: POST https://video.bunnycdn.com/library/{libraryId}/videos
        
        using var streamClient = new RestClient("https://video.bunnycdn.com");
        var response = await streamClient.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new BunnyApiException("Failed to create video object", response.StatusCode, response.Content);
        }
        
        // Parse response to get guid
        var json = System.Text.Json.JsonDocument.Parse(response.Content!);
        var videoId = json.RootElement.GetProperty("guid").GetString();

        if (string.IsNullOrEmpty(videoId))
            throw new BunnyApiException("Video ID not found in response", response.StatusCode, response.Content);

        // 2. Generate Authorization Signature for Upload
        // Signature = SHA256(libraryId + apiKey + expiration + videoId)
        var expiration = DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds();
        var dataToSign = $"{_streamLibraryId}{_streamApiKey}{expiration}{videoId}";
        var signature = ComputeSha256Hash(dataToSign);

        return new BunnyStreamVideoDto
        {
            Guid = videoId,
            AuthorizationSignature = signature,
            AuthorizationExpire = expiration,
            LibraryId = _streamLibraryId,
            // TUS upload endpoint — the frontend must PUT to this URL using TUS protocol headers
            UploadUrl = $"https://video.bunnycdn.com/tusupload",
            EmbedUrl = $"https://iframe.mediadelivery.net/embed/{_streamLibraryId}/{videoId}"
        };
    }

    private static string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

    // Custom Exception للـ Bunny API
    public class BunnyApiException : Exception
    {
        public System.Net.HttpStatusCode? StatusCode { get; }
        public string? Details { get; }

        public BunnyApiException(string message, System.Net.HttpStatusCode? statusCode, string? details)
            : base($"{message} - Status: {statusCode} - Details: {details}")
        {
            StatusCode = statusCode;
            Details = details;
        }
    }
    
    
    