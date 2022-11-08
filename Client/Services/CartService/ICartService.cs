using BlazorEcommerce.Shared.DTOs;

namespace BlazorEcommerce.Client.Services.CartService;

public interface ICartService
{
  event Action OnChange;

  Task<List<CartProductDto>> GetCartItems();
  Task<List<CartItem>> GetLocalStorageCartItems();
  Task AddToCart(CartItem cartItem);
  Task UpdateQuantity(CartProductDto cartItem);
  Task RemoveFromCart(int productId, int productTypeId);
}