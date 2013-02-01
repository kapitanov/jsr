using System.ComponentModel.Composition;

namespace JavaScript.Runtime.TypeSystem
{
    [Export(typeof(ITypeSystemExtension))]
    public sealed class JsrHttpTypeSystem : IInteroperableTypeSystem
    {
        public void Initialize(ITypeSystemContext context)
        { }

        public InteropObject CreateInteropObject(ITypeSystemContext context)
        {
            return new JsrHttpInterop().ToInteropObject("jsr.http");
        }
    }
}