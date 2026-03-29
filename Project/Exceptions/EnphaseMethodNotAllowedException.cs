namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 405 Method Not Allowed error returned by the Enphase API.
/// </summary>
public class EnphaseMethodNotAllowedException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseMethodNotAllowedException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseMethodNotAllowedException(string? message, string? details) : base(405, message, details) { }
}
