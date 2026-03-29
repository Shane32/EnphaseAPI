namespace Shane32.EnphaseAPI;

public class EnphaseServerException : EnphaseException
{
    public EnphaseServerException(string? message, string? details) : base(500, message, details) { }
}
