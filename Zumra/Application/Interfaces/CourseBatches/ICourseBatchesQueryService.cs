using Zumra.DTOs.Response.Section;

namespace Zumra.Application.Interfaces.CourseBatches;

public interface ICourseBatchesQueryService
{
    Task<List<Models.CourseBatches>> GetAllAsync(int CourseId);
    Task<List<SectionDto>> GetByIdAsync(int id);
}
