using DomainEevent.Framework;
using DomainEvents.Core.Domain.Events;
using Newtonsoft.Json;

namespace DomainEvents.Core.ApplicationServices.People.EventHandler
{
    public class WritePerssonCreatedToFile : IDomainEventHandler<PersonCreated>
    {
        public Task Handle(PersonCreated domainEvent)
        {
            string personCreatedstring = JsonConvert.SerializeObject(domainEvent);
            System.IO.File.WriteAllText("d:\\personCreated.txt", personCreatedstring);
            return Task.CompletedTask;
        }
    }
}
