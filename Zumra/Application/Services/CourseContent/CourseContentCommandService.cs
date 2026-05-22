using Zumra.Application.Interfaces.CourseContent;

namespace Zumra.Application.Services.CourseContent;

public class CourseContentCommandService : ICourseContentCommandService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseContentCommandService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Models.CourseContent> Create(Models.CourseContent courseContent)
    {
        if (courseContent == null)
            throw new ArgumentNullException(nameof(courseContent));
            
        try
        {
            var cOrder = await _unitOfWork.CourseContent.MaxContentOrder(courseContent.CourseBatchId, courseContent.SectionId);
            courseContent.CourseOrder = cOrder;
            // لا نعمل CommitAsync هنا — المسؤولية على المستدعي
            var created = await _unitOfWork.CourseContent.CreatAsync(courseContent);
            return created;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to create course content: {ex.Message}", ex);
        }
    }

    public async Task<Models.CourseContent> Update(int id, Models.CourseContent courseContent)
    {
        if (courseContent == null)
            throw new ArgumentNullException(nameof(courseContent));

        try
        {
            var existing = await _unitOfWork.CourseContent.GetByIdAsync(id);
            if (existing == null)
                throw new InvalidOperationException($"Course content with ID {id} not found");

            existing.ContentType = courseContent.ContentType;
            existing.ContentId = courseContent.ContentId;
            existing.CourseOrder = courseContent.CourseOrder;
            existing.CourseId = courseContent.CourseId;
            existing.CourseBatchId = courseContent.CourseBatchId;
            existing.SectionId = courseContent.SectionId;

            await _unitOfWork.CourseContent.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update course content: {ex.Message}", ex);
        }
    }

    public async Task Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid course content ID", nameof(id));

        try
        {
            await _unitOfWork.CourseContent.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete course content: {ex.Message}", ex);
        }
    }
}
