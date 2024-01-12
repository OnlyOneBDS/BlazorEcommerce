using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.Interfaces;

public interface ICategoryService
{
  List<Category> Categories { get; set; }
  Task GetCategories();
}
