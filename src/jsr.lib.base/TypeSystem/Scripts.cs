using System.IO;
using JavaScript.Runtime.Startup;
using JetBrains.Annotations;

namespace JavaScript.Runtime.TypeSystem
{
    public static class Scripts
    {
        [NotNull]
        public static string Io
        {
            get { return GetScript("jsr.io.js"); }
        }

        [NotNull]
        public static string Json
        {
            get { return GetScript("jsr.json.js"); }
        }

        [NotNull]
        public static string Lib
        {
            get { return GetScript("jsr.lib.js"); }
        }

        [NotNull]
        private static string GetScript([NotNull] string name)
        {
            var assembly = typeof(Scripts).Assembly;
            var fullName = "JavaScript.Runtime.Scripts." + name;

            using (var stream = assembly.GetManifestResourceStream(fullName))
            {
                if (stream == null)
                {
                    throw new JsrStartupException(
                        string.Format("Unable to load resource {0}!{1}", assembly.GetName().Name, fullName));
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}