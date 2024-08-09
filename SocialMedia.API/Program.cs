using SocialMedia.Infrastructure;
using SocialMedia.Application;
using Microsoft.OpenApi.Models;
using SocialMedia.API.Common.Middlewares;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options=>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "SocialMedia.Api",
            Version = "v1"
        });
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme 
        {
            Name = "Authorization",
            Description = @"Enter the bearer token in format 'Bearer TOKEN'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new String[]{ }
            }
        });
    })
    .AddApplication(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddHttpContextAccessor();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog();

var app = builder.Build();

app.UseAuthentication();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialMedia.Api v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
