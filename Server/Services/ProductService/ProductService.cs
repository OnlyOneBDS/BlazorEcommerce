using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Services.ProductService;

public class ProductService : IProductService
{
  private readonly DataContext context;

  public ProductService(DataContext context)
  {
    this.context = context;
  }

  public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
  {
    var response = new ServiceResponse<Product>();
    var product = await context.Products.FindAsync(productId);

    if (product == null)
    {
      response.Success = false;
      response.Message = "This product could not be found";
    }
    else
    {
      response.Data = product;
    }

    return response;
  }

  public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
  {
    var response = new ServiceResponse<List<Product>>
    {
      Data = await context.Products.ToListAsync()
    };

    return response;
  }
}
