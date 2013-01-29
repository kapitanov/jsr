using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrIo
    {
        public string read(string path)
        {
            try
            {
                var fullPath = PathHelper.ResolveReadPath(path, ".json");
                if (fullPath == null)
                {
                    return null;
                }

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
                var fullPath = PathHelper.ResolveWritePath(path);
                if (fullPath == null)
                {
                    return false;
                }

                File.WriteAllText(fullPath, text, Encoding.UTF8);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public object read_json(string path)
        {
            try
            {
                var fullPath = PathHelper.ResolveReadPath(path, ".json");
                if (fullPath == null)
                {
                    return null;
                }


                var json = read(path);

                var obj = JsonConvert.DeserializeObject(json);
                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool write_json(string path, object obj)
        {
            try
            {
                var fullPath = PathHelper.ResolveWritePath(path);
                if (fullPath == null)
                {
                    return false;
                }

                var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                return write(fullPath, json);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}