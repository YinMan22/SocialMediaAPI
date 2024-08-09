using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common.Interfaces;
using SocialMedia.Infrastructure.Persistence;

namespace SocialMedia.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) 
        {
            string connectionString = configuration.GetConnectionString("SocialMediaDb");

            services.AddDbContextPool<SocialMediaDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.CommandTimeout(30);
                })
                .EnableDetailedErrors();
            });
            
            services.AddScoped<ISocialMediaDbContext>(provider => provider.GetRequiredService<SocialMediaDbContext>());

            return services;
        }
    }
}
