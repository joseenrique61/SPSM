using UserService.Application.DTOs;

namespace UserService.Domain.Repositories
{
    public interface IClientRepository
    {
        public Task<ClientDTO> GetClientByUserId(int id);
    }
}
