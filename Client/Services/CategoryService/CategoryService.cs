namespace BlazorEcommerce.Client.Services.CategoryService;

public class CategoryService : ICategoryService
{
  private readonly HttpClient httpClient;

  public CategoryService(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public List<Category> Categories { get; set; } = new List<Category>();

  public async Task GetCategories()
  {
    var response = await httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/categories");

    if (response != null && response.Data != null)
    {
      Categories = response.Data;
    }
  }
}