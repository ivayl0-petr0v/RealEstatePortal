namespace RealEstatePortal.GCommon.Exceptions;

public class RealEstateCreateFailureException : Exception
{
    public RealEstateCreateFailureException()
    {
    }

    public RealEstateCreateFailureException(string message)
            : base(message)
    {
    }

    public RealEstateCreateFailureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
