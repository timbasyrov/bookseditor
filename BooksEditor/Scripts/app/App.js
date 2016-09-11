App = new Ractive({

    el: '#main',
    _navigationPanel: null,
    template: '<Router componentName="{{componentName}}"/>',
    data: {
        componentName: 'BookList'
    },
    oninit: function () {
        var it = this;

        var navigationPanel = new it.components.NavigationPanel({
            el: "#navigation_panel",
            data: {
                navigation: [
                        {
                            Title: 'Books',
                            ImageClass: 'fa-book',
                            Url: '/books/list'
                        },
                        {
                            Title: 'Authors',
                            ImageClass: 'fa-user',
                            Url: '/authors/list'
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

routeConfig.AddRoute('/books/list', 'BookList', true);
routeConfig.AddRoute('/books/:id', 'Book');
routeConfig.AddRoute('/books/', 'Book');
routeConfig.AddRoute('/authors/list', 'AuthorList');
routeConfig.AddRoute('/authors/:id', 'Author');
routeConfig.AddRoute('/authors/', 'Author');
routeConfig.AddRoute('*', 'Page404');

App.RegisterRoutes(routeConfig);

Ractive.prototype.NavigateTo = function (clientUrl) {
    page.show(clientUrl);
};