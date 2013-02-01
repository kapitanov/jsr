using System.ComponentModel.Composition;

namespace JavaScript.Runtime.TypeSystem
{
    [Export(typeof(ITypeSystemExtension))]
    public sealed class JsrAppTypeSystem : IInteroperableTypeSystem
    {
        public void Initialize(ITypeSystemContext context)
        { }

        public InteropObject CreateInteropObject(ITypeSystemContext context)
        {
            return new JsrAppInterop(context).ToInteropObject("jsr.app");
        }
    }
}