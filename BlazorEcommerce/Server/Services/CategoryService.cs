using BlazorEcommerce.Server.Services.Interfaces;

namespace BlazorEcommerce.Server.Services;

public class CategoryService : ICategoryService
{
  private readonly ApplicationDbContext _context;

  public CategoryService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
  {
    var categories = await _context.Categories.ToListAsync();

    return new ServiceResponse<List<Category>> 
    { 
      Data = categories 
    };
  }
}