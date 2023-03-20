using System.Collections.ObjectModel;
using Swashbuckle.AspNetCore.Annotations;
using WingsOn.Dal.Configuration;
using WingsOn.Domain.Configuration;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Interfaces;
using WingsOn.WebApi.Configuration;
using WingsOn.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .ConfigureWebApiServices()
    .ConfigureDal()
    .ConfigureDomain();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGet("persons/{id:int}", (int id, IPersonService personService) 
    => Results.Ok(personService.Get(id)))
        .Produces<Person>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithMetadata(new SwaggerOperationAttribute(summary: "Endpoint that returns a Person by a person Id"));

app.MapGet("flights/{number}/passengers", (string number, IFlightService flightService) 
    => Results.Ok(flightService.GetPassengers(number)))
        .Produces<ReadOnlyCollection<Person>>()
        .Produces(StatusCodes.Status200OK)
        .WithMetadata(new SwaggerOperationAttribute(summary: "Endpoint that returns all passengers on the flight by a flight Id"));

app.UseHttpsRedirection();

app.Run();