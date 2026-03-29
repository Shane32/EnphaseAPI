using Shouldly;

namespace Tests.Production;

public class ProductionTests : TestBase
{
    [Fact]
    public async Task GetProductionMeterReadingsAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""meter_readings"":[{""serial_number"":""123"",""value"":6180635,""read_at"":1473901200}],""meta"":{""status"":""normal""}}");
        var result = await Client.GetProductionMeterReadingsAsync(66);
        result.SystemId.ShouldBe(66);
        result.MeterReadings!.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/66/production_meter_readings");
    }

    [Fact]
    public async Task GetRgmStatsAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""total_devices"":2,""intervals"":[],""meter_intervals"":[]}");
        var result = await Client.GetRgmStatsAsync(66);
        result.SystemId.ShouldBe(66);
        AssertRequest("/api/v4/systems/66/rgm_stats");
    }

    [Fact]
    public async Task GetEnergyLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""start_date"":""2013-01-01"",""production"":[15422,15421]}");
        var result = await Client.GetEnergyLifetimeAsync(66);
        result.SystemId.ShouldBe(66);
        result.Production!.Count.ShouldBe(2);
        AssertRequest("/api/v4/systems/66/energy_lifetime");
    }

    [Fact]
    public async Task GetProductionMicroTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""total_devices"":9,""start_at"":1496526300,""end_at"":1496528300,""items"":""intervals"",""intervals"":[]}");
        var result = await Client.GetProductionMicroTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        AssertRequest("/api/v4/systems/698905955/telemetry/production_micro");
    }

    [Fact]
    public async Task GetProductionMeterTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""total_devices"":9,""start_at"":1496526300,""end_at"":1496529300,""items"":""intervals"",""intervals"":[]}");
        var result = await Client.GetProductionMeterTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        AssertRequest("/api/v4/systems/698905955/telemetry/production_meter");
    }
}
