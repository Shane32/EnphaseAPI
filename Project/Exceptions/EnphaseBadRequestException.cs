namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 400 Bad Request error returned by the Enphase API.
/// </summary>
public class EnphaseBadRequestException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseBadRequestException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseBadRequestException(string? message, string? details) : base(400, message, details) { }
}
