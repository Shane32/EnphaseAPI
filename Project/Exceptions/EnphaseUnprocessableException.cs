namespace Shane32.EnphaseAPI;

public class EnphaseUnprocessableException : EnphaseException
{
    public EnphaseUnprocessableException(string? message, string? details) : base(422, message, details) { }
}
