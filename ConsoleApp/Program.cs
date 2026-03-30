using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Options;
using Shane32.EnphaseAPI;
using Shane32.EnphaseAPI.Models;

Console.WriteLine("=== Enphase API Console ===");
Console.WriteLine();

// ── Step 1: API key ────────────────────────────────────────────────────────
var apiKey = Prompt("Enter API key: ", required: true);

// ── Step 2: Token ──────────────────────────────────────────────────────────
Console.Write("Do you have an access token or refresh token? Enter 'a' for access or 'r' for refresh: ");
var tokenChoice = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";

string accessToken;
if (tokenChoice == "r") {
    var clientId = Prompt("Enter client ID: ", required: true);
    var clientSecret = Prompt("Enter client secret: ", required: true);
    var refreshToken = Prompt("Enter refresh token: ", required: true);
    Console.WriteLine("Exchanging refresh token for access token...");
    accessToken = await ExchangeRefreshTokenAsync(clientId, clientSecret, refreshToken);
    Console.WriteLine("Access token obtained successfully.");
} else {
    accessToken = Prompt("Enter access token: ", required: true);
}

// ── Step 3: Build client ───────────────────────────────────────────────────
using var httpClient = new HttpClient { BaseAddress = new Uri("https://api.enphaseenergy.com") };
var client = new EnphaseClient(
    httpClient,
    new OptionsWrapper<EnphaseClientOptions>(new EnphaseClientOptions { ApiKey = apiKey }),
    TimeProvider.System);
client.AccessToken = accessToken;

Console.WriteLine();
Console.WriteLine("Client ready.");

// ── Step 4: Main menu loop ─────────────────────────────────────────────────
while (true) {
    Console.WriteLine();
    Console.WriteLine("=== Main Menu ===");
    Console.WriteLine("1. Systems");
    Console.WriteLine("2. Production Monitoring");
    Console.WriteLine("3. Consumption Monitoring");
    Console.WriteLine("4. Device Monitoring");
    Console.WriteLine("5. Configuration");
    Console.WriteLine("6. EV Charger");
    Console.WriteLine("0. Exit");
    Console.Write("Select: ");

    var choice = Console.ReadLine()?.Trim() ?? "";
    if (choice == "0")
        break;

    try {
        switch (choice) {
            case "1":
                await SystemsMenuAsync(client);
                break;
            case "2":
                await ProductionMenuAsync(client);
                break;
            case "3":
                await ConsumptionMenuAsync(client);
                break;
            case "4":
                await DeviceMenuAsync(client);
                break;
            case "5":
                await ConfigMenuAsync(client);
                break;
            case "6":
                await EvChargerMenuAsync(client);
                break;
            default:
                Console.WriteLine("Invalid selection.");
                break;
        }
    } catch (EnphaseRateLimitException ex) {
        Console.WriteLine($"Error: Rate limit exceeded (HTTP {ex.HttpStatusCode})");
        if (ex.Message != null)
            Console.WriteLine($"  Message: {ex.Message}");
        if (ex.Details != null)
            Console.WriteLine($"  Details: {ex.Details}");
        if (ex.Period != null)
            Console.WriteLine($"  Period: {ex.Period}");
        if (ex.Limit.HasValue)
            Console.WriteLine($"  Limit: {ex.Limit}");
        if (ex.PeriodStart.HasValue)
            Console.WriteLine($"  Period start: {DateTimeOffset.FromUnixTimeSeconds(ex.PeriodStart.Value)}");
        if (ex.PeriodEnd.HasValue)
            Console.WriteLine($"  Period end: {DateTimeOffset.FromUnixTimeSeconds(ex.PeriodEnd.Value)}");
    } catch (EnphaseException ex) {
        Console.WriteLine($"Error: Enphase API error (HTTP {ex.HttpStatusCode})");
        if (ex.Message != null)
            Console.WriteLine($"  Message: {ex.Message}");
        if (ex.Details != null)
            Console.WriteLine($"  Details: {ex.Details}");
    } catch (HttpRequestException ex) {
        Console.WriteLine("Error: HTTP request failed");
        if (ex.Message != null)
            Console.WriteLine($"  Message: {ex.Message}");
    } catch (Exception ex) {
        Console.WriteLine("Error: Unexpected error");
        Console.WriteLine($"  {ex.Message}");
    }
}

