using System;
using System.Runtime.Serialization;

namespace RT.Domain.Exceptions
{

    [Serializable]
    public sealed class InvalidArgumentException : BaseApplicationException
    {
        public InvalidArgumentException(string message) : base(message)
        {
        }
        private InvalidArgumentException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
    }
}
