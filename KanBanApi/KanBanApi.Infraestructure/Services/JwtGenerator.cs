using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanBanApi.Application.Services;
using KanBanApi.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace KanBanApi.Infraestructure.Services;

public sealed class JwtGenerator : IJwtGenerator
{
    private readonly ConfigurationManager _configuration;

    public JwtGenerator(ConfigurationManager configuration)
    {
        _configuration = configuration;
    }

    public string GetToken(AppUser user)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"]!;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.UserName ?? String.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? String.Empty)
        };

        var token = new JwtSecurityToken(issuer,
                                         audience,
                                         claims,
                                         expires: DateTime.Now.AddMinutes(30),
                                         signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}