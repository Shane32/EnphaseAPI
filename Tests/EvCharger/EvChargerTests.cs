using System.Net;
using System.Text.Json;
using Shane32.EnphaseAPI.Models;
using Shouldly;

namespace Tests.EvCharger;

public class EvChargerTests : TestBase
{
    [Fact]
    public async Task GetEvChargerDevicesAsync()
    {
        SetupJsonResponse(@"{""items"":""devices"",""total_devices"":1,""system_id"":698989834,""devices"":{""ev_chargers"":[{""id"":""202320010308:698989834"",""sku"":""IQ-EVSE-NA-1040-0110-0100"",""status"":""normal"",""serial_number"":""202320010308"",""name"":""IQ EV Charger NACS"",""model"":""IQ-EVSE-40R"",""part_number"":""800-00555 0303"",""last_report_at"":1700074065,""firmware"":""v0.04.22"",""active"":true}]}}");
        var result = await Client.GetEvChargerDevicesAsync(698989834);
        result.SystemId.ShouldBe(698989834);
        result.Devices!.EvChargers!.Count.ShouldBe(1);
        result.Devices!.EvChargers![0].SerialNumber.ShouldBe("202320010308");
        AssertRequest("/api/v4/systems/698989834/ev_charger/devices");
    }

    [Fact]
    public async Task GetEvChargerEventsAsync()
    {
        SetupJsonResponse(@"{""count"":1,""events"":[{""status"":""Info"",""triggered_date"":1705399759,""cleared_date"":1705399759,""details"":""Charging started""}],""system_id"":701052773}");
        var result = await Client.GetEvChargerEventsAsync(701052773);
        result.SystemId.ShouldBe(701052773);
        result.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/701052773/ev_charger/events");
    }

    [Fact]
    public async Task GetEvChargerSessionsAsync()
    {
        SetupJsonResponse(@"{""count"":1,""system_id"":698989834,""sessions"":[{""start_time"":1700059683,""end_time"":1700071180,""duration"":11497,""energy_added"":14.83,""charge_time"":7080,""miles_added"":1.2,""cost"":0.5}]}");
        var result = await Client.GetEvChargerSessionsAsync(698989834, "202320010308");
        result.SystemId.ShouldBe(698989834);
        result.Sessions!.Count.ShouldBe(1);
        result.Sessions![0].EnergyAdded.ShouldBe(14.83);
        AssertRequest("/api/v4/systems/698989834/ev_charger/202320010308/sessions");
    }

    [Fact]
    public async Task GetEvChargerSchedulesAsync()
    {
        SetupJsonResponse(@"{""system_id"":698989834,""charger_schedules"":[{""schedules"":[{""days"":[1,2],""start_time"":""1:00"",""end_time"":""2:00"",""charging_level"":70}],""type"":""Custom"",""is_active"":false,""reminder_flag"":true,""reminder_timer"":10}]}");
        var result = await Client.GetEvChargerSchedulesAsync(698989834, "202320010308");
        result.SystemId.ShouldBe(698989834);
        result.ChargerSchedules!.Count.ShouldBe(1);
        AssertRequest("/api/v4/systems/698989834/ev_charger/202320010308/schedules");
    }

    [Fact]
    public async Task GetEvChargerLifetimeAsync()
    {
        SetupJsonResponse(@"{""system_id"":698989834,""start_date"":""2024-01-01"",""consumption"":[3494,21929,0,0,0,0]}");
        var result = await Client.GetEvChargerLifetimeAsync(698989834, "190179855", new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero));
        result.SystemId.ShouldBe(698989834);
        result.Consumption!.Count.ShouldBe(6);
        AssertRequest("/api/v4/systems/698989834/ev_charger/190179855/lifetime?start_date=2024-01-01");
    }

    [Fact]
    public async Task GetEvChargerTelemetryAsync()
    {
        SetupJsonResponse(@"{""granularity"":""day"",""consumption"":[{""consumption"":0,""end_at"":1705385700},{""consumption"":38,""end_at"":1705400100}],""system_id"":700460094,""start_date"":""2024-01-16"",""end_date"":""2024-01-16"",""start_at"":0,""end_at"":0}");
        var result = await Client.GetEvChargerTelemetryAsync(700460094, "202109116909");
        result.SystemId.ShouldBe(700460094);
        result.Consumption!.Count.ShouldBe(2);
        AssertRequest("/api/v4/systems/700460094/ev_charger/202109116909/telemetry");
    }

    [Fact]
    public async Task StartChargingAsync()
    {
        SetupJsonResponse(@"{""message"":""Request sent successfully""}", HttpStatusCode.Accepted);
        var request = new StartChargingRequest { ConnectorId = "1", ChargingLevel = "40" };
        var result = await Client.StartChargingAsync(701052773, "202312100006", request);
        result.Message.ShouldBe("Request sent successfully");
        AssertRequest("/api/v4/systems/701052773/ev_charger/202312100006/start_charging", HttpMethod.Post);
        var body = await GetRequestBodyAsync();
        var doc = JsonDocument.Parse(body);
        doc.RootElement.GetProperty("connectorId").GetString().ShouldBe("1");
    }

    [Fact]
    public async Task StopChargingAsync()
    {
        SetupJsonResponse(@"{""message"":""Request sent successfully""}", HttpStatusCode.Accepted);
        var result = await Client.StopChargingAsync(701052773, "202312100006");
        result.Message.ShouldBe("Request sent successfully");
        AssertRequest("/api/v4/systems/701052773/ev_charger/202312100006/stop_charging", HttpMethod.Post);
    }
}
