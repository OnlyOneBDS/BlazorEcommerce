using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.Interfaces;

public interface IProductService
{
  event Action ProductsChanged;

  string Message { get; set; }
  int CurrentPage { get; set; }
  int PageCount { get; set; }
  string LastSearchText { get; set; }
  List<Product> Products { get; set; }
  Task GetProducts(string? categoryUrl = null);
  Task SearchProducts(string searchText, int page);
  Task<List<string>> GetProductSearchSuggestions(string searchText);
  Task<ServiceResponse<Product>> GetProduct(int id);
}
