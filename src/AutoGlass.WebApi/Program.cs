using AutoGlass.Infra.CrossCutting.IoC;
using AutoGlass.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Resolve dependencies
builder.Services.AddDependencyInjectionConfiguration();

// Adding SQLServer database configuration
builder.Services.AddDatabaseConfiguration(builder.Configuration);

var app = builder.Build();

app.UseCors(c =>
        {
            c.WithHeaders("*");
            c.WithOrigins("*");
            c.WithMethods("*");
        });

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
