using System.Net;
using System.Threading;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Shane32.EnphaseAPI;
using Shouldly;

namespace Tests;

public class RetryTests
{
    private readonly Queue<HttpResponseMessage> _responses = new();
    private readonly FakeTimeProvider _fakeTimeProvider = new();

    private EnphaseClient CreateClient(int retryCount, TimeSpan? retryDelay = null, double? backoffMultiplier = null)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(() => Task.FromResult(_responses.Dequeue()));

        var httpClient = new HttpClient(handlerMock.Object) {
            BaseAddress = new Uri("https://api.enphaseenergy.com")
        };

        var options = new EnphaseClientOptions {
            ApiKey = "test-api-key",
            RetryCount = retryCount,
            RetryDelay = retryDelay ?? TimeSpan.Zero,
            RetryBackoffMultiplier = backoffMultiplier ?? 1.0,
        };

        var client = new EnphaseClient(httpClient, Options.Create(options), _fakeTimeProvider);
        client.AccessToken = "test-token";
        return client;
    }

    private static HttpResponseMessage JsonResponse(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new HttpResponseMessage(statusCode) {
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

    private static readonly string _rateLimitJson = @"{""message"":""Too Many Requests"",""period"":""minute"",""period_start"":1623825660,""period_end"":1623825720,""limit"":5}";
    private static readonly string _systemsJson = @"{""total"":1,""current_page"":1,""size"":10,""count"":1,""items"":""systems"",""systems"":[]}";
    private static readonly string _authErrorJson = @"{""message"":""Not Authorized"",""details"":""User is not authorized"",""code"":401}";

    [Fact]
    public async Task NoRetry_WhenRetryCountIsZeroAsync()
    {
        var client = CreateClient(retryCount: 0);
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));

        var ex = await Should.ThrowAsync<EnphaseRateLimitException>(() => client.GetSystemsAsync());
        ex.Message.ShouldBe("Too Many Requests");
        _responses.Count.ShouldBe(0);
        _fakeTimeProvider.RecordedDelays.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Retry_OnRateLimit_EventuallySucceedsAsync()
    {
        var client = CreateClient(retryCount: 2);
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_systemsJson));

        var result = await client.GetSystemsAsync();
        result.ShouldNotBeNull();
        _responses.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Retry_ExhaustedRetries_ThrowsLastExceptionAsync()
    {
        var client = CreateClient(retryCount: 2);
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));

        var ex = await Should.ThrowAsync<EnphaseRateLimitException>(() => client.GetSystemsAsync());
        ex.Message.ShouldBe("Too Many Requests");
        _responses.Count.ShouldBe(0);
    }

    [Fact]
    public async Task NoRetry_OnNonRetriableExceptionAsync()
    {
        var client = CreateClient(retryCount: 3);
        _responses.Enqueue(JsonResponse(_authErrorJson, HttpStatusCode.Unauthorized));

        var ex = await Should.ThrowAsync<EnphaseAuthenticationException>(() => client.GetSystemsAsync());
        ex.Message.ShouldBe("Not Authorized");
        _fakeTimeProvider.RecordedDelays.Count.ShouldBe(0);
    }

    [Fact]
    public async Task Retry_OnHttpRequestException_EventuallySucceedsAsync()
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        int callCount = 0;
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(() => {
                callCount++;
                if (callCount < 3)
                    throw new HttpRequestException("Connection refused");
                return Task.FromResult(JsonResponse(_systemsJson));
            });

        var httpClient = new HttpClient(handlerMock.Object) {
            BaseAddress = new Uri("https://api.enphaseenergy.com")
        };
        var options = Options.Create(new EnphaseClientOptions { ApiKey = "test-api-key", RetryCount = 3 });
        var client = new EnphaseClient(httpClient, options, _fakeTimeProvider);
        client.AccessToken = "test-token";

        var result = await client.GetSystemsAsync();
        result.ShouldNotBeNull();
        callCount.ShouldBe(3);
    }

    [Fact]
    public async Task Retry_WithDelay_RecordsCorrectDelaysAsync()
    {
        var client = CreateClient(retryCount: 2, retryDelay: TimeSpan.FromSeconds(1));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_systemsJson));

        await client.GetSystemsAsync();

        _fakeTimeProvider.RecordedDelays.Count.ShouldBe(2);
        _fakeTimeProvider.RecordedDelays[0].ShouldBe(TimeSpan.FromSeconds(1));
        _fakeTimeProvider.RecordedDelays[1].ShouldBe(TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task Retry_WithBackoff_AppliesMultiplierToDelayAsync()
    {
        var client = CreateClient(retryCount: 3, retryDelay: TimeSpan.FromSeconds(1), backoffMultiplier: 2.0);
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_systemsJson));

        await client.GetSystemsAsync();

        _fakeTimeProvider.RecordedDelays.Count.ShouldBe(3);
        _fakeTimeProvider.RecordedDelays[0].ShouldBe(TimeSpan.FromSeconds(1));
        _fakeTimeProvider.RecordedDelays[1].ShouldBe(TimeSpan.FromSeconds(2));
        _fakeTimeProvider.RecordedDelays[2].ShouldBe(TimeSpan.FromSeconds(4));
    }

    [Fact]
    public async Task Retry_WithZeroDelay_DoesNotCallTimeProviderCreateTimerAsync()
    {
        var client = CreateClient(retryCount: 2, retryDelay: TimeSpan.Zero);
        _responses.Enqueue(JsonResponse(_rateLimitJson, HttpStatusCode.TooManyRequests));
        _responses.Enqueue(JsonResponse(_systemsJson));

        await client.GetSystemsAsync();

        _fakeTimeProvider.RecordedDelays.Count.ShouldBe(0);
    }

    private sealed class FakeTimeProvider : TimeProvider
    {
        public List<TimeSpan> RecordedDelays { get; } = new();

        public override ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period)
        {
            RecordedDelays.Add(dueTime);
            callback(state);
            return new NopTimer();
        }

        private sealed class NopTimer : ITimer
        {
            public bool Change(TimeSpan dueTime, TimeSpan period) => true;
            public void Dispose() { }
            public ValueTask DisposeAsync() { Dispose(); return ValueTask.CompletedTask; }
        }
    }
}
