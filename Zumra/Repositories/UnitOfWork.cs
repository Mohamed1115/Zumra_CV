using Microsoft.EntityFrameworkCore.Storage;
using Zumra.Data;
using Zumra.IRepositories;

namespace Zumra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserImage = new UserImageRepository(context);
        Category = new CategoryRepository(context) ;
        Groups = new GroupRepository(context) ;
        FacilityUser = new Repository<UserFacility>(context);
        Facilities = new Repository<Facility>(context);
        Categories = new Repository<Category>(context);
        Courses = new CourseRepository(context);
        PayFac = new PayFacRepository(context);
        UserFacility = new UserFacilityRepository(context);
        Facility = new FacilityRepository(context);
        
        CourseBatches = new CourseBatchesRepository(context);
        CourseContent = new CourseContentRepository(context);
        Enrollments = new EnrollmentsRepository(context);
        Lessons = new LessonsRepository(context);
        Sections = new SectionsRepository(context);
        Tasks = new TasksRepository(context);
        Tasks = new TasksRepository(context);
        TaskSubmissions = new TaskSubmissionsRepository(context);
        
        LessonRecs = new LessonRecRepository(context);
        LessonLives = new LessonLiveRepository(context);
    }
    public IUserImageRepository  UserImage { get; }

    public IRepository<Facility> Facilities { get; }
    public IRepository<Category> Categories  { get; }
    public ICourseRepository Courses { get; }
    public IRepository<UserFacility> FacilityUser { get; }
    public IPayFacRepository PayFac { get; }
    public IUserFacilityRepository UserFacility { get; }
    public IFacilityRepository Facility { get; }
    public ICategoryRepository Category { get; }
    public IGroupRepository Groups { get; }
    
    public ICourseBatchesRepository CourseBatches { get; }
    public ICourseContentRepository CourseContent { get; }
    public IEnrollmentsRepository Enrollments { get; }
    public ILessonsRepository Lessons { get; }
    public ISectionsRepository Sections { get; }
    public ITasksRepository Tasks { get; }
    public ITaskSubmissionsRepository TaskSubmissions { get; }
    
    public ILessonRecRepository LessonRecs { get; }
    public ILessonLiveRepository LessonLives { get; }


    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task ExecuteTransactionAsync(Func<Task> action)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync<object, bool>(
            null,
            async (dbContext, state, ct) =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await action();
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            },
            null);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}