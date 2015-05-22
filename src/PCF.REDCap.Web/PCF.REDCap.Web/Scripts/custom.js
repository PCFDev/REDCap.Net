/* tslint:disable:max-line-length */
/* tslint:disable:comment-format */
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/nprogress/NProgress.d.ts" />
/// <reference path="typings/toastr/toastr.d.ts" />
;
;
(function (window, $, NProgress, toastr) {
    var App = window.App || {};
    $(function () {
        $("#logout").on("click", function (e) {
            e.preventDefault();
            App.Post("/api/v1/account/logout").done(function (response) {
                if (response.Success) {
                    document.location.reload();
                }
            });
        });
    });
    //#region Misc
    //App.Object = function (): any {
    //    return $.isFunction(Object.create)
    //        ? Object.create(null)
    //        : {};
    //};
    App.Nonce = function () {
        /* tslint:disable:no-bitwise */
        return parseInt(new Date().getTime().toString() + ((Math.random() * 1e5) | 0), 10).toString(36);
        /* tslint:enable:no-bitwise */
    };
    //#endregion Misc
    //#region Error Handlers
    function handle201(data, textStatus, jqXHR) {
        //TODO: Can we fetch the sub-location if there is one?
        //var location: string = jqXHR.getResponseHeader("location");
        //if (location !== null) {
        //    //Perform sub-request? Not sure this is possible, it would either need to be sync or have a way to replace the deferred object.
        //}
    }
    function handle401(jqXHR, messageHandler) {
        if ($.isFunction(messageHandler)) {
            var response = jqXHR.responseJSON || {};
            var message = response.Message || "An unknown authorization error has occured.";
            messageHandler(message);
        }
    }
    function handle400(jqXHR, errorThrown, messageHandler) {
        if ($.isFunction(messageHandler)) {
            var response = jqXHR.responseJSON || {};
            var state = response.ModelState || {};
            var fields = [];
            var errors = [];
            $.map(state, function (value, key) {
                fields.push(key.replace(/^[^\.]+\./, "")); //I'm not entirely sure this is a safe replace.
                errors.push(value.join("<br>"));
            });
            var message = errors.join("<br>") || errorThrown;
            messageHandler(message);
        }
    }
    function handle404(jqXHR, errorThrown, messageHandler) {
        handle400(jqXHR, errorThrown, messageHandler);
    }
    function handle500(jqXHR, messageHandler) {
        if ($.isFunction(messageHandler)) {
            var response = jqXHR.responseJSON || {};
            var message = response.Message || "An unknown error has occured.";
            if (response.MessageDetail) {
                messageHandler(response.MessageDetail, message);
            }
            else {
                messageHandler(message);
            }
        }
    }
    //#endregion Error Handlers
    //#region Ajax Handlers
    var tokenCache = {};
    function getToken(url, selector, timeout) {
        var promise = tokenCache[url];
        if (promise) {
            return promise;
        }
        var delay = timeout || 30 * 1000;
        var input = selector || "#VerificationToken";
        var src = url + (url.indexOf("?") > -1 ? "&" : "?") + "_=" + App.Nonce();
        var dfd = $.Deferred();
        if ($.active++ === 0) {
            $.event.trigger("ajaxStart");
        }
        var $iframe = $("<iframe src='" + src + "' style='height: 0; width: 0; border: 0; padding: 0; margin: 0; position: absolute; top: 0; left: 0;'>").on("load", function () {
            var token = $(this).contents().find(input).val();
            if (token) {
                dfd.resolve(token);
            }
            else {
                dfd.reject();
            }
            $iframe.remove();
        }).appendTo("body");
        setTimeout(function () {
            if (dfd.state() === "pending") {
                dfd.reject();
                $iframe.remove();
            }
        }, delay);
        promise = tokenCache[url] = dfd.promise().fail(function () {
            tokenCache[url] = null;
            //TODO: Trigger an handler event
        }).always(function () {
            if (!(--$.active)) {
                $.event.trigger("ajaxStop");
            }
        });
        return promise;
    }
    ;
    function getTokenMethod(verb) {
        return function (url, data, settings) {
            var dfd = $.Deferred();
            getToken("/token").always(function (token) {
                var opts = $.extend({}, { data: data }, settings, { type: verb, headers: { "X-CSRF-Token": token } });
                App.Ajax(url, opts).done(function () {
                    dfd.resolveWith(this, arguments);
                }).fail(function () {
                    dfd.rejectWith(this, arguments);
                });
            });
            return dfd.promise();
        };
    }
    ;
    App.Ajax = function (url, settings) {
        var defaults = {
            type: "POST",
            data: null,
            dataType: "json",
            messageHandler: toastr.error,
            statusCode: {
                201: handle201,
                400: function (jqXHR, textStatus, errorThrown) {
                    handle400(jqXHR, errorThrown, this.messageHandler);
                },
                401: function (jqXHR, textStatus, errorThrown) {
                    handle401(jqXHR, this.messageHandler);
                },
                404: function (jqXHR, textStatus, errorThrown) {
                    handle404(jqXHR, errorThrown, this.messageHandler);
                },
                500: function (jqXHR, textStatus, errorThrown) {
                    handle500(jqXHR, this.messageHandler);
                }
            }
        };
        var opts = $.extend({}, defaults, settings);
        return $.ajax(url, opts);
    };
    App.Get = function (url, data, settings) {
        var opts = $.extend({}, { data: data }, settings, { type: "GET" });
        return App.Ajax(url, opts);
    };
    App.Delete = getTokenMethod("DELETE");
    App.Patch = getTokenMethod("PATCH");
    App.Post = getTokenMethod("POST");
    App.Put = getTokenMethod("PUT");
    //#endregion Ajax Handlers
    $(document).ajaxStart(function () {
        $("html").addClass("busy");
        NProgress.start();
    }).ajaxStop(function () {
        $("html").removeClass("busy");
        NProgress.done();
    });
    window.App = App;
}(window, window.jQuery, window.NProgress, window.toastr));
//# sourceMappingURL=custom.js.map