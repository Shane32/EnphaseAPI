using System.Net;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Shane32.EnphaseAPI;
using Shouldly;

namespace Tests;

public abstract class TestBase : IDisposable
{
    private HttpRequestMessage? _capturedRequest;
    private string? _capturedRequestBody;
    private HttpResponseMessage? _response;
    protected IEnphaseClient Client { get; }
    private const string TEST_ACCESS_TOKEN = "test-token";
    private const string TEST_API_KEY = "test-api-key";

    protected TestBase(TimeProvider? timeProvider = null)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(async (HttpRequestMessage request, CancellationToken cancellationToken) => {
                _capturedRequest = request;
                _capturedRequestBody = request.Content != null
                    ? await request.Content.ReadAsStringAsync(cancellationToken)
                    : null;
                return _response!;
            });

        var httpClient = new HttpClient(handlerMock.Object) {
            BaseAddress = new Uri("https://api.enphaseenergy.com")
        };
        var options = Options.Create(new EnphaseClientOptions { ApiKey = TEST_API_KEY });
        Client = new EnphaseClient(httpClient, options, timeProvider ?? TimeProvider.System);
        Client.AccessToken = TEST_ACCESS_TOKEN;
    }

    public void Dispose()
    {
        _response?.Dispose();
        GC.SuppressFinalize(this);
    }

    protected void SetupJsonResponse(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        _response = new HttpResponseMessage(statusCode) {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };
    }

    protected void AssertRequest(string expectedPath, HttpMethod? expectedMethod = null)
    {
        _capturedRequest.ShouldNotBeNull();
        _capturedRequest!.RequestUri!.PathAndQuery.ShouldBe(expectedPath);
        if (expectedMethod != null)
            _capturedRequest.Method.ShouldBe(expectedMethod);
        _capturedRequest.Headers.Authorization!.Scheme.ShouldBe("Bearer");
        _capturedRequest.Headers.Authorization.Parameter.ShouldBe(TEST_ACCESS_TOKEN);
        _capturedRequest.Headers.TryGetValues("key", out var keyValues).ShouldBeTrue();
        keyValues!.First().ShouldBe(TEST_API_KEY);
    }

    protected Task<string> GetRequestBodyAsync()
    {
        return Task.FromResult(_capturedRequestBody ?? string.Empty);
    }
}
