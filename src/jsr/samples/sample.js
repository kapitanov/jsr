/// <reference path="../lib/_jsr.js"/>
"use strict";

var conui = jsr.lib.import('jsr://lib/conui.js');

function sample_json() {
    jsr.con.warn(":: JSON string formatting ::\n\n");

    var obj = { d: [1, 2, 3], x: 'abc' };
    jsr.con.print("JSON string formatting:\n");
    jsr.con.print("Source object: { d: [1, 2, 3], x: 'abc' }\n");
    var json = jsr.json.stringify(obj);
    jsr.con.printf("JSON string: {{json}}\n", { json: json });
    obj = jsr.json.parse(json);
    json = jsr.json.stringify(obj);
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

function sample_http() {
    jsr.con.warn(":: HTTP GET API ::\n\n");
    var url = "ajax.googleapis.com/ajax/services/search/web";
    var json = jsr.http.get(url, { v: '1.0', q: 'javascript' });
    var response = jsr.json.parse(json);

    var count = response.responseData.results.length;
    if (count > 5) {
        jsr.con.warn("Only 5 top results will be shown");
        count = 5;
    }

    for (var i = 0; i < count; i++) {
        var result = response.responseData.results[i];
        jsr.con.printf('{{titleNoFormatting}}\n  {{url}}\n', result);
    }
    
    jsr.con.printf('\n{{resultCount}} pages found\n', response.responseData.cursor);
}

function menu() {
    var terminate = false;
    while (!terminate) {
        conui.menu({
            title: 'Select sample:',
            items: [
                conui.option('Parse/format JSON', sample_json),
                conui.option('Text file I/O', sample_textfile),
                conui.option('HTTP GET', sample_http),
                conui.option('Exit', function() {
                    terminate = true;
                })
            ]
        });

        if (!terminate) {
            jsr.con.print('\nPress <Enter> to continue... ');
            
            while(jsr.con.read() != 13) { }
        }
    }
}

menu();
