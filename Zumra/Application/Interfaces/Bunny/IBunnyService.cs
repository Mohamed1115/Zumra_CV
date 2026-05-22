using Zumra.DTOs.Response.Bunny;

namespace Zumra.Application.Interfaces.Bunny;

public interface IBunnyService
{
    Task<string> GetStorageZonesAsync();

    Task UploadFileAsync(
        string zoneName,
        string path,
        IFormFile file);

    Task<string> ListFilesAsync(string zoneName, string path);
    Task DeleteFileAsync(string zoneName, string path);
    Task<Stream> GetFileAsync(string zoneName, string path);

    Task<(Stream FileStream, string FileName)> GetFileWithNameAsync(
        string zoneName,
        string path,
        string? fileName = null);

    Task UpdateFileAsync(
        string zoneName,
        string path,
        IFormFile newFile);
    
    string GetFileUrl(string zoneName, string path);
    string GetSignedFileUrl(string zoneName, string path, TimeSpan validFor);
    
    Task<BunnyStreamVideoDto> CreateStreamVideoAsync(string title);
}