using System.Runtime.Serialization;

namespace expenses.core.CustomException
{
    public class InvalidUsernamePaswwordException : Exception
    {
        public InvalidUsernamePaswwordException()
        {
        }

        public InvalidUsernamePaswwordException(string? message) : base(message)
        {
        }

        public InvalidUsernamePaswwordException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidUsernamePaswwordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
