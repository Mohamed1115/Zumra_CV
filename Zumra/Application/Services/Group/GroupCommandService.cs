using Zumra.Application.Interfaces.Group;

namespace Zumra.Application.Services.Group;

public class GroupCommandService:IGroupCommandService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GroupCommandService> _logger;

    public GroupCommandService(IUnitOfWork unitOfWork, ILogger<GroupCommandService> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger;
    }

    public async Task Create(Models.Group group)
    {
        try
        {
            await _unitOfWork.Groups.CreatAsync(group);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Group {GroupId} created successfully", group.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create group {GroupName}", group.Name);
            throw;
        }
    }

    public async Task Update(Models.Group group)
    {
        try
        {
            await _unitOfWork.Groups.UpdateAsync(group);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Group {GroupId} updated successfully", group.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update group {GroupId}", group.Id);
            throw;
        }
    }
    
    public async Task Delete(int id)
    {
        try
        {
            await _unitOfWork.Groups.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
            _logger.LogInformation("Group {GroupId} deleted successfully", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete group {GroupId}", id);
            throw;
        }
    }
    
    
    
}