using Microsoft.Extensions.DependencyInjection;
using TopScoreValidator.Application.Services;
using TopScoreValidator.Domain.Interfaces;

namespace TopScoreValidator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidatorService, ValidatorService>();

        services.AddScoped<IWordService, WordService>();

        return services;
    }
}
