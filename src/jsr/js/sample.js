/// <reference path="/lib/_jsr.js"/>
"use strict";

function sample_json() {
    jsr.con.warn(":: JSON string formatting ::\n\n");

    var obj = { d: [1, 2, 3], x: 'abc' };
    jsr.con.print("JSON string formatting:\n");
    jsr.con.print("Source object: { d: [1, 2, 3], x: 'abc' }\n");
    var json = jsr.util.format_json(obj);
    jsr.con.printf("JSON string: {{json}}\n", { json: json });
    obj = jsr.util.parse_json(json);
    json = jsr.util.format_json(obj);
    jsr.con.printf("Restored object: {{json}}\n", { json: json });
}

function sample_textfile() {
    jsr.con.warn(":: Text file API ::\n\n");
    var filename = "sample.txt";
    var text = "Sample text";
    jsr.con.printf("Writing \"{{text}}\" into file \"{{filename}}\"\n", { filename: filename, text: text });
    jsr.io.write(filename, text);
    text = jsr.io.read(filename);
    jsr.con.printf("Reading text from the same file: \"{{text}}\"\n", { text: text });
}

jsr.lib.import('js/lib/conui');

conui.menu({
    title: 'Select sample:',
    items: [
        conui.option('Parse/format JSON', sample_json),
        conui.option('Text file I/O', sample_textfile),
        conui.option('Exit', function() {
        })
    ]
});
jsr.exit(0);