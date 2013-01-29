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
        get_foreground: function() {
            /// <summary>Returns console foreground color</summary>
            /// <returns type="String" />
        },
        get_background: function () {
            /// <summary>Returns console background color</summary>
            /// <returns type="String" />
        },
        set_foreground: function (color) {
            /// <summary>Sets console foreground color</summary>
            /// <param name="message" type="String">Color name</param>
        },
        set_background: function (color) {
            /// <summary>Sets console background color</summary>
            /// <param name="message" type="String">Color name</param>
        },
        cursor: {
            x: 0,
            y: 0
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
    util: {
        parse_json: function (json) {
            /// <summary>Parsed object from JSON string</summary>
            /// <param name="json" type="String">JSON string</param>
            /// <returns type="Object" />
        },
        format_json: function (obj) {
            /// <summary>Formats object as JSON string</summary>
            /// <param name="obj" type="Object">Object</param>
            /// <returns type="Boolean" />
        }
    },
    exit: function(retval) {
        /// <summary>Exits application</summary>
        /// <param name="retval" type="Integer">Exit code</param>
    }
};