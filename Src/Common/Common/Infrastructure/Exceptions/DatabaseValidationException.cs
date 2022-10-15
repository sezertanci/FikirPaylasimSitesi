using System.Runtime.Serialization;

namespace Common.Infrastructure.Exceptions
{
    public class DatabaseValidationException : Exception
    {
        public DatabaseValidationException()
        {
        }

        public DatabaseValidationException(string message = null) : base(message)
        {
        }

        public DatabaseValidationException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }

        protected DatabaseValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
