using BlazorEcommerce.Shared.DTOs;

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
  public string LastSearchText { get; set; } = string.Empty;
  public string Message { get; set; } = "Loading Products...";
  public int CurrentPage { get; set; } = 1;
  public int PageCount { get; set; } = 0;

  public async Task GetProducts(string? categoryUrl = null)
  {
    var products = categoryUrl == null ?
      await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products/featured") :
      await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/category/{categoryUrl}");

    if (products != null && products.Data != null)
    {
      Products = products.Data;
    }

    CurrentPage = 1;
    PageCount = 0;

    if (Products.Count == 0)
    {
      Message = "No products found";
    }

    ProductsChanged.Invoke();
  }

  public async Task SearchProducts(string searchText, int page)
  {
    LastSearchText = searchText;

    var products = await httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchDto>>($"api/products/search/{searchText}/{page}");

    if (products != null && products.Data != null)
    {
      Products = products.Data.Products;
      CurrentPage = products.Data.CurrentPage;
      PageCount = products.Data.Pages;
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