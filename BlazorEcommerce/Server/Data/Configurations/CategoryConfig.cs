using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorEcommerce.Server.Data.Configurations;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.HasData(
      new Category 
      {
        Id = 1,
        Name = "Books",
        Url = "books"
      },
      new Category
      {
        Id = 2,
        Name = "Movies",
        Url = "movies"
      },
      new Category
      {
        Id = 3,
        Name = "Video Games",
        Url = "video-games"
      });
  }
}