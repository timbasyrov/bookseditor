var fileuploadDecorator;

fileuploadDecorator = function (node, options) {
    var $node = $(node);
    var ractive = node._ractive.root;
    var accept = node.accept;   

    if (!options)
        throw 'options not specified';

    if (!options.url)
        throw 'url not specified';

    if (!options.maxFileSize)
        throw 'maxFileSize not specified';

    options.allowedExtensions = ['.jpg', '.jpeg', '.bmp', '.gif', '.png'];

    $node.on('change', function (event) {
        var file = node.files[0];

        if (!file) return;

        if (!isValidExtension(file.name, options.allowedExtensions)) {
            alert('Invalid file extension. Valid extensions are: ' + options.allowedExtensions.join(', '));
            node.value = '';
            return;
        }

        if (file.size > options.maxFileSize) {
            alert('Wrong file size. Max file size allowed: ' + (options.maxFileSize / 1024) + ' Kb');
            node.value = '';
            return;
        }

        ractive.apiUrlPostFile(options.url, file,
            function (response) {
                if (response.isSuccess) {
                    $node.trigger('r_fileupload_success', response);
                } else {
                    $node.trigger('r_fileupload_error', response);
                }
            },
            function (error) {
                $node.trigger('r_fileupload_error', error);
            });
        node.value = '';
    });

    return {
        teardown: function () {
            //
        }
    }
}

Ractive.decorators.fileupload = fileuploadDecorator;

//event plugins
Ractive.events.r_fileupload_success = function (node, fire) {
    var $node = $(node);
    $node.on('r_fileupload_success',
        function (evt, data) {
            evt.preventDefault();
            fire({
                node: node,
                original: evt,
                response: data
            });
        });
}

Ractive.events.r_fileupload_error = function (node, fire) {
    var $node = $(node);
    $node.on('r_fileupload_error',
        function (evt, data) {
            evt.preventDefault();
            fire({
                node: node,
                original: evt,
                response: data
            });
        });
}

isValidExtension = function (fileName, exts) {
    return (new RegExp('(' + exts.join('|').replace(/\./g, '\\.') + ')$', 'i')).test(fileName);
}