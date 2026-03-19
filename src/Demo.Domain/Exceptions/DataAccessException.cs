namespace Demo.Domain.Exceptions
{
    public class DataAccessException : Exception
    {
        public readonly string[] Errors;

        public DataAccessException(string error)
        {
            Errors = [error];
        }

        public DataAccessException(string[] errors)
        {
            Errors = errors;
        }
    }
}
