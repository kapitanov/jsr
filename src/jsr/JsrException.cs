using System;
using System.Runtime.Serialization;

namespace JavaScript.Runtime
{
    [Serializable]
    public class JsrException : Exception
    {
        public JsrException()
        {
        }

        public JsrException(string message)
            : base(message)
        {
        }

        public JsrException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected JsrException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}