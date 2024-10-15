using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using PMS.Domain.Entities;
using PMS.Application.Interfaces;
using PMS.Domain.Entities.DTOs; // Adjust this based on your namespaces
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> Authenticate(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role) // Assuming Role property exists on ApplicationUser
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new AuthResponseDto
            {
                IsSuccess = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                User = new
                {
                    Role = user.Role // Make sure this property exists on your user class
                }
            };
        }

        return new AuthResponseDto
        {
            IsSuccess = false,
            Error = "Invalid login attempt."
        };
    }
}