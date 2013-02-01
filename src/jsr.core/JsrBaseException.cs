using System;
using System.Runtime.Serialization;

namespace JavaScript.Runtime
{
    [Serializable]
    public abstract class JsrBaseException : Exception
    {
        protected JsrBaseException()
        {
        }

        protected JsrBaseException(string message)
            : base(message)
        {
        }

        protected JsrBaseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected JsrBaseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}