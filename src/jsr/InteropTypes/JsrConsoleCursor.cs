using System;

namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrConsoleCursor
    {
        public int x
        {
            get { return Console.CursorLeft; }
            set { Console.CursorLeft = value; }
        }

        public int y
        {
            get { return Console.CursorTop; }
            set { Console.CursorTop = value; }
        }
    }
}