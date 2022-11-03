namespace BlazorEcommerce.Server.Services.CategoryService;

public class CategoryService : ICategoryService
{
  private readonly DataContext context;

  public CategoryService(DataContext context)
  {
    this.context = context;
  }

  public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
  {
    var categories = await context.Categories.ToListAsync();

    return new ServiceResponse<List<Category>>
    {
      Data = categories
    };
  }
}