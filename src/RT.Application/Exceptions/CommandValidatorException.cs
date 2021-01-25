using RT.Domain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace RT.Application.Exceptions
{
    [Serializable]
    public sealed class CommandValidatorException : BaseApplicationException
    {
        public CommandValidatorException(string message) : base(message)
        {
        }
        private CommandValidatorException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {}
    }
}
