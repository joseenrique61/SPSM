using Microsoft.EntityFrameworkCore;
using UserService.Application;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext.ApplicationDbContext context) : IUserRepository
{
    public async Task<bool> RegisterUserAsync(Client client)
    {
	    try
	    {
		    client.User!.PasswordHash = PasswordHasher.Hash(client.User.Password!);
		    await context.Users.AddAsync(client.User);
		    await context.SaveChangesAsync();

		    await context.Clients.AddAsync(client);
		    await context.SaveChangesAsync();

		    // return Ok(_responseGenerator.Generate(client.User.Email, UserTypes.Client, client.Id));
		    return true;
	    }
	    catch
	    {
		    return false;
	    }
    }

    public async Task<bool> LoginAsync(User user)
    {
	    try
	    {
		    var userFromDb = await context.Users.FirstAsync(x => x.Email == user.Email);
		    return PasswordHasher.Verify(user.Password!, userFromDb.PasswordHash!);
	    }
	    catch
	    {
		    return false;
	    }
    }
}