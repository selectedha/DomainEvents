using DomainEevent.Framework;
using DomainEvents.Core.Domain.Events;
using Newtonsoft.Json;

namespace DomainEvents.Core.ApplicationServices.People.EventHandler
{
    public class WriteFirstNameChangedToConsole : IDomainEventHandler<FirstNameChanged>
    {
        public Task Handle(FirstNameChanged domainEvent)
        {
            string firstnameChanged = JsonConvert.SerializeObject(domainEvent);
            Console.WriteLine(firstnameChanged);
            return Task.CompletedTask;
        }
    }
}
