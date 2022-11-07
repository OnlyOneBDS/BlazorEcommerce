using BlazorEcommerce.Server.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly IProductService productService;

  public ProductsController(IProductService productService)
  {
    this.productService = productService;
  }

  [HttpGet("featured")]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
  {
    var products = await productService.GetFeaturedProductsAsync();
    return Ok(products);
  }

  [HttpGet]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
  {
    var products = await productService.GetProductsAsync();
    return Ok(products);
  }

  [HttpGet("category/{categoryUrl}")]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategory(string categoryUrl)
  {
    var products = await productService.GetProductsByCategoryAsync(categoryUrl);
    return Ok(products);
  }

  [HttpGet("search/{searchText}/{page}")]
  public async Task<ActionResult<ServiceResponse<ProductSearchDto>>> SearchProducts(string searchText, int page = 1)
  {
    var products = await productService.SearchProductsAsync(searchText, page);
    return Ok(products);
  }

  [HttpGet("search-suggestions/{searchText}")]
  public async Task<ActionResult<ServiceResponse<List<Product>>>> GetSearchSuggestions(string searchText)
  {
    var products = await productService.GetSearchSuggestionsAsync(searchText);
    return Ok(products);
  }

  [HttpGet("{productId}")]
  public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
  {
    var product = await productService.GetProductAsync(productId);
    return Ok(product);
  }
}
