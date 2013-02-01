using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    public static class InteropObjectFactory
    {
        [NotNull]
        public static InteropObject ToInteropObject([NotNull] this object obj, [NotNull] string ns)
        {
            return new InteropObject(obj, ns);
        }
    }
}