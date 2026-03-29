using System;
using Microsoft.Extensions.DependencyInjection;

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

        return services.AddHttpClient<IEnphaseClient, EnphaseClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.enphaseenergy.com");
        });
    }
}
