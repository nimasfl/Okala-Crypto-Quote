using Microsoft.AspNetCore.Mvc;
using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Api.Endpoints;

public class QuotesEndpoints : IEndpoint
{
    public void MapGroupEndpoints(IEndpointRouteBuilder app)
    {
        var routes = app
            .MapGroup(Tags.Quotes)
            .WithTags(Tags.Quotes);

        routes.MapGet("/", GetQuote).AddEndpointFilter(async (context, next) =>
        {
            try
            {
                return await next(context);
            }
            catch (BadHttpRequestException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        });
    }

    private static Task<Result<GetQuoteResponse>> GetQuote([AsParameters] GetQuoteRequest request,
        IGetQuoteHandler getQuoteHandler, CancellationToken ct)
    {
        return getQuoteHandler.GetQuote(request, ct);
    }
}
