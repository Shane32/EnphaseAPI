namespace Shane32.EnphaseAPI;

public class EnphaseAuthenticationException : EnphaseException
{
    public EnphaseAuthenticationException(string? message, string? details) : base(401, message, details) { }
}
