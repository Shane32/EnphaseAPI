namespace Shane32.EnphaseAPI;

public class EnphaseNotImplementedException : EnphaseException
{
    public EnphaseNotImplementedException(string? message, string? details) : base(501, message, details) { }
}
