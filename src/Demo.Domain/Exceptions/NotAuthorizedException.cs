namespace Demo.Domain.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public readonly string[] Errors;

        public NotAuthorizedException(string error)
        {
            Errors = [error];
        }

        public NotAuthorizedException(string[] errors)
        {
            Errors = errors;
        }
    }
}
