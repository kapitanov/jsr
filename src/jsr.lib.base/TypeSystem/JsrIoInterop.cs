using System;
using System.IO;
using System.Text;

namespace JavaScript.Runtime.TypeSystem
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrIoInterop
    {
        private readonly ITypeSystemContext _context;

        public JsrIoInterop(ITypeSystemContext context)
        {
            _context = context;
        }

        public string read(string path)
        {
            try
            {
                var fullPath = _context.PathResolver.ResolvePath(path);
                return File.ReadAllText(fullPath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool write(string path, string text)
        {
            try
            {
                var fullPath = _context.PathResolver.ResolvePath(path);
               
                File.WriteAllText(fullPath, text, Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}