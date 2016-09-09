var unobtrusiveDecorator;

unobtrusiveDecorator = function (node) {

    $(node).validate();
    $.validator.setDefaults({ ignore: null });
    $.validator.unobtrusive.parseDynamicContent(node);

    return {
        teardown: function () {
            $(node).validator = null;
        }
    };
}

Ractive.decorators.unobtrusive = unobtrusiveDecorator;