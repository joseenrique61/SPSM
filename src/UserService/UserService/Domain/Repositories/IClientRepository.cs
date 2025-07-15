using UserService.Application.DTOs;
using UserService.Domain.Models;

namespace UserService.Domain.Repositories
{
    public interface IClientRepository
    {
        public Task<ClientDTO> GetClientByUserId(int id);
        
        public Task<Client> GetFullClientById(int id);
    }
}
