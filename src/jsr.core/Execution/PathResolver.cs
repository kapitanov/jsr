using System;
using System.IO;
using System.Reflection;
using JavaScript.Runtime.Startup;
using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Execution
{
    public sealed class PathResolver
    {
        private const string JsrSchemaPrefix = "jsr://";

        [NotNull]
        private readonly string _applicationDirectory;
        [NotNull]
        private readonly string _jsrDirectory;

        public PathResolver([NotNull] ApplicationDefinition definition)
        {
            Verify.ArgumentNotNull(definition, "definition");

            var applicationDirectory = Path.GetDirectoryName(definition.Executable);
            if (applicationDirectory == null)
            {
                throw new Exception(); ;
            }
            _applicationDirectory = applicationDirectory;

            var jsrDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (jsrDirectory == null)
            {
                throw new Exception(); ;
            }
            _jsrDirectory = jsrDirectory;
        }

        public string ApplicationDirectory { get { return _applicationDirectory; } }

        public string JsrDirectory { get { return _jsrDirectory; } }

        [NotNull]
        public string ResolvePath(PathRelativeTo relativeTo, [NotNull] string path)
        {
            Verify.ArgumentNotNullOrEmpty(path, "path");

            var fullPath = Path.Combine(GetBasePath(relativeTo), path);
            fullPath = Path.GetFullPath(fullPath);
            return fullPath;
        }

        [NotNull]
        public string ResolvePath([NotNull] string path)
        {
            Verify.ArgumentNotNullOrEmpty(path, "path");

            string actualPath;
            PathRelativeTo relativeTo;
            if (path.StartsWith(JsrSchemaPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                actualPath = path.Substring(JsrSchemaPrefix.Length);
                relativeTo = PathRelativeTo.JsrDirectory;
            }
            else
            {
                actualPath = path;
                relativeTo = PathRelativeTo.ApplicationDirectory;
            }

            return ResolvePath(relativeTo, actualPath);
        }

        private string GetBasePath(PathRelativeTo relativeTo)
        {
            switch (relativeTo)
            {
                case PathRelativeTo.ApplicationDirectory:
                    return _applicationDirectory;
                case PathRelativeTo.JsrDirectory:
                    return _jsrDirectory;

                default:
                    throw new ArgumentOutOfRangeException("relativeTo");
            }
        }
    }
}