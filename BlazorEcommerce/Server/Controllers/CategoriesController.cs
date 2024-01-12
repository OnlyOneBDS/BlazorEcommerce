using BlazorEcommerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly ICategoryService _categoryService;

  public CategoriesController(ICategoryService categoryService)
  {
    _categoryService = categoryService;
  }

  [HttpGet]
  public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
  {
    var result = await _categoryService.GetCategoriesAsync();
    return Ok(result);
  }
}
