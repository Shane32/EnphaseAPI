/// <summary>Configuration options for the Enphase API console application.</summary>
internal sealed class ConsoleAppOptions
{
    /// <summary>Gets or sets the Enphase API key.</summary>
    public string? ApiKey { get; set; }

    /// <summary>Gets or sets the OAuth 2.0 client ID.</summary>
    public string? ClientId { get; set; }

    /// <summary>Gets or sets the OAuth 2.0 client secret.</summary>
    public string? ClientSecret { get; set; }

    /// <summary>Gets or sets a pre-configured OAuth 2.0 access token.</summary>
    public string? AccessToken { get; set; }

    /// <summary>Gets or sets a pre-configured OAuth 2.0 refresh token.</summary>
    public string? RefreshToken { get; set; }
}
