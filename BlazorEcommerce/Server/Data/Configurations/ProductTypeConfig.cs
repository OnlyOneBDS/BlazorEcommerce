﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorEcommerce.Server.Data.Configurations;

public class ProductTypeConfig : IEntityTypeConfiguration<ProductType>
{
  public void Configure(EntityTypeBuilder<ProductType> builder)
  {
    builder.HasData(
      new ProductType { Id = 1, Name = "Default" },
      new ProductType { Id = 2, Name = "Paperback" },
      new ProductType { Id = 3, Name = "E-Book" },
      new ProductType { Id = 4, Name = "Audiobook" },
      new ProductType { Id = 5, Name = "Stream" },
      new ProductType { Id = 6, Name = "Blu-Ray" },
      new ProductType { Id = 7, Name = "VHS" },
      new ProductType { Id = 8, Name = "PC" },
      new ProductType { Id = 9, Name = "Playstation" },
      new ProductType { Id = 10, Name = "Xbox" }
    );
  }
}
