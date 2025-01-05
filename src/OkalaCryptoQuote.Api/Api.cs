using OkalaCryptoQuote.Api.Middlewares;
using OkalaCryptoQuote.Domain.Features.CoinMarketCap;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;

namespace OkalaCryptoQuote.Api;

public static class Api
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails()
            .ConfigureOption<ExchangeRatesOptions>(ExchangeRatesOptions.SectionName)
            .ConfigureOption<CoinMarketCapOptions>(CoinMarketCapOptions.SectionName)
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}
