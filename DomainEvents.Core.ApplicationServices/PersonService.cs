using DomainEevent.Framework;
using DomainEvents.Core.Domain;
using DomainEvents.Infra.Dal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DomainEvents.Core.ApplicationServices
{
    public class PersonService
    {
        private readonly SampleContext _ctx;
        private readonly ILogger<PersonService> _logger;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public PersonService(SampleContext ctx, ILogger<PersonService> logger, IDomainEventDispatcher domainEventDispatcher)
        {
            _ctx = ctx;
            _logger = logger;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task AddPerson(string firstName, string lastName)
        {
            Person person = new Person(firstName, lastName);
            await _domainEventDispatcher.Dispatch(person.Events);
            await _ctx.SaveChangesAsync();
        }

        public async Task SetFirstName(long id, string firstName)
        {
            var person = _ctx.People.FirstOrDefault(x => x.Id == id);
            await _domainEventDispatcher.Dispatch(person.Events);
            person.ChangeFirstName(firstName);
            await _ctx.SaveChangesAsync();
        }

        public async Task SetLastName(long id, string lastName)
        {
            var person = _ctx.People.FirstOrDefault(x => x.Id == id);
            await _domainEventDispatcher.Dispatch(person.Events);
            person.ChangeLastName(lastName);
            await _ctx.SaveChangesAsync();

        }
    }
}