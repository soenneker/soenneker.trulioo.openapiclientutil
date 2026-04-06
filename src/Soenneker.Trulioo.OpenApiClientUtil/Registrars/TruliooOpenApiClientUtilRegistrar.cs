using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Trulioo.HttpClients.Registrars;
using Soenneker.Trulioo.OpenApiClientUtil.Abstract;

namespace Soenneker.Trulioo.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class TruliooOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="TruliooOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddTruliooOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddTruliooOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ITruliooOpenApiClientUtil, TruliooOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="TruliooOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddTruliooOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddTruliooOpenApiHttpClientAsSingleton()
                .TryAddScoped<ITruliooOpenApiClientUtil, TruliooOpenApiClientUtil>();

        return services;
    }
}
