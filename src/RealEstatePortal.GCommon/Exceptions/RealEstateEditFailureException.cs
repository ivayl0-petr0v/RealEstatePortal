namespace RealEstatePortal.GCommon.Exceptions;

public class RealEstateEditFailureException : Exception
{
    public RealEstateEditFailureException()
    {

    }

    public RealEstateEditFailureException(string message)
        : base(message)
    {
    }

    public RealEstateEditFailureException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
