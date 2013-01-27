using System;
using System.IO;
using System.Linq;

namespace JavaScript.Runtime
{
    internal static class PathHelper
    {
        public static string ResolveReadPath(string path, params string[] exts)
        {
            var rootPathes = new[]
                                 {
                                     Directory.GetCurrentDirectory(),
                                     Path.GetDirectoryName(typeof(Program).Assembly.Location)
                                 };

            var relativePathes = exts.Concat(new[] { "" })
                                     .Select(ext => path + ext)
                                     .ToArray();

            var pathes = from root in rootPathes
                         from relativePath in relativePathes
                         let fullPath = Path.Combine(root, relativePath)
                         select fullPath;

            var resolvedPathes = from p in pathes
                                 where File.Exists(p)
                                 select p;

            return resolvedPathes.FirstOrDefault();
        }

        public static string ResolveWritePath(string path, string ext)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }

            var actualExt = Path.GetExtension(path);
            if (!string.Equals(actualExt, ext, StringComparison.InvariantCultureIgnoreCase))
            {
                path = Path.ChangeExtension(path, ext);
            }

            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return path;
        }
    }
}