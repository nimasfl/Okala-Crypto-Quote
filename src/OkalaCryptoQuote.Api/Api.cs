using FluentValidation;
using OkalaCryptoQuote.Api.Middlewares;
using OkalaCryptoQuote.Domain.Features.CoinMarketCap;
using OkalaCryptoQuote.Domain.Features.ExchangeRates;

namespace OkalaCryptoQuote.Api;

public static class Api
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddValidatorsFromAssembly(Application.Application.Assembly)
            .AddProblemDetails()
            .ConfigureOption<ExchangeRatesOptions>(ExchangeRatesOptions.SectionName)
            .ConfigureOption<CoinMarketCapOptions>(CoinMarketCapOptions.SectionName)
            .AddEndpointsApiExplorer()
            .AddEndpoints()
            .AddSwaggerGen();

        return services;
    }


}
