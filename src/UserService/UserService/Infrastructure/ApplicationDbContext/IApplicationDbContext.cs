using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;

namespace UserService.Infrastructure.ApplicationDbContext;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Administrator> Administrators { get; set; }
}