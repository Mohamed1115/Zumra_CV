using Zumra.DTOs.Response.Section;

namespace Zumra.Application.Interfaces.Sections;

public interface ISectionsQueryService
{
    Task<List<SectionDto>> GetAllAsync(int BatchId);
    Task<Models.Sections?> GetByIdAsync(int id);
}
