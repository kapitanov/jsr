using System;

namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrConsole
    {
        private readonly Script _script;

        public JsrConsole(Script script)
        {
            _script = script;
        }

        public string[] args()
        {
            return _script.Arguments;
        }

        public void print(string message)
        {
            Console.Write(message);
        }

        public void printf(string format, object args)
        {
            var message = JsrHelper.FormatString(format, args);
            Console.WriteLine(message);
        }

        public string readln()
        {
            return Console.ReadLine();
        }
    }
}