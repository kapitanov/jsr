using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JavaScript.Runtime.Util
{
    public static class JsrHelper
    {
        private static readonly IDictionary<string, object> _EmptyArguments = new Dictionary<string, object>();
        private static readonly Regex _FormatStringRegex = new Regex(@"\{\{(?<name>[a-zA-Z0-9_]*)\}\}", RegexOptions.Compiled);

        public static string FormatString(string format, object args)
        {
            var arguments = AnalyseArguments(args);

            var result = _FormatStringRegex.Replace(
                format,
                match =>
                {
                    var name = match.Groups["name"].Value;

                    object arg;
                    if (!arguments.TryGetValue(name, out arg))
                    {
                        arg = name;
                    }

                    return (arg ?? string.Empty).ToString();
                });

            return result;
        }

        private static IDictionary<string, object> AnalyseArguments(object args)
        {
            var result = args as IDictionary<string, object>;
            if (result == null)
            {
                result = _EmptyArguments;
            }

            return result;
        }
    }
}
