using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Shane32.EnphaseAPI;

/// <summary>
/// Extension methods for registering Enphase API services with <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="IEnphaseClient"/> and its <see cref="HttpClient"/> with the dependency injection container,
    /// optionally configuring <see cref="EnphaseClientOptions"/> via the provided delegate.
    /// </summary>
    /// <param name="services">The service collection to add the client to.</param>
    /// <param name="configure">An optional delegate to configure <see cref="EnphaseClientOptions"/>.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for further configuration of the underlying <see cref="HttpClient"/>.</returns>
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

    /// <summary>
    /// Registers <see cref="IEnphaseClient"/> and its <see cref="HttpClient"/> with the dependency injection container,
    /// binding <see cref="EnphaseClientOptions"/> from the supplied <see cref="IConfiguration"/> section.
    /// </summary>
    /// <param name="services">The service collection to add the client to.</param>
    /// <param name="configuration">A configuration section containing <see cref="EnphaseClientOptions"/> values.</param>
    /// <returns>An <see cref="IHttpClientBuilder"/> for further configuration of the underlying <see cref="HttpClient"/>.</returns>
    public static IHttpClientBuilder AddEnphaseClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<EnphaseClientOptions>(configuration);
        services.TryAddSingleton(TimeProvider.System);
        return services.AddHttpClient<IEnphaseClient, EnphaseClient>(client => client.BaseAddress = new Uri("https://api.enphaseenergy.com"));
    }
}
