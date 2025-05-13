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
                IClientApi clientApi = new Providers.ClientApi(baseUrl);
                builder.Services.AddSingleton<IClientApi>(clientApi);
                break;
            case ServiceLifetime.Scoped:
                builder.Services.AddScoped<IClientApi, Providers.ClientApi>(provider => new Providers.ClientApi(baseUrl));
                break;
            case ServiceLifetime.Transient:
                builder.Services.AddTransient<IClientApi, Providers.ClientApi>(provider => new Providers.ClientApi(baseUrl));
                break;
            default:
                builder.Services.AddTransient<IClientApi, Providers.ClientApi>(provider => new Providers.ClientApi(baseUrl));
                break;
        }
        return builder;
    }
}