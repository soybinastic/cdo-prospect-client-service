using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CDOProspectClient.Infrastructure.Helpers.Jwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(string userId, string userCredential, List<string> roles)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]!)),
            SecurityAlgorithms.HmacSha256
        );
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, userCredential),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var securityToken = new JwtSecurityToken(
            issuer : _configuration["JWTSettings:Issuer"],
            expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["JWTSettings:Expire"])),
            claims : claims,
            signingCredentials : signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}