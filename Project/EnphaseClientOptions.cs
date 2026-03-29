using System;

namespace Shane32.EnphaseAPI;

/// <summary>
/// Configuration options for <see cref="EnphaseClient"/>.
/// </summary>
public class EnphaseClientOptions
{
    /// <summary>Gets or sets the Enphase API key used to authenticate requests.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>Gets or sets the number of times to retry a failed request before throwing. Defaults to 0 (no retries).</summary>
    public int RetryCount { get; set; }

    /// <summary>Gets or sets the initial delay between retry attempts. Defaults to <see cref="TimeSpan.Zero"/>.</summary>
    public TimeSpan RetryDelay { get; set; } = TimeSpan.Zero;

    /// <summary>Gets or sets the multiplier applied to <see cref="RetryDelay"/> after each retry (exponential back-off). Defaults to 1.0 (no back-off).</summary>
    public double RetryBackoffMultiplier { get; set; } = 1.0;
}
