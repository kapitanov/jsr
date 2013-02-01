using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    public interface ITypeSystemExtension
    {
        void Initialize([NotNull] ITypeSystemContext context);
    }
}
