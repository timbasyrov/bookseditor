Ractive.components.Author = Ractive.extend({
    template: Url.GetTemplate('/Templates/authors/Author.html'),
    data: function () {
        var it = this;

        return {
            sectionTitle: 'View author',
            id: null,
            author: {},
        }
    },
    
    oninit: function () {
        var it = this;
        var params = it.get('params');

        it.on({
            'onSaveButton': $.proxy(it.onSaveButton, it),
            'onCancelButton': $.proxy(it.onCancelButton, it),
        });

        if (params && params.id) {
            it.set('id', params.id);
        }

        it.loadAuthor();
    },

    loadAuthor: function () {
        var it = this;
        var id = it.get('id');

        if (id == null) {
            it.set('sectionTitle', 'Add author');
        }
        else {
            $.getJSON('/api/author/' + id, null, function (data) {
                it.set('author', data);
            });
        }
    },

    onSaveButton: function () {
        var it = this;

        $.validator.unobtrusive.parseDynamicContent($('#author'));

        if ($('#author').valid()) {
            var params = it.get('author');

            it.apiUrlPost('/api/author/', params, function (data) {
                if (data.IsSuccess) {
                    it.set('errors', null);
                    it.NavigateTo('/authors/list');
                } else {
                    it.set('errors', data.Errors);
                    // Operation failed
                    console.log(data);
                }
            }, function (data) {
                // Request error
                console.log(data);
            });
        }
    },

    onCancelButton: function () {
        var it = this;

        it.NavigateTo('/authors/list');
    },
});