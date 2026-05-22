using Zumra.DTOs.Response.Section;

namespace Zumra.IRepositories;

public interface ISectionsRepository : IRepository<Sections>
{
    /// <summary>
    /// جلب جميع الـ Sections لـ Batch معين مع محتوياتها (Lessons & Tasks)
    /// </summary>
    Task<List<SectionDto>> GetAllByBatchIdAsync(int batchId);
}
