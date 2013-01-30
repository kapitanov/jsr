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
