using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

internal sealed partial class App
{
    private static async Task<string> ExchangeRefreshTokenAsync(string clientId, string clientSecret, string refreshToken)
    {
        using var http = new HttpClient();
        var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
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
        var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(responseBody);
        var token = tokenResponse?["access_token"]?.GetValue<string>();

        if (string.IsNullOrEmpty(token))
            throw new InvalidOperationException("Token exchange response did not contain an access_token.");

        return token;
    }

    private static async Task<string> AuthorizeAsync(string clientId, string clientSecret)
    {
        const string redirectUri = "https://api.enphaseenergy.com/oauth/redirect_uri";
        var authUrl = $"https://api.enphaseenergy.com/oauth/authorize?response_type=code&client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}";

        Console.WriteLine("Opening browser to authorize the application...");
        Console.WriteLine($"Auth URL: {authUrl}");

        try {
            Process.Start(new ProcessStartInfo {
                FileName = authUrl,
                UseShellExecute = true,
            });
        } catch {
            Console.WriteLine("Could not open browser automatically. Please open the URL above manually.");
        }

        Console.WriteLine("After approving access in the browser, copy the authorization code from the redirect URL.");
        var code = Prompt("Enter the authorization code: ", required: true);

        using var http = new HttpClient();
        var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("redirect_uri", redirectUri),
            new KeyValuePair<string, string>("code", code),
        });

        var response = await http.PostAsync("https://api.enphaseenergy.com/oauth/token", content);
        if (!response.IsSuccessStatusCode) {
            var errorBody = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException($"Token exchange failed (HTTP {(int)response.StatusCode}): {errorBody}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(responseBody);

        var accessToken = tokenResponse?["access_token"]?.GetValue<string>();
        if (string.IsNullOrEmpty(accessToken))
            throw new InvalidOperationException("Token exchange response did not contain an access_token.");

        var refreshToken = tokenResponse?["refresh_token"]?.GetValue<string>();
        if (!string.IsNullOrEmpty(refreshToken))
            Console.WriteLine($"Refresh token (save for future use): {refreshToken}");

        Console.WriteLine("Access token obtained successfully.");
        return accessToken;
    }
}
