namespace Shane32.EnphaseAPI;

/// <summary>
/// Represents an HTTP 401 Unauthorized error returned by the Enphase API.
/// </summary>
public class EnphaseAuthenticationException : EnphaseException
{
    /// <summary>
    /// Initializes a new instance of <see cref="EnphaseAuthenticationException"/>.
    /// </summary>
    /// <param name="message">The error message returned by the API.</param>
    /// <param name="details">Additional error details returned by the API.</param>
    public EnphaseAuthenticationException(string? message, string? details) : base(401, message, details) { }
}
