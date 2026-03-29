namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 404 Not Found error returned by the Enphase API.
/// </summary>
public class EnphaseNotFoundException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseNotFoundException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseNotFoundException(string? message, string? details) : base(404, message, details) { }
}
