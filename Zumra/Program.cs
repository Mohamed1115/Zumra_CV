using System.Text;
using Zumra.Application.Interfaces.Jitsi;
using Zumra.Application.Services.Jitsi;
using Zumra.DTOs.Request.Jitsi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Stripe;
using Zumra.Data;
using Zumra.IRepositories;
using Zumra.Models;
using Zumra.Repositories;
using Zumra.Utilites;
using Zumra.Utilites.DBInitializer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication.Cookies;
using Zumra.Application.Interfaces.Facility;
using Zumra.Application.Services.Facility;
using Zumra.Application.Interfaces.Bunny;
using Zumra.Application.Services.Bunny;
using Zumra.Application.Interfaces.Category;
using Zumra.Application.Services.Category;
using Zumra.Application.Interfaces.Course;
using Zumra.Application.Services.Course;
using Zumra.Application.Interfaces.CourseBatches;
using Zumra.Application.Services.CourseBatches;
using Zumra.Application.Interfaces.CourseContent;
using Zumra.Application.Services.CourseContent;
using Zumra.Application.Interfaces.Enrollments;
using Zumra.Application.Interfaces.Group;
using Zumra.Application.Services.Enrollments;
using Zumra.Application.Interfaces.Lessons;
using Zumra.Application.Services.Lessons;
using Zumra.Application.Interfaces.Sections;
using Zumra.Application.Services.Sections;
using Zumra.Application.Interfaces.Tasks;
using Zumra.Application.Services.Tasks;
using Zumra.Application.Interfaces.TaskSubmissions;
using Zumra.Application.Services.Group;
// using Zumra.Application.Services.TaskSubmissions;

using Zumra.Application.Services.TaskSubmissions;
using Zumra.Application.Interfaces.LessonRec;
using Zumra.Application.Services.LessonRec;
using Zumra.Application.Interfaces.LessonLive;
using Zumra.Application.Services.LessonLive;
using Zumra.Application.Interfaces.Favorite;
using Zumra.Application.Services.Favorite;
using Zumra.Application.Services.Redis;
using Zumra.Application.Interfaces.UserImage;
using Zumra.Application.Services.UserImage;

