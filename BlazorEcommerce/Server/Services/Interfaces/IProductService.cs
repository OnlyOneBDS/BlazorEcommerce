namespace BlazorEcommerce.Server.Services.Interfaces;

public interface IProductService
{
  Task<ServiceResponse<List<Product>>> GetProductsAsync();
}