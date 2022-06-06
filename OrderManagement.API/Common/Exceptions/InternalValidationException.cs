using System.Runtime.Serialization;

namespace OrderManagement.API.Common.Exceptions
{
    [Serializable]
    public class InternalValidationException : Exception
    {
        public InternalValidationException() { }

        public InternalValidationException(string message) : base(message) { }

        public InternalValidationException(string message, Exception inner) : base(message, inner) { }

        protected InternalValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext) { }
    }
}
