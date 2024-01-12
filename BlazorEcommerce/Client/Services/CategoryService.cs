using System.Net.Http.Json;
using BlazorEcommerce.Client.Services.Interfaces;
using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services;

public class CategoryService : ICategoryService
{
  private readonly HttpClient _httpClient;

  public CategoryService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public List<Category> Categories { get; set; } = new();

  public async Task GetCategories()
  {
    var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/categories");

    if (response != null && response.Data != null)
    {
      Categories = response.Data;
    }
  }
}
