using System;

namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrTypeSystem
    {
        private readonly JsrConsole _console;
        private readonly JsrLibrary _lib;
        private readonly JsrIo _io;

        public JsrTypeSystem(JsrConsole console, JsrLibrary lib, JsrIo io)
        {
            _console = console;
            _lib = lib;
            _io = io;
        }

        public JsrConsole con { get { return _console; } }
        public JsrLibrary lib { get { return _lib; } }
        public JsrIo io { get { return _io; } }

        public void exit(int retval)
        {
            Environment.Exit(retval);
        }
    }
}
