using Shane32.EnphaseAPI;
using Shane32.EnphaseAPI.Models;

internal sealed partial class App
{
    // =============================================================================
    // Systems menu
    // =============================================================================

    private static async Task SystemsMenuAsync(IEnphaseClient client)
    {
        Console.WriteLine();
        Console.WriteLine("=== Systems ===");
        Console.WriteLine(" 1. GetSystems");
        Console.WriteLine(" 2. SearchSystems");
        Console.WriteLine(" 3. GetSystem");
        Console.WriteLine(" 4. GetSystemSummary");
        Console.WriteLine(" 5. GetSystemDevices");
        Console.WriteLine(" 6. RetrieveSystemId");
        Console.WriteLine(" 7. GetSystemEvents");
        Console.WriteLine(" 8. GetSystemAlarms");
        Console.WriteLine(" 9. GetEventTypes");
        Console.WriteLine("10. GetOpenEvents");
        Console.WriteLine("11. GetInvertersSummary");
        Console.WriteLine(" 0. Back");
        Console.Write("Select: ");

        var choice = Console.ReadLine()?.Trim() ?? "";
        switch (choice) {
            case "1": {
                var page = PromptIntOptional("Page");
                var size = PromptIntOptional("Size");
                var sortBy = PromptOptional("Sort by");
                PrintResponse(await client.GetSystemsAsync(page, size, sortBy));
                break;
            }
            case "2": {
                var sortBy = PromptOptional("Sort by");
                var name = PromptOptional("System name filter");
                var reference = PromptOptional("Reference filter");
                var otherReference = PromptOptional("Other reference filter");

                Console.Write("System IDs (comma-separated, optional, press Enter to skip): ");
                var idsInput = Console.ReadLine()?.Trim();
                List<int>? ids = null;
                if (!string.IsNullOrEmpty(idsInput)) {
                    ids = idsInput.Split(',')
                        .Select(s => s.Trim())
                        .Where(s => int.TryParse(s, out _))
                        .Select(int.Parse)
                        .ToList();
                }

                Console.Write("Statuses (comma-separated, optional, press Enter to skip): ");
                var statusesInput = Console.ReadLine()?.Trim();
                List<string>? statuses = null;
                if (!string.IsNullOrEmpty(statusesInput)) {
                    statuses = statusesInput.Split(',')
                        .Select(s => s.Trim())
                        .Where(s => s.Length > 0)
                        .ToList();
                }

                var hasFilter = name != null || reference != null || otherReference != null || ids != null || statuses != null;
                var request = new SearchSystemsRequest {
                    SortBy = sortBy,
                    System = hasFilter ? new SearchSystemsFilter {
                        Name = name,
                        Reference = reference,
                        OtherReference = otherReference,
                        Ids = ids,
                        Statuses = statuses,
                    } : null,
                };

                var page = PromptIntOptional("Page");
                var size = PromptIntOptional("Size");
                var liveStream = PromptBoolOptional("Live stream");
                PrintResponse(await client.SearchSystemsAsync(request, page, size, liveStream));
                break;
            }
            case "3": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetSystemAsync(systemId));
                break;
            }
            case "4": {
                var systemId = PromptInt("System ID: ");
                var summaryDate = PromptDateOptional("Summary date");
                PrintResponse(await client.GetSystemSummaryAsync(systemId, summaryDate));
                break;
            }
            case "5": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetSystemDevicesAsync(systemId));
                break;
            }
            case "6": {
                var serialNum = Prompt("Serial number: ", required: true);
                PrintResponse(await client.RetrieveSystemIdAsync(serialNum));
                break;
            }
            case "7": {
                var systemId = PromptInt("System ID: ");
                var startTime = PromptTimestamp("Start time");
                var endTime = PromptTimestampOptional("End time");
                PrintResponse(await client.GetSystemEventsAsync(systemId, startTime, endTime));
                break;
            }
            case "8": {
                var systemId = PromptInt("System ID: ");
                var startTime = PromptTimestamp("Start time");
                var endTime = PromptTimestampOptional("End time");
                var cleared = PromptBoolOptional("Cleared");
                PrintResponse(await client.GetSystemAlarmsAsync(systemId, startTime, endTime, cleared));
                break;
            }
            case "9": {
                var eventTypeId = PromptIntOptional("Event type ID");
                PrintResponse(await client.GetEventTypesAsync(eventTypeId));
                break;
            }
            case "10": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetOpenEventsAsync(systemId));
                break;
            }
            case "11": {
                var siteId = PromptIntOptional("Site ID");
                var envoySerialNumber = PromptIntOptional("Envoy serial number (integer ID)");
                var page = PromptIntOptional("Page");
                var size = PromptIntOptional("Size");
                PrintResponse(await client.GetInvertersSummaryAsync(siteId, envoySerialNumber, page, size));
                break;
            }
            case "0":
                return;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }

    // =============================================================================
    // Production Monitoring menu
    // =============================================================================

    private static async Task ProductionMenuAsync(IEnphaseClient client)
    {
        Console.WriteLine();
        Console.WriteLine("=== Production Monitoring ===");
        Console.WriteLine("1. GetProductionMeterReadings");
        Console.WriteLine("2. GetRgmStats");
        Console.WriteLine("3. GetEnergyLifetime");
        Console.WriteLine("4. GetProductionMicroTelemetry");
        Console.WriteLine("5. GetProductionMeterTelemetry");
        Console.WriteLine("0. Back");
        Console.Write("Select: ");

        var choice = Console.ReadLine()?.Trim() ?? "";
        switch (choice) {
            case "1": {
                var systemId = PromptInt("System ID: ");
                var endAt = PromptTimestampOptional("End at");
                PrintResponse(await client.GetProductionMeterReadingsAsync(systemId, endAt));
                break;
            }
            case "2": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var endAt = PromptTimestampOptional("End at");
                PrintResponse(await client.GetRgmStatsAsync(systemId, startAt, endAt));
                break;
            }
            case "3": {
                var systemId = PromptInt("System ID: ");
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                var production = PromptOptional("Production (e.g. 'all')");
                PrintResponse(await client.GetEnergyLifetimeAsync(systemId, startDate, endDate, production));
                break;
            }
            case "4": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetProductionMicroTelemetryAsync(systemId, startAt, granularity));
                break;
            }
            case "5": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetProductionMeterTelemetryAsync(systemId, startAt, granularity));
                break;
            }
            case "0":
                return;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }

    // =============================================================================
    // Consumption Monitoring menu
    // =============================================================================

    private static async Task ConsumptionMenuAsync(IEnphaseClient client)
    {
        Console.WriteLine();
        Console.WriteLine("=== Consumption Monitoring ===");
        Console.WriteLine(" 1. GetConsumptionMeterReadings");
        Console.WriteLine(" 2. GetStorageMeterReadings");
        Console.WriteLine(" 3. GetConsumptionLifetime");
        Console.WriteLine(" 4. GetBatteryLifetime");
        Console.WriteLine(" 5. GetEnergyImportLifetime");
        Console.WriteLine(" 6. GetEnergyExportLifetime");
        Console.WriteLine(" 7. GetBatteryTelemetry");
        Console.WriteLine(" 8. GetConsumptionMeterTelemetry");
        Console.WriteLine(" 9. GetEnergyImportTelemetry");
        Console.WriteLine("10. GetEnergyExportTelemetry");
        Console.WriteLine("11. GetLatestTelemetry");
        Console.WriteLine(" 0. Back");
        Console.Write("Select: ");

        var choice = Console.ReadLine()?.Trim() ?? "";
        switch (choice) {
            case "1": {
                var systemId = PromptInt("System ID: ");
                var endAt = PromptTimestampOptional("End at");
                PrintResponse(await client.GetConsumptionMeterReadingsAsync(systemId, endAt));
                break;
            }
            case "2": {
                var systemId = PromptInt("System ID: ");
                var endAt = PromptTimestampOptional("End at");
                PrintResponse(await client.GetStorageMeterReadingsAsync(systemId, endAt));
                break;
            }
            case "3": {
                var systemId = PromptInt("System ID: ");
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetConsumptionLifetimeAsync(systemId, startDate, endDate));
                break;
            }
            case "4": {
                var systemId = PromptInt("System ID: ");
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetBatteryLifetimeAsync(systemId, startDate, endDate));
                break;
            }
            case "5": {
                var systemId = PromptInt("System ID: ");
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetEnergyImportLifetimeAsync(systemId, startDate, endDate));
                break;
            }
            case "6": {
                var systemId = PromptInt("System ID: ");
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetEnergyExportLifetimeAsync(systemId, startDate, endDate));
                break;
            }
            case "7": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetBatteryTelemetryAsync(systemId, startAt, granularity));
                break;
            }
            case "8": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetConsumptionMeterTelemetryAsync(systemId, startAt, granularity));
                break;
            }
            case "9": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetEnergyImportTelemetryAsync(systemId, startAt, granularity));
                break;
            }
            case "10": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetEnergyExportTelemetryAsync(systemId, startAt, granularity));
                break;
            }
            case "11": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetLatestTelemetryAsync(systemId));
                break;
            }
            case "0":
                return;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }

    // =============================================================================
    // Device Monitoring menu
    // =============================================================================

    private static async Task DeviceMenuAsync(IEnphaseClient client)
    {
        Console.WriteLine();
        Console.WriteLine("=== Device Monitoring ===");
        Console.WriteLine("1. GetMicroTelemetry");
        Console.WriteLine("2. GetEnchargeTelemetry");
        Console.WriteLine("3. GetEvseLifetime");
        Console.WriteLine("4. GetEvseTelemetry");
        Console.WriteLine("5. GetHpLifetime");
        Console.WriteLine("6. GetHpTelemetry");
        Console.WriteLine("0. Back");
        Console.Write("Select: ");

        var choice = Console.ReadLine()?.Trim() ?? "";
        switch (choice) {
            case "1": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetMicroTelemetryAsync(systemId, serialNo, startAt, granularity));
                break;
            }
            case "2": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                PrintResponse(await client.GetEnchargeTelemetryAsync(systemId, serialNo, startAt, granularity));
                break;
            }
            case "3": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetEvseLifetimeAsync(systemId, serialNo, startDate, endDate));
                break;
            }
            case "4": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var startAt = PromptTimestampOptional("Start at");
                var granularity = PromptGranularityOptional("Granularity");
                var intervalDuration = PromptOptional("Interval duration");
                PrintResponse(await client.GetEvseTelemetryAsync(systemId, serialNo, startAt, granularity, intervalDuration));
                break;
            }
            case "5": {
                var systemId = PromptInt("System ID: ");
                var startDate = PromptDateOptional("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetHpLifetimeAsync(systemId, startDate, endDate));
                break;
            }
            case "6": {
                var systemId = PromptInt("System ID: ");
                var startAt = PromptTimestampOptional("Start at");
                var startDate = PromptDateOptional("Start date");
                var granularity = PromptGranularityOptional("Granularity");
                var intervalDuration = PromptOptional("Interval duration");
                PrintResponse(await client.GetHpTelemetryAsync(systemId, startAt, startDate, granularity, intervalDuration));
                break;
            }
            case "0":
                return;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }

    // =============================================================================
    // Configuration menu
    // =============================================================================

    private static async Task ConfigMenuAsync(IEnphaseClient client)
    {
        Console.WriteLine();
        Console.WriteLine("=== Configuration ===");
        Console.WriteLine("1. GetBatterySettings");
        Console.WriteLine("2. UpdateBatterySettings");
        Console.WriteLine("3. GetStormGuard");
        Console.WriteLine("4. GetGridStatus");
        Console.WriteLine("5. GetLoadControl");
        Console.WriteLine("0. Back");
        Console.Write("Select: ");

        var choice = Console.ReadLine()?.Trim() ?? "";
        switch (choice) {
            case "1": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetBatterySettingsAsync(systemId));
                break;
            }
            case "2": {
                var systemId = PromptInt("System ID: ");
                var batteryMode = PromptOptional("Battery mode (e.g. 'backup', 'self-consumption')");
                var reserveSoc = PromptIntOptional("Reserve SoC (%)");
                var energyIndependence = PromptBoolOptional("Energy independence");
                var request = new UpdateBatterySettingsRequest {
                    BatteryMode = batteryMode,
                    ReserveSoc = reserveSoc,
                    EnergyIndependence = energyIndependence,
                };
                PrintResponse(await client.UpdateBatterySettingsAsync(systemId, request));
                break;
            }
            case "3": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetStormGuardAsync(systemId));
                break;
            }
            case "4": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetGridStatusAsync(systemId));
                break;
            }
            case "5": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetLoadControlAsync(systemId));
                break;
            }
            case "0":
                return;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }

    // =============================================================================
    // EV Charger menu
    // =============================================================================

    private static async Task EvChargerMenuAsync(IEnphaseClient client)
    {
        Console.WriteLine();
        Console.WriteLine("=== EV Charger ===");
        Console.WriteLine("1. GetEvChargerDevices");
        Console.WriteLine("2. GetEvChargerEvents");
        Console.WriteLine("3. GetEvChargerSessions");
        Console.WriteLine("4. GetEvChargerSchedules");
        Console.WriteLine("5. GetEvChargerLifetime");
        Console.WriteLine("6. GetEvChargerTelemetry");
        Console.WriteLine("7. StartCharging");
        Console.WriteLine("8. StopCharging");
        Console.WriteLine("0. Back");
        Console.Write("Select: ");

        var choice = Console.ReadLine()?.Trim() ?? "";
        switch (choice) {
            case "1": {
                var systemId = PromptInt("System ID: ");
                PrintResponse(await client.GetEvChargerDevicesAsync(systemId));
                break;
            }
            case "2": {
                var systemId = PromptInt("System ID: ");
                var offset = PromptIntOptional("Offset");
                var serialNum = PromptOptional("Serial number");
                var limit = PromptIntOptional("Limit");
                PrintResponse(await client.GetEvChargerEventsAsync(systemId, offset, serialNum, limit));
                break;
            }
            case "3": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var offset = PromptIntOptional("Offset");
                var limit = PromptIntOptional("Limit");
                PrintResponse(await client.GetEvChargerSessionsAsync(systemId, serialNo, offset, limit));
                break;
            }
            case "4": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                PrintResponse(await client.GetEvChargerSchedulesAsync(systemId, serialNo));
                break;
            }
            case "5": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var startDate = PromptDate("Start date");
                var endDate = PromptDateOptional("End date");
                PrintResponse(await client.GetEvChargerLifetimeAsync(systemId, serialNo, startDate, endDate));
                break;
            }
            case "6": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var granularity = PromptGranularityOptional("Granularity");
                var startDate = PromptDateOptional("Start date");
                var startAt = PromptTimestampOptional("Start at");
                PrintResponse(await client.GetEvChargerTelemetryAsync(systemId, serialNo, granularity, startDate, startAt));
                break;
            }
            case "7": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                var connectorId = PromptOptional("Connector ID");
                var chargingLevel = PromptOptional("Charging level");
                var request = new StartChargingRequest {
                    ConnectorId = connectorId,
                    ChargingLevel = chargingLevel,
                };
                PrintResponse(await client.StartChargingAsync(systemId, serialNo, request));
                break;
            }
            case "8": {
                var systemId = PromptInt("System ID: ");
                var serialNo = Prompt("Serial number: ", required: true);
                PrintResponse(await client.StopChargingAsync(systemId, serialNo));
                break;
            }
            case "0":
                return;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    }
}
