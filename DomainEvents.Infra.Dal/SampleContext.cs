using DomainEevent.Framework;
using DomainEvents.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http.Json;

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
            AddToOUtBox();
            DispatchEvents();
        }

        private void AddToOUtBox()
        {
            var entities = ChangeTracker
                .Entries<Entity>()
                .Where(p => p.State == EntityState.Added || p.State == EntityState.Modified)
                .Select(p => p.Entity).ToList();

            var dateTime = DateTime.Now;

            foreach (var entity in entities)
            {
                foreach(var @event in  entity.Events)
                {
                    OutBoxEventItem.Add(new OutBoxEventItem
                    {
                        EventId = Guid.NewGuid(),
                        AccuredByUserId = "1",
                        AccuredOn = dateTime,
                        AggregateId = "1",
                        AggregateName = entity.GetType().Name,
                        AggregateTypeName = entity.GetType().FullName,
                        EventName = @event.GetType().Name,
                        EventTypeName = @event.GetType().FullName,
                        EventPayLoad = JsonConvert.SerializeObject(@event),
                        IsProcessed = false
                    });
                }

            }

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

        public DbSet<OutBoxEventItem> OutBoxEventItem { get; set; }
    }
}