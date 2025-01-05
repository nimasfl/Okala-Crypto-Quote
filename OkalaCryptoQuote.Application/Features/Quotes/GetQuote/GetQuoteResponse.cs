namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public record GetQuoteResponse(string Slug, string Symbol, Dictionary<string, decimal?> Prices);
