namespace RealEstatePortal.GCommon.Exceptions
{
    public class AgentCreateFailureException : Exception
    {
        public AgentCreateFailureException()
        {
        }

        public AgentCreateFailureException(string message)
            : base(message)
        {
        }

        public AgentCreateFailureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
