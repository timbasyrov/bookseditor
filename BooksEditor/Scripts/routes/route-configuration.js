var RouteConfiguration = function ()
{
};

RouteConfiguration.prototype = {
    _routes: [],

    AddRoute: function (route, componentName, isDefault) {
        this._routes.push({ route: route, componentName: componentName, isDefault: isDefault });
    },

    Register: function (app) {
        var it = this;

        // set hashband as root for client routing; that means, that reload page won't cause 404
        page.base('/#!');
        
        // finding default route, if multiple routes were marked as default, we take the first one.
        var defaultRoute = _.find(it._routes, function (route) {
            return route.isDefault;
        });

        // if no one was marked as default we take the first added route.
        if (!defaultRoute) {
            defaultRoute = it._routes[0];
        };

        if (defaultRoute) {
            page('/', defaultRoute.route);
        }
        
        _.each(it._routes, function (route) {
            page(route.route, function(context, next){
                context.componentName = route.componentName;
                app.onNavigation(null, context);
            });
        });

        //set all other routes *
        page();

        page({ hashbang: true });
    },
};