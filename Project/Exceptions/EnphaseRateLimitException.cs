namespace Shane32.EnphaseAPI;

public class EnphaseRateLimitException : EnphaseException
{
    public string? Period { get; }
    public long? PeriodStart { get; }
    public long? PeriodEnd { get; }
    public int? Limit { get; }

    public EnphaseRateLimitException(string? message, string? details, string? period, long? periodStart, long? periodEnd, int? limit)
        : base(429, message, details)
    {
        Period = period;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        Limit = limit;
    }
}
