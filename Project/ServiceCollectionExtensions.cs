using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Shane32.EnphaseAPI;

public static class ServiceCollectionExtensions
{
    public static IHttpClientBuilder AddEnphaseClient(this IServiceCollection services,
        Action<EnphaseClientOptions>? configure = null)
    {
        if (configure != null)
            services.Configure<EnphaseClientOptions>(configure);
        else
            services.Configure<EnphaseClientOptions>(_ => { });

        services.TryAddSingleton(TimeProvider.System);
        return services.AddHttpClient<IEnphaseClient, EnphaseClient>(client => client.BaseAddress = new Uri("https://api.enphaseenergy.com"));
    }

    public static IHttpClientBuilder AddEnphaseClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EnphaseClientOptions>(configuration);
        services.TryAddSingleton(TimeProvider.System);
        return services.AddHttpClient<IEnphaseClient, EnphaseClient>(client => client.BaseAddress = new Uri("https://api.enphaseenergy.com"));
    }
}
