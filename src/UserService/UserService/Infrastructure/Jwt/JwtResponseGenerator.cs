using UserService.Application.Jwt;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Jwt;

public class JwtResponseGenerator(IJwtGenerator tokenGenerator) : IJwtResponseGenerator
{
    public JwtResponse Generate(string email, string role, int clientId)
    {
        return new JwtResponse
        {
            Token = tokenGenerator.GenerateToken(email, role),
            Email = email,
            Role = role,
            ClientId = clientId
        };
    }
}