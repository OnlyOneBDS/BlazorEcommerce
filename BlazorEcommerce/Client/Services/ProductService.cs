using System.Net.Http.Json;
using BlazorEcommerce.Client.Services.Interfaces;
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services;

public class ProductService : IProductService
{
  private readonly HttpClient _httpClient;

  public ProductService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public event Action ProductsChanged;

  public string Message { get; set; } = "Loading products...";
  public int CurrentPage { get; set; } = 1;
  public int PageCount { get; set; } = 0;
  public string LastSearchText { get; set; } = string.Empty;
  public List<Product> Products { get; set; } = new();

  public async Task GetProducts(string? categoryUrl = null)
  {
    var result = categoryUrl == null ? 
      await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products/featured") : 
      await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/products/category/{categoryUrl}");

    if (result != null && result.Data != null)
    {
      Products = result.Data;
    }

    CurrentPage = 1;
    PageCount = 0;

    if (Products.Count == 0)
    {
      Message = "No products found";
    }

    ProductsChanged?.Invoke();
  }

  public async Task SearchProducts(string searchText, int page)
  {
    LastSearchText = searchText;
    var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/products/search/{searchText}/{page}");

    if (result != null && result.Data != null)
    {
      Products = result.Data.Products;
      CurrentPage = result.Data.CurrentPage;
      PageCount = result.Data.TotalPages;
    }

    if (Products.Count == 0)
    {
      Message = "No products found";
    }

    ProductsChanged?.Invoke();
  }

  public async Task<List<string>> GetProductSearchSuggestions(string searchText)
  {
    var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/products/searchsuggestions/{searchText}");
    return result.Data;
  }

  public async Task<ServiceResponse<Product>> GetProduct(int id)
  {
    var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/{id}");
    return result;
  }
}
