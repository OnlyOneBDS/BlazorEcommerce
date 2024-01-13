namespace BlazorEcommerce.Server.Services.Interfaces;

public interface IAuthService
{
  int GetUserId();
  string GetUserEmail();

  Task<ServiceResponse<string>> LoginAsync(string email, string password);
  Task<ServiceResponse<int>> RegisterAsync(User user, string password);
  Task<bool> UserExistsAsync(string email);
  Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string newPassword);
  Task<User> GetUserByEmailAsync(string email);
}
