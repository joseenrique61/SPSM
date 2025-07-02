using Microsoft.EntityFrameworkCore;
using UserService.Application;
using UserService.Application.Jwt;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext.ApplicationDbContext context, IPasswordHasher passwordHasher, IJwtResponseGenerator responseGenerator) : IUserRepository
{
    public async Task<JwtResponse> RegisterUserAsync(Client client)
    {
	    client.User!.PasswordHash = passwordHasher.GenerateHash(client.User.Password!);
	    await context.Users.AddAsync(client.User);
	    await context.SaveChangesAsync();

	    await context.Clients.AddAsync(client);
	    await context.SaveChangesAsync();

	    return responseGenerator.Generate(client.User.Email, "client", client.Id);
    }

    public async Task<JwtResponse> LoginAsync(User user)
    {
	    var userFromDb = await context.Users.FirstAsync(x => x.Email == user.Email);
	    
	    if (!passwordHasher.VerifyHash(user.Password!, userFromDb.PasswordHash!))
	    {
		    throw new Exception("The user and/or password are incorrect.");
	    }
	    
	    var client = await context.Clients.Include(nameof(Client.User)).FirstOrDefaultAsync(c => c.User!.Email == user.Email);
	    return client != null ? responseGenerator.Generate(client.User!.Email, "client", client.Id) : responseGenerator.Generate(userFromDb.Email, "admin", userFromDb.Id);
    }
}