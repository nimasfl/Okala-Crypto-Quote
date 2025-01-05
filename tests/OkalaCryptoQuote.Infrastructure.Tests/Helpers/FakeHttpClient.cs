using System.Net.Http.Json;
using System.Text.Json;
using FakeItEasy;

namespace OkalaCryptoQuote.Infrastructure.Tests.Helpers;

public class FakeHttpClient
{
    public readonly System.Net.Http.HttpClient HttpClient;
    private readonly FakeableHttpMessageHandler _httpHandler;

    public FakeHttpClient()
    {
        _httpHandler = A.Fake<FakeableHttpMessageHandler>();
        HttpClient = new System.Net.Http.HttpClient(_httpHandler);
    }

    public FakeHttpClient(string baseUrl) : this()
    {
        HttpClient.BaseAddress = new Uri(baseUrl);
    }

    public void MockHttpResponseTo<T>(T response)
    {
        var serializedResponse = new HttpResponseMessage
        {
            Content = new StringContent(JsonSerializer.Serialize(response))
        };

        A.CallTo(() => _httpHandler.FakeSendAsync(
                A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
            .Returns(serializedResponse);
    }

    public void MockHttpResponseToNull()
    {
        var serializedResponse = new HttpResponseMessage
        {
            Content = new StringContent("{}")
        };

        A.CallTo(() => _httpHandler.FakeSendAsync(
                A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
            .Returns(serializedResponse);
    }

    public void MockHttpToThrow<T>() where T : Exception, new()
    {
        A.CallTo(() => _httpHandler.FakeSendAsync(
                A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
            .Throws<T>();
    }
}
