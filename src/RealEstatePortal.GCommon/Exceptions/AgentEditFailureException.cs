namespace RealEstatePortal.GCommon.Exceptions
{
    public class AgentEditFailureException : Exception
    {
        public AgentEditFailureException()
        {
        }

        public AgentEditFailureException(string message)
            : base(message)
        {
        }

        public AgentEditFailureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
