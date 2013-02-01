using System.ComponentModel.Composition;

namespace JavaScript.Runtime.TypeSystem
{
    [Export(typeof(ITypeSystemExtension))]
    public sealed class JsrLibTypeSystem : ITypeSystemExtension
    {
        public void Initialize(ITypeSystemContext context)
        {
            context.EvaluateJavascript(Scripts.Lib);
        }
    }
}