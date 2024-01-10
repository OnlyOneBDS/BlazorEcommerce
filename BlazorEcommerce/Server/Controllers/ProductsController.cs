using BlazorEcommerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{  
  private readonly IProductService _productService;

  public ProductsController(IProductService productService)
  {
    _productService = productService;
  }

  [HttpGet]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
  {
    var result = await _productService.GetProductsAsync();
    return Ok(result);
  }
}