namespace Zumra;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins("http://localhost:5173")  // ← عنوان الـ React app
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("RedisConn");
            options.InstanceName = "Zumra_";
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler =
                    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

                options.JsonSerializerOptions.MaxDepth = 256;
            });


        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            
                // Lockout settings - تعطيل أو تقليل القفل
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // مدة القفل 5 دقائق فقط
                options.Lockout.MaxFailedAccessAttempts = 10; // 10 محاولات قبل القفل
                options.Lockout.AllowedForNewUsers = true;
            
                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true; // Enable email confirmation
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.ExternalScheme, options =>
        {
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme; // For external login flow
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                // ClockSkew = TimeSpan.Zero,
                // RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        })
        .AddGoogle("Google", opt =>
        {
            var googleAuth = builder.Configuration.GetSection("Authentication:Google");
            opt.ClientId = googleAuth["ClientId"]??"";
            opt.ClientSecret = googleAuth["ClientSecret"]??"";
            opt.SignInScheme = IdentityConstants.ExternalScheme;
            
            opt.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
            opt.CorrelationCookie.SameSite = SameSiteMode.Lax;
            opt.CorrelationCookie.HttpOnly = true;

            // Expose the real OAuth error for debugging — replace with a frontend redirect in production
            opt.Events.OnRemoteFailure = context =>
            {
                var error = context.Failure?.Message ?? "Unknown OAuth error";
                var stackTrace = context.Failure?.ToString() ?? "";
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain; charset=utf-8";
                context.Response.WriteAsync(
                    $"OAuth RemoteFailure:\n{error}\n\nFull details:\n{stackTrace}"
                ).Wait();
                context.HandleResponse();
                return Task.CompletedTask;
            };
        });

        // Email Sender
        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
        builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
        builder.Services.AddTransient<Zumra.IRepositories.IEmailSender, EmailSender>();

        builder.Services.AddScoped<IRepository<Otp>, Repository<Otp>>();
        
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<ICouponRepository, CouponRepository>();
        builder.Services.AddScoped<IDBInitializer, DBInitializer>();
        builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
        
        // Course-related repositories
        builder.Services.AddScoped<ICourseBatchesRepository, CourseBatchesRepository>();
        builder.Services.AddScoped<ICourseContentRepository, CourseContentRepository>();
        builder.Services.AddScoped<IEnrollmentsRepository, EnrollmentsRepository>();
        builder.Services.AddScoped<ILessonsRepository, LessonsRepository>();
        builder.Services.AddScoped<ILessonRecRepository, LessonRecRepository>();
        builder.Services.AddScoped<ILessonLiveRepository, LessonLiveRepository>();
        builder.Services.AddScoped<ISectionsRepository, SectionsRepository>();

        builder.Services.AddScoped<ISectionsRepository, SectionsRepository>();
        builder.Services.AddScoped<ITasksRepository, TasksRepository>();
        builder.Services.AddScoped<ITaskSubmissionsRepository, TaskSubmissionsRepository>();
        
        // Bunny CDN Service
        builder.Services.AddScoped<IBunnyService, BunnyService>();
        
        // Facility Services
        builder.Services.AddScoped<IFacilityCommandService, FacilityCommandService>();
        builder.Services.AddScoped<IFacilityQueryService, FacilityQueryService>();
        
        builder.Services.AddScoped<IGroupQueryService, GroupQueryService>();
        builder.Services.AddScoped<IGroupCommandService, GroupCommandService>();


        
        // Category Services
        builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
        builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        
        // Course Services
        builder.Services.AddScoped<ICourseCommandService, CourseCommandService>();
        builder.Services.AddScoped<ICourseQueryService, CourseQueryService>();

        // CourseBatches Services
        builder.Services.AddScoped<ICourseBatchesCommandService, CourseBatchesCommandService>();
        builder.Services.AddScoped<ICourseBatchesQueryService, CourseBatchesQueryService>();
        
        // CourseContent Services
        builder.Services.AddScoped<ICourseContentCommandService, CourseContentCommandService>();
        builder.Services.AddScoped<ICourseContentQueryService, CourseContentQueryService>();
        
        // Enrollments Services
        builder.Services.AddScoped<IEnrollmentsCommandService, EnrollmentsCommandService>();
        builder.Services.AddScoped<IEnrollmentsQueryService, EnrollmentsQueryService>();
        
        // Lessons Services
        builder.Services.AddScoped<ILessonsCommandService, LessonsCommandService>();
        builder.Services.AddScoped<ILessonsQueryService, LessonsQueryService>();

        // LessonRec Services
        builder.Services.AddScoped<ILessonRecCommandService, LessonRecCommandService>();
        builder.Services.AddScoped<ILessonRecQueryService, LessonRecQueryService>();

        // LessonLive Services
        builder.Services.AddScoped<ILessonLiveCommandService, LessonLiveCommandService>();
        builder.Services.AddScoped<ILessonLiveQueryService, LessonLiveQueryService>();
        
        
        // Sections Services
        builder.Services.AddScoped<ISectionsCommandService, SectionsCommandService>();
        builder.Services.AddScoped<ISectionsQueryService, SectionsQueryService>();
        
        // Tasks Services
        builder.Services.AddScoped<ITasksCommandService, TasksCommandService>();
        builder.Services.AddScoped<ITasksQueryService, TasksQueryService>();
        
        // TaskSubmissions Services
        builder.Services.AddScoped<ITaskSubmissionsCommandService, TaskSubmissionsCommandService>();
        builder.Services.AddScoped<ITaskSubmissionsQueryService, TaskSubmissionsQueryService>();
        
        builder.Services.Configure<JitsiConfiguration>(builder.Configuration.GetSection("Jitsi"));
        builder.Services.AddScoped<IJitsiService, JitsiService>();
        builder.Services.AddHttpClient();
        
        // Favorite Services
        builder.Services.AddScoped<IFavoriteCommandService, FavoriteCommandService>();
        builder.Services.AddScoped<IFavoriteQueryService, FavoriteQueryService>();
        
        // Redis Cache Service
        builder.Services.AddScoped<RedisCacheService>();
        
        // UserImage Services
        builder.Services.AddScoped<IImageCommandService, ImageCommandService>();






        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("FacilitySuperAdmin", policy =>
                policy.Requirements.Add(new FacilityRequirement(FacilityRole.SuperAdmin)));
            // Policy للـ Leader فقط
            options.AddPolicy("FacilityLeader", policy =>
                policy.Requirements.Add(new FacilityRequirement(FacilityRole.Leader)));

            // Policy للـ Instructor وأعلى
            options.AddPolicy("FacilityInstructor", policy =>
                policy.Requirements.Add(new FacilityRequirement(FacilityRole.Instructor)));

            // Policy للـ Member وأعلى (أي عضو)
            options.AddPolicy("FacilityMember", policy =>
                policy.Requirements.Add(new FacilityRequirement(FacilityRole.Member)));
            
            options.AddPolicy("FacilityAdmin", policy =>
                policy.Requirements.Add(new FacilityRequirement(FacilityRole.Leader)));

        });

        // تسجيل الـ Handler
        builder.Services.AddScoped<IAuthorizationHandler, FacilityAuthorizationHandler>();
        


        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        var app = builder.Build();

        app.UseForwardedHeaders();
        // NOTE: HttpsRedirection is intentionally disabled behind a reverse proxy.
        // The proxy (Nginx) handles TLS termination. Enabling this causes redirect
        // loops or incorrect scheme detection inside the Docker container.
        // app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseCors("AllowReactApp"); 
        app.MapScalarApiReference();
        // Configure the HTTP request pipeline.
        app.MapOpenApi();

        
        app.UseStaticFiles(); // Enable serving static files from wwwroot
        
        app.UseAuthentication();

        app.UseAuthorization();
        using (var  scope = app.Services.CreateScope())
        {
            try
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
                initializer.Initialize().GetAwaiter().GetResult();
                Console.WriteLine("DBInitializer executed successfully ✅");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DBInitializer failed ❌: {ex}");
            }
        }


        app.MapControllers();

        app.Run();
    }
}