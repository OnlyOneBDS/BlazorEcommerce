using BlazorEcommerce.Shared;

namespace BlazorEcommerce.Client.Services.Interfaces;

public interface IAuthService
{
  Task<ServiceResponse<string>> Login(UserLogin request);
  Task<ServiceResponse<int>> Register(UserRegister request);
  Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
  Task<bool> IsUserAuthenticated();
}
