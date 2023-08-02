using DomainEevent.Framework;

namespace DomainEvents.Core.Domain.Events
{
    public class FirstNameChanged: IDomainEvent
    {
        public long Id { get; }
        public string FirstName { get; }
        public FirstNameChanged(long id, string firstName)
        {
            Id = id;
            FirstName = firstName;
        }        
    }
}
