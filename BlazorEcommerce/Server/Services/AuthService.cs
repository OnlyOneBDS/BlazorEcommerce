﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BlazorEcommerce.Server.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BlazorEcommerce.Server.Services;

public class AuthService : IAuthService
{
  private readonly ApplicationDbContext _context;
  private readonly IConfiguration _configuration;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public AuthService(ApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
  {
    _context = context;
    _configuration = configuration;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<ServiceResponse<string>> LoginAsync(string email, string password)
  {
    var response = new ServiceResponse<string>();
    var user = await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());

    if (user == null)
    {
      response.Success = false;
      response.Message = "User not found";
    }
    else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
    {
      response.Success = false;
      response.Message = "Wrong password";
    }
    else
    {
      response.Data = CreateToken(user);
    }

    return response;
  }

  public async Task<ServiceResponse<int>> RegisterAsync(User user, string password)
  {
    if (await UserExistsAsync(user.Email))
    {
      return new ServiceResponse<int>
      {
        Success = false,
        Message = "User already exists"
      };
    }

    CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

    user.PasswordHash = passwordHash;
    user.PasswordSalt = passwordSalt;

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return new ServiceResponse<int>
    {
      Data = user.Id,
      Message = "User registration successful!"
    };
  }

  public async Task<bool> UserExistsAsync(string email)
  {
    if (await _context.Users.AnyAsync(user => user.Email.ToLower() == email.ToLower()))
    {
      return true;
    }

    return false;
  }

  public async Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, string newPassword)
  {
    var user = await _context.Users.FindAsync(userId);

    if (user == null)
    {
      return new ServiceResponse<bool>
      {
        Success = false,
        Message = "User not found."
      };
    }

    CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

    user.PasswordHash = passwordHash;
    user.PasswordSalt = passwordSalt;

    await _context.SaveChangesAsync();

    return new ServiceResponse<bool> 
    { 
      Data = true, 
      Message = "Password has been changed." 
    };
  }

  public int GetUserId()
  {
    return int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
  }

  public string GetUserEmail()
  {
    return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
  }

  public async Task<User> GetUserByEmailAsync(string email)
  {
    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }

  private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
  {
    using var hmac = new HMACSHA512();

    passwordSalt = hmac.Key;
    passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
  }

  private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
  {
    using var hmac = new HMACSHA512(passwordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

    return computedHash.SequenceEqual(passwordHash);
  }

  private string CreateToken(User user)
  {
    var claims = new List<Claim>
    {
      new(ClaimTypes.NameIdentifier, user.Id.ToString()),
      new(ClaimTypes.Name, user.Email)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

    return jwt;
  }
}