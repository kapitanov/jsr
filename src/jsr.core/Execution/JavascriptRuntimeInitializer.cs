namespace JavaScript.Runtime.Execution
{
    internal static class JavascriptRuntimeInitializer
    {
        public static void Initialize(JavascriptRuntime runtime)
        {
            runtime.EvaluateJavascript(JsrInitializerScript);
        }

        private const string JsrInitializerScript = @"
/* Create 'window' virtual root  */
var window = { };

/* Create 'jsr' namespace root  */
var jsr = { };

window.jsr = jsr;

/* jsr-namespace extension function  */
jsr.register_namespace = function (ns_name, ns_obj) {
    'use strict';

    var parts = ns_name.split('.');
    var current_part = jsr;

    var index = 1;
    if(parts.length < 2) {
        return;
    }

    while(index < parts.length)
    {
        var part_name = parts[index];
        var part = current_part[part_name];
        if(!part) {
            if(index != parts.length - 1) {
                part = { };
            }
            else {
                part = ns_obj;
            }
        }

        current_part[part_name] = part;
        current_part = part;

        index++;
    }
};";
    }
}