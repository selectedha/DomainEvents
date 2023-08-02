using DomainEevent.Framework;

namespace DomainEvents.Core.Domain.Events
{
    public class LastNameChanged : IDomainEvent
    {
        public long Id { get; }
        public string LastName { get; }
        public LastNameChanged(long id, string lastName)
        {
            Id = id;
            LastName = lastName;
        }
            
    }
}
