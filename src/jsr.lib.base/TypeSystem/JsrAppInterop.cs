using System;
using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrAppInterop
    {
        [NotNull]
        private readonly ITypeSystemContext _context;

        public JsrAppInterop([NotNull] ITypeSystemContext context)
        {
            _context = context;
        }

        public bool has_flag(string name)
        {
            return _context.Application.ProgramParameters.Flags.Contains(name);
        }

        public string param(string name)
        {
            string value;
            _context.Application.ProgramParameters.Parameters.TryGetValue(name, out value);
            return value;
        }

        public void exit(int code)
        {
            Environment.Exit(code);
        }
    }
}