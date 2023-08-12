using DomainEevent.Framework;
using DomainEvents.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DomainEvents.Infra.Dal
{
    public class SampleContext:DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            HandleBeforeSaveChanges();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            HandleBeforeSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            HandleBeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleBeforeSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void HandleBeforeSaveChanges()
        {
            DispatchEvents();
        }

        private void DispatchEvents()
        {
            var dispathcer = this.GetService<IDomainEventDispatcher>();

            var entities = ChangeTracker
                .Entries<Entity>()
                .Where(p => p.State == EntityState.Added || p.State == EntityState.Modified)
                .Select(p => p.Entity).ToList();

            foreach(var entity in entities )
            {
                dispathcer.Dispatch(entity.Events);
                entity.ClearEvent();
            }
        }

        public DbSet<Person> People { get; set; }
    }
}