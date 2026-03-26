namespace RealEstatePortal.GCommon.Exceptions
{
    public class DbEntityCreateFailureException : Exception
    {
        public DbEntityCreateFailureException()
        {
        }
        public DbEntityCreateFailureException(string message)
            : base(message)
        {
        }
        public DbEntityCreateFailureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
