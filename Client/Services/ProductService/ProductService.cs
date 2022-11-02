namespace BlazorEcommerce.Client.Services.ProductService;

public class ProductService : IProductService
{
  private readonly HttpClient httpClient;

  public ProductService(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public List<Product> Products { get; set; } = new List<Product>();

  public async Task<ServiceResponse<Product>> GetProduct(int productId)
  {
    var product = await httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/products/{productId}");

    return product;
  }

  public async Task GetProducts()
  {
    var products = await httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/products");

    if (products != null && products.Data != null)
    {
      Products = products.Data;
    }
  }
}