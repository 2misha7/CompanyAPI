using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApbdProject.Context;
using ApbdProject.Helpers;
using GakkoHorizontalSlice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApbdProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly MyContext _context;

    public UsersController(IConfiguration configuration, MyContext context)
    {
        _configuration = configuration;
        _context = context;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult RegisterStudent(RegisterRequest model)
    {
        
        if (_context.Users.Any(u => u.Login.Equals(model.Login)))
        {
            throw new Exception("Such login already exists");
        }
        if (model.Role != "admin" && model.Role != "user")
        {
            return BadRequest("Invalid role");
        }
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);
        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Role = model.Role,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok();
    }
    
    
    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        AppUser user = _context.Users.FirstOrDefault(u => u.Login == loginRequest.Login)!;

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword && user.Role == loginRequest.Role)
        {
            return Unauthorized();
        }
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Role, user.Role)
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        });
    }
    
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public IActionResult Refresh(RefreshTokenRequest refreshTokenRequest)
    {
        var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshTokenRequest.RefreshToken);

        if (user == null)
        {
            return Unauthorized("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            return Unauthorized("Refresh token expired");
        }
        
        var userClaims = new[]
        {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _context.SaveChanges();

        return Ok(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        });
    }
    
}