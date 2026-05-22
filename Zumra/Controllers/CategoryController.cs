using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zumra.Application.Interfaces.Category;
using Zumra.DTOs.Request.Category;

namespace Zumra.Controllers;
[Route("Api/[controller]")]
[ApiController]
public class CategoryController:ControllerBase
{
    private readonly ICategoryQueryService _categoryQueryService;
    private readonly ICategoryCommandService _categoryCommandService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryQueryService categoryQueryService,
        ICategoryCommandService categoryCommandService,
        ILogger<CategoryController> logger)
    {
        _categoryQueryService = categoryQueryService;
        _categoryCommandService = categoryCommandService;
        _logger = logger;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var cats = await _categoryQueryService.All();
            return Ok(new
            {
                success = true,
                data = cats
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all categories");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting all categories"
            });
        }
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        try
        {
            var cat = await _categoryQueryService.GetCategory(id);
            return Ok(new
            {
                success = true,
                data = cat
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting category");
            return StatusCode(500, new
            {
                success = false,
                message = "An error occurred getting category"
            });
        }
    }


    [HttpPost]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public async Task<IActionResult> CreatCategory([FromForm] CategoryCreateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to existing format for service
            var categoryDto = new CategoryCreat
            {
                Name = request.Name,
                Description = request.Description
            };

            var cat = await _categoryCommandService.Create(categoryDto, request.Image);
            return CreatedAtAction(nameof(GetCategory), new { id = cat.Id }, new
            {
                success = true,
                message = "Category created successfully",
                data = cat
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating category");
            return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred creating category"
                }
            );
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromForm] CategoryUpdateRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Model
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                Description = request.Description
            };

            var updatedCategory = await _categoryCommandService.Update(id, category, request.Image);
            return Ok(new
            {
                success = true,
                message = "Category updated successfully",
                data = updatedCategory
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating category");
            return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred updating category"
                }
            );
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{SD.SuperAdminRole},{SD.AdminRole}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            await _categoryCommandService.Delete(id);
            return Ok(new
            {
                success = true,
                message = "Category deleted successfully"
            });

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting category");
            return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred deleting category"
                }
            );
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}