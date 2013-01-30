using System.Collections.Generic;

namespace JavaScript.Runtime
{
    public sealed class CommandLineParameters
    {
        private readonly string _executablePath;
        private readonly Dictionary<string, CommandLineParameter> _runtimeParameters;
        private readonly Dictionary<string, CommandLineParameter> _programParameters;

        public CommandLineParameters(
            string executablePath,
            Dictionary<string, CommandLineParameter> runtimeParameters,
            Dictionary<string, CommandLineParameter> programParameters)
        {
            _executablePath = executablePath;
            _runtimeParameters = runtimeParameters;
            _programParameters = programParameters;
        }

        public bool IsValid { get { return !string.IsNullOrWhiteSpace(_executablePath); } }

        public string ExecutablePath { get { return _executablePath; } }

        public Dictionary<string, CommandLineParameter> RuntimeParameters { get { return _runtimeParameters; } }

        public Dictionary<string, CommandLineParameter> ProgramParameters { get { return _programParameters; } }

        public static CommandLineParameters Merge(
            CommandLineParameters lowPriorityParameters,
            CommandLineParameters hiPriorityParameters)
        {
            return new CommandLineParameters(
                hiPriorityParameters._executablePath ?? lowPriorityParameters._executablePath,
                MergeDictionaries(lowPriorityParameters._runtimeParameters, hiPriorityParameters._runtimeParameters),
                MergeDictionaries(lowPriorityParameters._programParameters, hiPriorityParameters._programParameters));
        }

        private static Dictionary<string, CommandLineParameter> MergeDictionaries(
            Dictionary<string, CommandLineParameter> lowPriorityParameters,
            Dictionary<string, CommandLineParameter> hiPriorityParameters)
        {
            var result = new Dictionary<string, CommandLineParameter>();

            foreach (var parameter in lowPriorityParameters.Values)
            {
                result[parameter.Name] = parameter;
            }

            foreach (var parameter in hiPriorityParameters.Values)
            {
                result[parameter.Name] = parameter;
            }

            return result;
        }
    }
}