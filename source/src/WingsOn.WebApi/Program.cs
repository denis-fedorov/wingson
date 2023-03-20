using Swashbuckle.AspNetCore.Annotations;
using WingsOn.Dal.Configuration;
using WingsOn.Domain.Configuration;
using WingsOn.Domain.Entities;
using WingsOn.Domain.Interfaces;
using WingsOn.WebApi.Configuration;

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

app.MapGet("persons/{id:int}", (int id, IPersonService personService) 
    => Results.Ok(personService.Get(id)))
        .Produces<Person>()
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithMetadata(new SwaggerOperationAttribute(summary: "Endpoint that returns a Person by Id"));

app.UseHttpsRedirection();

app.Run();