Ractive.components.Book = Ractive.extend({
    template: Url.GetTemplate('/Templates/books/Book.html'),
    data: function () {
        var it = this;

        return {
            sectionTitle: 'Edit book',
            id: null,
            book: {},
            allAuthors: [],
            maxPublicationYearAllowed: function () {
                var year = new Date();
                // Sometimes publication of book planned in near future
                return year.getFullYear() + 3;
            }
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
            // Need to initialize field Authors with empty value for valid work of chosen dropdown
            it.set('book', { Authors: [] });
            it.set('sectionTitle', 'Add book');
        }
        else {
            it.apiUrlGet('/api/book/' + id, null, function (data) {
                it.set('book', data);
            }, function (data) {
                if (data.status == 404) {
                    it.set('book', null);
                } else {
                    // Request error
                    console.log(data);
                }
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

            // Edit or add data
            var apiUrl = params.Id ? '/api/book/' + params.Id : '/api/book';
            var httpMethod = params.Id ? 'put' : 'post';

            it.apiUrlCall(apiUrl, httpMethod, params, function (data) {
                it.NavigateTo('/books/list');
            }, function (data) {
                // If not found or validation errors
                if (data.status == 404 || data.status == 422) {
                    it.set('errors', data.responseJSON.Errors);
                } else {
                    // Request error
                    console.log(data);
                }
            });
        }
    },

    onImageUploadSuccess: function (event) {
        var it = this;

        it.set(event.keypath + '.ImageUrl', event.response.Url);
    },

    onImageUploadError: function (event) {
        console.log(event.response.Message);
    },

    onImageDeleteClick: function (event) {
        var it = this;

        it.set(event.keypath + '.ImageUrl', null);
    },

    onCancelButton: function () {
        var it = this;

        it.NavigateTo('/books/list');
    },
});