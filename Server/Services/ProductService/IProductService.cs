namespace BlazorEcommerce.Server.Services.ProductService;

public interface IProductService
{
  Task<ServiceResponse<List<Product>>> GetProductsAsync();
  Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
  Task<ServiceResponse<List<string>>> GetSearchSuggestionsAsync(string searchText);
  Task<ServiceResponse<List<Product>>> SearchProductsAsync(string searchText);
  Task<ServiceResponse<Product>> GetProductAsync(int productId);
}
