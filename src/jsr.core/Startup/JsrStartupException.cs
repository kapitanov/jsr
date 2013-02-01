using System;
using System.Runtime.Serialization;

namespace JavaScript.Runtime.Startup
{
    [Serializable]
    public class JsrStartupException : JsrBaseException
    {
        public JsrStartupException()
        {
        }

        public JsrStartupException(string message) : base(message)
        {
        }

        public JsrStartupException(string message, Exception inner) : base(message, inner)
        {
        }

        protected JsrStartupException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}