using Microsoft.EntityFrameworkCore;
using NotificationService.Domain.Entities;
using NotificationService.Application.DTOs;

namespace NotificationService.Infraestructure.Persistence
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options)
    {
        public DbSet<Notification> Notification { get; set; }
    }
}
