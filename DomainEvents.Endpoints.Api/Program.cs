using DomainEevent.Framework;
using DomainEvents.Core.ApplicationServices;
using DomainEvents.Core.ApplicationServices.People.EventHandler;
using DomainEvents.Core.Domain.Events;
using DomainEvents.Infra.Dal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SampleContext>(c => c.UseSqlServer("TrustServerCertificate=True;Password=h0t@@TL7X5At;User ID=ha;Initial Catalog=DomainEvents;Data Source=db.avang.ir\\devops"));
builder.Services.AddScoped<PersonService>();
builder.Services.AddTransient<IDomainEventHandler<PersonCreated>, WritePerssonCreatedToConsole>();
builder.Services.AddTransient<IDomainEventHandler<PersonCreated>, WritePerssonCreatedToFile>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
