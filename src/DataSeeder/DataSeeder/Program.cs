using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostApplicationBuilder();

builder.Services.AddScoped<DataSeeder.DataSeeder>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder.DataSeeder>();
await seeder.SeedProducts();
await seeder.SeedAdmin();
