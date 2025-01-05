namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public interface IGetQuoteHandler
{
    public Task<Result<GetQuoteResponse>> GetQuote(GetQuoteRequest request, CancellationToken cancellationToken);
}
