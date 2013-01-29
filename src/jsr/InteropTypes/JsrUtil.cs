using System;
using Newtonsoft.Json;

namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrUtil
    {
        public object parse_json(string json)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(json);
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string format_json(object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                return json;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}