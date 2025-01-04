using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Api.Endpoints;

public class QuotesEndpoints: IEndpoint
{
    public void MapGroupEndpoints(IEndpointRouteBuilder app)
    {
        var routes = app
            .MapGroup(Tags.Quotes)
            .WithTags(Tags.Quotes);

        routes.MapGet("/", GetQuote);
    }

    private static Result<GetQuoteResponse> GetQuote(IGetQuoteHandler getQuoteHandler,CancellationToken ct)
    {
        return getQuoteHandler.GetQuote(new GetQuoteRequest("BTC"), ct);
    }
}
