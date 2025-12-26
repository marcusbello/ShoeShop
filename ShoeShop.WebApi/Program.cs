using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ShoeShop.DataContext;
using ShoeShop.WebApi.Mappings;
using ShoeShop.WebApi.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Resolve Postgres connection string: prefer environment variable `POSTGRES_CONNECTIONSTRING` or `DATABASE_URL` if set; otherwise fallback to configuration.
var pgConnection = Environment.GetEnvironmentVariable("POSTGRES_CONNECTIONSTRING") ?? Environment.GetEnvironmentVariable("DATABASE_URL");
if (string.IsNullOrWhiteSpace(pgConnection))
    // Fallback to configuration (appsettings)
    pgConnection = builder.Configuration.GetConnectionString("Postgres");

builder.Services.AddDbContext<ShoeshopDbContext>(options =>
    options.UseNpgsql(pgConnection));
// Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ShoeShop API",
        Description = "An ASP.NET Core Web API for managing ShoeShop Store",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Marcus Bello",
            Url = new Uri("https://github.com/marcusbello")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
    "ShoeShop API Version 1");
    c.RoutePrefix = "swagger";
    c.SupportedSubmitMethods(new[] {
        SubmitMethod.Get, SubmitMethod.Post,
        SubmitMethod.Put, SubmitMethod.Delete
        });
});
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
