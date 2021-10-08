using System;

namespace NATS.POC.Common.Exceptions
{
    /// <summary>
    /// Exception type for application exceptions
    /// </summary>
    public class ApplicationException : Exception
    {
        public ApplicationException()
        { }

        public ApplicationException(string message)
            : base(message)
        { }

        public ApplicationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
