using System;
using JavaScript.Runtime.Util;

namespace JavaScript.Runtime.TypeSystem
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrConsoleInterop
    {
        private readonly JsrConsoleCursorInterop _cursor = new JsrConsoleCursorInterop();

        public JsrConsoleCursorInterop cursor  { get { return _cursor; } }
        
        public void print(string message)
        {
            Console.Write(message);
        }

        public void printf(string format, object args)
        {
            var message = JsrHelper.FormatString(format, args);
            Console.Write(message);
        }

        public void error(string message)
        {
            using (ConsoleHelper.Foreground(ConsoleColor.Red))
            {
                Console.Write(message);
            }
        }

        public void errorf(string format, object args)
        {
            using (ConsoleHelper.Foreground(ConsoleColor.Red))
            {
                var message = JsrHelper.FormatString(format, args);
                Console.Write(message);
            }
        }

        public void warn(string message)
        {
            using (ConsoleHelper.Foreground(ConsoleColor.Yellow))
            {
                Console.Write(message);
            }
        }

        public void warnf(string format, object args)
        {
            using (ConsoleHelper.Foreground(ConsoleColor.Yellow))
            {
                var message = JsrHelper.FormatString(format, args);
                Console.Write(message);
            }
        }

        public string readln()
        {
            return Console.ReadLine();
        }

        public int read()
        {
            return (int)Console.ReadKey().Key;
        }

        public void clear()
        {
            Console.Clear();
        }

        private const string RedColor = "red";
        private const string DarkRedColor = "dark-red";
        private const string GreenColor = "green";
        private const string DarkGreenColor = "dark-green";
        private const string BlueColor = "blue";
        private const string DarkBlueColor = "dark-blue";
        private const string CyanColor = "cyan";
        private const string DarkCyanColor = "dark-cyan";
        private const string YellowColor = "yellow";
        private const string DarkYellowColor = "dark-yellow";
        private const string MagentaColor = "magenta";
        private const string DarkMagentaColor = "dark-magenta";
        private const string GrayColor = "gray";
        private const string DarkGrayColor = "dark-gray";
        private const string BlackColor = "black";
        private const string WhiteColor = "white";

        public string RED { get { return RedColor; } }
        public string DARK_RED { get { return DarkRedColor; } }
        public string GREEN { get { return GreenColor; } }
        public string DARK_GREEN { get { return DarkGreenColor; } }
        public string BLUE { get { return BlueColor; } }
        public string DARK_BLUE { get { return DarkBlueColor; } }
        public string CYAN { get { return CyanColor; } }
        public string DARK_CYAN { get { return DarkCyanColor; } }
        public string YELLOW { get { return YellowColor; } }
        public string DARK_YELLOW { get { return DarkYellowColor; } }
        public string MAGENTA { get { return MagentaColor; } }
        public string DARK_MAGENTA { get { return DarkMagentaColor; } }
        public string GRAY { get { return GrayColor; } }
        public string DARK_GRAY { get { return DarkGrayColor; } }
        public string WHITE { get { return WhiteColor; } }
        public string BLACK { get { return BlackColor; } }

        public string foreground()
        {
            return GetColorName(Console.ForegroundColor);
        }

        public string background()
        {
            return GetColorName(Console.BackgroundColor);
        }

        public void foreground(string color)
        {
            Console.ForegroundColor = ParseColor(color);
        }

        public void background(string color)
        {
            Console.BackgroundColor = ParseColor(color);
        }

        private static ConsoleColor ParseColor(string colorName)
        {
            switch (colorName)
            {
                case RedColor: return ConsoleColor.Red;
                case DarkRedColor: return ConsoleColor.DarkRed;
                case GreenColor: return ConsoleColor.Green;
                case DarkGreenColor: return ConsoleColor.DarkGreen;
                case BlueColor: return ConsoleColor.Blue;
                case DarkBlueColor: return ConsoleColor.DarkBlue;
                case CyanColor: return ConsoleColor.Cyan;
                case DarkCyanColor: return ConsoleColor.DarkCyan;
                case YellowColor: return ConsoleColor.Yellow;
                case DarkYellowColor: return ConsoleColor.DarkYellow;
                case MagentaColor: return ConsoleColor.Magenta;
                case DarkMagentaColor: return ConsoleColor.DarkMagenta;
                case GrayColor: return ConsoleColor.Gray;
                case DarkGrayColor: return ConsoleColor.DarkGray;
                case BlackColor: return ConsoleColor.Black;
                case WhiteColor: return ConsoleColor.White;
                default:
                    throw new ArgumentOutOfRangeException("colorName");
            }
        }

        private static string GetColorName(ConsoleColor color)
        {
            switch (color)
            {
                case ConsoleColor.Black: return BlackColor;
                case ConsoleColor.DarkBlue: return DarkBlueColor;
                case ConsoleColor.DarkGreen: return DarkGreenColor;
                case ConsoleColor.DarkCyan: return DarkCyanColor;
                case ConsoleColor.DarkRed: return DarkRedColor;
                case ConsoleColor.DarkMagenta: return DarkMagentaColor;
                case ConsoleColor.DarkYellow: return DarkYellowColor;
                case ConsoleColor.Gray: return GrayColor;
                case ConsoleColor.DarkGray: return DarkGrayColor;
                case ConsoleColor.Blue: return BlueColor;
                case ConsoleColor.Green: return GreenColor;
                case ConsoleColor.Cyan: return CyanColor;
                case ConsoleColor.Red: return RedColor;
                case ConsoleColor.Magenta: return MagentaColor;
                case ConsoleColor.Yellow: return YellowColor;
                case ConsoleColor.White: return WhiteColor;
                default:
                    throw new ArgumentOutOfRangeException("color");
            }
        }
    }
}