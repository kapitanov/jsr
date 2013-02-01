using System;
using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    public sealed class ApplicationRuntimeParameters
    {
        private static readonly string[] _EmptyLibraryReferences = new string[0];

        [NotNull]
        private readonly string[] _libraryReferences;

        public ApplicationRuntimeParameters([NotNull] string[] libraryReferences)
        {
            Verify.ArgumentNotNull(libraryReferences, "libraryReferences");

            _libraryReferences = libraryReferences;
        }

        [NotNull]
        public string[] LibraryReferences { get { return _libraryReferences; } }

        [NotNull]
        public static ApplicationRuntimeParameters FromCommandLine([NotNull] CommandLineParameters commandLine)
        {
            return new ApplicationRuntimeParameters(
                GetLibraryReferences(commandLine)
                );
        }

        [NotNull]
        private static string[] GetLibraryReferences([NotNull] CommandLineParameters commandLine)
        {
            CommandLineParameter parameter;
            if (commandLine.RuntimeParameters.TryGetValue(JsrCommandLine.LibrariesParameterName, out parameter))
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