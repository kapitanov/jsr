/// <reference path="../lib/_jsr.js"/>
"use strict";

var threshold_logic = jsr.lib.import('jsr://lib/threshold_logic.js');

function test_binary_network(name, network) {
    var inputs = [
        { x1: 0, x2: 0 },
        { x1: 0, x2: 1 },
        { x1: 1, x2: 0 },
        { x1: 1, x2: 1 }
    ];

    for (var i = 0; i < inputs.length; i++) {
        var x = inputs[i];
        var y = network.feed(x);
        jsr.con.printf(
            '{{x1}} {{name}} {{x2}} = {{y}}\n',
            { x1: x.x1, x2: x.x2, name: name, y: y.y });
    }

    jsr.con.print('\n');
}

function test_stream_network(name, network) {
    var inputs = [0, 0, 1, 1, 0, 1, 1, 0];
    var outputs = [];
    for (var i = 0; i < inputs.length; i++) {
        var x = inputs[i];
        var y = network.feed({ x: x });
        outputs.push(y.y);
    }

    jsr.con.printf('{{name}} [ ', { name: name });
    for (var i = 0; i < inputs.length; i++) {
        var x = inputs[i];
        jsr.con.printf('{{x}}', { x: x });
        if (i != inputs.length - 1) {
            jsr.con.print(', ');
        }
    }
    jsr.con.print(' ] -> [ ');
    
    for (var i = 0; i < outputs.length; i++) {
        var y = outputs[i];
        jsr.con.printf('{{y}}', { y: y });
        if (i != outputs.length - 1) {
            jsr.con.print(', ');
        }
    }
    jsr.con.print(' ]\n');
}

function and_network() {
    var builder = threshold_logic.build_network();

    var i1 = builder.create_input('x1');
    var i2 = builder.create_input('x2');
    var y = builder.create_unit(2.0, 'y');
    y.link(i1);
    y.link(i2);

    return builder.create_network();
}

function or_network() {
    var builder = threshold_logic.build_network();

    var i1 = builder.create_input('x1');
    var i2 = builder.create_input('x2');
    var y = builder.create_unit(1.0, 'y');
    y.link(i1);
    y.link(i2);

    return builder.create_network();
}

function binary_scaler() {
    var builder = threshold_logic.build_network();

    var x = builder.create_input('x');
    var state = builder.create_unit(1.0, 'state');
    var y = builder.create_unit(2.0, 'y');
    
    state.link(x);
    state.link(state, threshold_logic.EXCITATION_REC);
    state.link(y, threshold_logic.INHIBITION_REC);
    
    y.link(x);
    y.link(state);
    y.link(y, threshold_logic.INHIBITION_REC);
    
    return builder.create_network();
}

test_binary_network("&&", and_network());
test_binary_network("||", or_network());
test_stream_network("binary_scaler", binary_scaler());