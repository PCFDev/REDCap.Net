/// <reference path="typings/jquery/jquery.d.ts" />

interface Window {
    jQuery: JQueryStatic;
    NProgress: any;// TODO: Proper typing
}

interface JQueryStatic {
    put: (url: string, data?: any, callback?: any, type?: string) => JQueryXHR;
    patch: (url: string, data?: any, callback?: any, type?: string) => JQueryXHR;
    delete: (url: string, data?: any, callback?: any, type?: string) => JQueryXHR;
};

(function (window: Window, $: JQueryStatic, NProgress: any) {// TODO: Proper $ type jQueryStatic, need to handle extension unless we change it. (probably should)
    $(function () {
        $("#logout").on("click", function (e: JQueryEventObject) {
            e.preventDefault();

            $.post("/api/v1/account/logout")
            .done(function (response: any) {
                if (response.Success) {
                    document.location.reload();
                }
            });
        });
    });

    $.put = function (url: string, data?: any, callback?: any, type?: string): JQueryXHR {// TODO: Proper types
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

    $.patch = function (url: string, data?: any, callback?: any, type?: string): JQueryXHR {// TODO: Proper types
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

    $.delete = function (url: string, data?: any, callback?: any, type?: string): JQueryXHR {// TODO: Proper types
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

    $(document)
    .ajaxStart(function (): void {
        NProgress.start();
    })
    .ajaxStop(function (): void {
        NProgress.done();
    });

} (window, window.jQuery, window.NProgress));