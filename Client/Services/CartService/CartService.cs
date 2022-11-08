using BlazorEcommerce.Shared.DTOs;
using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services.CartService;

public class CartService : ICartService
{
  private readonly ILocalStorageService localStorage;
  private readonly HttpClient httpClient;

  public CartService(ILocalStorageService localStorage, HttpClient httpClient)
  {
    this.localStorage = localStorage;
    this.httpClient = httpClient;
  }

  public event Action OnChange;

  public async Task AddToCart(CartItem cartItem)
  {
    var cart = await localStorage.GetItemAsync<List<CartItem>>("cart");

    if (cart == null)
    {
      cart = new List<CartItem>();
    }

    var sameItem = cart.Find(i => i.ProductId == cartItem.ProductId && i.ProductTypeId == cartItem.ProductTypeId);

    if (sameItem == null)
    {
      cart.Add(cartItem);
    }
    else
    {
      sameItem.Quantity += cartItem.Quantity;
    }

    await localStorage.SetItemAsync("cart", cart);
    OnChange.Invoke();
  }

  public async Task<List<CartProductDto>> GetCartItems()
  {
    var cartItems = await localStorage.GetItemAsync<List<CartItem>>("cart");
    var response = await httpClient.PostAsJsonAsync("api/cart/products", cartItems);
    var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductDto>>>();

    return result.Data;
  }

  public async Task<List<CartItem>> GetLocalStorageCartItems()
  {
    var cart = await localStorage.GetItemAsync<List<CartItem>>("cart");

    if (cart == null)
    {
      cart = new List<CartItem>();
    }

    return cart;
  }

  public async Task RemoveFromCart(int productId, int productTypeId)
  {
    var cart = await localStorage.GetItemAsync<List<CartItem>>("cart");

    if (cart == null)
    {
      return;
    }

    var cartItem = cart.Find(ci => ci.ProductId == productId && ci.ProductTypeId == productTypeId);

    if (cartItem != null)
    {
      cart.Remove(cartItem);
      await localStorage.SetItemAsync("cart", cart);
      OnChange.Invoke();
    }
  }

  public async Task UpdateQuantity(CartProductDto cartItem)
  {
    var cart = await localStorage.GetItemAsync<List<CartItem>>("cart");

    if (cart == null)
    {
      return;
    }

    var item = cart.Find(ci => ci.ProductId == cartItem.ProductId && ci.ProductTypeId == cartItem.ProductTypeId);

    if (cartItem != null)
    {
      item.Quantity = cartItem.Quantity;
      await localStorage.SetItemAsync("cart", cart);
    }
  }
}
