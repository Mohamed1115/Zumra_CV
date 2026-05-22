using Zumra.Application.Interfaces.Enrollments;
using Zumra.Data;

namespace Zumra.Application.Services.Enrollments;

public class EnrollmentsCommandService : IEnrollmentsCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EnrollmentsCommandService> _logger;

    public EnrollmentsCommandService(IUnitOfWork unitOfWork, ILogger<EnrollmentsCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Create(int batchId, string userId)
    {
        try
        {
            var enrollment = new Models.Enrollments
            {
                CourseBatchId = batchId,
                UserId = userId,
                Status = SD.EnrollmentStatusInCart
            };
            await _unitOfWork.Enrollments.CreatAsync(enrollment);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Enrollment created for user {UserId} in batch {BatchId}", userId, batchId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create enrollment for user {UserId} in batch {BatchId}", userId, batchId);
            throw;
        }
    }

    public async Task<Models.Enrollments> Update(int id, Models.Enrollments enrollment)
    {
        if (enrollment == null)
            throw new ArgumentNullException(nameof(enrollment));

        try
        {
            var existing = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Attempted to update non-existent enrollment with ID {EnrollmentId}", id);
                throw new InvalidOperationException($"Enrollment with ID {id} not found");
            }

            existing.Status = enrollment.Status;
            existing.AccessType = enrollment.AccessType;
            existing.CreatedAt = enrollment.CreatedAt;
            existing.UserId = enrollment.UserId;
            existing.CourseBatchId = enrollment.CourseBatchId;

            await _unitOfWork.Enrollments.UpdateAsync(existing);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Enrollment {EnrollmentId} updated successfully", id);
            return existing;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update enrollment with ID {EnrollmentId}", id);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid enrollment ID", nameof(id));

        try
        {
            await _unitOfWork.Enrollments.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Enrollment {EnrollmentId} deleted successfully", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete enrollment with ID {EnrollmentId}", id);
            return false;
        }
    }

    public async Task<bool> EnrollCourses(int enrollId)
    {
        try
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollId);
            if (enrollment == null)
            {
                _logger.LogWarning("Attempted to enroll with non-existent enrollment ID {EnrollmentId}", enrollId);
                return false;
            }
            
            enrollment.Status = SD.EnrollmentStatusOwned;
            await _unitOfWork.Enrollments.UpdateAsync(enrollment);
            await _unitOfWork.CommitAsync();
            
            _logger.LogInformation("Successfully enrolled course for enrollment {EnrollmentId}", enrollId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enroll course for enrollment {EnrollmentId}", enrollId);
            throw;
        }
    }
}

