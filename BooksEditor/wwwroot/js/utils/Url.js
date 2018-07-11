Url = {
    GetTemplate: function(url) {
        return Url.GetPage(url).html;
    },

    GetPage: function (url) {

        var result = {
            result: false,
            html: 'Failed to get page from "' + url + '" </br>review the console',
            status: "error",
            xhr: null
        };

        result.xhr = $.ajax({
            url: url,
            dataType: "html",
            async: false,
            success:
                function (pageHtml, textStatus, xhr) {
                    result.html = pageHtml;
                    result.result = true;
                    result.status = xhr.statusText
                },
            error:
                function (xhr, thrownError) {
                    result.status = xhr.statusText
                    console.log('Failed to get page from "' + url + '"; error: "' + thrownError + '". Response follows:');
                    console.log(xhr.responseText);
                }
        });
        return result;
    },
};