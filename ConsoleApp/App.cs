using Microsoft.Extensions.Options;
using Shane32.EnphaseAPI;

internal sealed partial class App
{
    private readonly ConsoleAppOptions _options;

    public App(IOptions<ConsoleAppOptions> options)
    {
        _options = options.Value;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("=== Enphase API Console ===");
        Console.WriteLine();

        // ── Step 1: API key ────────────────────────────────────────────────────
        var apiKey = !string.IsNullOrEmpty(_options.ApiKey)
            ? _options.ApiKey
            : Prompt("Enter API key: ", required: true);

        // ── Step 2: Token ──────────────────────────────────────────────────────
        string accessToken;
        if (!string.IsNullOrEmpty(_options.AccessToken)) {
            accessToken = _options.AccessToken;
        } else if (!string.IsNullOrEmpty(_options.RefreshToken)
                   && !string.IsNullOrEmpty(_options.ClientId)
                   && !string.IsNullOrEmpty(_options.ClientSecret)) {
            Console.WriteLine("Exchanging refresh token for access token...");
            accessToken = await ExchangeRefreshTokenAsync(_options.ClientId, _options.ClientSecret, _options.RefreshToken);
            Console.WriteLine("Access token obtained successfully.");
        } else {
            Console.Write("Do you have an access token or refresh token, or do you need to authorize? Enter 'a' for access, 'r' for refresh, or 'o' to authorize: ");
            var tokenChoice = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";

            if (tokenChoice == "r") {
                var clientId = !string.IsNullOrEmpty(_options.ClientId) ? _options.ClientId : Prompt("Enter client ID: ", required: true);
                var clientSecret = !string.IsNullOrEmpty(_options.ClientSecret) ? _options.ClientSecret : Prompt("Enter client secret: ", required: true);
                var refreshToken = Prompt("Enter refresh token: ", required: true);
                Console.WriteLine("Exchanging refresh token for access token...");
                accessToken = await ExchangeRefreshTokenAsync(clientId, clientSecret, refreshToken);
                Console.WriteLine("Access token obtained successfully.");
            } else if (tokenChoice == "o") {
                var clientId = !string.IsNullOrEmpty(_options.ClientId) ? _options.ClientId : Prompt("Enter client ID: ", required: true);
                var clientSecret = !string.IsNullOrEmpty(_options.ClientSecret) ? _options.ClientSecret : Prompt("Enter client secret: ", required: true);
                accessToken = await AuthorizeAsync(clientId, clientSecret);
            } else {
                accessToken = Prompt("Enter access token: ", required: true);
            }
        }

        // ── Step 3: Build client ───────────────────────────────────────────────
        using var httpClient = new HttpClient { BaseAddress = new Uri("https://api.enphaseenergy.com") };
        var client = new EnphaseClient(
            httpClient,
            new Microsoft.Extensions.Options.OptionsWrapper<EnphaseClientOptions>(new EnphaseClientOptions { ApiKey = apiKey }),
            TimeProvider.System);
        client.AccessToken = accessToken;

        Console.WriteLine();
        Console.WriteLine("Client ready.");

        // ── Step 4: Main menu loop ─────────────────────────────────────────────
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
    }
}
