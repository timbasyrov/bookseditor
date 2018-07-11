Ractive.components.AuthorList = Ractive.extend({
    template: Url.GetTemplate('/Templates/authors/AuthorList.html'),
    isolated: false,
    data: function () {
        var it = this;
        return {
            items: [],
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
        it.NavigateTo('/authors/' + event.context.id);
    },

    onAddAuthorButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/authors');
    },

    onDeleteButtonClick: function (event) {
        var it = this;
        
        it.apiUrlDelete('/api/author/' + event.context.id, null, function (data) {
                it.getAuthorList();
        }, function (data) {
            if (data.status == 404 || data.status == 422) {
                it.set('modalText', data.responseJSON.errors);
                $('#modal-dialog').modal('show');
            } else {
                console.log(data);
            }
        });
    },

    getAuthorList: function () {
        var it = this;

        it.apiUrlGet('/api/author', null, function (data) {
            it.set('items', data);
        })
    }
});