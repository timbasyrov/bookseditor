Ractive.components.AuthorList = Ractive.extend({
    template: Url.GetTemplate('/Templates/authors/AuthorList.html'),
    isolated: false,
    data: function () {
        var it = this;
        return {
            Items: [],
        };
    },

    oninit: function () {
        var it = this;

        it.on({
            'onAuthorEditButtonClick'           : $.proxy(it.onAuthorEditButtonClick, it),
            'onAddAuthorButtonClick'            : $.proxy(it.onAddAuthorButtonClick, it),
            'onDeleteButtonClick'               : $.proxy(it.onDeleteButtonClick, it)
        });

        it.getAuthorList();
    },
    
    onAuthorEditButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/authors/' + event.context.Id);
    },

    onAddAuthorButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/authors');
    },

    onDeleteButtonClick: function (event) {
        var it = this;
        
        it.apiUrlDelete('/api/author/' + event.context.Id, null, function (data) {
            console.log(data);

            if (data.IsSuccess) {
                it.getAuthorList();
            } else {
                // TODO: show modal window with message
                console.log(data);
            }
        }, function (data) {
            // Request error
            console.log(data);
        });
    },

    getAuthorList: function () {
        var it = this;

        it.apiUrlGet('/api/author', null, function (data) {
            it.set('Items', data);
        })
    }
});