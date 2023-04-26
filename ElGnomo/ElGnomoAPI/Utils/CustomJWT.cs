using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElGnomoAPI.Utils;

public class CustomJWT
{
    public static string GetToken(string Email, IConfiguration _configuration)
    {
        var secretKey = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!);
        var issuer = _configuration["JWT:Issuer"];
        var audience = _configuration["JWT:Audience"];


        var _symmetricSecurityKey = new SymmetricSecurityKey(secretKey);
        var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var _Header = new JwtHeader(_signingCredentials);
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, Email)
        };
        var _Payload = new JwtPayload(
            issuer,
            audience,
            claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(2)
        );
        var _Token = new JwtSecurityToken(_Header, _Payload);
        return new JwtSecurityTokenHandler().WriteToken(_Token);
    }
}
