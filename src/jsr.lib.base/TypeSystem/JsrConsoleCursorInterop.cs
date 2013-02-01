using System;

namespace JavaScript.Runtime.TypeSystem
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrConsoleCursorInterop
    {
        public int x()
        {
            return Console.CursorLeft;
        }

        public void x(int value)
        {
            Console.CursorLeft = value;
        }

        public int y()
        {
            return Console.CursorTop;
        }

        public void y(int value)
        {
            Console.CursorTop = value;
        }

        public bool visible()
        {
            return Console.CursorVisible;
        }

        public void visible(bool value)
        {
            Console.CursorVisible = value;
        }
    }
}