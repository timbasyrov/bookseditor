// Source: http://paquitosoftware.com/ractive-js-tutorial-routing/
Ractive.components.Router = Ractive.extend({
    template: '<router-handler params="{{params}}"/>',
    components: {
        'router-handler': function () {
            return this.get('componentName');
        }
    },
    oninit: function () {
        var it = this;
        it.observe('componentName', function (newValue, oldValue) {
            if (this.fragment.rendered) {
                this.reset();
            }
        });
    },
});