using Microsoft.AspNetCore.Mvc;

namespace OkalaCryptoQuote.Application.Features.Quotes.GetQuote;

public record GetQuoteRequest([FromQuery] string CryptoCode);
