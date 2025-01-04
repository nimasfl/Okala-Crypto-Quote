namespace OkalaCryptoQuote.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapGroupEndpoints(app);
        }

        return app;
    }
}
