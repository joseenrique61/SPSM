using UserService.Domain.Models;

namespace UserService.Domain.Repositories
{
    public interface IClientRepository
    {
        public Task<Client> GetClientByUserId(int id);
    }
}
