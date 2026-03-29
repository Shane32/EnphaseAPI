namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 501 Not Implemented error returned by the Enphase API.
/// </summary>
public class EnphaseNotImplementedException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseNotImplementedException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseNotImplementedException(string? message, string? details) : base(501, message, details) { }
}
