using System.Text.Json;
using Shouldly;

namespace Tests.Config;

public class ConfigTests : TestBase
{
    [Fact]
    public async Task GetBatterySettings()
    {
        SetupJsonResponse(@"{""system_id"":1765,""battery_mode"":""Self - Consumption"",""reserve_soc"":95,""energy_independence"":""enabled"",""charge_from_grid"":""disabled"",""battery_shutdown_level"":13}");
        var result = await Client.GetBatterySettingsAsync(1765);
        result.SystemId.ShouldBe(1765);
        result.BatteryMode.ShouldBe("Self - Consumption");
        result.ReserveSoc.ShouldBe(95);
        AssertRequest("/api/v4/systems/config/1765/battery_settings");
    }

    [Fact]
    public async Task UpdateBatterySettings()
    {
        SetupJsonResponse(@"{""system_id"":1765,""battery_mode"":""self-consumption"",""reserve_soc"":80,""energy_independence"":""enabled"",""charge_from_grid"":""disabled"",""battery_shutdown_level"":13}");
        var request = new Shane32.EnphaseAPI.Models.UpdateBatterySettingsRequest { BatteryMode = "self-consumption", ReserveSoc = 80 };
        var result = await Client.UpdateBatterySettingsAsync(1765, request);
        result.ReserveSoc.ShouldBe(80);
        AssertRequest("/api/v4/systems/config/1765/battery_settings", HttpMethod.Put);
        var body = await GetRequestBodyAsync();
        var doc = JsonDocument.Parse(body);
        doc.RootElement.GetProperty("battery_mode").GetString().ShouldBe("self-consumption");
        doc.RootElement.GetProperty("reserve_soc").GetInt32().ShouldBe(80);
    }

    [Fact]
    public async Task GetStormGuard()
    {
        SetupJsonResponse(@"{""system_id"":1765,""storm_guard_status"":""enabled"",""storm_alert"":""false""}");
        var result = await Client.GetStormGuardAsync(1765);
        result.SystemId.ShouldBe(1765);
        result.StormGuardStatus.ShouldBe("enabled");
        AssertRequest("/api/v4/systems/config/1765/storm_guard");
    }

    [Fact]
    public async Task GetGridStatus()
    {
        SetupJsonResponse(@"{""system_id"":1765,""grid_state"":""On Grid"",""last_report_date"":1676029267}");
        var result = await Client.GetGridStatusAsync(1765);
        result.SystemId.ShouldBe(1765);
        result.GridState.ShouldBe("On Grid");
        AssertRequest("/api/v4/systems/config/1765/grid_status");
    }

    [Fact]
    public async Task GetLoadControl()
    {
        SetupJsonResponse(@"{""system_id"":1932237,""load_control_data"":[{""name"":""NC1"",""load_name"":""A/C"",""owner_can_override"":true,""mode"":""Basic"",""soc_low"":30,""soc_high"":50,""status"":""enabled"",""essential_start_time"":32400,""essential_end_time"":57600}]}");
        var result = await Client.GetLoadControlAsync(1932237);
        result.SystemId.ShouldBe(1932237);
        result.LoadControlData!.Count.ShouldBe(1);
        result.LoadControlData![0].Name.ShouldBe("NC1");
        AssertRequest("/api/v4/systems/config/1932237/load_control");
    }
}
