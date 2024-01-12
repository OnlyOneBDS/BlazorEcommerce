using System.Net.Http.Json;
using BlazorEcommerce.Client.Services.Interfaces;
using BlazorEcommerce.Shared;
using Blazored.LocalStorage;

namespace BlazorEcommerce.Client.Services;

public class CartService : ICartService
{
  private readonly ILocalStorageService _localStorage;
  private readonly HttpClient _httpClient;

  public CartService(ILocalStorageService localStorage, HttpClient httpClient)
  {
    _localStorage = localStorage;
    _httpClient = httpClient;
  }

  public event Action OnChange;

  public async Task AddToCart(CartItem cartItem)
  {
    var cart = await GetCartAsync();
    var sameItem = cart.Find(p => p.ProductId == cartItem.ProductId && p.ProductTypeId == cartItem.ProductTypeId);

    if (sameItem == null)
    {
      cart.Add(cartItem);
    }
    else
    {
      sameItem.Quantity += cartItem.Quantity;
    }

    await _localStorage.SetItemAsync("cart", cart);
    OnChange?.Invoke();
  }

  public async Task<List<CartItem>> GetCartItems()
  {
    return await GetCartAsync();
  }

  public async Task<List<CartProductResponse>> GetCartProducts()
  {
    var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
    var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
    var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();

    return cartProducts.Data;
  }

  public async Task RemoveFromCart(int productId, int productTypeId)
  {
    var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

    if (cart == null)
    {
      return;
    }

    var cartItem = cart.Find(p => p.ProductId == productId && p.ProductTypeId == productTypeId);

    if (cartItem != null)
    {
      cart.Remove(cartItem);
      await _localStorage.SetItemAsync("cart", cart);
      OnChange?.Invoke();
    }
  }

  public async Task UpdateQuantity(CartProductResponse product)
  {
    var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

    if (cart == null)
    {
      return;
    }

    var cartItem = cart.Find(p => p.ProductId == product.ProductId && p.ProductTypeId == product.ProductTypeId);

    if (cartItem != null)
    {
      cartItem.Quantity = product.Quantity;
      await _localStorage.SetItemAsync("cart", cart);
    }
  }

  private async Task<List<CartItem>> GetCartAsync()
  {
    return await _localStorage.GetItemAsync<List<CartItem>>("cart") ?? new List<CartItem>();
  }
}
