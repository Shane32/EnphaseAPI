using System.Net;
using System.Text.Json;
using Shane32.EnphaseAPI;
using Shouldly;

namespace Tests;

public class ErrorTests : TestBase
{
    [Fact]
    public async Task Returns_401_AuthenticationExceptionAsync()
    {
        SetupJsonResponse(@"{""message"":""Not Authorized"",""details"":""User is not authorized"",""code"":401}", HttpStatusCode.Unauthorized);
        var ex = await Should.ThrowAsync<EnphaseAuthenticationException>(() => Client.GetSystemsAsync());
        ex.Message.ShouldBe("Not Authorized");
        ex.Details.ShouldBe("User is not authorized");
        ex.HttpStatusCode.ShouldBe(401);
    }

    [Fact]
    public async Task Returns_403_ForbiddenExceptionAsync()
    {
        SetupJsonResponse(@"{""message"":""Forbidden"",""details"":""Not authorized"",""code"":403}", HttpStatusCode.Forbidden);
        var ex = await Should.ThrowAsync<EnphaseForbiddenException>(() => Client.GetSystemsAsync());
        ex.Message.ShouldBe("Forbidden");
        ex.HttpStatusCode.ShouldBe(403);
    }

    [Fact]
    public async Task Returns_404_NotFoundExceptionAsync()
    {
        SetupJsonResponse(@"{""message"":""Not Found"",""details"":""System not found"",""code"":404}", HttpStatusCode.NotFound);
        var ex = await Should.ThrowAsync<EnphaseNotFoundException>(() => Client.GetSystemAsync(999));
        ex.Message.ShouldBe("Not Found");
        ex.HttpStatusCode.ShouldBe(404);
    }

    [Fact]
    public async Task Returns_405_MethodNotAllowedExceptionAsync()
    {
        SetupJsonResponse(@"{""reason"":""405"",""message"":[""Method not allowed""]}", HttpStatusCode.MethodNotAllowed);
        var ex = await Should.ThrowAsync<EnphaseMethodNotAllowedException>(() => Client.GetSystemsAsync());
        ex.Message.ShouldBe("Method not allowed");
        ex.HttpStatusCode.ShouldBe(405);
    }

    [Fact]
    public async Task Returns_422_UnprocessableExceptionAsync()
    {
        SetupJsonResponse(@"{""message"":""Unprocessable Entity"",""details"":""Invalid request"",""code"":422}", HttpStatusCode.UnprocessableEntity);
        var ex = await Should.ThrowAsync<EnphaseUnprocessableException>(() => Client.GetSystemsAsync());
        ex.Message.ShouldBe("Unprocessable Entity");
        ex.HttpStatusCode.ShouldBe(422);
    }

    [Fact]
    public async Task Returns_429_RateLimitExceptionAsync()
    {
        SetupJsonResponse(@"{""message"":""Too Many Requests"",""details"":""Usage limit exceeded for plan Kilowatt"",""code"":429,""period"":""minute"",""period_start"":1623825660,""period_end"":1623825720,""limit"":5}", HttpStatusCode.TooManyRequests);
        var ex = await Should.ThrowAsync<EnphaseRateLimitException>(() => Client.GetSystemsAsync());
        ex.Message.ShouldBe("Too Many Requests");
        ex.Details.ShouldBe("Usage limit exceeded for plan Kilowatt");
        ex.Period.ShouldBe("minute");
        ex.PeriodStart.ShouldBe(1623825660L);
        ex.PeriodEnd.ShouldBe(1623825720L);
        ex.Limit.ShouldBe(5);
        ex.HttpStatusCode.ShouldBe(429);
    }

    [Fact]
    public async Task Returns_500_ServerExceptionAsync()
    {
        SetupJsonResponse(@"{""message"":""Internal Server Error"",""details"":""unable to fetch data"",""code"":500}", HttpStatusCode.InternalServerError);
        var ex = await Should.ThrowAsync<EnphaseServerException>(() => Client.GetSystemsAsync());
        ex.HttpStatusCode.ShouldBe(500);
    }

    [Fact]
    public async Task Returns_500_RgmFormatAsync()
    {
        SetupJsonResponse(@"{""errorCode"":7,""errorMessages"":[""Data is temporarily unavailable""]}", HttpStatusCode.InternalServerError);
        var ex = await Should.ThrowAsync<EnphaseServerException>(() => Client.GetRgmStatsAsync(123));
        ex.Message.ShouldBe("Data is temporarily unavailable");
        ex.HttpStatusCode.ShouldBe(500);
    }

    [Fact]
    public async Task Returns_400_BadRequestException_EvChargerStyleAsync()
    {
        SetupJsonResponse(@"{""message"":""Bad request"",""code"":""400"",""details"":""Invalid system_id""}", HttpStatusCode.BadRequest);
        var ex = await Should.ThrowAsync<EnphaseBadRequestException>(() => Client.GetEvChargerDevicesAsync(0));
        ex.Message.ShouldBe("Bad request");
        ex.Details.ShouldBe("Invalid system_id");
        ex.HttpStatusCode.ShouldBe(400);
    }

    [Fact]
    public async Task Returns_501_NotImplementedExceptionAsync()
    {
        SetupJsonResponse(@"{""reason"":""501"",""message"":[""Not Implemented""]}", HttpStatusCode.NotImplemented);
        var ex = await Should.ThrowAsync<EnphaseNotImplementedException>(() => Client.GetSystemsAsync());
        ex.Message.ShouldBe("Not Implemented");
        ex.HttpStatusCode.ShouldBe(501);
    }

    [Fact]
    public async Task UnmappedMember_ThrowsJsonExceptionAsync()
    {
        SetupJsonResponse(@"{""system_id"":72,""name"":""Test"",""status"":""normal"",""unknown_field"":""value""}");
        await Should.ThrowAsync<JsonException>(() => Client.GetSystemAsync(72));
    }
}
