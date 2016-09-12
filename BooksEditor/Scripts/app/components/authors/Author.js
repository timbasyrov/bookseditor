Ractive.components.Author = Ractive.extend({
    template: Url.GetTemplate('/Templates/authors/Author.html'),
    data: function () {
        var it = this;

        return {
            sectionTitle: 'Edit author',
            id: null,
            author: {},
        }
    },
    
    oninit: function () {
        var it = this;
        var params = it.get('params');

        it.on({
            'onSaveButton'                   : $.proxy(it.onSaveButton, it),
            'onCancelButton'                : $.proxy(it.onCancelButton, it),
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
            it.apiUrlGet('/api/author/' + id, null, function (data) {
                it.set('author', data);
            }, function (data) {
                if (data.status == 404) {
                    it.set('author', null);
                } else {
                    // Request error
                    console.log(data);
                }
            });
        }
    },

    onSaveButton: function () {
        var it = this;

        $.validator.unobtrusive.parseDynamicContent($('#author'));

        if ($('#author').valid()) {
            var params = it.get('author');

            // Edit or add data
            var apiUrl = params.Id ? '/api/author/' + params.Id : '/api/author';
            var httpMethod = params.Id ? 'put' : 'post';

            it.apiUrlCall(apiUrl, httpMethod, params, function (data) {
                it.NavigateTo('/authors/list');
            }, function (data) {
                // If not found or validation errors
                if (data.status == 404 || data.status == 422) {
                    it.set('errors', data.responseJSON.Errors);
                } else {
                    console.log(data);
                }
            });
        }
    },

    onCancelButton: function () {
        var it = this;

        it.NavigateTo('/authors/list');
    },
});