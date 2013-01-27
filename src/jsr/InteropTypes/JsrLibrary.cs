namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrLibrary
    {
        private readonly Script _script;

        public JsrLibrary(Script script)
        {
            _script = script;
        }

        public object import(string path)
        {
            return _script.LoadLibrary(path);
        }
    }
}