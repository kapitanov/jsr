using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    public sealed class InteropObject
    {
        [NotNull]
        private readonly object _obj;
        [NotNull]
        private readonly string _namespace;

        public InteropObject([NotNull] object obj, [NotNull] string ns)
        {
            Verify.ArgumentNotNull(obj, "obj");
            Verify.ArgumentNotNullOrEmpty(ns, "ns");

            _obj = obj;
            _namespace = ns;
        }

        [NotNull]
        public object Object { get { return _obj; } }

        [NotNull]
        public string Namespace { get { return _namespace; } }
    }
}