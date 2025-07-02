namespace UserService.Application.Jwt;

public interface IJwtGenerator
{
    public string GenerateToken(string email, string role);
}