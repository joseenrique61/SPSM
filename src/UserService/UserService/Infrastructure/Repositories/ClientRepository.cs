using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Repositories
{
    public class ClientRepository(ApplicationDbContext.ApplicationDbContext context) : IClientRepository
    {
        public async Task<Client> GetClientByUserId(int id)
        {
            var client = await context.Clients.FirstAsync(c => c.UserId == id);       

            if (client == null)
                throw new Exception($"There is no customer information associated with this UserId: {id}");

            return client;
        }
    }
}
