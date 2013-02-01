var jsr = {
    con: {
        print: function (message) {
            /// <summary>Prints text to console</summary>
            /// <param name="message" type="String">Text to print</param>
        },
        printf: function(message, args) {
            /// <summary>Prints formatted text to console</summary>
            /// <param name="message" type="String">Format string</param>
            /// <param name="args" type="Object">Format arguments</param>
        },
        warn: function (message) {
            /// <summary>Prints warning to console</summary>
            /// <param name="message" type="String">Text to print</param>
        },
        warnf: function (message, args) {
            /// <summary>Prints formatted warning to console</summary>
            /// <param name="message" type="String">Format string</param>
            /// <param name="args" type="Object">Format arguments</param>
        },
        error: function (message) {
            /// <summary>Prints error to console</summary>
            /// <param name="message" type="String">Text to print</param>
        },
        errorf: function (message, args) {
            /// <summary>Prints formatted error to console</summary>
            /// <param name="message" type="String">Format string</param>
            /// <param name="args" type="Object">Format arguments</param>
        },
        read: function () {
            /// <summary>Reads character' code from console</summary>
            /// <returns type="Integer" />
        },
        readln: function() {
            /// <summary>Reads line from console</summary>
            /// <returns type="String" />
        },
        clear: function() {
            /// <summary>Clears console</summary>
        },
        RED: 'red',
        DARK_RED: 'dark-red',
        GREEN: 'green',
        DARK_GREEN: 'dark-green',
        BLUE: 'red',
        DARK_BLUE: 'dark-blue',
        CYAN: 'cyan',
        DARK_CYAN: 'dark-cyan',
        YELLOW: 'yellow',
        DARK_YELLOW: 'dark-yellow',
        MAGENTA: 'magenta',
        DARK_MAGENTA: 'dark-magenta',
        GRAY: 'gray',
        DARK_GRAY: 'dark-gray',
        WHITE: 'white',
        BLACK: 'black',
        foreground: function (color) {
            /// <summary>Gets and Sets console foreground color</summary>
            /// <param name="message" type="String">Color name</param>
            /// <returns type="String" />
        },
        background: function (color) {
            /// <summary>Gets and sets console background color</summary>
            /// <param name="message" type="String">Color name</param>
            /// <returns type="String" />
        },
        cursor: {
            x: function (value) {
                /// <summary>Gets and sets console cursor x coordinate</summary>
                /// <param name="value" type="Integer">Coordinate value</param>
                /// <returns type="Integer" />
            },
            y: function (value) {
                /// <summary>Gets and sets console cursor y coordinate</summary>
                /// <param name="value" type="Integer">Coordinate value</param>
                /// <returns type="Integer" />
            },
            visible: function (value) {
                /// <summary>Gets and sets console cursor visibility</summary>
                /// <param name="value" type="Boolean">Cursor visibility</param>
                /// <returns type="Boolean" />
            }
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
    http: {
        get: function (url, args) {
            /// <summary>Loads a string via HTTP GET</summary>
            /// <param name="url" type="String">URL</param>
            /// <param name="args" type="Object">GET parameters</param>
            /// <returns type="String" />
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
        read_json: function (path) {
            /// <summary>Reads file's content as JSON object</summary>
            /// <param name="path" type="String">File path</param>
            /// <returns type="Object" />
        },
        write_json: function (path, obj) {
            /// <summary>Writes file's content as JSON</summary>
            /// <param name="path" type="String">File path</param>
            /// <param name="obj" type="Object">File content</param>
            /// <returns type="Boolean" />
        }
    },
    json: {
        parse: function (json) {
            /// <summary>Parsed object from JSON string</summary>
            /// <param name="json" type="String">JSON string</param>
            /// <returns type="Object" />
        },
        stringify: function (obj) {
            /// <summary>Formats object as JSON string</summary>
            /// <param name="obj" type="Object">Object</param>
            /// <returns type="Boolean" />
        }
    },
    app: {
        has_flag: function (name) {
            /// <summary>Checks application command line flag</summary>
            /// <param name="retval" type="String">Flag name</param>
            /// <returns type="Boolean" />
        },
        param: function (name) {
            /// <summary>GEt application command line parameter value</summary>
            /// <param name="name" type="Integer">Parameter name</param>
            /// <returns type="Boolean" />
        },
        exit: function (code) {
            /// <summary>Exits application</summary>
            /// <param name="code" type="Integer">Exit code</param>
        }
    }
};