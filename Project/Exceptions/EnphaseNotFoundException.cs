namespace Shane32.EnphaseAPI;

public class EnphaseNotFoundException : EnphaseException
{
    public EnphaseNotFoundException(string? message, string? details) : base(404, message, details) { }
}
