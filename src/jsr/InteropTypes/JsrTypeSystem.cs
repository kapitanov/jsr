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
        private readonly JsrHttp _http;

        public JsrTypeSystem(JsrConsole console, JsrLibrary lib, JsrIo io, JsrUtil util, JsrHttp http)
        {
            _console = console;
            _lib = lib;
            _io = io;
            _util = util;
            _http = http;
        }

        public JsrConsole con { get { return _console; } }
        public JsrLibrary lib { get { return _lib; } }
        public JsrIo io { get { return _io; } }
        public JsrUtil util { get { return _util; } }
        public JsrHttp http { get { return _http; } }

        public void exit(int retval)
        {
            Environment.Exit(retval);
        }
    }
}
