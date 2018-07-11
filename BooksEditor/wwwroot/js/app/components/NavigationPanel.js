Ractive.components.NavigationPanel = Ractive.extend({
    isolated: false,
    template: Url.GetTemplate('/Templates/NavigationPanel.html'),
    data: function () {
        return {
            navigation: {
            }
        }
    },
    oninit: function () {
        var it = this;

        it.on({
            'onMenuButtonClick': $.proxy(it.onMenuButtonClick, it),
        });
    },

    onMenuButtonClick: function (event) {
        var it = this;
        event.original.preventDefault();
        if (event.context.Url) {
            it.NavigateTo(event.context.Url);
        };
    }
});