using System;
using System.Diagnostics;

namespace JavaScript.Runtime
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {

                // CommandLineParser tests
                var p = CommandLineParser.Parse("/x=1 /y=\"abc\" script /x=231! /zyx");
               var p1 = CommandLineParser.Parse("/x=1 /y=\"abc\" script");
               var p2 = CommandLineParser.Parse("script /x=231! /zyx");
               var p3 = CommandLineParser.Parse("script");
               var p4 = CommandLineParser.Parse("");

                var script = new Script(args);
                script.Run();

                if (Debugger.IsAttached)
                {
                    Console.ReadLine();
                }
            }
            catch (JsrException exception)
            {
                using (ConsoleHelper.Foreground(ConsoleColor.Red))
                {
                    Console.WriteLine("jsr: {0}", exception.Message);
                    if (exception.InnerException != null)
                    {
                        Console.WriteLine(exception.InnerException.Message);
                    }

                    Console.WriteLine();
                }

                Environment.Exit(-1);
            }
        }
    }
}