Console.WriteLine("Goodbye!");

// =============================================================================
// Helper functions
// =============================================================================

static string Prompt(string message, bool required = false)
{
    while (true) {
        Console.Write(message);
        var value = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!required || value.Length > 0)
            return value;
        Console.WriteLine("This field is required.");
    }
}

static string? PromptOptional(string message)
{
    Console.Write($"{message} (optional, press Enter to skip): ");
    var value = Console.ReadLine()?.Trim();
    return string.IsNullOrEmpty(value) ? null : value;
}

static int PromptInt(string message)
{
    while (true) {
        Console.Write(message);
        var input = Console.ReadLine()?.Trim();
        if (int.TryParse(input, out int value))
            return value;
        Console.WriteLine("Invalid integer. Please try again.");
    }
}

static int? PromptIntOptional(string message)
{
    Console.Write($"{message} (optional, press Enter to skip): ");
    var input = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(input))
        return null;
    if (int.TryParse(input, out int value))
        return value;
    Console.WriteLine("Invalid integer, treating as empty.");
    return null;
}

static long PromptLong(string message)
{
    while (true) {
        Console.Write(message);
        var input = Console.ReadLine()?.Trim();
        if (long.TryParse(input, out long value))
            return value;
        Console.WriteLine("Invalid integer. Please try again.");
    }
}

static long? PromptLongOptional(string message)
{
    Console.Write($"{message} (optional, press Enter to skip): ");
    var input = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(input))
        return null;
    if (long.TryParse(input, out long value))
        return value;
    Console.WriteLine("Invalid integer, treating as empty.");
    return null;
}

static bool? PromptBoolOptional(string message)
{
    Console.Write($"{message} (y/n, optional, press Enter to skip): ");
    var input = Console.ReadLine()?.Trim().ToLowerInvariant();
    return input == "y" ? true : input == "n" ? false : null;
}

static void PrintResponse(object response)
{
    var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
    Console.WriteLine(json);
}

static async Task<string> ExchangeRefreshTokenAsync(string clientId, string clientSecret, string refreshToken)
{
    using var http = new HttpClient();
    var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

    var content = new FormUrlEncodedContent(new[]
    {
        new KeyValuePair<string, string>("grant_type", "refresh_token"),
        new KeyValuePair<string, string>("refresh_token", refreshToken),
    });

    var response = await http.PostAsync("https://api.enphaseenergy.com/oauth/token", content);
    if (!response.IsSuccessStatusCode) {
        var errorBody = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException($"Token exchange failed (HTTP {(int)response.StatusCode}): {errorBody}");
    }

    var responseBody = await response.Content.ReadAsStringAsync();
    var tokenResponse = JsonSerializer.Deserialize<JsonNode>(responseBody);
    var token = tokenResponse?["access_token"]?.GetValue<string>();

    if (string.IsNullOrEmpty(token))
        throw new InvalidOperationException("Token exchange response did not contain an access_token.");

    return token;
}

// =============================================================================
// Systems menu
// =============================================================================

static async Task SystemsMenuAsync(IEnphaseClient client)
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
            var summaryDate = PromptOptional("Summary date (YYYY-MM-DD)");
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
            var startTime = PromptLong("Start time (Unix timestamp): ");
            var endTime = PromptLongOptional("End time (Unix timestamp)");
            PrintResponse(await client.GetSystemEventsAsync(systemId, startTime, endTime));
            break;
        }
        case "8": {
            var systemId = PromptInt("System ID: ");
            var startTime = PromptLong("Start time (Unix timestamp): ");
            var endTime = PromptLongOptional("End time (Unix timestamp)");
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

static async Task ProductionMenuAsync(IEnphaseClient client)
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
            var endAt = PromptLongOptional("End at (Unix timestamp)");
            PrintResponse(await client.GetProductionMeterReadingsAsync(systemId, endAt));
            break;
        }
        case "2": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var endAt = PromptLongOptional("End at (Unix timestamp)");
            PrintResponse(await client.GetRgmStatsAsync(systemId, startAt, endAt));
            break;
        }
        case "3": {
            var systemId = PromptInt("System ID: ");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            var production = PromptOptional("Production (e.g. 'all')");
            PrintResponse(await client.GetEnergyLifetimeAsync(systemId, startDate, endDate, production));
            break;
        }
        case "4": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            PrintResponse(await client.GetProductionMicroTelemetryAsync(systemId, startAt, granularity));
            break;
        }
        case "5": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
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

