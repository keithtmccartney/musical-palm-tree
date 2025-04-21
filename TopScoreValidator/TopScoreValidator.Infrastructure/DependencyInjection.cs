using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopScoreValidator.Domain.Interfaces;
using TopScoreValidator.Infrastructure.Data;
using TopScoreValidator.Infrastructure.Repositories;

namespace TopScoreValidator.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IValidWordRepository, ValidWordRepository>();

        return services;
    }

    public static IHost InitialiseDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.EnsureCreated();
        }

        return host;
    }
}
