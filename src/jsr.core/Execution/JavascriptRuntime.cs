using System;
using JavaScript.Runtime.TypeSystem;
using JetBrains.Annotations;
using Noesis.Javascript;

namespace JavaScript.Runtime.Execution
{
    internal sealed class JavascriptRuntime : IDisposable
    {
        [NotNull]
        private readonly JavascriptContext _context;

        public JavascriptRuntime()
        {
            _context = new JavascriptContext();

            JavascriptRuntimeInitializer.Initialize(this);
        }

        public void RegisterTypeSystem(ITypeSystemContext context, ITypeSystemExtension typeSystem)
        {
            var interoperableTypeSystem = typeSystem as IInteroperableTypeSystem;
            if (interoperableTypeSystem == null)
            {
                return;
            }

            var interopObject = interoperableTypeSystem.CreateInteropObject(context);
            var hiddenObjectName = GenerateHiddenObjectName(interopObject);
            _context.SetParameter(hiddenObjectName, interopObject.Object);
            EvaluateJavascript(string.Format("jsr.register_namespace(\"{0}\", {1});", interopObject.Namespace, hiddenObjectName));
        }

        public object EvaluateJavascript(string source)
        {
            var result = _context.Run(source);
            return result;
        }

        public void RegisterInteropObject(string name, object interopObject)
        {
            _context.SetParameter(name, interopObject);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private static string GenerateHiddenObjectName(InteropObject interopObject)
        {
            var name = interopObject.Namespace.Replace(".", "$");
            var fullName = "$_hidden_object_$jsr$" + name;
            return fullName;
        }
    }
}