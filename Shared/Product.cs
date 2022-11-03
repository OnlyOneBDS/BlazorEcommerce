using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEcommerce.Shared;

public class Product
{
  public int Id { get; set; }

  public int CategoryId { get; set; }

  public string Title { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public string ImageUrl { get; set; } = string.Empty;

  public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();


  public Category? Category { get; set; }
}