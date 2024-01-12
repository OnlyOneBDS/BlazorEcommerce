using BlazorEcommerce.Server.Services.Interfaces;

namespace BlazorEcommerce.Server.Services;

public class CartService : ICartService
{
  private readonly ApplicationDbContext _context;

  public CartService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
  {
    var result = new ServiceResponse<List<CartProductResponse>>
    {
      Data = new List<CartProductResponse>()
    };

    foreach (var cartItem in cartItems)
    {
      var product = await _context.Products
        .FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

      if (product == null)
      {
        continue;
      }

      var producVariant = await _context.ProductVariants
        .Include(p => p.ProductType)
        .FirstOrDefaultAsync(p => p.ProductId == cartItem.ProductId && p.ProductTypeId == cartItem.ProductTypeId);
        
      if (producVariant == null)
      {
        continue;
      }

      var cartProduct = new CartProductResponse
      {
        ProductId = product.Id,
        ProductTypeId = producVariant.ProductTypeId,
        Title = product.Title,
        ProductType = producVariant.ProductType.Name,
        ImageUrl = product.ImageUrl,
        Price = producVariant.Price,
        Quantity = cartItem.Quantity
      };

      result.Data.Add(cartProduct);
    }

    return result;
  }
}
