using AutoGlass.WebApi.Configurations;
using AutoGlass.WebApi.Configurations.Swagger;
using Microsoft.AspNetCore.OData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddOData(opt =>
    {
        opt.Select().Count().Filter().OrderBy().SetMaxTop(100).SkipToken().Expand();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Resolve dependencies
builder.Services.AddDependencyInjectionConfiguration();

// Adding SQLServer database configuration
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Adding AutoMapper configuration
builder.Services.AddAutoMapperConfiguration();

var app = builder.Build();

app.UseCors(c =>
        {
            c.WithHeaders("*");
            c.WithOrigins("*");
            c.WithMethods("*");
        });

app.UseSwaggerSetup();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
