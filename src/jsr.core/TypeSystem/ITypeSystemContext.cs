using JavaScript.Runtime.Execution;
using JavaScript.Runtime.Startup;
using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    public interface ITypeSystemContext
    {
        [NotNull] 
        ApplicationDefinition Application { get; }

        [NotNull] 
        PathResolver PathResolver { get; }

        [CanBeNull]
        object EvaluateJavascript([NotNull] string source);

        void RegisterInteropObject([NotNull] string name, [NotNull] object interopObject);
    }
}