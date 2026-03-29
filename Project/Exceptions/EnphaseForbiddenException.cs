namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 403 Forbidden error returned by the Enphase API.
/// </summary>
public class EnphaseForbiddenException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseForbiddenException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseForbiddenException(string? message, string? details) : base(403, message, details) { }
}
