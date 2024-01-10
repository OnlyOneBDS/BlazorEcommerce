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

  public List<Product> Products { get; set; } = new();

  public async Task GetProducts()
  {
    var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products");

    if (result != null && result.Data != null)
    {
      Products = result.Data;
    }
  }
}
