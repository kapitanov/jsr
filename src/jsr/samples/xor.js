/// <reference path="../lib/_jsr.js"/>
/// <reference path="../lib/encog.js"/>

function run(settings) {
    var XOR_INPUT = [
        [0, 0],
        [1, 0],
        [0, 1],
        [1, 1]
    ];

    var XOR_IDEAL = [
        [0],
        [1],
        [1],
        [0]
    ];
    
    jsr.con.print('jsr+encog:xor\n');
    jsr.con.printf('max-iterations: {{iterations}}\nmax-error: {{error}}\n\n', settings);

    var network = ENCOG.BasicNetwork.create([
        ENCOG.BasicLayer.create(ENCOG.ActivationSigmoid.create(), 2, 1),
        ENCOG.BasicLayer.create(ENCOG.ActivationSigmoid.create(), 3, 1),
        ENCOG.BasicLayer.create(ENCOG.ActivationSigmoid.create(), 1, 0)]);
    network.randomize();

    var train = ENCOG.PropagationTrainer.create(network, XOR_INPUT, XOR_IDEAL, "RPROP", 0, 0);

    var iteration = 1;

    do {
        train.iteration();
        var str = "Training Iteration #" + iteration + ", Error: " + train.error + "\n";
        jsr.con.print(str);
        iteration++;
    } while (iteration < settings.iterations && train.error > settings.error);

    var input = [0, 0];
    var output = new Array(1);

    jsr.con.print("Testing neural network\n");
    for (var i = 0; i < XOR_INPUT.length; i++) {
        network.compute(XOR_INPUT[i], output);
        var str = "Input: " + String(XOR_INPUT[i][0])
                + " ; " + String(XOR_INPUT[i][1])
                + "   Output: " + String(output[0])
                + "   Ideal: " + String(XOR_IDEAL[i][0])
                + "\n";
        jsr.con.print(str);
    }

    jsr.con.print('Writing weights matrix... ');
    jsr.io.write_json('xor-weights', network.weights);
    jsr.con.print('done\n');
}

function parseArgs() {
    jsr.con.print("jsr+encog\nusage:\n");
    jsr.con.print("jsr xor.js [max-iterations = 1000] [max-error = 0.001]\n\n");

    var iterations = 1000;
    var error = 0.001;

    var args = jsr.con.args();
    if (args.length > 0) {
        iterations = parseInt(args[0]);
    }
    
    if (args.length > 1) {
        error = parseFloat(args[1]);
    }

    return {
        iterations: iterations,
        error: error
    };
}

jsr.con.print('con: print');
jsr.con.warn('con: warn');
jsr.con.error('con: error');

jsr.con.set_foreground(jsr.con.RED);
jsr.con.print('jsr+encog:xor');
jsr.lib.import('js/lib/encog');
jsr.con.set_foreground(jsr.con.CYAN);
var settings = parseArgs();

jsr.con.print("hit any key to start...");
jsr.con.readln();

run(settings);
jsr.con.set_foreground(jsr.con.WHITE);
jsr.exit(0);