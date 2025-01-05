using System.Net.Mime;
using System.Reflection;
using OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

namespace OkalaCryptoQuote.Application;

public static class Application
{
    public static readonly Assembly Assembly = typeof(Application).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetQuoteHandler, GetQuoteHandler>();

        return services;
    }
}
