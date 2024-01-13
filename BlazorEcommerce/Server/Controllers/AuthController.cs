using System.Security.Claims;
using BlazorEcommerce.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("login")]
  public async Task<ActionResult<ServiceResponse<int>>> LoginAsync(UserLogin request)
  {
    var response = await _authService.LoginAsync(request.Email, request.Email);

    if (!response.Success)
    {
      return BadRequest(response);
    }

    return Ok(response);
  }

  [HttpPost("register")]
  public async Task<ActionResult<ServiceResponse<int>>> RegisterAsync(UserRegister request)
  {
    var response = await _authService.RegisterAsync(new User
    {
      Email = request.Email
    },
    request.Email);

    if (!response.Success)
    {
      return BadRequest(response);
    }

    return Ok(response);
  }

  [Authorize]
  [HttpPost("change-password")]
  public async Task<ActionResult<ServiceResponse<bool>>> ChangePasswordAsync([FromBody] string password)
  {
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
    var response = await _authService.ChangePasswordAsync(int.Parse(userId), password);

    if (!response.Success)
    {
      return BadRequest(response);
    }

    return Ok(response);
  }
}
