namespace RealEstatePortal.GCommon.Exceptions;

public class AgentDeleteFailureException : Exception
{
    public AgentDeleteFailureException()
    {
    }

    public AgentDeleteFailureException(string message)
        : base(message)
    {
    }

    public AgentDeleteFailureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
