using Microsoft.EntityFrameworkCore;
using SearchService.Domain.Models;

namespace SearchService.Infrastructure.ApplicationDbContext;

public interface IApplicationDbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}