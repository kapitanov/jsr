(function() {
    'use strict';

    var import_obj = {
        import: function(path) {
            var source = jsr.io.read(path);
            return eval(source);
        }
    };

    jsr.register_namespace('jsr.lib', import_obj);
})();