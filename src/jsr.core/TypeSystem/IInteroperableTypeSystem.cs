using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    public interface IInteroperableTypeSystem : ITypeSystemExtension
    {
        [NotNull] 
        InteropObject CreateInteropObject(ITypeSystemContext context);
    }
}