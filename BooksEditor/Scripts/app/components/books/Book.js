Ractive.components.Book = Ractive.extend({
    template: Url.GetTemplate('/Templates/books/Book.html'),
    data: function () {
        var it = this;

        return {
            sectionTitle: 'Edit book',
            id: null,
            book: {},
            allAuthors: []
        }
    },
    
    oninit: function () {
        var it = this;
        var params = it.get('params');

        it.on({
            'onSaveButton'                  : $.proxy(it.onSaveButton, it),
            'onCancelButton'                : $.proxy(it.onCancelButton, it),
            'onImageUploadSuccess'          : $.proxy(it.onImageUploadSuccess, it),
            'onImageUploadError'            : $.proxy(it.onImageUploadError, it),
            'onImageDeleteClick'            : $.proxy(it.onImageDeleteClick, it)
        });

        it.decorators.chosen.type.authors = {
            width: '500px',
            onchange: function (event, params) {
                $('#book').validate().element('#authors');
            }
        }

        if (params && params.id) {
            it.set('id', params.id);
        }

        it.set('allAuthors', it.getAuthors());

        it.loadBook();
    },

    getAuthors: function () {
        var it = this;

        it.apiUrlGet('/api/author', null, function (data) {
            if (!data)
                return;

            var authors = [];

            data.forEach(function (item) {
                authors.push({ Id: item.Id, Name: item.Name, Surname: item.Surname })
            });

            it.set('allAuthors', authors);
        });
    },

    loadBook: function () {
        var it = this;
        var id = it.get('id');

        if (id == null) {
            it.set('sectionTitle', 'Add book');
        }
        else {
            it.apiUrlGet('/api/book/' + id, null, function (data) {
                it.set('book', data);
            });
        }
    },

    onSaveButton: function () {
        var it = this;

        $.validator.unobtrusive.parseDynamicContent($('#book'));

        if ($('#book').valid()) {

            var params = it.get('book');

            // Remove value of first disabled element from chosen select
            if (typeof params.Authors[0] === 'undefined') {
                params.Authors.shift();
            }

            it.apiUrlPost('/api/book/', params, function (data) {
                if (data.IsSuccess) {
                    it.set('errors', null);
                    it.NavigateTo('/books/list');
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

    onImageUploadSuccess: function (event) {
        var it = this;
        console.log(event.keypath);

        it.set(event.keypath + '.ImagePath', event.response.Url);
    },

    onImageUploadError: function (event) {
        // TODO: show modal window with error message
        console.log(event.response.Message);
    },

    onImageDeleteClick: function (event) {
        var it = this;

        it.set(event.keypath + '.ImagePath', null);
    },

    onCancelButton: function () {
        var it = this;

        it.NavigateTo('/books/list');
    },
});