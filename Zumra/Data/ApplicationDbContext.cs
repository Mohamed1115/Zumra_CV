using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zumra.Models;

namespace Zumra.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    public DbSet<Otp> Otps { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PayFac> PayFacs { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserFacility> UserFacilities { get; set; }

    public DbSet<CourseBatches> CourseBatches { get; set; }
    public DbSet<CourseContent> CourseContents { get; set; }
    public DbSet<Enrollments> Enrollments { get; set; }
    public DbSet<Lessons> Lessons { get; set; }
    public DbSet<LessonLive> LessonLives { get; set; }
    public DbSet<LessonRec> LessonRecs { get; set; }
    public DbSet<Sections> Sections { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<TaskSubmissions> TaskSubmissions { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // UserFacility Many-to-Many Configuration
        modelBuilder.Entity<UserFacility>()
            .HasKey(uf => new { uf.UserId, uf.FacilityId });

        modelBuilder.Entity<UserFacility>()
            .HasOne(uf => uf.User)
            .WithMany(u => u.UserFacilities)
            .HasForeignKey(uf => uf.UserId);

        modelBuilder.Entity<UserFacility>()
            .HasOne(uf => uf.Facility)
            .WithMany(f => f.UserFacilities)
            .HasForeignKey(uf => uf.FacilityId);
        
        // Course Relationships
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Group)
            .WithMany(g => g.Courses)
            .HasForeignKey(c => c.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Facility)
            .WithMany()
            .HasForeignKey(c => c.FacilityId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // CourseBatches Relationships
        modelBuilder.Entity<CourseBatches>()
            .HasOne(cb => cb.Course)
            .WithMany(c => c.CourseBatches)
            .HasForeignKey(cb => cb.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Sections Relationships
        modelBuilder.Entity<Sections>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Sections)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Sections>()
            .HasOne(s => s.CourseBatch)
            .WithMany(cb => cb.Sections)
            .HasForeignKey(s => s.CourseBatchId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Lessons Relationships
        modelBuilder.Entity<Lessons>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Lessons>()
            .HasOne(l => l.CourseBatch)
            .WithMany(cb => cb.Lessons)
            .HasForeignKey(l => l.CourseBatchId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        
        // LessonLive One-to-One Relationship
        modelBuilder.Entity<Lessons>()
            .HasOne(l => l.Live)
            .WithOne(ll => ll.Lesson)
            .HasForeignKey<Lessons>(l => l.MeetingId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // LessonRec One-to-One Relationship
        modelBuilder.Entity<Lessons>()
            .HasOne(l => l.Rec)
            .WithOne(lr => lr.Lesson)
            .HasForeignKey<Lessons>(l => l.VideoId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Tasks Relationships
        modelBuilder.Entity<Tasks>()
            .HasOne(t => t.Section)
            .WithMany(s => s.Tasks)
            .HasForeignKey(t => t.SectionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // TaskSubmissions Relationships
        modelBuilder.Entity<TaskSubmissions>()
            .HasOne(ts => ts.Task)
            .WithMany(t => t.TaskSubmissions)
            .HasForeignKey(ts => ts.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TaskSubmissions>()
            .HasOne(ts => ts.User)
            .WithMany()
            .HasForeignKey(ts => ts.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Enrollments Relationships
        modelBuilder.Entity<Enrollments>()
            .HasOne(e => e.CourseBatch)
            .WithMany(cb => cb.Enrollments)
            .HasForeignKey(e => e.CourseBatchId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Enrollments>()
            .HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // CourseContent Relationships
        modelBuilder.Entity<CourseContent>()
            .HasOne(cc => cc.Course)
            .WithMany()
            .HasForeignKey(cc => cc.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<CourseContent>()
            .HasOne(cc => cc.CourseBatch)
            .WithMany(cb => cb.CourseContents)
            .HasForeignKey(cc => cc.CourseBatchId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<CourseContent>()
            .HasOne(cc => cc.Section)
            .WithMany(s => s.CourseContents)
            .HasForeignKey(cc => cc.SectionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // CourseContent Polymorphic Relationships (No FK, handled manually)
        modelBuilder.Entity<CourseContent>()
            .Ignore(cc => cc.Lesson);
        
        modelBuilder.Entity<CourseContent>()
            .Ignore(cc => cc.Task);
        
        // Favorites Many-to-Many Configuration
        modelBuilder.Entity<Favorite>()
            .HasKey(f => new { f.UserId, f.CourseId });

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Course)
            .WithMany(c => c.Favorites)
            .HasForeignKey(f => f.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    
}