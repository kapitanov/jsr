using System.ComponentModel.Composition;
using System.Xml;

namespace JavaScript.Runtime.TypeSystem
{
    [Export(typeof(ITypeSystemExtension))]
    public class JsrConsoleTypeSystem : IInteroperableTypeSystem
    {
        public void Initialize(ITypeSystemContext context) { }

        public InteropObject CreateInteropObject(ITypeSystemContext context)
        {
            return new JsrConsoleInterop().ToInteropObject("jsr.con");
        }
    }
}
