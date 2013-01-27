using System;

namespace JavaScript.Runtime
{
    internal static class ConsoleHelper
    {
        public static ForegroundColorToken Foreground(ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            return new ForegroundColorToken(oldColor);
        }

        public struct ForegroundColorToken : IDisposable
        {
            private readonly ConsoleColor _color;

            public ForegroundColorToken(ConsoleColor color)
                : this()
            {
                _color = color;
            }

            public void Dispose()
            {
                Console.ForegroundColor = _color;
            }
        }
    }
}