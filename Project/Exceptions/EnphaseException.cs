namespace Shane32.EnphaseAPI;

public class EnphaseException : Exception
{
    public int HttpStatusCode { get; }
    public string? Details { get; }

    public EnphaseException(int httpStatusCode, string? message, string? details)
        : base(message)
    {
        HttpStatusCode = httpStatusCode;
        Details = details;
    }
}
