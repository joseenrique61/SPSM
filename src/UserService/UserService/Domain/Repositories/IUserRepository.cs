using UserService.Domain.Models;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    public Task<bool> RegisterUserAsync(Client client);

    public Task<bool> LoginAsync(User user);
}