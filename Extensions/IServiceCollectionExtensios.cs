using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RhbkSdk.Interfaces;

namespace RhbkSdk.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRhbkClient(this IServiceCollection services, string baseUrl, ServiceLifetime? lifetime = null)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                IRhbkClient clientApi = new Providers.RhbkClient(baseUrl);
                services.AddSingleton<IRhbkClient>(clientApi);
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<IRhbkClient, Providers.RhbkClient>(provider => new Providers.RhbkClient(baseUrl));
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<IRhbkClient, Providers.RhbkClient>(provider => new Providers.RhbkClient(baseUrl));
                break;
            default:
                services.AddTransient<IRhbkClient, Providers.RhbkClient>(provider => new Providers.RhbkClient(baseUrl));
                break;
        }
        return services;
    }
}