namespace BlazorEcommerce.Server.Services.ProductService;

public interface IProductService
{
  Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
  Task<ServiceResponse<List<Product>>> GetProductsAsync();
  Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
  Task<ServiceResponse<List<string>>> GetSearchSuggestionsAsync(string searchText);
  Task<ServiceResponse<ProductSearchDto>> SearchProductsAsync(string searchText, int page);
  Task<ServiceResponse<Product>> GetProductAsync(int productId);
}
