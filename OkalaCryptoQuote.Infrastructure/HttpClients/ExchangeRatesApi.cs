using Microsoft.Extensions.Options;

namespace OkalaCryptoQuote.Infrastructure.HttpClients;

public sealed class ExchangeRatesApi(HttpClient httpClient, IOptions<ExchangeRatesOptions> options) : IExchangeRatesApi
{
    public async Task<Result<LatestExchangeRateResponse>> GetLatestRates()
    {
        try
        {
            var requestUrl = GenerateUrl("/latest?&symbols=USD,EUR,BRL,GBP,AUD");
            var result = await httpClient.GetFromJsonAsync<LatestExchangeRateResponse>(requestUrl);
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
