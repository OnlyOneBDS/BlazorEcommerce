namespace BlazorEcommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
  private readonly DataContext context;

  public ProductService(DataContext context)
  {
    this.context = context;
  }

  public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
  {
    return new ServiceResponse<List<Product>>
    {
      Data = await context.Products.Include(p => p.Variants).ToListAsync()
    };
  }

  public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
  {
    return new ServiceResponse<List<Product>>
    {
      Data = await context.Products
        .Include(p => p.Variants)
        .Where(p => p.Category.Url.ToLower() == categoryUrl.ToLower())
        .ToListAsync()
    };
  }

  public async Task<ServiceResponse<List<string>>> GetSearchSuggestionsAsync(string searchText)
  {
    var products = await FilterProductsAsync(searchText);
    var searchSuggestions = new List<string>();

    foreach (var product in products)
    {
      if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
      {
        searchSuggestions.Add(product.Title);
      }

      if (product.Description != null)
      {
        var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
        var words = product.Description.Split().Select(s => s.Trim(punctuation));

        foreach (var word in words)
        {
          if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !searchSuggestions.Contains(word))
          {
            searchSuggestions.Add(word);
          }
        }
      }
    }

    return new ServiceResponse<List<string>> { Data = searchSuggestions };
  }

  public async Task<ServiceResponse<List<Product>>> SearchProductsAsync(string searchText)
  {
    return new ServiceResponse<List<Product>>
    {
      Data = await FilterProductsAsync(searchText)
    };
  }

  public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
  {
    var response = new ServiceResponse<Product>();
    var product = await context.Products
      .Include(p => p.Variants)
      .ThenInclude(v => v.ProductType)
      .FirstOrDefaultAsync(p => p.Id == productId);

    if (product == null)
    {
      response.Success = false;
      response.Message = "This product could not be found";
    }
    else
    {
      response.Data = product;
    }

    return response;
  }

  private async Task<List<Product>> FilterProductsAsync(string searchText)
  {
    return await context.Products
      .Include(p => p.Variants)
      .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) || p.Description.ToLower().Contains(searchText.ToLower()))
      .ToListAsync();
  }
}
