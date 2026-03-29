namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 429 Too Many Requests error returned by the Enphase API.
/// </summary>
public class EnphaseRateLimitException : EnphaseException
{
    /// <summary>Gets the rate-limit period (e.g. "minute", "day").</summary>
    public string? Period { get; }

    /// <summary>Gets the Unix timestamp at which the current rate-limit period started.</summary>
    public long? PeriodStart { get; }

    /// <summary>Gets the Unix timestamp at which the current rate-limit period ends.</summary>
    public long? PeriodEnd { get; }

    /// <summary>Gets the maximum number of requests allowed in the rate-limit period.</summary>
    public int? Limit { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseRateLimitException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    /// <param name="period">The rate-limit period name.</param>
    /// <param name="periodStart">The Unix timestamp when the current period started.</param>
    /// <param name="periodEnd">The Unix timestamp when the current period ends.</param>
    /// <param name="limit">The maximum allowed requests for the period.</param>
    public EnphaseRateLimitException(string? message, string? details, string? period, long? periodStart, long? periodEnd, int? limit)
        : base(429, message, details)
    {
        Period = period;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        Limit = limit;
    }
}
