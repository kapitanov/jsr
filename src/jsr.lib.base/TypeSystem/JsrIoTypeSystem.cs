using System.ComponentModel.Composition;

namespace JavaScript.Runtime.TypeSystem
{
    [Export(typeof(ITypeSystemExtension))]
    public sealed class JsrIoTypeSystem : ITypeSystemExtension
    {
        public void Initialize(ITypeSystemContext context)
        {
            context.EvaluateJavascript(Scripts.Io);
            var io = new JsrIoInterop(context);
            context.RegisterInteropObject("$jsr_interop$jsr$io$internal", io);
            context.EvaluateJavascript("jsr.register_namespace('jsr.io.internal', $jsr_interop$jsr$io$internal);");
        }
    }
}