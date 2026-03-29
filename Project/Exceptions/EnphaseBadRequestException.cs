namespace Shane32.EnphaseAPI;

public class EnphaseBadRequestException : EnphaseException
{
    public EnphaseBadRequestException(string? message, string? details) : base(400, message, details) { }
}
