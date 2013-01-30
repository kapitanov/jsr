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
             //   : this()
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

                    //if (_index == - 1)
                    {
                        if (!MoveNext())
                        {
                            yield break;
                        }
                    }

                    if (Current != '/')
                    {
                        StepBack();
                        yield break;
                    }

                    char lastCharacter;
                    var name = ReadUntil(out lastCharacter, ' ', '\t', '=');
                    
                    if (lastCharacter == '=')
                    {
                        var value = ReadUntil(out lastCharacter, ' ', '\t');
                        yield return new CommandLineParameter(name, value.Substring(1));
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

            private string ReadUntil(out char lastCharacter, params char[] separators)
            {
                lastCharacter = (char) 0;
                var buffer = new StringBuilder();
                while (MoveNext())
                {
                    if (separators.Contains(Current))
                    {
                        lastCharacter = Current;
                        StepBack();
                        break;
                    }
                    
                    buffer.Append(Current);
                }

                return buffer.ToString();
            }
        }
    }
}