using UI.Models;

namespace UI.Data.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        public Task<bool> Login(string email, string password);
        public Task<bool> Register(Client client);

        public Task<Client?> GetById(int id);
    }
}