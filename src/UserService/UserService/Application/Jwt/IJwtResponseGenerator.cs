using UserService.Domain.Models;

namespace UserService.Application.Jwt;

public interface IJwtResponseGenerator
{
    public JwtResponse Generate(string email, string role, int clientId);
}