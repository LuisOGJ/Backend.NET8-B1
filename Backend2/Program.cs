using Backend2.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Definición de implementación de interface, este registro indica a .net que implementación usar para la inyección de dependencias
//builder.Services.AddSingleton<IPeopleService, PeopleService>();

// con esto podemos espeficiar en cada controlador que implementación usar con la propierdad Key
// Disponible a partir de .NET 8
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("PeopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("People2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

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
