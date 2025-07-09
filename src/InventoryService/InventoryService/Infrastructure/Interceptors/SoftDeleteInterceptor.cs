using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using InventoryService.Domain.Interfaces;

namespace InventoryService.Infrastructure.Interceptors
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<ISoftDelete>> entries =
                eventData
                    .Context
                    .ChangeTracker
                    .Entries<ISoftDelete>()
                    .Where(e => e.State == EntityState.Deleted);

            foreach (EntityEntry<ISoftDelete> softDeletableEntity in entries)
            {
                softDeletableEntity.State = EntityState.Modified;
                softDeletableEntity.Entity.IsDeleted = true;
                softDeletableEntity.Entity.DeletedAt = DateTimeOffset.UtcNow;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
