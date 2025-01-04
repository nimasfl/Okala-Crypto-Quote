using OkalaCryptoQuote.Domain.Features.CoinMarketCap;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;

namespace OkalaCryptoQuote.Api;

public static class Api
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .ConfigureOption<ExchangeRatesOptions>(ExchangeRatesOptions.SectionName)
            .ConfigureOption<CoinMarketCapOptions>(CoinMarketCapOptions.SectionName)
            .AddEndpointsApiExplorer()
            .AddEndpoints()
            .AddSwaggerGen();

        return services;
    }


}
