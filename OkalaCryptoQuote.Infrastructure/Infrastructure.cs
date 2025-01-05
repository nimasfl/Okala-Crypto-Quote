namespace OkalaCryptoQuote.Infrastructure;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IExchangeRatesApi, ExchangeRatesApi>((sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<ExchangeRatesOptions>>();
            var baseUrl = options.Value.BaseUrl;
            client.BaseAddress = new Uri(baseUrl);
        });

        services.AddHttpClient<ICoinMarketCapApi, CoinMarketCapApi>((sp, client) =>
        {
            const string AuthHeaderName = "X-CMC_PRO_API_KEY";
            var options = sp.GetRequiredService<IOptions<CoinMarketCapOptions>>();
            var baseUrl = options.Value.BaseUrl;
            var apiKey = options.Value.ApiKey;
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add(AuthHeaderName, apiKey);
        });

        return services;
    }
}
