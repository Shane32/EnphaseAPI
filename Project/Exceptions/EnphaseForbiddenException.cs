namespace Shane32.EnphaseAPI;

public class EnphaseForbiddenException : EnphaseException
{
    public EnphaseForbiddenException(string? message, string? details) : base(403, message, details) { }
}
