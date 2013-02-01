using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JavaScript.Runtime.TypeSystem
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrHttpInterop
    {
        public string get(string url, object args)
        {
            var formattedUrl = BuildUrl(url, args as IDictionary<string, object>);
            var webClient = new WebClient();
            var result = webClient.DownloadString(formattedUrl);
            return result;
        }

        public string get(string url)
        {
            return get(url, null);
        }

        private static Uri BuildUrl(string url, IEnumerable<KeyValuePair<string, object>> getParameters)
        {
            var builder = new StringBuilder();
            if (!url.StartsWith("http://") &&
                !url.StartsWith("https://"))
            {
                builder.Append("http://");
            }

            builder.Append(url);

            if (getParameters != null)
            {
                var parameters = getParameters.ToArray();
                builder.Append("?");
                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    builder.AppendFormat(
                        "{0}={1}",
                        Uri.EscapeDataString(parameter.Key),
                        Uri.EscapeDataString((parameter.Value ?? string.Empty).ToString()));

                    if (index != parameters.Length - 1)
                    {
                        builder.Append("&");
                    }
                }
            }

            return new Uri(builder.ToString());
        }
    }
}