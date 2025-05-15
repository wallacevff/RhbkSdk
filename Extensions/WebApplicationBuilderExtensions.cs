using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RhbkSdk.Interfaces;

namespace RhbkSdk.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddRhbkClient(this WebApplicationBuilder builder, string baseUrl, ServiceLifetime? lifetime = null)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                IRhbkClient clientApi = new Providers.RhbkClient(baseUrl);
                builder.Services.AddSingleton<IRhbkClient>(clientApi);
                break;
            case ServiceLifetime.Scoped:
                builder.Services.AddScoped<IRhbkClient, Providers.RhbkClient>(provider => new Providers.RhbkClient(baseUrl));
                break;
            case ServiceLifetime.Transient:
                builder.Services.AddTransient<IRhbkClient, Providers.RhbkClient>(provider => new Providers.RhbkClient(baseUrl));
                break;
            default:
                builder.Services.AddTransient<IRhbkClient, Providers.RhbkClient>(provider => new Providers.RhbkClient(baseUrl));
                break;
        }
        return builder;
    }
}