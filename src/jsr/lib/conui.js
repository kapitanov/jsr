/// <reference path="_jsr.js"/>
(function () {
    "use strict";
    
    function create_option(title, handler) {
        return {
            title: title,  
            handler: handler
        };
    }
    
    function create_menu(options) {

        var title = options.title;
        var items = options.items;
        if (items.length == 0) {
            return;
        }

        var selectedIndex = 0;
        while (true) {
            // print menu
            jsr.con.clear();

            jsr.con.print(title);
            jsr.con.print('\n');

            for (var i = 0; i < items.length; i++) {
                if (i == selectedIndex) {
                    jsr.con.foreground(jsr.con.CYAN);
                    jsr.con.print(' [x] ');
                } else {
                    jsr.con.foreground(jsr.con.WHITE);
                    jsr.con.print(' [ ] ');
                }

                jsr.con.print(items[i].title);
                jsr.con.print('\n');
            }
            
            jsr.con.foreground(jsr.con.WHITE);
            jsr.con.print('\n');

            var key = jsr.con.read();
            switch (key) {
                case 38: // up arrow
                    selectedIndex--;
                    if (selectedIndex < 0) {
                        selectedIndex = 0;
                    }
                    break;
                case 40: // down arrow
                    selectedIndex++;
                    if (selectedIndex >= items.length) {
                        selectedIndex = items.length - 1;
                    }
                    break;
                case 13: // enter
                    items[selectedIndex].handler();
                    return;
            }
        }

    }

    var conui = {
        option: create_option,
        menu: create_menu
    };

    return conui;
})();
