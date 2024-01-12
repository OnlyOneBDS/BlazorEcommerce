namespace BlazorEcommerce.Shared;

public class ProductSearchResult
{
  public List<Product> Products { get; set; } = new List<Product>();
  public int CurrentPage { get; set; }
  public int TotalPages { get; set; }
}
