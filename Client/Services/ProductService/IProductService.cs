namespace BlazorEcommerce.Client.Services.ProductService;

public interface IProductService
{
  event Action ProductsChanged;

  List<Product> Products { get; set; }
  string Message { get; set; }
  int CurrentPage { get; set; }
  int PageCount { get; set; }
  public string LastSearchText { get; set; }

  Task GetProducts(string? categoryUrl = null);
  Task SearchProducts(string searchText, int page);
  Task<List<string>> GetSearchSuggestions(string searchText);
  Task<ServiceResponse<Product>> GetProduct(int productId);
}
