using Backend2.Automappers;
using Backend2.DTOs;
using Backend2.DTOs.BrandDTOs;
using Backend2.Models;
using Backend2.Repository;
using Backend2.Services;
using Backend2.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddScoped<IPostsService, PostsService>();

builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");
builder.Services.AddKeyedScoped<ICommonService<BrandDTO, BrandInsertDTOs, BrandUpdateDto>, BrandService>("brandService");

// Implemented Repositories
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();
builder.Services.AddScoped<IRepository<Brand>, BrandRepository>();


// Inyección de HttpClient servicio a jsonplaceholder
builder.Services.AddHttpClient<IPostsService, PostsService>( c => {
    // con bluilder.Configuration accedemos a la varibles de appsettings.json
    c.BaseAddress = new Uri(builder.Configuration["baseUrlPosts"]);
});

// Configuración de EntityFramework
builder.Services.AddDbContext<StoreContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});


// Validadores
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

builder.Services.AddScoped<IValidator<BrandInsertDTOs>, BrandInserValidator>();
builder.Services.AddScoped<IValidator<BrandUpdateDto>, BrandUpdateValidator>();


// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));


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
