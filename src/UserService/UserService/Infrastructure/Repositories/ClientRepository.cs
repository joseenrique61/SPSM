using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Repositories
{
    public class ClientRepository(ApplicationDbContext.ApplicationDbContext context) : IClientRepository
    {
        public async Task<ClientDTO> GetClientByUserId(int id)
        {
            var client = await context.Clients.Where(c => c.UserId == id).Include(x => x.User).FirstAsync();       

            if (client == null)
                throw new Exception($"There is no customer information associated with this UserId: {id}");
            
            ClientDTO clientDTO = new ClientDTO { 
                Name = client.Name,
                Email = client.User!.Email,
                Address = client.Address
            };

            return clientDTO;
        }

        public async Task<Client> GetFullClientById(int id)
        {
            var client = await context.Clients.Where(c => c.Id == id).Include(x => x.User).FirstAsync();       
            
            if (client == null)
                throw new Exception($"There is no customer information associated with this UserId: {id}");

            return client;
        }
    }
}
