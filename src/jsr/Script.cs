using System;
using System.Collections.Generic;
using System.Linq;
using JavaScript.Runtime.InteropTypes;
using Noesis.Javascript;

namespace JavaScript.Runtime
{
    public sealed class Script : IDisposable
    {
        private readonly string _scriptPath;
        private readonly string[] _scriptArgs;

        private readonly JavascriptContext _context;

        private readonly Dictionary<string, object> _libraries = new Dictionary<string, object>();

        public Script(string[] args)
        {
            AnalyseParameters(args, out _scriptArgs, out _scriptPath);
            
            _context = new JavascriptContext();

            var typeSystem = new JsrTypeSystem(
                   new JsrConsole(this),
                   new JsrLibrary(this),
                   new JsrIo(),
                   new JsrUtil(),
                   new JsrHttp()
                   );

            _context.SetParameter("jsr", typeSystem);
            _context.Run("var window = { };");
        }

        public string[] Arguments { get { return _scriptArgs; } }

        public void Run()
        {
            var script = ScriptLoader.LoadScript(_scriptPath);
            _context.Run(script);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public object LoadLibrary(string path)
        {
            path = path.ToLowerInvariant();

            object result;
            if (_libraries.TryGetValue(path, out result))
            {
                return result;
            }

            var script = ScriptLoader.LoadScript(path, throwExceptionIfNotFound: false);
            if (script == null)
            {
                return null;
            }
            
            result = _context.Run(script);
            if (result == null)
            {
                return null;
            }

            _libraries[path] = result;
            return result;
        }

        private static void AnalyseParameters(string[] args, out string[] scriptArgs, out string scriptPath)
        {
            if (args.Length == 0)
            {
                throw new JsrException("script path is not set");
            }

            scriptPath = args[0];
            scriptArgs = args.Skip(1).ToArray();
        }
    }
}