using Shane32.EnphaseAPI.Models;
using Shouldly;

namespace Tests.Systems;

public class SystemsTests : TestBase
{
    [Fact]
    public async Task GetSystems_NoParamsAsync()
    {
        SetupJsonResponse(@"{""total"":28,""current_page"":1,""size"":10,""count"":2,""items"":""systems"",""systems"":[]}");
        var result = await Client.GetSystemsAsync();
        result.Total.ShouldBe(28);
        result.Systems.ShouldNotBeNull();
        AssertRequest("/api/v4/systems");
    }

    [Fact]
    public async Task GetSystems_WithParamsAsync()
    {
        SetupJsonResponse(@"{""total"":28,""current_page"":2,""size"":5,""count"":5,""items"":""systems"",""systems"":[]}");
        var result = await Client.GetSystemsAsync(page: 2, size: 5, sortBy: "-id");
        result.CurrentPage.ShouldBe(2);
        AssertRequest("/api/v4/systems?page=2&size=5&sort_by=-id");
    }

    [Fact]
    public async Task SearchSystemsAsync()
    {
        SetupJsonResponse(@"{""total"":1,""current_page"":1,""size"":10,""count"":1,""items"":""systems"",""systems"":[]}");
        var request = new SearchSystemsRequest { System = new SearchSystemsFilter { Statuses = new List<string> { "normal" } } };
        var result = await Client.SearchSystemsAsync(request);
        result.Total.ShouldBe(1);
        AssertRequest("/api/v4/systems/search", HttpMethod.Post);
    }

    [Fact]
    public async Task GetSystemAsync()
    {
        SetupJsonResponse(@"{""system_id"":72,""name"":""Test"",""status"":""normal""}");
        var result = await Client.GetSystemAsync(72);
        result.SystemId.ShouldBe(72);
        result.Name.ShouldBe("Test");
        AssertRequest("/api/v4/systems/72");
    }

    [Fact]
    public async Task GetSystemSummaryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698910067,""current_power"":0,""energy_lifetime"":0,""energy_today"":0}");
        var result = await Client.GetSystemSummaryAsync(698910067);
        result.SystemId.ShouldBe(698910067);
        AssertRequest("/api/v4/systems/698910067/summary");
    }

    [Fact]
    public async Task GetSystemSummary_WithDateAsync()
    {
        SetupJsonResponse(@"{""system_id"":698910067,""current_power"":0,""energy_lifetime"":0,""energy_today"":0}");
        await Client.GetSystemSummaryAsync(698910067, summaryDate: new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero));
        AssertRequest("/api/v4/systems/698910067/summary?summary_date=2024-01-01");
    }

    [Fact]
    public async Task GetSystemDevicesAsync()
    {
        SetupJsonResponse(@"{""system_id"":698910067,""total_devices"":11,""items"":""devices"",""devices"":{""micros"":[],""meters"":[],""gateways"":[],""q_relays"":[],""acbs"":[],""encharges"":[],""enpowers"":[],""ev_chargers"":[],""iq_collars"":[]}}");
        var result = await Client.GetSystemDevicesAsync(698910067);
        result.SystemId.ShouldBe(698910067);
        result.Devices.ShouldNotBeNull();
        AssertRequest("/api/v4/systems/698910067/devices");
    }

    [Fact]
    public async Task RetrieveSystemIdAsync()
    {
        SetupJsonResponse(@"{""system_id"":123}");
        var result = await Client.RetrieveSystemIdAsync("SN123456");
        result.SystemId.ShouldBe(123);
        AssertRequest("/api/v4/systems/retrieve_system_id?serial_num=SN123456");
    }

    [Fact]
    public async Task GetSystemEventsAsync()
    {
        SetupJsonResponse(@"{""events"":[],""system_id"":701644354}");
        var result = await Client.GetSystemEventsAsync(701644354, DateTimeOffset.FromUnixTimeSeconds(1740213328));
        result.SystemId.ShouldBe(701644354);
        AssertRequest("/api/v4/systems/701644354/events?start_time=1740213328");
    }

    [Fact]
    public async Task GetSystemAlarmsAsync()
    {
        SetupJsonResponse(@"{""alarms"":[],""system_id"":701644354}");
        var result = await Client.GetSystemAlarmsAsync(701644354, DateTimeOffset.FromUnixTimeSeconds(1740213328));
        result.SystemId.ShouldBe(701644354);
        AssertRequest("/api/v4/systems/701644354/alarms?start_time=1740213328");
    }

    [Fact]
    public async Task GetEventTypesAsync()
    {
        SetupJsonResponse(@"{""event_types"":[{""event_type_id"":28,""event_type_key"":""envoy_no_report"",""stateful"":true,""event_name"":""Gateway not reporting"",""event_description"":""desc"",""recommended_action"":""action""}]}");
        var result = await Client.GetEventTypesAsync();
        result.EventTypes.ShouldNotBeNull();
        result.EventTypes!.Count.ShouldBe(1);
        result.EventTypes![0].EventTypeId.ShouldBe(28);
        AssertRequest("/api/v4/systems/event_types");
    }

    [Fact]
    public async Task GetOpenEventsAsync()
    {
        SetupJsonResponse(@"{""events"":[],""system_id"":701644354}");
        var result = await Client.GetOpenEventsAsync(701644354);
        result.SystemId.ShouldBe(701644354);
        AssertRequest("/api/v4/systems/701644354/open_events");
    }

    [Fact]
    public async Task GetInvertersSummaryAsync()
    {
        SetupJsonResponse(@"[{""signal_strength"":5,""micro_inverters"":[]}]");
        var result = await Client.GetInvertersSummaryAsync(siteId: 12345);
        result.Count.ShouldBe(1);
        result[0].SignalStrength.ShouldBe(5);
        AssertRequest("/api/v4/systems/inverters_summary_by_envoy_or_site?site_id=12345");
    }
}
