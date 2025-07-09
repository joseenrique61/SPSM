using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = new HostApplicationBuilder();

builder.Services.AddScoped<DataSeeder.DataSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<DataSeeder.DataSeeder>().SeedProducts();
}

// app.Run();