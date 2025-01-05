using FakeItEasy;
using OkalaCryptoQuote.Api.Controllers;
using OkalaCryptoQuote.Application.Features.Quotes.GetQuote;
using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Api.Tests.Controllers;

public class QuotesControllerTests
{
    [Fact]
    public async Task GetQuote_ValidRequest_ReturnsSuccessResult()
    {
        var getQuoteHandler = A.Fake<IGetQuoteHandler>();
        var request = new GetQuoteRequest("BTC");
        var response = new GetQuoteResponse("bitcoin", "BTC", new Dictionary<string, decimal?> { { "USD", 50000m } });

        A.CallTo(() => getQuoteHandler.GetQuote(request, A<CancellationToken>.Ignored))
            .Returns(response);

        var controller = new QuotesController(getQuoteHandler);

        var result = await controller.GetQuote(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(response, result.Value);
    }

    [Fact]
    public async Task GetQuote_HandlerReturnsFailure_ReturnsErrorResult()
    {
        var getQuoteHandler = A.Fake<IGetQuoteHandler>();
        var request = new GetQuoteRequest("BTC");
        var error = Error.Problem("test", "test description");

        A.CallTo(() => getQuoteHandler.GetQuote(request, A<CancellationToken>.Ignored))
            .Returns(error);

        var controller = new QuotesController(getQuoteHandler);

        var result = await controller.GetQuote(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.Equal(error.Description, result.Error.Description);
        Assert.Equal(error.Type, result.Error.Type);
    }

    [Fact]
    public async Task GetQuote_HandlerThrowsException_ReturnsException()
    {
        var getQuoteHandler = A.Fake<IGetQuoteHandler>();
        var request = new GetQuoteRequest("BTC");

        A.CallTo(() => getQuoteHandler.GetQuote(request, A<CancellationToken>.Ignored))
            .ThrowsAsync(new Exception("Unexpected error"));

        var controller = new QuotesController(getQuoteHandler);

        var exception = await Assert.ThrowsAsync<Exception>(() => controller.GetQuote(request, CancellationToken.None));
        Assert.Equal("Unexpected error", exception.Message);
    }
}
