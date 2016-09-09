﻿Ractive.prototype.apiUrlCall = function (url, method, params, onSuccess, onError) {
    var it = this;

    var ajaxData = params;
    
    if (method == 'post') {
        ajaxData = JSON.stringify(ajaxData);
    };

    return $.ajax({
        dataType: "json",
        contentType: method == 'post'? "application/json" : "",
        method: method,
        url: url,
        data: ajaxData,
        success: onSuccess,
        error: onError
    });
};

Ractive.prototype.apiUrlGet = function (url, params, onSuccess, onError) {
    return this.apiUrlCall(url, HttpMethods.GET, params, onSuccess, onError);
};

Ractive.prototype.apiUrlPost = function (url, params, onSuccess, onError) {
    return this.apiUrlCall(url, HttpMethods.POST, params, onSuccess, onError);
};

Ractive.prototype.apiUrlDelete = function (url, params, onSuccess, onError) {
    return this.apiUrlCall(url, HttpMethods.DELETE, params, onSuccess, onError);
};

HttpMethods = {
    GET: 'get',
    POST: 'post',
    DELETE: 'delete'
};
