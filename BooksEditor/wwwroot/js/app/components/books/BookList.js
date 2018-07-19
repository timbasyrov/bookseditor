Ractive.components.BookList = Ractive.extend({
    template: Url.GetTemplate('/Templates/books/BookList.html'),
    isolated: false,
    data: function () {
        var it = this;
        return {
            sort: 
                {
                    titleOrder: '',
                    yearOrder: '',
                },
            items: [],
        };
    },

    oninit: function () {
        var it = this;

        it.on({
            'onBookEditButtonClick'             : $.proxy(it.onBookEditButtonClick, it),
            'onAddBookButtonClick'              : $.proxy(it.onAddBookButtonClick, it),
            'onDeleteButtonClick'               : $.proxy(it.onDeleteButtonClick, it),
            'onTitleLabelClick'                 : $.proxy(it.onTitleLabelClick, it),
            'onYearLabelClick'                  : $.proxy(it.onYearLabelClick, it)
        });

        it.set('sort.TitleOrder', sessionStorage.getItem('TitleOrder') ? sessionStorage.getItem('TitleOrder') : '');
        it.set('sort.YearOrder', sessionStorage.getItem('YearOrder') ? sessionStorage.getItem('YearOrder') : '');

        it.getBookList();
    },
    
    onBookEditButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/books/' + event.context.id);
    },

    onAddBookButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/books');
    },

    onDeleteButtonClick: function (event) {
        var it = this;
        
        it.apiUrlDelete('/api/book/' + event.context.id, null, function (data) {
            it.getBookList();
        }, function (data) {
            if (data.status == 404) {
                alert('Book not found');
            } else {
                console.log(data);
            }
        });
    },

    onTitleLabelClick: function () {
        var it = this;

        var titleOrder = it.get('sort.titleOrder');

        switch (titleOrder) {
            case null:
            case '':
                titleOrder = 'asc';
                break;
            case 'asc':
                titleOrder = 'desc';
                break;
            case 'desc':
                titleOrder = '';
                break;
        }

        it.set('sort.titleOrder', titleOrder);
        it.set('sort.yearOrder', '');
        sessionStorage.setItem('titleOrder', titleOrder);
        sessionStorage.setItem('yearOrder', '');

        it.getBookList();
    },

    onYearLabelClick: function () {
        var it = this;

        var yearOrder = it.get('sort.yearOrder');

        switch (yearOrder) {
            case null:
            case '':
                yearOrder = 'asc';
                break;
            case 'asc':
                yearOrder = 'desc';
                break;
            case 'desc':
                yearOrder = '';
                break;
        }

        it.set('sort.yearOrder', yearOrder);
        it.set('sort.titleOrder', '');
        sessionStorage.setItem('yearOrder', yearOrder);
        sessionStorage.setItem('titleOrder', '');

        it.getBookList();
    },

    getBookList: function () {
        var it = this;

        var sort = it.get("sort");
        if (sort.TitleOrder === '')
            delete sort.TitleOrder;
        if (sort.YearOrder === '')
            delete sort.YearOrder;

        it.apiUrlGet('/api/book', sort, function (data) {
            it.set('items', data);
        })
    }
});