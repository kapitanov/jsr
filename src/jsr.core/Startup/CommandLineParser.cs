using System.Collections.Generic;
using System.Linq;
using System.Text;
using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    public static class CommandLineParser
    {
        [NotNull] 
        public static CommandLineParameters Parse([NotNull] string[] args)
        {
            Verify.ArgumentNotNull(args, "args");

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

        [NotNull] 
        public static CommandLineParameters Parse([NotNull] string commandline)
        {
            Verify.ArgumentNotNull(commandline, "commandline");

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
}