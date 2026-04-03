namespace Shane32.EnphaseAPI.Models;

/// <summary>
/// Specifies the time interval granularity for telemetry data requests.
/// </summary>
public enum Granularity
{
    /// <summary>Data aggregated by week.</summary>
    Week,

    /// <summary>Data aggregated by day.</summary>
    Day,

    /// <summary>Data aggregated in 15-minute intervals.</summary>
    FifteenMinutes,

    /// <summary>Data aggregated in 5-minute intervals.</summary>
    FiveMinutes,
}

internal static class GranularityExtensions
{
    /// <summary>
    /// Returns the API query-string value for the granularity.
    /// </summary>
    public static string ToApiString(this Granularity granularity)
        => granularity switch {
            Granularity.Week => "week",
            Granularity.Day => "day",
            Granularity.FifteenMinutes => "15mins",
            Granularity.FiveMinutes => "5min",
            _ => throw new System.ArgumentOutOfRangeException(nameof(granularity), granularity, null),
        };
}
