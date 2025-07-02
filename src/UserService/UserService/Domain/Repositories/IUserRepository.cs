using UserService.Domain.Models;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    public Task<JwtResponse> RegisterUserAsync(Client client);

    public Task<JwtResponse> LoginAsync(User user);
}