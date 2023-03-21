using System.Collections.ObjectModel;
using Swashbuckle.AspNetCore.Annotations;
using WingsOn.Dal.Configuration;
using WingsOn.Domain.Configuration;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Interfaces;
using WingsOn.WebApi.Configuration;
using WingsOn.WebApi.Middleware;
using WingsOn.WebApi.Validators;

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

app.MapGet("flights/{number}/passengers", (string number, int? gender, IFlightService flightService) =>
    {
        var genderType = GenderValidator.TryParse(gender);
        
        return Results.Ok(flightService.GetPassengers(number, genderType));
    })
        .Produces<ReadOnlyCollection<Person>>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithMetadata(new SwaggerOperationAttribute(
            summary: "Endpoint that returns all passengers on the flight by a flight Id",
            description: "There is an optional filter by a gender"));

app.UseHttpsRedirection();

app.Run();