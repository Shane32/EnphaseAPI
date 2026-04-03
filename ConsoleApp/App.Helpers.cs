using System.Text.Json;
using Shane32.EnphaseAPI.Models;

internal sealed partial class App
{
    private static readonly JsonSerializerOptions _printJsonOptions = new() { WriteIndented = true };
    private static string Prompt(string message, bool required = false)
    {
        while (true) {
            Console.Write(message);
            var value = Console.ReadLine()?.Trim() ?? string.Empty;
            if (!required || value.Length > 0)
                return value;
            Console.WriteLine("This field is required.");
        }
    }

    private static string? PromptOptional(string message)
    {
        Console.Write($"{message} (optional, press Enter to skip): ");
        var value = Console.ReadLine()?.Trim();
        return string.IsNullOrEmpty(value) ? null : value;
    }

    private static int PromptInt(string message)
    {
        while (true) {
            Console.Write(message);
            var input = Console.ReadLine()?.Trim();
            if (int.TryParse(input, out int value))
                return value;
            Console.WriteLine("Invalid integer. Please try again.");
        }
    }

    private static int? PromptIntOptional(string message)
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

    private static DateTimeOffset PromptTimestamp(string message)
    {
        while (true) {
            Console.Write($"{message} (e.g. 2025-01-15T08:30:00Z): ");
            var input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input) &&
                DateTimeOffset.TryParse(input, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out var dto))
                return dto;
            Console.WriteLine("Invalid date/time. Please try again.");
        }
    }

    private static DateTimeOffset? PromptTimestampOptional(string message)
    {
        while (true) {
            Console.Write($"{message} (e.g. 2025-01-15T08:30:00Z, optional, press Enter to skip): ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
                return null;
            if (DateTimeOffset.TryParse(input, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out var dto))
                return dto;
            Console.WriteLine("Invalid date/time. Please try again or press Enter to skip.");
        }
    }

    private static DateTimeOffset? PromptDateOptional(string message)
    {
        while (true) {
            Console.Write($"{message} (e.g. 2025-01-15, optional, press Enter to skip): ");
            var input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input))
                return null;
            if (DateTimeOffset.TryParse(input, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out var dto))
                return dto;
            Console.WriteLine("Invalid date. Please try again or press Enter to skip.");
        }
    }

    private static DateTimeOffset PromptDate(string message)
    {
        while (true) {
            Console.Write($"{message} (e.g. 2025-01-15): ");
            var input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input) &&
                DateTimeOffset.TryParse(input, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AssumeUniversal | System.Globalization.DateTimeStyles.AdjustToUniversal, out var dto))
                return dto;
            Console.WriteLine("Invalid date. Please try again.");
        }
    }

    private static Granularity? PromptGranularityOptional(string message)
    {
        while (true) {
            Console.Write($"{message} (week/day/15mins/5min, optional, press Enter to skip): ");
            var input = Console.ReadLine()?.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(input))
                return null;
            switch (input) {
                case "week":
                    return Granularity.Week;
                case "day":
                    return Granularity.Day;
                case "15mins":
                    return Granularity.FifteenMinutes;
                case "5min":
                    return Granularity.FiveMinutes;
                default:
                    Console.WriteLine("Invalid granularity. Valid values: week, day, 15mins, 5min. Please try again or press Enter to skip.");
                    break;
            }
        }
    }

    private static bool? PromptBoolOptional(string message)
    {
        Console.Write($"{message} (y/n, optional, press Enter to skip): ");
        var input = Console.ReadLine()?.Trim().ToLowerInvariant();
        return input == "y" ? true : input == "n" ? false : null;
    }

    private static void PrintResponse(object response)
    {
        var json = JsonSerializer.Serialize(response, _printJsonOptions);
        Console.WriteLine(json);
    }
}
