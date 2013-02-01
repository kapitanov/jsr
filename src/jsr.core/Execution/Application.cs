using System;
using System.IO;
using JavaScript.Runtime.Startup;
using JavaScript.Runtime.TypeSystem;
using JavaScript.Runtime.Util;
using JetBrains.Annotations;

namespace JavaScript.Runtime.Execution
{
    public sealed class Application : ITypeSystemContext, IDisposable
    {
        [NotNull]
        private readonly ApplicationDefinition _definition;
        [NotNull]
        private readonly PathResolver _pathResolver;
        [NotNull]
        private readonly ComponentLoader _componentLoader;
        [NotNull]
        private readonly JavascriptRuntime _runtime;

        public Application([NotNull] ApplicationDefinition definition)
        {
            Verify.ArgumentNotNull(definition, "definition");

            _definition = definition;
            _pathResolver = new PathResolver(definition);
            _componentLoader = ComponentLoader.Load(_pathResolver);

            _runtime = new JavascriptRuntime();
        }

        public void Run()
        {
            try
            {
                foreach (var typeSystem in _componentLoader.TypeSystemExtensions)
                {
                    typeSystem.Initialize(this);
                    _runtime.RegisterTypeSystem(this, typeSystem);
                }
            }
            catch (Exception exception)
            {
                throw new JsrRuntimeException("Unable to initialize type systems", exception);
            }

            try
            {
                foreach (var libraryReference in _definition.RuntimeParameters.LibraryReferences)
                {
                    var libraryPath = _pathResolver.ResolvePath(libraryReference);
                    var librarySource = File.ReadAllText(libraryPath);
                    _runtime.EvaluateJavascript(librarySource);
                }

                var applicationSource = File.ReadAllText(_definition.Executable);
                _runtime.EvaluateJavascript(applicationSource);
            }
            catch (Exception exception)
            {
                throw new JsrRuntimeException("Runtime error", exception);
            }
        }

        #region ITypeSystemContext

        ApplicationDefinition ITypeSystemContext.Application { get { return _definition; } }

        PathResolver ITypeSystemContext.PathResolver { get { return _pathResolver; } }

        object ITypeSystemContext.EvaluateJavascript(string source)
        {
            Verify.ArgumentNotNull(source, "source");

            return _runtime.EvaluateJavascript(source);
        }

        void ITypeSystemContext.RegisterInteropObject(string name, object interopObject)
        {
            Verify.ArgumentNotNullOrEmpty(name, "name");
            Verify.ArgumentNotNull(interopObject, "interopObject");

            _runtime.RegisterInteropObject(name, interopObject);
        }

        #endregion

        public void Dispose()
        {
            _runtime.Dispose();
            _componentLoader.Dispose();
        }
    }
}
