Ractive.components.AuthorList = Ractive.extend({
    template: Url.GetTemplate('/Templates/authors/AuthorList.html'),
    isolated: false,
    data: function () {
        var it = this;
        return {
            filter: 
                {
                    NameOrder: '',
                    SurnameOrder: '',
                },
            Items: [],
        };
    },

    oninit: function () {
        var it = this;

        it.on({
            'onAuthorEditButtonClick'           : $.proxy(it.onAuthorEditButtonClick, it),
            'onAddAuthorButtonClick'            : $.proxy(it.onAddAuthorButtonClick, it),
            'onDeleteButtonClick'               : $.proxy(it.onDeleteButtonClick, it),
            'onNameLabelClick'                  : $.proxy(it.onNameLabelClick, it)
        });

        if (sessionStorage.getItem('NameOrder')) {
            it.set('filter.NameOrder', sessionStorage.getItem('NameOrder'));
        }

        if (sessionStorage.getItem('SurnameOrder')) {
            it.set('filter.SurnameOrder', sessionStorage.getItem('SurnameOrder'));
        }

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

    onNameLabelClick: function () {
        var it = this;
        var nameOrder = it.get('filter.NameOrder');

        switch (nameOrder) {
            case '':
                nameOrder = 'asc';
                break;
            case 'asc':
                nameOrder = 'desc';
                break;
            case 'desc':
                nameOrder = '';
                break;
        }

        it.set('filter.NameOrder', nameOrder);
        sessionStorage.setItem('NameOrder', nameOrder);
        it.getAuthorList();
    },

    getAuthorList: function () {
        var it = this;

        var filter = it.get("filter");

        it.apiUrlGet('/api/author', filter, function (data) {
            it.set('Items', data);
        })
    }
});