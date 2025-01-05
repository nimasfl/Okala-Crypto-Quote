namespace OkalaCryptoQuote.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureOption<T>(this IServiceCollection services, string sectionName) where T : class
    {
        services.AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
