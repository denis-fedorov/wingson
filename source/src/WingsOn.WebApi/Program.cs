using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WingsOn.Dal.Configuration;
using WingsOn.Domain.Configuration;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Interfaces;
using WingsOn.WebApi.Configuration;
using WingsOn.WebApi.Middleware;
using WingsOn.WebApi.Models;
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

app.MapPost("flights/{number}/passengers", 
    (string number, [FromBody]AddPassengerModel passengerModel, IFlightService flightService) =>
    {
        AddPassengerModelValidator.Validate(passengerModel);
        
        var newBookingNumber = flightService.CreateBookingOnFlight(number, passengerModel.PersonId, passengerModel.BookingTime);
        
        return Results.Created("bookings",newBookingNumber);
    })
    .Produces<string>()
    .Produces(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithMetadata(new SwaggerOperationAttribute(
        summary: "Endpoint that creates a booking of an existing flight for a new passenger"));

app.MapPut("flights/{number}/passengers/{id:int}/address", 
    (string number, int id, [FromBody]UpdateAddressModel addressModel, IFlightService flightService, IPersonService personService) =>
    {
        UpdateAddressModelValidator.Validate(addressModel);
        
        flightService.ValidatePassengerForFlight(number, id);
        personService.UpdateAddress(id, addressModel.Address);

        return Results.NoContent();
    })
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithMetadata(new SwaggerOperationAttribute(summary: "Endpoint that updates passenger’s address"));


app.UseHttpsRedirection();

app.Run();