using System;

namespace JavaScript.Runtime.InteropTypes
{
    // ReSharper disable InconsistentNaming
    public sealed class JsrTypeSystem
    {
        private readonly JsrConsole _console;
        private readonly JsrLibrary _lib;
        private readonly JsrIo _io;
        private readonly JsrUtil _util;

        public JsrTypeSystem(JsrConsole console, JsrLibrary lib, JsrIo io, JsrUtil util)
        {
            _console = console;
            _lib = lib;
            _io = io;
            _util = util;
        }

        public JsrConsole con { get { return _console; } }
        public JsrLibrary lib { get { return _lib; } }
        public JsrIo io { get { return _io; } }
        public JsrUtil util { get { return _util; } }

        public void exit(int retval)
        {
            Environment.Exit(retval);
        }
    }
}
