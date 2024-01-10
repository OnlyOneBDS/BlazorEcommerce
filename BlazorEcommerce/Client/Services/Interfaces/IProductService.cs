using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.Interfaces;

public interface IProductService
{
  List<Product> Products { get; set; }
  Task GetProducts();
}
