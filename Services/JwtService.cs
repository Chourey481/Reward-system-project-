using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RewardSystemAPI.Services;

public class JwtService
{
    private readonly IConfiguration _config;
    public JwtService(IConfiguration config) => _config = config;
    public string GenerateToken(int memberId, string mobile)
    {
        var key = _config["Jwt:Key"] ?? "DefaultKey";
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var expiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"] ?? "60"));

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, memberId.ToString()),
            new Claim("mobile", mobile)
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var token = new JwtSecurityToken(issuer, audience, claims, expires: expiry, signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
