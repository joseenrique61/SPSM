using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SearchService.Domain.Repositories;
using SearchService.Infrastructure.ApplicationDbContext;
using SearchService.Infrastructure.Interfaces;
using SearchService.Infrastructure.QueueManager;
using SearchService.Infrastructure.QueueManager.Consumers;
using SearchService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// RabbitMQ Configuration 

builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSingleton<IQueueConnection, RabbitMQConnection>();

builder.Services.AddHostedService<InventoryEventsConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.MapControllers();

app.Run();
