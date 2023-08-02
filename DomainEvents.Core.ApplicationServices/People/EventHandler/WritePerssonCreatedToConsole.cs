using DomainEevent.Framework;
using DomainEvents.Core.Domain.Events;
using Newtonsoft.Json;

namespace DomainEvents.Core.ApplicationServices.People.EventHandler
{
    public class WritePerssonCreatedToConsole : IDomainEventHandler<PersonCreated>
    {
        public Task Handle(PersonCreated domainEvent)
        {
            string personCreatedstring = JsonConvert.SerializeObject(domainEvent);
            Console.WriteLine(personCreatedstring);
            return Task.CompletedTask;
        }
    }
}
