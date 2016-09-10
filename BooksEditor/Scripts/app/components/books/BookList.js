Ractive.components.BookList = Ractive.extend({
    template: Url.GetTemplate('/Templates/books/BookList.html'),
    isolated: false,
    data: function () {
        var it = this;
        return {
            filter: 
                {
                    TitleOrder: '',
                    YearOrder: '',
                },
            Items: [],
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

        if (sessionStorage.getItem('TitleOrder')) {
            it.set('filter.TitleOrder', sessionStorage.getItem('TitleOrder'));
        }

        if (sessionStorage.getItem('YearOrder')) {
            it.set('filter.YearOrder', sessionStorage.getItem('YearOrder'));
        }

        it.getBookList();
    },
    
    onBookEditButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/books/' + event.context.Id);
    },

    onAddBookButtonClick: function (event) {
        var it = this;
        it.NavigateTo('/books');
    },

    onDeleteButtonClick: function (event) {
        var it = this;
        
        it.apiUrlDelete('/api/book/' + event.context.Id, null, function (data) {
            console.log(data);

            if (data.IsSuccess) {
                it.getBookList();
            } else {
                // TODO: show modal window with message
                console.log(data);
            }
        }, function (data) {
            // Request error
            console.log(data);
        });
    },

    onTitleLabelClick: function () {
        var it = this;

        var titleOrder = it.get('filter.TitleOrder');

        switch (titleOrder) {
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

        it.set('filter.TitleOrder', titleOrder);
        it.set('filter.YearOrder', '');
        sessionStorage.setItem('TitleOrder', titleOrder);
        sessionStorage.setItem('YearOrder', '');

        it.getBookList();
    },

    onYearLabelClick: function () {
        var it = this;

        var yearOrder = it.get('filter.YearOrder');

        switch (yearOrder) {
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

        it.set('filter.YearOrder', yearOrder);
        it.set('filter.TitleOrder', '');
        sessionStorage.setItem('YearOrder', yearOrder);
        sessionStorage.setItem('TitleOrder', '');

        it.getBookList();
    },

    getBookList: function () {
        var it = this;

        var filter = it.get("filter");

        it.apiUrlGet('/api/book', filter, function (data) {
            it.set('Items', data);
        })
    }
});