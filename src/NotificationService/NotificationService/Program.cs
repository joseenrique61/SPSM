using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;
using NotificationService.Domain.Interfaces;
using NotificationService.Infraestructure.NotificationsProvider;
using NotificationService.Infraestructure.Persistence;
using NotificationService.Infraestructure.Persistence.Repositories;
using NotificationService.Infrastructure;
using NotificationService.Infrastructure.Consumers;
using NotificationService.Infrastructure.Interfaces;
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

builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the implementation of the Repository.
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Register the Main Application service.
builder.Services.AddScoped<NotificationAppService>();

// RabbitMQ
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<IQueueConnection, RabbitMQConnection>();
builder.Services.AddHostedService<NotificationsEventsConsumer>();

builder.Services.AddTransient<EmailProvider>();
builder.Services.AddTransient<SMSProvider>();

builder.Services.AddTransient<Func<string, INotificationProvider>>
    (serviceProvider => key =>
    {
        switch (key.ToLowerInvariant())
        {
            case "email":
                return serviceProvider.GetRequiredService<EmailProvider>();
            case "sms":
                return serviceProvider.GetRequiredService<SMSProvider>();
            default:
                throw new NotSupportedException($"The provider '{key}' is not supported.");
        }
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    try
    {
        // Apply any pending migrations to create the database and tables
        dbContext.Database.Migrate();
        Console.WriteLine("Database migration completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during database migration: {ex.Message}");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Simple Healthcheck endpoint to check the health of the microservice (monitor it)
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
