namespace Demo.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public readonly string[] Errors;

        public DomainException(string error)
        {
            Errors = [error];
        }

        public DomainException(string[] errors)
        {
            Errors = errors;
        }
    }
}
