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

  [HttpGet("featured")]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
  {
    var result = await _productService.GetFeaturedProductsAsync();
    return Ok(result);
  }

  [HttpGet("category/{categoryUrl}")]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
  {
    var result = await _productService.GetProductsByCategoryAsync(categoryUrl);
    return Ok(result);
  }

  [HttpGet("search/{searchText}/{page}")]
  public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string searchText, int page = 1)
  {
    var result = await _productService.SearchProductsAsync(searchText, page);
    return Ok(result);
  }

  [HttpGet("searchsuggestions/{searchText}")]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSearchSuggestions(string searchText)
  {
    var result = await _productService.GetProductSearchSuggestionsAsync(searchText);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int id)
  {
    var result = await _productService.GetProductAsync(id);
    return Ok(result);
  }
}
