using DomainEevent.Framework;
using DomainEvents.Core.Domain.Events;

namespace DomainEvents.Core.Domain
{
    public class Person:Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Person(string firstName, string lastName)
        {
            if(string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if(string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            AddEvent(new PersonCreated(firstName, lastName));
        }

        public void ChangeFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));
            FirstName = firstName;
            AddEvent(new FirstNameChanged(Id, firstName));
        }

        public void ChangeLastName(string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException(nameof(lastName));
            LastName = lastName;
            AddEvent(new LastNameChanged(Id, lastName));
        }

    }
}