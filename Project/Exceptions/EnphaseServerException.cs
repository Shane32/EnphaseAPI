namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 500 Internal Server Error returned by the Enphase API.
/// </summary>
public class EnphaseServerException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseServerException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseServerException(string? message, string? details) : base(500, message, details) { }
}
