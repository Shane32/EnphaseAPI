using Shouldly;

namespace Tests.Devices;

public class DevicesTests : TestBase
{
    [Fact]
    public async Task GetMicroTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":1765,""serial_number"":""12345"",""granularity"":""day"",""total_devices"":1,""start_at"":1496526300,""end_at"":1496529300,""items"":""intervals"",""intervals"":[{""end_at"":1496526300,""powr"":30,""enwh"":40}]}");
        var result = await Client.GetMicroTelemetryAsync(1765, "12345");
        result.SystemId.ShouldBe(1765);
        result.SerialNumber.ShouldBe("12345");
        result.Intervals!.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/1765/devices/micros/12345/telemetry");
    }

    [Fact]
    public async Task GetEnchargeTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":1765,""serial_number"":""12345"",""granularity"":""day"",""total_devices"":1,""start_at"":1496526300,""end_at"":1496529300,""items"":""intervals"",""intervals"":[{""end_at"":1384122700,""charge"":{""enwh"":40},""discharge"":{""enwh"":0},""soc"":{""percent"":25}}],""last_reported_time"":1650349170,""last_reported_soc"":""99%""}");
        var result = await Client.GetEnchargeTelemetryAsync(1765, "12345");
        result.SystemId.ShouldBe(1765);
        result.Intervals!.Count.ShouldBe(1);
        result.Intervals![0].Charge!.Enwh.ShouldBe(40);
        result.LastReportedSoc.ShouldBe("99%");
        AssertRequest("/api/v4/systems/1765/devices/encharges/12345/telemetry");
    }

    [Fact]
    public async Task GetEvseLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""start_date"":""2024-11-22"",""end_date"":""2024-11-28"",""consumption"":[40,35,40,20,15,10,2]}");
        var result = await Client.GetEvseLifetimeAsync(698905955, "SN123");
        result.SystemId.ShouldBe(698905955);
        result.Consumption!.Count.ShouldBe(7);
        AssertRequest("/api/v4/systems/698905955/SN123/evse_lifetime");
    }

    [Fact]
    public async Task GetEvseTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""interval_duration"":""5mins"",""start_at"":1496526300,""end_at"":1496528100,""items"":""intervals"",""intervals"":[{""end_at"":1496527200,""wh_consumed"":40}]}");
        var result = await Client.GetEvseTelemetryAsync(698905955, "SN123");
        result.SystemId.ShouldBe(698905955);
        result.Intervals!.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/698905955/SN123/evse_telemetry");
    }

    [Fact]
    public async Task GetHpLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""start_date"":""2024-11-22"",""end_date"":""2024-11-28"",""consumption"":[40,35]}");
        var result = await Client.GetHpLifetimeAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        result.Consumption!.Count.ShouldBe(2);
        AssertRequest("/api/v4/systems/698905955/hp_lifetime");
    }

    [Fact]
    public async Task GetHpTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""interval_duration"":""5mins"",""start_at"":1496526300,""end_at"":1496528100,""items"":""intervals"",""intervals"":[{""end_at"":1496526600,""wh_consumed"":40}]}");
        var result = await Client.GetHpTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        result.Intervals!.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/698905955/hp_telemetry");
    }
}
