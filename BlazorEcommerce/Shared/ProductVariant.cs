using System.Text.Json.Serialization;

namespace BlazorEcommerce.Shared;

public class ProductVariant
{
  public int ProductId { get; set; }
  public int ProductTypeId { get; set; }
  public decimal Price { get; set; }
  public decimal OriginalPrice { get; set; }

  [JsonIgnore]
  public Product Product { get; set; }
  public ProductType ProductType { get; set; }
}