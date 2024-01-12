namespace BlazorEcommerce.Server.Services.Interfaces;

public interface IProductService
{
  Task<ServiceResponse<List<Product>>> GetProductsAsync();
  Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
  Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
  Task<ServiceResponse<ProductSearchResult>> SearchProductsAsync(string searchText, int page);
  Task<ServiceResponse<List<string>>> GetProductSearchSuggestionsAsync(string searchText);
  Task<ServiceResponse<Product>> GetProductAsync(int productId);
}