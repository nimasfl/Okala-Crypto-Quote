namespace OkalaCryptoQuote.Infrastructure.Tests.Helpers;

public abstract class FakeableHttpMessageHandler : HttpMessageHandler
{
    public abstract Task<HttpResponseMessage> FakeSendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken);

    protected sealed override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        => FakeSendAsync(request, cancellationToken);
}
