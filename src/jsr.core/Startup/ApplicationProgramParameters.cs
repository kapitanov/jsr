using System.Collections.Generic;
using System.Linq;
using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    public sealed class ApplicationProgramParameters
    {
        [NotNull]
        private readonly HashSet<string> _flags;
        [NotNull]
        private readonly Dictionary<string, string> _parameters;

        public ApplicationProgramParameters([NotNull] HashSet<string> flags, [NotNull] Dictionary<string, string> parameters)
        {
            Verify.ArgumentNotNull(flags, "flags");
            Verify.ArgumentNotNull(parameters, "parameters");

            _flags = flags;
            _parameters = parameters;
        }

        [NotNull]
        public HashSet<string> Flags { get { return _flags; } }

        [NotNull]
        public Dictionary<string, string> Parameters { get { return _parameters; } }

        [NotNull]
        public static ApplicationProgramParameters FromCommandLine([NotNull] CommandLineParameters commandLine)
        {
            Verify.ArgumentNotNull(commandLine, "commandLine");

            return new ApplicationProgramParameters(
                GetFlags(commandLine),
                GetParameters(commandLine));
        }

        [NotNull]
        private static HashSet<string> GetFlags([NotNull] CommandLineParameters commandLine)
        {
            return new HashSet<string>(GetFlagsIterator(commandLine));
        }

        [NotNull]
        private static IEnumerable<string> GetFlagsIterator([NotNull] CommandLineParameters commandLine)
        {
            return from value in commandLine.ProgramParameters.Values
                   where !value.HasValue
                   select value.Name;
        }

        [NotNull]
        private static Dictionary<string, string> GetParameters([NotNull] CommandLineParameters commandLine)
        {
            return (from value in commandLine.ProgramParameters.Values
                    where value.HasValue
                    select value)
                .ToDictionary(_ => _.Name, _ => _.Value);
        }
    }
}