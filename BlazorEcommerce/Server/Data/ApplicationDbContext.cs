namespace BlazorEcommerce.Server.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<Product> Products { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<ProductType> ProductTypes { get; set; }
  public DbSet<ProductVariant> ProductVariants { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
  }
}
