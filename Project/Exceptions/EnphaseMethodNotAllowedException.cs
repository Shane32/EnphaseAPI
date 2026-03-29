namespace Shane32.EnphaseAPI;

public class EnphaseMethodNotAllowedException : EnphaseException
{
    public EnphaseMethodNotAllowedException(string? message, string? details) : base(405, message, details) { }
}
