using Microsoft.AspNetCore.Mvc;
using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class QuotesController(IGetQuoteHandler GetQuoteHandler) : ControllerBase
{

    [HttpGet]
    public Task<Result<GetQuoteResponse>> GetQuote([FromQuery] GetQuoteRequest request, CancellationToken ct)
        => GetQuoteHandler.GetQuote(request, ct);
}
