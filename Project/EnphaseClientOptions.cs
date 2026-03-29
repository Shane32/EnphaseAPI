using System;

namespace Shane32.EnphaseAPI;

public class EnphaseClientOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 0;
    public TimeSpan RetryDelay { get; set; } = TimeSpan.Zero;
    public double RetryBackoffMultiplier { get; set; } = 1.0;
}