static async Task ConsumptionMenuAsync(IEnphaseClient client)
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
            var endAt = PromptLongOptional("End at (Unix timestamp)");
            PrintResponse(await client.GetConsumptionMeterReadingsAsync(systemId, endAt));
            break;
        }
        case "2": {
            var systemId = PromptInt("System ID: ");
            var endAt = PromptLongOptional("End at (Unix timestamp)");
            PrintResponse(await client.GetStorageMeterReadingsAsync(systemId, endAt));
            break;
        }
        case "3": {
            var systemId = PromptInt("System ID: ");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetConsumptionLifetimeAsync(systemId, startDate, endDate));
            break;
        }
        case "4": {
            var systemId = PromptInt("System ID: ");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetBatteryLifetimeAsync(systemId, startDate, endDate));
            break;
        }
        case "5": {
            var systemId = PromptInt("System ID: ");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetEnergyImportLifetimeAsync(systemId, startDate, endDate));
            break;
        }
        case "6": {
            var systemId = PromptInt("System ID: ");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetEnergyExportLifetimeAsync(systemId, startDate, endDate));
            break;
        }
        case "7": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            PrintResponse(await client.GetBatteryTelemetryAsync(systemId, startAt, granularity));
            break;
        }
        case "8": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            PrintResponse(await client.GetConsumptionMeterTelemetryAsync(systemId, startAt, granularity));
            break;
        }
        case "9": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            PrintResponse(await client.GetEnergyImportTelemetryAsync(systemId, startAt, granularity));
            break;
        }
        case "10": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
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

static async Task DeviceMenuAsync(IEnphaseClient client)
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
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            PrintResponse(await client.GetMicroTelemetryAsync(systemId, serialNo, startAt, granularity));
            break;
        }
        case "2": {
            var systemId = PromptInt("System ID: ");
            var serialNo = Prompt("Serial number: ", required: true);
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            PrintResponse(await client.GetEnchargeTelemetryAsync(systemId, serialNo, startAt, granularity));
            break;
        }
        case "3": {
            var systemId = PromptInt("System ID: ");
            var serialNo = Prompt("Serial number: ", required: true);
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetEvseLifetimeAsync(systemId, serialNo, startDate, endDate));
            break;
        }
        case "4": {
            var systemId = PromptInt("System ID: ");
            var serialNo = Prompt("Serial number: ", required: true);
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            var intervalDuration = PromptOptional("Interval duration");
            PrintResponse(await client.GetEvseTelemetryAsync(systemId, serialNo, startAt, granularity, intervalDuration));
            break;
        }
        case "5": {
            var systemId = PromptInt("System ID: ");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetHpLifetimeAsync(systemId, startDate, endDate));
            break;
        }
        case "6": {
            var systemId = PromptInt("System ID: ");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
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

static async Task ConfigMenuAsync(IEnphaseClient client)
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
            var energyIndependence = PromptOptional("Energy independence");
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

static async Task EvChargerMenuAsync(IEnphaseClient client)
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
            var startDate = Prompt("Start date (YYYY-MM-DD): ", required: true);
            var endDate = PromptOptional("End date (YYYY-MM-DD)");
            PrintResponse(await client.GetEvChargerLifetimeAsync(systemId, serialNo, startDate, endDate));
            break;
        }
        case "6": {
            var systemId = PromptInt("System ID: ");
            var serialNo = Prompt("Serial number: ", required: true);
            var granularity = PromptOptional("Granularity (e.g. 'day', 'week', 'month', 'year')");
            var startDate = PromptOptional("Start date (YYYY-MM-DD)");
            var startAt = PromptLongOptional("Start at (Unix timestamp)");
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
