namespace BlazorEcommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
  private readonly HttpClient httpClient;

  public event Action ProductsChanged;

  public ProductService(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public List<Product> Products { get; set; } = new List<Product>();
  public string Message { get; set; } = "Loading Products...";

  public async Task GetProducts(string? categoryUrl = null)
  {
    var products = categoryUrl == null ?
      await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products") :
      await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/category/{categoryUrl}");

    if (products != null && products.Data != null)
    {
      Products = products.Data;
    }

    ProductsChanged.Invoke();
  }

  public async Task SearchProducts(string searchText)
  {
    var products = await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/search/{searchText}");

    if (products != null && products.Data != null)
    {
      Products = products.Data;
    }

    if (Products.Count == 0)
    {
      Message = "No products found";
    }

    ProductsChanged.Invoke();
  }

  public async Task<List<string>> GetSearchSuggestions(string searchText)
  {
    var suggestions = await httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/products/search-suggestions/{searchText}");

    return suggestions.Data;
  }

  public async Task<ServiceResponse<Product>> GetProduct(int productId)
  {
    var product = await httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/{productId}");

    return product;
  }
}