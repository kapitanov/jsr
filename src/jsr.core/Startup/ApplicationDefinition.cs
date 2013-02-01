using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Startup
{
    public sealed class ApplicationDefinition
    {
        [NotNull] 
        private readonly string _executable;
        [NotNull] 
        private readonly ApplicationProgramParameters _programParameters;
        [NotNull] 
        private readonly ApplicationRuntimeParameters _runtimeParameters;

        public ApplicationDefinition(
            [NotNull] string executable,
            [NotNull] ApplicationProgramParameters programParameters,
            [NotNull] ApplicationRuntimeParameters runtimeParameters)
        {
            Verify.ArgumentNotNullOrEmpty(executable, "executable");
            Verify.ArgumentNotNull(programParameters, "programParameters");
            Verify.ArgumentNotNull(runtimeParameters, "runtimeParameters");

            _executable = executable;
            _programParameters = programParameters;
            _runtimeParameters = runtimeParameters;
        }

        [NotNull] 
        public string Executable { get { return _executable; } }

        [NotNull] 
        public ApplicationProgramParameters ProgramParameters { get { return _programParameters; } }

        [NotNull] 
        public ApplicationRuntimeParameters RuntimeParameters { get { return _runtimeParameters; } }
    }
}