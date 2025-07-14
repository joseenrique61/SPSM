using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Repositories;

public class ClientRepository(ApplicationDbContext.ApplicationDbContext context) : IClientRepository
{
    public async Task<ClientDTO> GetClientByUserId(int id)
    {
        var client = await context.Clients.Where(c => c.UserId == id).Include(x => x.User).FirstAsync();

        if (client == null)
            throw new Exception("There is no customer information associated with the User ID.");

        ClientDTO clientDTO = new ClientDTO
        {
            Name = client.Name,
            Address = client.Address,
            Email = client.User!.Email
        };

        return clientDTO;
    }
}