namespace OkalaCryptoQuote.Infrastructure.HttpClients;

public sealed class ExchangeRatesApi(HttpClient httpClient, IOptions<ExchangeRatesOptions> options) : IExchangeRatesApi
{
    public async Task<Result<LatestExchangeRateResponse>> GetLatestRates(CancellationToken ct = default)
    {
        var currencies = new List<string> { options.Value.BaseCurrency };
        currencies.AddRange(options.Value.Currencies.Split(','));
        var currenciesString = string.Join(',', currencies);
        try
        {
            var requestUrl = GenerateUrl($"/latest?&symbols={currenciesString}");
            var result = await httpClient.GetFromJsonAsync<LatestExchangeRateResponse>(requestUrl, ct);
            if (result is null || result.Success == false)
            {
                return ExchangeRatesError.InvalidFormatResult;
            }

            return result;
        }
        catch (Exception)
        {
            return ExchangeRatesError.ServiceUnavailable;
        }
    }

    private string GenerateUrl(string requestUri)
    {
        var accessKey = options.Value.AccessKey;
        if (requestUri.Contains('?'))
        {
            return $"{requestUri}&access_key={accessKey}";
        }

        return $"{requestUri}?access_key={accessKey}";
    }
}
