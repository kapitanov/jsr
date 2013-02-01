using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using JavaScript.Runtime.TypeSystem;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Execution
{
    internal sealed class ComponentLoader : IDisposable
    {
        [NotNull]
        private readonly CompositionContainer _container;

        private ComponentLoader([NotNull] CompositionContainer container)
        {
            _container = container;
        }

        [ImportMany]
        public IEnumerable<ITypeSystemExtension> TypeSystemExtensions { get; set; }

        [NotNull]
        public static ComponentLoader Load([NotNull] PathResolver pathResolver)
        {
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Application).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new DirectoryCatalog(
                                     pathResolver.ResolvePath(PathRelativeTo.JsrDirectory, "exts"),
                                     @"*.dll"));

            var container = new CompositionContainer(catalog);

            var loader = new ComponentLoader(container);
            container.ComposeParts(loader);

            return loader;
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}