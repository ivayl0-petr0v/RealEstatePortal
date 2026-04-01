namespace RealEstatePortal.GCommon.Exceptions
{
    public class DbEntityEditFailureException : Exception
    {
        public DbEntityEditFailureException()
        {
        }

        public DbEntityEditFailureException(string message)
            : base(message)
        {
        }

        public DbEntityEditFailureException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
