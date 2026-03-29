namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an error returned by the Enphase API.
/// </summary>
public class EnphaseException : Exception
{
    /// <summary>Gets the HTTP status code returned by the Enphase API.</summary>
    public int HttpStatusCode { get; }

    /// <summary>Gets additional error details returned by the Enphase API, if any.</summary>
    public string? Details { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseException"/>.
    /// </summary>
    /// <param name="httpStatusCode">The HTTP status code returned by the API.</param>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseException(int httpStatusCode, string? message, string? details)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
        Details = details;
    }
}
