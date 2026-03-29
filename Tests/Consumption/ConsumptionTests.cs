using Shouldly;

namespace Tests.Consumption;

public class ConsumptionTests : TestBase
{
    [Fact]
    public async Task GetConsumptionMeterReadingsAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""meter_readings"":[],""meta"":{""status"":""normal""}}");
        var result = await Client.GetConsumptionMeterReadingsAsync(66);
        result.SystemId.ShouldBe(66);
        AssertRequest("/api/v4/systems/66/consumption_meter_readings");
    }

    [Fact]
    public async Task GetStorageMeterReadingsAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""meter_readings"":[],""meta"":{""status"":""normal""}}");
        var result = await Client.GetStorageMeterReadingsAsync(66);
        result.SystemId.ShouldBe(66);
        AssertRequest("/api/v4/systems/66/storage_meter_readings");
    }

    [Fact]
    public async Task GetConsumptionLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""start_date"":""2016-08-01"",""consumption"":[15422,15421]}");
        var result = await Client.GetConsumptionLifetimeAsync(66);
        result.SystemId.ShouldBe(66);
        result.Consumption!.Count.ShouldBe(2);
        AssertRequest("/api/v4/systems/66/consumption_lifetime");
    }

    [Fact]
    public async Task GetBatteryLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""start_date"":""2016-08-01"",""charge"":[15422],""discharge"":[0]}");
        var result = await Client.GetBatteryLifetimeAsync(66);
        result.SystemId.ShouldBe(66);
        result.Charge!.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/66/battery_lifetime");
    }

    [Fact]
    public async Task GetEnergyImportLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""start_date"":""2016-08-01"",""import"":[100,200]}");
        var result = await Client.GetEnergyImportLifetimeAsync(66);
        result.SystemId.ShouldBe(66);
        AssertRequest("/api/v4/systems/66/energy_import_lifetime");
    }

    [Fact]
    public async Task GetEnergyExportLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":66,""start_date"":""2016-08-01"",""export"":[50,100]}");
        var result = await Client.GetEnergyExportLifetimeAsync(66);
        result.SystemId.ShouldBe(66);
        AssertRequest("/api/v4/systems/66/energy_export_lifetime");
    }

    [Fact]
    public async Task GetBatteryTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""total_devices"":9,""start_at"":1496526300,""end_at"":1496529300,""items"":""intervals"",""intervals"":[{""end_at"":1384122700,""charge"":{""enwh"":40,""devices_reporting"":1},""discharge"":{""enwh"":0,""devices_reporting"":4},""soc"":{""percent"":25,""devices_reporting"":4}}],""last_reported_aggregate_soc"":""97%""}");
        var result = await Client.GetBatteryTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        result.Intervals!.Count.ShouldBe(1);
        result.Intervals![0].Charge!.Enwh.ShouldBe(40);
        result.LastReportedAggregateSoc.ShouldBe("97%");
        AssertRequest("/api/v4/systems/698905955/telemetry/battery");
    }

    [Fact]
    public async Task GetConsumptionMeterTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""total_devices"":1,""start_at"":1496526300,""end_at"":1496529300,""items"":""intervals"",""intervals"":[]}");
        var result = await Client.GetConsumptionMeterTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        AssertRequest("/api/v4/systems/698905955/telemetry/consumption_meter");
    }

    [Fact]
    public async Task GetEnergyImportTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""total_devices"":0,""start_at"":1496526300,""end_at"":1496528300,""items"":""intervals"",""intervals"":[[{""end_at"":1384122700,""wh_imported"":40}]]}");
        var result = await Client.GetEnergyImportTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        result.Intervals!.Count.ShouldBe(1);
        result.Intervals![0][0].WhImported.ShouldBe(40);
        AssertRequest("/api/v4/systems/698905955/energy_import_telemetry");
    }

    [Fact]
    public async Task GetEnergyExportTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698905955,""granularity"":""day"",""total_devices"":0,""start_at"":1496526300,""end_at"":1496528300,""items"":""intervals"",""intervals"":[[{""end_at"":1384122700,""wh_exported"":40}]]}");
        var result = await Client.GetEnergyExportTelemetryAsync(698905955);
        result.SystemId.ShouldBe(698905955);
        result.Intervals!.Count.ShouldBe(1);
        result.Intervals![0][0].WhExported.ShouldBe(40);
        AssertRequest("/api/v4/systems/698905955/energy_export_telemetry");
    }

    [Fact]
    public async Task GetLatestTelemetryAsync()
    {
        SetupJsonResponse(@"{""system_id"":698943141,""items"":""devices"",""devices"":{""meters"":[{""id"":1084690247,""name"":""production"",""channel"":1,""last_report_at"":1701418500,""power"":755}],""encharges"":[],""heat-pump"":[],""evse"":[]}}");
        var result = await Client.GetLatestTelemetryAsync(698943141);
        result.SystemId.ShouldBe(698943141);
        result.Devices!.Meters!.Count.ShouldBe(1);
        result.Devices!.Meters![0].Power.ShouldBe(755);
        AssertRequest("/api/v4/systems/698943141/latest_telemetry");
    }
}
