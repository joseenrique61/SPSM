using DataSeeder.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostApplicationBuilder(args);

builder.Services.AddDbContext<InventoryApplicationDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("InventoryConnection"));
});
builder.Services.AddDbContext<SearchApplicationDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("SearchConnection"));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var inventoryDbContext = scope.ServiceProvider.GetRequiredService<InventoryApplicationDbContext>();
inventoryDbContext.Database.Migrate();
DataSeeder.DataSeeder.SeedProducts(inventoryDbContext);

var searchDbContext = scope.ServiceProvider.GetRequiredService<SearchApplicationDbContext>();
searchDbContext.Database.Migrate();
DataSeeder.DataSeeder.SeedProducts(searchDbContext);