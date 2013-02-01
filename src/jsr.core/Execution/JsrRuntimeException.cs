using System;
using System.Runtime.Serialization;

namespace JavaScript.Runtime.Execution
{
    [Serializable]
    public class JsrRuntimeException : JsrBaseException
    {
        public JsrRuntimeException()
        {
        }

        public JsrRuntimeException(string message)
            : base(message)
        {
        }

        public JsrRuntimeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected JsrRuntimeException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}