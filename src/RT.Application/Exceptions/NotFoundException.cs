using RT.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace RT.Application.Exceptions
{
    [Serializable]
    public sealed class NotFoundException : BaseApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
        }
        private NotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        { }
    }
}
