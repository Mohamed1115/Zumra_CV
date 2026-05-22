namespace Zumra.Application.Interfaces.Enrollments;

public interface IEnrollmentsCommandService
{
    Task<bool> Create(int batchId,string userId);
    Task<Models.Enrollments> Update(int id, Models.Enrollments enrollment);
    Task<bool> Delete(int id);
    Task<bool> EnrollCourses(int EnrollId);
}
