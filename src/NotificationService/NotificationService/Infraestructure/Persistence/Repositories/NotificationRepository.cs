using NotificationService.Domain.Entities;
using NotificationService.Domain.Interfaces;

namespace NotificationService.Infraestructure.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDBContext _context;
        public NotificationRepository(ApplicationDBContext context) 
        { 
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notification.AddAsync(notification);
        }

        public async Task<Notification> GetById(Guid id)
        {
            return (await _context.Notification.FindAsync(id))!;
        }
    }
}
