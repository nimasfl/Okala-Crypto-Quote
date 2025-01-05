namespace OkalaCryptoQuote.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var serviceDescriptors = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IServiceCollection ConfigureOption<T>(this IServiceCollection services, string sectionName) where T : class
    {
        services.AddOptions<T>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}
