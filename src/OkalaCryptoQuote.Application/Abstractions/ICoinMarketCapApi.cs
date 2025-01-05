using OkalaCryptoQuote.Domain.Features.CoinMarketCap;

namespace OkalaCryptoQuote.Application.Abstractions;

public interface ICoinMarketCapApi
{
    Task<Result<CryptoDetail>> GetCryptoDetail(string cryptoCode, CancellationToken ct = default);
}
