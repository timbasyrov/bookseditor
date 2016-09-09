App = new Ractive({

    el: '#main',
    _navigationPanel: null,
    template: '<Router componentName="{{componentName}}"/>',
    data: {
        componentName: 'AuthorList'
    },
    oninit: function () {
        var it = this;

        var navigationPanel = new it.components.NavigationPanel({
            el: "#navigation_panel",
            data: {
                navigation: [
                        {
                            Title: 'Authors',
                            ImageClass: 'fa-files-o',
                            Url: '/authors/list'
                        },
                        {
                            Title: 'Books',
                            ImageClass: 'fa-edit',
                            Url: '/books/list'
                        }
                    ]
                }
        })
    },
    onNavigation: function (error, navigationContext) {
        if (error) {
            console.warn('App: onNavigation error', error);
        } else {
            this.set({
                params: $.extend(true, {},  navigationContext.params),
                componentName: navigationContext.componentName
            });
        }
    },

    RegisterRoutes: function (routeConfiguration) {
        var it = this;
        routeConfiguration.Register(this);
    }
});

var routeConfig = new RouteConfiguration();

routeConfig.AddRoute('/authors/list', 'AuthorList', true);
routeConfig.AddRoute('/authors/:id', 'Author');
routeConfig.AddRoute('/authors/', 'Author');

App.RegisterRoutes(routeConfig);

Ractive.prototype.NavigateTo = function (clientUrl) {
    page.show(clientUrl);
};