/// <reference path="_jsr.js"/>
(function () {
    "use strict";
   
    var LINK_EXCITATION = 'excitation';
    var LINK_EXCITATION_REC = 'excitation-rec';
    var LINK_INHIBITION = 'inhibition';
    var LINK_INHIBITION_REC = 'inhibition-rec';

    function methodUnit_reset($this) {
        for (var i = 0; i < $this.dependents.length; i++) {
            $this.dependents[i].reset();
        }
    };

    function methodUnit_feed_value($this) {
        for (var i = 0; i < $this.dependents.length; i++) {
            $this.dependents[i].receive_value();
        }
    };

    function createUnit($this) {
        $this.prev_value = 0;
        $this.value = 0;
        $this.dependents = [];

        $this.reset = function () {
            methodUnit_reset($this);
        };

        $this.feed_value = function () {
            methodUnit_feed_value($this);
        };

        $this.receive_value = function () { };
    }

    function InputUnit(name) {
        var $this = this;
        createUnit(this);

        this.prev_value = 0;
        this.value = 0;
        this.dependents = [];
        this.name = name;

        this.set_value = function (vector_value) {
            var value = vector_value[$this.name];
            $this.prev_value = $this.value;
            $this.value = value;
            $this.feed_value();
        };
    }

    function ThresholdUnit(threshold, name) {
        var $this = this;

        createUnit(this);

        this.prev_value = 0;
        this.value = 0;
        this.dependents = [];
        this.name = name;
        this.threshold = threshold;

        this.dependencies = [];
        this.dependencies_rec = [];
        this.pending_dependencies = 0;

        this.add_excitation = function (input) {
            $this.link(input, 1);
        };

        this.link = function (input, type) {
            type = type || LINK_EXCITATION;

            var weight;
            var fun;
            switch(type) {
                case LINK_EXCITATION:
                    weight = 1;
                    fun = $this.add_link;
                    break;
                case LINK_EXCITATION_REC:
                    weight = 1;
                    fun = $this.add_link_rec;
                    break;
                case LINK_INHIBITION:
                    weight = -1;
                    fun = $this.add_link;
                    break;
                case LINK_INHIBITION_REC:
                    weight = -1;
                    fun = $this.add_link_rec;
                    break;
                default:
                    throw "Unknowns link type";
            }

            fun(input, weight);
        };

        this.add_link = function (input, weight) {
            input.dependents.push($this);
            $this.dependencies.push({ unit: input, weight: weight });
        };
        
        this.add_link_rec = function (input, weight) {
            $this.dependencies_rec.push({ unit: input, weight: weight });
        };

        this.reset = function () {
            methodUnit_reset($this);
            $this.pending_dependencies = $this.dependencies.length;
        };

        this.receive_value = function () {
            $this.pending_dependencies--;
            if ($this.pending_dependencies <= 0) {
                $this.compute_value();
            }
        };

        this.compute_value = function() {
            var sum = 0;
            for (var i = 0; i < $this.dependencies.length; i++) {
                var dependency = $this.dependencies[i];
                sum += dependency.unit.value * dependency.weight;
            }
            
            for (var i = 0; i < $this.dependencies_rec.length; i++) {
                var dependency = $this.dependencies_rec[i];
                sum += dependency.unit.prev_value * dependency.weight;
            }

            var value = sum >= $this.threshold ? 1 : 0;

            $this.prev_value = $this.value;
            $this.value = value;
            $this.feed_value();
        };
    }

    function Network(inputs, units, outputs) {
        var $this = this;

        this.inputs = inputs;
        this.units = units;
        this.outputs = outputs;

        this.reset = function () {
            for (var i = 0; i < $this.inputs.length; i++) {
                $this.inputs[i].reset();
            }
        };

        this.feed = function (value) {
            for (var i = 0; i < $this.inputs.length; i++) {
                $this.inputs[i].set_value(value);
            }

            var output = {};
            for (var i = 0; i < $this.outputs.length; i++) {
                var unit = $this.outputs[i];
                output[unit.name] = unit.value;
            }
            
            return output;
        };
    }

    function NetworkBuilder() {
        var $this = this;

        this.units = [];
        this.inputs = [];

        this.create_input = function (name) {
            var input = new InputUnit(name);
            $this.inputs.push(input);
            return input;
        };

        this.create_unit = function (threshold, name) {
            name = name || '';
            var input = new ThresholdUnit(threshold, name);
            $this.units.push(input);
            return input;
        };

        this.create_network = function () {
            var outputs = [];
            for (var i = 0; i < $this.units.length; i++) {
                var unit = $this.units[i];
                if (unit.name.length > 0) {
                    outputs.push(unit);
                }
            }

            var network = new Network($this.inputs, $this.units, outputs);
            network.reset();
            return network;
        };
    }

    function ThresholdLogic() {
        this.build_network = function () {
            return new NetworkBuilder();
        };

        this.EXCITATION = LINK_EXCITATION;
        this.EXCITATION_REC = LINK_EXCITATION_REC;
        this.INHIBITION = LINK_INHIBITION;
        this.INHIBITION_REC = LINK_INHIBITION_REC;
    }

    var threshold_logic = new ThresholdLogic();
    return threshold_logic;
})();
