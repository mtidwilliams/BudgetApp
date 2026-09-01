using BudgetApp.Common;
using BudgetApp.Core.Common.DTOs;
using BudgetApp.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface ITokenService
{
    string GenerateToken(User user);
}

public class TokenService : ITokenService
{
    private readonly AppSettings? _appSettings;

    public TokenService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings?.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings?.SecretKey ?? Guid.NewGuid().ToString()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _appSettings?.Issuer,
            _appSettings?.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
