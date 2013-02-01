using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaScript.Runtime
{
    public static class CommandLineParser
    {
        public static CommandLineParameters Parse(string[] args)
        {
            var builder = new StringBuilder();
            for (var index = 0; index < args.Length; index++)
            {
                var arg = args[index];
                builder.Append(arg);

                if (index != args.Length - 1)
                {
                    builder.Append(' ');
                }
            }

            var commandline = builder.ToString();
            return Parse(commandline);
        }

        public static CommandLineParameters Parse(string commandline)
        {
            var context = new ParserContext(commandline);
            return context.Parse();
        }

        private class ParserContext
        {
            private readonly string _commandline;
            private int _index;

            public ParserContext(string commandline)
            {
                _commandline = commandline;
                _index = -1;
            }

            public CommandLineParameters Parse()
            {
                var runtimeParameters = ReadParameters();
                var executableName = ReadExecutableName();
                var programParameters = ReadParameters();

                return new CommandLineParameters(
                    executableName,
                    runtimeParameters,
                    programParameters);
            }

            private char Current { get { return _commandline[_index]; } }

            private bool EndOfStream { get { return _index >= _commandline.Length; } }

            private bool MoveNext()
            {
                _index++;
                return _index < _commandline.Length;
            }

            private void StepBack()
            {
                _index--;
            }

            private Dictionary<string, CommandLineParameter> ReadParameters()
            {
                var parameters = new Dictionary<string, CommandLineParameter>();
                foreach (var parameter in ReadParametersIterator())
                {
                    parameters[parameter.Name] = parameter;
                }
                return parameters;
            }

            private IEnumerable<CommandLineParameter> ReadParametersIterator()
            {
                while (true)
                {
                    SkipWhitespaces();

                    if (EndOfStream)
                    {
                        yield break;
                    }

                    if (!MoveNext())
                    {
                        yield break;
                    }

                    if (Current != '/')
                    {
                        StepBack();
                        yield break;
                    }

                    char lastCharacter;
                    var name = ReadUntil(QuotedValues.Disable, out lastCharacter, ' ', '\t', '=');

                    if (lastCharacter == '=')
                    {
                        if (!MoveNext())
                        {
                            // Valueless parameter
                            yield return new CommandLineParameter(name, string.Empty);
                        }

                        var value = ReadUntil(QuotedValues.Enable, out lastCharacter, ' ', '\t');
                        yield return new CommandLineParameter(name, value);
                    }
                    else
                    {
                        // Valueless parameter
                        yield return new CommandLineParameter(name, null);
                    }
                }
            }

            private string ReadExecutableName()
            {
                var buffer = new StringBuilder();
                while (MoveNext())
                {
                    if (char.IsWhiteSpace(Current))
                    {
                        StepBack();
                        break;
                    }

                    buffer.Append(Current);
                }

                return buffer.ToString();
            }

            private void SkipWhitespaces()
            {
                while (MoveNext())
                {
                    if (!char.IsWhiteSpace(Current))
                    {
                        StepBack();
                        break;
                    }
                }
            }

            private enum QuotedValues
            {
                Disable,
                Enable
            }

            private string ReadUntil(QuotedValues quotedValues, out char lastCharacter, params char[] separators)
            {
                var count = 0;
                var isQuoted = false;
                lastCharacter = (char)0;
                var buffer = new StringBuilder();

                while (MoveNext())
                {
                    if (quotedValues == QuotedValues.Enable &&
                        count == 0 &&
                        Current == '\"')
                    {
                        separators = new[] { '\"' };
                        isQuoted = true;
                    }
                    else
                    {
                        if (separators.Contains(Current))
                        {
                            lastCharacter = Current;
                            if (!isQuoted)
                            {
                                StepBack();
                            }
                            break;
                        }

                        buffer.Append(Current);
                    }

                    count++;
                }
                
                return buffer.ToString();
            }
        }
    }

    public static class ApplicationDefinitionLoader
    {
        
        public static ApplicationDefinition FromCommandLine(CommandLineParameters commandLine)
        {
            string parameter;
            if (commandLine.RuntimeParameters.TryGetValue("", out parameter))
            {
                
            }
            var libraryReferences = 

            return new ApplicationDefinition(
                commandLine.ExecutablePath,
                GetLibraryReferences(commandLine),

                
                );
        }

    }

    public sealed class ApplicationDefinition
    {
        private readonly string _executable;
        private readonly string[] _libraries;
        private readonly Dictionary<string, CommandLineParameter> _arguments;
        private readonly ApplicationRuntimeParameters _runtimeParameters;

        public ApplicationDefinition(
            string executable, 
            string[] libraries,
            Dictionary<string, CommandLineParameter> arguments,
            ApplicationRuntimeParameters runtimeParameters)
        {
            _executable = executable;
            _libraries = libraries;
            _arguments = arguments;
            _runtimeParameters = runtimeParameters;
        }

        public string Executable { get { return _executable; } }

        public string[] Libraries { get { return _libraries; } }

        public Dictionary<string, CommandLineParameter> Arguments { get { return _arguments; } }

        public ApplicationRuntimeParameters RuntimeParameters { get { return _runtimeParameters; } }
    }

    public sealed class ApplicationProgramParameters
    {
        private readonly HashSet<string> _flags;
        private readonly Dictionary<string, string> _parameters;

        public ApplicationProgramParameters(HashSet<string> flags, Dictionary<string, string> parameters)
        {
            _flags = flags;
            _parameters = parameters;
        }

        public HashSet<string> Flags { get { return _flags; } }

        public Dictionary<string, string> Parameters { get { return _parameters; } }

        public static ApplicationProgramParameters FromCommandLine(CommandLineParameters commandLine)
        {
            return new ApplicationProgramParameters(
               new HashSet<string>(GetFlagsIterator(commandLine)),
               /* */
                );
        }

        private static IEnumerable<string> GetFlagsIterator(CommandLineParameters commandLine)
        {
            return from value in commandLine.ProgramParameters.Values
                   where !value.HasValue 
                   select value.Name;
        }
    }

    public sealed class ApplicationRuntimeParameters
    {
        private static readonly string[] _EmptyLibraryReferences = new string[0];

        private readonly string[] _libraryReferences;

        public ApplicationRuntimeParameters(string[] libraryReferences)
        {
            _libraryReferences = libraryReferences;
        }

        public string[] LibraryReferences { get { return _libraryReferences; } }

        public static ApplicationRuntimeParameters FromCommandLine(CommandLineParameters commandLine)
        {
            return new ApplicationRuntimeParameters(
                GetLibraryReferences(commandLine)
                );
        }

        private static string[] GetLibraryReferences(CommandLineParameters commandLine)
        {
            CommandLineParameter parameter;
            if (commandLine.RuntimeParameters.TryGetValue("", out parameter))
            {
                if (parameter.HasValue &&
                    !string.IsNullOrWhiteSpace(parameter.Value))
                {
                    var libraryReferences = parameter.Value
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    return libraryReferences;
                }
            }

            return _EmptyLibraryReferences;
        }
    }
}