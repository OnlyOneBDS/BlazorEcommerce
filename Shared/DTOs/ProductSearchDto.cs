using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Shared.DTOs;

public class ProductSearchDto
{
  public List<Product> Products { get; set; } = new List<Product>();
  public int Pages { get; set; }
  public int CurrentPage { get; set; }

}