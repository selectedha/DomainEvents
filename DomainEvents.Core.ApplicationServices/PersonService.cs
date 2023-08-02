using DomainEvents.Core.Domain;
using DomainEvents.Infra.Dal;
using Microsoft.Extensions.Logging;

namespace DomainEvents.Core.ApplicationServices
{
    public class PersonService
    {
        private readonly SampleContext _ctx;
        private readonly ILogger<PersonService> _logger;

        public PersonService(SampleContext ctx, ILogger<PersonService> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task AddPerson(string firstName, string lastName)
        {
            Person person = new Person(firstName, lastName);
            _ctx.Add(person);
            foreach (var item in person.Events)
            {
                _logger.LogInformation("Event is {EventType}", item.GetType().FullName);
            }
            await _ctx.SaveChangesAsync();
        }
        public async Task SetFirstName(long id, string firstName)
        {
            var person = _ctx.People.FirstOrDefault(x => x.Id == id);
            person.ChangeFirstName(firstName);
            foreach (var item in person.Events)
            {
                _logger.LogInformation("Event is {EventType}", item.GetType().FullName);
            }
            await _ctx.SaveChangesAsync();
        }
        public async Task SetLastName(long id, string lastName)
        {
            var person = _ctx.People.FirstOrDefault(x => x.Id == id);
            person.ChangeLastName(lastName);
            foreach (var item in person.Events)
            {
                _logger.LogInformation("Event is {EventType}", item.GetType().FullName);
            }
            await _ctx.SaveChangesAsync();

        }
    }
}