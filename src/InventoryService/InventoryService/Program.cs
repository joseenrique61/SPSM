using InventoryService.Application.Interfaces;
using InventoryService.Application.Services;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.ApplicationDBContext;
using InventoryService.Infrastructure.Interceptors;
using InventoryService.Infrastructure.Interfaces;
using InventoryService.Infrastructure.Interfaces.Producers;
using InventoryService.Infrastructure.QueueManager.RabbitMQ;
using InventoryService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SoftDeleteInterceptor>();
builder.Services.AddDbContext<IApplicationDBContext, ApplicationDBContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();

    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddInterceptors(interceptor);
});

builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddSingleton<IQueueConnection, RabbitMQConnection>();

builder.Services.AddScoped<IProducer, RabbitMQProducer>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    db.Database.Migrate();
}

app.MapControllers();

app.Run();