using System;
using System.Diagnostics;
using JavaScript.Runtime.Execution;
using JavaScript.Runtime.Startup;
using JavaScript.Runtime.Util;

namespace JavaScript.Runtime
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Run(args);

                if (Debugger.IsAttached)
                {
                    using (ConsoleHelper.Foreground(ConsoleColor.DarkGray))
                    {
                        Console.WriteLine("Press <Enter> to exit...");
                    }

                    Console.ReadLine();
                }
            }
            catch (JsrStartupException exception)
            {
                PrintError("jsr-startup", exception);
            }
            catch (JsrRuntimeException exception)
            {
                PrintError("jsr", exception);
            }
            catch (Exception exception)
            {
                PrintError("runtime", exception);
            }
        }

        private static void Run(string[] args)
        {
            var commandLine = CommandLineParser.Parse(args);

            var applicationDefinition = ApplicationDefinitionLoader.FromCommandLine(commandLine);

            using (var application = new Application(applicationDefinition))
            {
                application.Run();
            }
        }

        private static void PrintError(string source, Exception exception)
        {
            using (ConsoleHelper.Foreground(ConsoleColor.Red))
            {
                Console.WriteLine("{0}: {1}", source, exception.Message);
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
