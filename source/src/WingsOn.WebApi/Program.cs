using WingsOn.Dal.Configuration;
using WingsOn.WebApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .ConfigureWebApiServices()
    .ConfigureDal();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("persons/{id:int}", (int id) =>
{
    return Results.Ok(id);
});

app.UseHttpsRedirection();

app.Run();