using OkalaCryptoQuote.Application.Features.Quotes;
using OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

namespace OkalaCryptoQuote.Application;

public static class Application
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGetQuoteHandler, GetQuoteHandler>();

        return services;
    }
}
