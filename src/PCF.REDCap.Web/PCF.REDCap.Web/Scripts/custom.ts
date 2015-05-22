/// <reference path="typings/jquery/jquery.d.ts" />

interface Window {
    jQuery: JQueryStatic;
    NProgress: any;// TODO: Proper typing
    toastr: any;
    App: any;
}

interface JQueryStatic {
    active: number;
    event: any;
};

interface JQuery {
    modal: any;
};

(function (window: Window, $: JQueryStatic, NProgress: any, toastr: any) {// TODO: Proper $ type jQueryStatic, need to handle extension unless we change it. (probably should)
    var App = window.App || {};

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

    //App.Object = function () {
    //    return $.isFunction(Object.create)
    //        ? Object.create(null)
    //        : {};
    //};

    App.Nonce = function () {
        return parseInt(new Date().getTime().toString() + ((Math.random() * 1e5) | 0), 10).toString(36);
    };

    //$.ajaxSetup({
    //    statusCode: {
    //        201: function (data: any, textStatus: string, jqXHR: JQueryXHR): void {
    //            var location = jqXHR.getResponseHeader("location");
    //            if (location !== null) {
    //                //Perform sub-request? Not sure this is possible, it would either need to be sync or have a way to replace the deferred object.
    //            }
    //        }
    //    }
    //});

    //#region Error Handlers

    var handle401 = function (jqXHR, messageHandler) {
        if ($.isFunction(messageHandler)) {
            var response = jqXHR.responseJSON || {};
            var message = response.Message || "An unknown authorization error has occured.";
            messageHandler(message);
            //Redirect to login page? prompt?
        }
    };

    var handle400 = function (jqXHR, errorThrown, messageHandler) {
        if ($.isFunction(messageHandler)) {
            var response = jqXHR.responseJSON || {};
            var state = response.ModelState || {};

            var fields = [];
            var errors = [];
            $.map(state, function (value, key) {
                fields.push(key.replace(/^[^\.]+\./, ""));//I'm not entirely sure this is a safe replace.
                errors.push(value.join("<br>"));
            });

            var message = errors.join("<br>") || errorThrown;
            messageHandler(message);
        }
    };

    var handle404 = handle400;

    var handle500 = function (jqXHR, messageHandler) {
        if ($.isFunction(messageHandler)) {
            var response = jqXHR.responseJSON || {};
            var message = response.Message || "An unknown error has occured.";

            if (response.MessageDetail) {
                messageHandler(response.MessageDetail, message);
            } else {
                messageHandler(message);
            }
        }
    };

    //#endregion Error Handlers

    //#region Ajax Handlers

    var tokenCache = {};

    var getToken = function (url: string, selector?: string, timeout?: number): JQueryPromise<any> {
        var promise = tokenCache[url];
        if (promise) {
            return promise;
        }

        var delay = timeout || 30 * 1000;
        var input = selector || "#VerificationToken";
        var src = url + (url.indexOf("?") > -1 ? "&" : "?") + "_=" + App.Nonce();
        var dfd = $.Deferred();

        if ($.active++ === 0) {//from jquery/src/ajax.js
            $.event.trigger("ajaxStart");
        }

        var $iframe = $('<iframe src="' + src + '" style="height: 0; width: 0; border: 0; padding: 0; margin: 0; position: absolute; top: 0; left: 0;">')
            .on("load", function () {
            var token: string = $(this).contents().find(input).val();
            if (token) {
                dfd.resolve(token);
            } else {
                dfd.reject();
            }

            $iframe.remove();
        })
        .appendTo("body");

        setTimeout(function () {
            if (dfd.state() === "pending") {
                dfd.reject();
                $iframe.remove();
            }
        }, delay);

        promise = tokenCache[url] = dfd.promise()
        .fail(function () {
            tokenCache[url] = null;
            //TODO: Trigger an handler event
        })
        .always(function () {
            if (!(--$.active)) {//from jquery/src/ajax.js //Will this call multiple times?
                $.event.trigger("ajaxStop");
            }
        });

        return promise;
    };

    var getTokenMethod = function (verb: string): Function {
        return function (url: string, data?: any, settings?: any): JQueryPromise<{}> {
            var dfd = $.Deferred();

            getToken("/token")
            .always(function (token) {
                var opts = $.extend({}, { data: data }, settings, { type: verb, headers: { "X-CSRF-Token": token } });
                App.Ajax(url, opts)
                .done(function () {
                    dfd.resolveWith(this, arguments);
                })
                .fail(function () {
                    dfd.rejectWith(this, arguments);
                });
            });
            return dfd.promise();
        };
    };

    App.Ajax = function (url: string, settings?: any) {
        var defaults = {
            type: "POST",
            data: null,
            dataType: "json",
            messageHandler: toastr.error,
            statusCode: {
                201: function (jqXHR, textStatus, errorThrown) { },//TODO: Can we fetch the sub-location if there is one?
                400: function (jqXHR, textStatus, errorThrown) { handle400(jqXHR, errorThrown, this.messageHandler); },
                401: function (jqXHR, textStatus, errorThrown) { handle401(jqXHR, this.messageHandler); },
                404: function (jqXHR, textStatus, errorThrown) { handle404(jqXHR, errorThrown, this.messageHandler); },
                500: function (jqXHR, textStatus, errorThrown) { handle500(jqXHR, this.messageHandler); },
            },
        };

        var opts = $.extend({}, defaults, settings);
        return $.ajax(url, opts);
    };

    App.Get = function (url: string, data?: any, settings?: any) {
        var opts = $.extend({}, { data: data }, settings, { type: "GET" });
        return App.Ajax(url, opts);
    };
    App.Delete = getTokenMethod("DELETE");
    App.Patch = getTokenMethod("PATCH");
    App.Post = getTokenMethod("POST");
    App.Put = getTokenMethod("PUT");

    //#endregion Ajax Handlers
    
    $(document)
    .ajaxStart(function (): void {
        $('html').addClass('busy');
        NProgress.start();
    })
    .ajaxStop(function (): void {
        $('html').removeClass('busy');
        NProgress.done();
    });

    window.App = App;
} (window, window.jQuery, window.NProgress, window.toastr));