using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Jwt;

namespace UserService.Infrastructure.Jwt;

public class JwtGenerator(IConfiguration configuration) : IJwtGenerator
{
    public string GenerateToken(string email, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(ClaimTypes.Role, role)
        };

        // Read private key from file
        var privateKeyPem = File.ReadAllText("private.pem");
        var rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem.ToCharArray());

        var credentials = new SigningCredentials(
            new RsaSecurityKey(rsa),
            SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"]!,
            audience: configuration["JwtSettings:Audience"]!,
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}