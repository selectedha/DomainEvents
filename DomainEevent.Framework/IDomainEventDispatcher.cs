using Microsoft.Extensions.DependencyInjection;

namespace DomainEevent.Framework
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IEnumerable<IDomainEvent> events);
    }

    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        public IServiceProvider _services { get; }

        public DomainEventDispatcher(IServiceProvider services)
        {
            _services = services;
        }

        public Task Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (dynamic @event in events)
            {
                DispatchEvent(@event);
            }
            return Task.CompletedTask;
        }

        public Task DispatchEvent<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            var handlers = _services.GetServices<IDomainEventHandler<TDomainEvent>>();
            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
            return Task.CompletedTask;
        }
    }
}
