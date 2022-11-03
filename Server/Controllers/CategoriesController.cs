using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
  private readonly ICategoryService categoryService;

  public CategoriesController(ICategoryService categoryService)
  {
    this.categoryService = categoryService;
  }

  [HttpGet]
  public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
  {
    var result = await categoryService.GetCategoriesAsync();
    return Ok(result);
  }
}
