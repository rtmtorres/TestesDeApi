using System;
using System.Runtime.Serialization;

namespace RT.Domain.Exceptions
{
    [Serializable]
    public abstract class BaseApplicationException : Exception
    {
        protected BaseApplicationException(string message) : base(message)
        {
        }

        protected BaseApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
