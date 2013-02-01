(function() {
    'use strict';

    jsr.register_namespace('jsr.io', {});

    jsr.io.read = function(path) {
        return jsr.io.internal.read(path);
    };

    jsr.io.write = function(path, text) {
        return jsr.io.internal.write(path, text);
    };

    jsr.io.read_json = function(path) {
        var json = jsr.io.internal.read(path);
        return jsr.json.parse(json);
    };

    jsr.io.write_json = function(path, obj) {
        var json = jsr.json.stringify(obj);
        return jsr.io.internal.write(path, json);
    };
})();