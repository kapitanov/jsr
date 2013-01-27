var jsr = {
    con: {
        args: function () {
            /// <summary>Returns script's command line parameters</summary>
            /// <returns type="Array" />
        },
        print: function (message) {
            /// <summary>Prints text to console</summary>
            /// <param name="message" type="String">Text to print</param>
        },
        printf: function(message, args) {
            /// <summary>Prints formatted text to console</summary>
            /// <param name="message" type="String">Format string</param>
            /// <param name="args" type="Object">Format arguments</param>
        },
        readln: function() {
            /// <summary>Reads line from console</summary>
            /// <returns type="String" />
        }
    },
    lib: {
// ReSharper disable UsingOfReservedWord
        import: function(path) {
// ReSharper restore UsingOfReservedWord
            /// <summary>Loads external JS library</summary>
            /// <param name="path" type="String">Library path</param>
            /// <returns type="Object" />
        }
    },
    io: {
        read: function(path) {
            /// <summary>Reads file's content as text</summary>
            /// <param name="path" type="String">File path</param>
            /// <returns type="String" />
        },
        write: function(path, text) {
            /// <summary>Writes file's content</summary>
            /// <param name="path" type="String">File path</param>
            /// <param name="text" type="String">File content</param>
            /// <returns type="Boolean" />
        },
        readobj: function(path) {
            /// <summary>Reads file's content as JSON object</summary>
            /// <param name="path" type="String">File path</param>
            /// <returns type="Object" />
        },
        writeobj: function(path, obj) {
            /// <summary>Writes file's content as JSON</summary>
            /// <param name="path" type="String">File path</param>
            /// <param name="obj" type="Object">File content</param>
            /// <returns type="Boolean" />
        }
    },
    exit: function(retval) {
        /// <summary>Exits application</summary>
        /// <param name="retval" type="Integer">Exit code</param>
    }
};