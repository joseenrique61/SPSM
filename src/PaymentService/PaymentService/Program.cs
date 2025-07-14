using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Clients;
using PaymentService.Application.Handlers;
using PaymentService.Domain.Repositories;
using PaymentService.Infrastructure.ApplicationDbContext;
using PaymentService.Infrastructure.Clients;
using PaymentService.Infrastructure.Clients.Inventory;
using PaymentService.Infrastructure.Interfaces;
using PaymentService.Infrastructure.Interfaces.Producers;
using PaymentService.Infrastructure.QueueManager.RabbitMQ;
using PaymentService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

ConfigureHttpClients();

// RabbitMQ
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IQueueConnection, RabbitMQConnection>();
builder.Services.AddSingleton<IProducer, RabbitMQProducer>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentHandler, PaymentHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
return;

void ConfigureHttpClients()
{
    const string baseAddress = "http://kong:8000/";

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddTransient<AuthenticationPropagationHandler>();
    builder.Services.AddHttpClient<IShoppingCartClient, ShoppingCartClient>(client =>
    {
        client.BaseAddress = new Uri(baseAddress + "shopping_cart/");
    })
    .AddHttpMessageHandler<AuthenticationPropagationHandler>();
    builder.Services.AddHttpClient<IInventoryClient, InventoryClient>(client =>
    {
        client.BaseAddress = new Uri(baseAddress + "inventory/product/");
    })
    .AddHttpMessageHandler<AuthenticationPropagationHandler>();
    builder.Services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
    {
        client.BaseAddress = new Uri(baseAddress + "client/");
    })
    .AddHttpMessageHandler<AuthenticationPropagationHandler>();
}