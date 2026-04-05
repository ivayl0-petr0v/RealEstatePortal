namespace RealEstatePortal.GCommon.Exceptions;

public class RealEstateDeleteFailureException : Exception
{
    public RealEstateDeleteFailureException()
    {
    }

    public RealEstateDeleteFailureException(string message)
        : base(message)
    {
    }

    public RealEstateDeleteFailureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
