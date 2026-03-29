namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 422 Unprocessable Entity error returned by the Enphase API.
/// </summary>
public class EnphaseUnprocessableException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseUnprocessableException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseUnprocessableException(string? message, string? details) : base(422, message, details) { }
}
