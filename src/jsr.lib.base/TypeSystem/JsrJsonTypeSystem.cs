using System.ComponentModel.Composition;

namespace JavaScript.Runtime.TypeSystem
{
    [Export(typeof(ITypeSystemExtension))]
    public sealed class JsrJsonTypeSystem : ITypeSystemExtension
    {
        public void Initialize(ITypeSystemContext context)
        {
            context.EvaluateJavascript(Scripts.Json);
        }
    }
}