namespace BlazorEcommerce.Shared;

public class User
{
  public int Id { get; set; }
  public string Email { get; set; } = string.Empty;
  public byte[] PasswordHash { get; set; }
  public byte[] PasswordSalt { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.Now;
  //public Address Address { get; set; }
  //public string Role { get; set; } = "Customer";
}

public class Address
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string FirstName { get; set; } = string.Empty;
  public string LastName { get; set; } = string.Empty;
  public string Street { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string Zip { get; set; } = string.Empty;
  public string Country { get; set; } = string.Empty;
}