namespace BlazorEcommerce.Server.Services.CartService;

public class CartService : ICartService
{
  private readonly DataContext context;

  public CartService(DataContext context)
  {
    this.context = context;
  }

  public async Task<ServiceResponse<List<CartProductDto>>> GetCartItems(List<CartItem> cartItems)
  {
    var result = new ServiceResponse<List<CartProductDto>>
    {
      Data = new List<CartProductDto>()
    };

    foreach (var item in cartItems)
    {
      var product = await context.Products.
        Where(p => p.Id == item.ProductId)
        .FirstOrDefaultAsync();

      if (product == null)
      {
        continue;
      }

      var productVariant = await context.ProductVariants
        .Include(v => v.ProductType)
        .Where(v => v.ProductId == item.ProductId && v.ProductTypeId == item.ProductTypeId)
        .FirstOrDefaultAsync();

      if (productVariant == null)
      {
        continue;
      }

      var cartItem = new CartProductDto
      {
        ProductId = product.Id,
        Title = product.Title,
        ImageUrl = product.ImageUrl,
        Price = productVariant.Price,
        ProductType = productVariant.ProductType.Name,
        ProductTypeId = productVariant.ProductTypeId,
        Quantity = item.Quantity
      };

      result.Data.Add(cartItem);
    }

    return result;
  }
}
