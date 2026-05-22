using Microsoft.EntityFrameworkCore.Storage;

namespace Zumra.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Facility> Facilities { get; }
    IRepository<UserFacility> FacilityUser { get; }
    IPayFacRepository PayFac { get; }
    IUserFacilityRepository UserFacility { get; }
    IFacilityRepository Facility { get; }
    public IRepository<Category> Categories  { get; }
    ICategoryRepository Category { get; }
    ICourseRepository Courses { get; }
    IGroupRepository Groups { get; }
    
    ICourseBatchesRepository CourseBatches { get; }
    ICourseContentRepository CourseContent { get; }
    IEnrollmentsRepository Enrollments { get; }
    ILessonsRepository Lessons { get; }
    ISectionsRepository Sections { get; }
    ITasksRepository Tasks { get; }
    ITaskSubmissionsRepository TaskSubmissions { get; }
    
    ILessonRecRepository LessonRecs { get; }
    ILessonLiveRepository LessonLives { get; }
    IUserImageRepository UserImage { get; }
    
    
    void Dispose();
    
    Task<int> CommitAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task ExecuteTransactionAsync(Func<Task> action);
}