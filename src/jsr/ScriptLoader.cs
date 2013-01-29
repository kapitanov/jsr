using System.Collections.Generic;
using System.IO;

namespace JavaScript.Runtime
{
    internal static class ScriptLoader
    {
        public static string LoadScript(string scriptPath, bool throwExceptionIfNotFound = true)
        {
            var fullPath = PathHelper.ResolveReadPath(scriptPath, ".jsr", ".js");
            if (fullPath == null)
            {
                if (!throwExceptionIfNotFound)
                {
                    return null;
                }

                throw new JsrException("script file not found");
            }

            return File.ReadAllText(fullPath);
        }
    }
}