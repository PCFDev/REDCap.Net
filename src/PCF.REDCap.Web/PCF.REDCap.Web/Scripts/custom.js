/// <reference path="typings/jquery/jquery.d.ts" />
;
(function (window, $, NProgress) {
    $(function () {
        $("#logout").on("click", function (e) {
            e.preventDefault();
            $.post("/api/v1/account/logout").done(function (response) {
                if (response.Success) {
                    document.location.reload();
                }
            });
        });
    });
    //$.ajaxSetup({
    //    statusCode: {
    //        201: function (data: any, textStatus: string, jqXHR: JQueryXHR): void {
    //            var location = jqXHR.getResponseHeader('location');
    //            if (location !== null) {
    //                //Perform sub-request? Not sure this is possible, it would either need to be sync or have a way to replace the deferred object.
    //            }
    //        }
    //    }
    //});
    $.put = function (url, data, callback, type) {
        // Shift arguments if data argument was omitted
        if ($.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = undefined;
        }
        return $.ajax({
            url: url,
            type: "PUT",
            dataType: type,
            data: data,
            success: callback
        });
    };
    $.patch = function (url, data, callback, type) {
        // Shift arguments if data argument was omitted
        if ($.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = undefined;
        }
        return $.ajax({
            url: url,
            type: "PATCH",
            dataType: type,
            data: data,
            success: callback
        });
    };
    $.delete = function (url, data, callback, type) {
        // Shift arguments if data argument was omitted
        if ($.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = undefined;
        }
        return $.ajax({
            url: url,
            type: "DELETE",
            dataType: type,
            data: data,
            success: callback
        });
    };
    $(document).ajaxStart(function () {
        NProgress.start();
    }).ajaxStop(function () {
        NProgress.done();
    });
}(window, window.jQuery, window.NProgress));
//# sourceMappingURL=custom.js.map