using DomainEvents.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DomainEvents.Infra.Dal
{
    public class SampleContext:DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {

        }
        public DbSet<Person> People { get; set; }
    }
}