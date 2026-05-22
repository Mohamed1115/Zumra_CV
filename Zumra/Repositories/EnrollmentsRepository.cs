using Microsoft.EntityFrameworkCore;
using Zumra.Data;
using Zumra.DTOs.Response;

namespace Zumra.Repositories;

public class EnrollmentsRepository : Repository<Enrollments>, IEnrollmentsRepository
{
    public EnrollmentsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Enrollments>> GetAllCartAsync(string id)
    {
        return await _context.Enrollments.Where(e => e.UserId == id && e.Status==SD.EnrollmentStatusInCart).ToListAsync();
    }

    public async Task<decimal> GetCartCostAsync(string id)
    {
        return await _context.Enrollments
            .Where(e => e.UserId == id && e.Status==SD.EnrollmentStatusInCart)
            .Select(e => (decimal?)e.CourseBatch.Course.Cost)
            .SumAsync() ?? 0;

    }

    public async Task<List<CartItemDto>> GetCartItemsAsync(string userId)
    {
        return await _context.Enrollments
            .Where(e => e.UserId == userId && e.Status == SD.EnrollmentStatusInCart)
            .Include(e => e.CourseBatch)
                .ThenInclude(cb => cb.Course)
            .Select(e => new CartItemDto
            {
                EnrollmentId = e.Id,
                CourseId = e.CourseBatch.Course.Id,
                CourseName = e.CourseBatch.Course.Name,
                CourseImage = e.CourseBatch.Course.ImageUrl,
                BatchId = e.CourseBatch.Id,
                BatchTitle = e.CourseBatch.Title,
                CourseCost = e.CourseBatch.Course.Cost
            })
            .ToListAsync();
    }
    
}
