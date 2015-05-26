﻿/* tslint:disable:max-line-length */
/* tslint:disable:comment-format */

/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/nprogress/NProgress.d.ts" />
/// <reference path="typings/toastr/toastr.d.ts" />

/* tslint:disable:interface-name */
interface Window {
/* tslint:enable:interface-name */
    jQuery: JQueryStatic;
    ko: KnockoutStatic;
    NProgress: NProgressStatic;
    toastr: Toastr;
    App: IApp;
}

/* tslint:disable:interface-name */
interface JQueryStatic {
/* tslint:enable:interface-name */
    active: number;
    event: any;
};

/* tslint:disable:interface-name */
interface KnockoutStatic {
/* tslint:enable:interface-name */
    templates: { [name: string]: string };
}

//https://stackoverflow.com/questions/12766528/build-a-function-object-with-properties-in-typescript
interface IModal {
    (template: string, settings?: any): JQuery;
    Defaults: any;
};

interface IApp {
    Nonce: () => string;
    //Modal: { (template: string, settings?: any): JQuery; Defaults: any; };
    //Modal: (template: string, settings?: any) => JQuery;
    //ModalDefaults: any;
    Modal: IModal;
    Ajax: (url: string, settings?: any) => JQueryXHR;
    Get: (url: string, data?: any, settings?: any) => JQueryXHR;
    Delete: (url: string, data?: any, settings?: any) => JQueryPromise<{}>;
    Patch: (url: string, data?: any, settings?: any) => JQueryPromise<{}>;
    Post: (url: string, data?: any, settings?: any) => JQueryPromise<{}>;
    Put: (url: string, data?: any, settings?: any) => JQueryPromise<{}>;
};

(function (window: Window, $: JQueryStatic, ko: KnockoutStatic, NProgress: NProgressStatic, toastr: Toastr): void {// TODO: Proper $ type jQueryStatic, need to handle extension unless we change it. (probably should)
    var App: IApp = window.App || <IApp>{};

    $(function (): void {
        $("#logout").on("click", function (e: JQueryEventObject): void {
            e.preventDefault();

            App.Post("/api/v1/account/logout")
            .done(function (response: any): void {
                if (response.Success) {
                    document.location.reload();
                }
            });
        });
    });

    //#region jQuery Extensions

    $.fn.applyBindings = function (viewModel: any): JQuery {
        this.each(function (i: number, element: HTMLElement): void {
            ko.applyBindings(viewModel, element);
        });
        return this;
    };

    //#endregion jQuery Extensions

    //#region Knockout Templates

    ko.templates["config-row"] =
        "<tr data-bind=\"attr: { 'data-id': Id }\">" +
            "<td class=\"break-block\" data-bind=\"text: Name\"></td>" +
            "<td class=\"break-block\"><a href=\"#\" target=\"_blank\" data-bind=\"attr: { href: Url }, text: Url\"></a></td>" +
            "<td class=\"break-block text-mono\" data-bind=\"text: Key\"></td>" +
            "<td class=\"break-inline\"><button type=\"button\" class=\"btn btn-xs btn-block\" data-bind=\"click: Toggle, css: ob_Enabled() ? 'btn-success' : 'btn-danger', text: ob_Enabled() ? 'enabled' : 'disabled'\"></button></td>" +
            "<td class=\"break-block text-nowrap\">" +
                //"<button type=\"button\" class=\"btn btn-xs btn-warning\" title=\"Edit\" data-toggle=\"tooltip\" data-placement=\"left\" data-bind=\"click: Edit\"><span class="glyphicon glyphicon-edit"></span></button>" +
                "<button type=\"button\" class=\"btn btn-xs btn-danger\" title=\"Delete\" data-toggle=\"tooltip\" data-placement=\"right\" data-bind=\"click: Delete\"><span class=\"glyphicon glyphicon-remove\"></span></button>" +
            "</td>" +
        "</tr>";

    ko.templates["modal-config"] =
        "<form class=\"_form-horizontal _form-bordered _form-modal\" role=\"form\" data-bind=\"submit: act_Submit, validate: {}\">" +
            "<input name=\"Enabled\" type=\"hidden\" value=\"true\" />" +
            "<div class=\"modal-header\">" +
                "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\" aria-hidden=\"true\"><span class=\"glyphicon glyphicon-remove\"></span></button>" +
                "<h4 class=\"modal-title\">Add REDCap Configuration</h4>" +
            "</div>" +
            "<div class=\"modal-body\">" +
                "<div class=\"form-group\">" +
                    "<div class=\"row\">" +
                        "<div class=\"col-sm-12\">" +
                            "<label for=\"Name\" class=\"control-label\">Name</label>" +
                            "<input id=\"Name\" type=\"text\" name=\"Name\" placeholder=\"Example Config\" class=\"form-control\" maxlength=\"255\" data-bind=\"value: Name || ''\" required>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
                "<div class=\"form-group\">" +
                    "<div class=\"row\">" +
                        "<div class=\"col-sm-12\">" +
                            "<label for=\"Url\" class=\"control-label\">API Url</label>" +
                            "<input id=\"Url\" type=\"url\" name=\"Url\" placeholder=\"https://redcap.example.com/api\" class=\"form-control\" maxlength=\"2048\" data-bind=\"value: Url || ''\" required>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
                "<div class=\"form-group\">" +
                    "<div class=\"row\">" +
                        "<div class=\"col-sm-12\">" +
                            "<label for=\"Key\" class=\"control-label\">API Key</label>" +
                            "<input id=\"Key\" type=\"text\" name=\"Key\" placeholder=\"\" class=\"form-control\" maxlength=\"32\" pattern=\"[a-fA-f0-9]{32}\" data-bind=\"value: Key || ''\" required>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
            "</div>" +
            "<div class=\"modal-footer\">" +
                "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Cancel</button>" +
                "<button type=\"submit\" class=\"btn btn-primary\"><span class=\"glyphicon glyphicon-floppy-save\"></span> Save</button>" +
            "</div>" +
        "</form>";

    //#endregion Knockout Templates

    //#region Misc

    //App.Object = function (): any {
    //    return $.isFunction(Object.create)
    //        ? Object.create(null)
    //        : {};
    //};

    App.Nonce = function (): string {
        /* tslint:disable:no-bitwise */
        return parseInt(new Date().getTime().toString() + ((Math.random() * 1e5) | 0), 10).toString(36);
        /* tslint:enable:no-bitwise */
    };

    //#endregion Misc

    //#region Modals

    App.Modal = <IModal>function (template: string, settings?: any): JQuery {
        var html = ko.templates[template];
        if (template === null)
            return $();

        //Deferred?
        var defaults: Object = {
            Size: '',//modal-lg for large or modal-sm for small, otherwise default.
            Animate: true
        };
        var opts: any = $.extend({}, defaults, settings);

        //Right now we only allow one modal to be open by closing any previous. Should we allow stacking? Should we fail the new one?
        var $existing: JQuery = $('#modal-container');
        if ($existing.hasClass('in')) {//Should we bother doing this check?
            $existing.modal('hide');
        } else {
            $existing.remove();
        }

        var scroll: number;
        var $modalContainer: JQuery = $(
            '<div id="modal-container" class="modal ' + (opts.Animate ? 'fade' : '') + '" tabindex="-1" role="dialog" aria-hidden="true" aria-labelledby="modal-title">' +
                '<div class="modal-dialog' + (opts.Size ? ' ' + opts.Size : '') + '">' +
                    '<div class="modal-content">' + ko.templates[template] + '</div>' +
                '</div>' +
            '</div>')
            .one('show.bs.modal', function () {
                scroll = $('body').scrollTop();
            })
            .one('hidden.bs.modal', function (): void {
                $(this).remove();
                $('body').scrollTop(scroll);
            });
        return $modalContainer;
    };

    App.Modal.Defaults = {
        backdrop: 'static',
        keyboard: false
    };

    //#endregion Modals

    //#region Error Handlers

    function handle201(data: any, textStatus: string, jqXHR: JQueryXHR): void {
        //TODO: Can we fetch the sub-location if there is one?
        //var location: string = jqXHR.getResponseHeader("location");
        //if (location !== null) {
        //    //Perform sub-request? Not sure this is possible, it would either need to be sync or have a way to replace the deferred object.
        //}
    }

    function handle401(jqXHR: JQueryXHR, messageHandler?: (message: string, title?: string, options?: any) => void): void {
        if ($.isFunction(messageHandler)) {
            var response: any = jqXHR.responseJSON || {};
            var message: string = response.Message || "An unknown authorization error has occured.";
            messageHandler(message);
            //Redirect to login page? prompt?
        }
    }

    function handle400(jqXHR: JQueryXHR, errorThrown: string, messageHandler?: (message: string, title?: string, options?: any) => void): void {
        if ($.isFunction(messageHandler)) {
            var response: any = jqXHR.responseJSON || {};
            var state: any = response.ModelState || {};

            var fields: string[] = [];
            var errors: string[] = [];
            $.map(state, function (value: string[], key: string): void {
                fields.push(key.replace(/^[^\.]+\./, ""));//I'm not entirely sure this is a safe replace.
                errors.push(value.join("<br>"));
            });

            var message: string = errors.join("<br>") || errorThrown;
            messageHandler(message);
        }
    }

    function handle404(jqXHR: JQueryXHR, errorThrown: string, messageHandler?: (message: string, title?: string, options?: any) => void): void {
        handle400(jqXHR, errorThrown, messageHandler);
    }

    function handle500(jqXHR: JQueryXHR, messageHandler?: (message: string, title?: string, options?: any) => void): void {
        if ($.isFunction(messageHandler)) {
            var response: any = jqXHR.responseJSON || {};
            var message: string = response.Message || "An unknown error has occured.";

            if (response.MessageDetail) {
                messageHandler(response.MessageDetail, message);
            } else {
                messageHandler(message);
            }
        }
    }

    //#endregion Error Handlers

    //#region Ajax Handlers

    var tokenCache: { [url: string]: JQueryPromise<{}> } = {};

    function getToken(url: string, selector?: string, timeout?: number): JQueryPromise<{}> {
        var promise: JQueryPromise<{}> = tokenCache[url];
        if (promise) {
            return promise;
        }

        var delay: number = timeout || 30 * 1000;
        var input: string = selector || "#VerificationToken";
        var src: string = url + (url.indexOf("?") > -1 ? "&" : "?") + "_=" + App.Nonce();
        var dfd: JQueryDeferred<{}> = $.Deferred();

        if ($.active++ === 0) {//from jquery/src/ajax.js
            $.event.trigger("ajaxStart");
        }

        var $iframe: JQuery = $("<iframe src=\"" + src + "\" style=\"height: 0; width: 0; border: 0; padding: 0; margin: 0; position: absolute; top: 0; left: 0;\">")
        .on("load", function (): void {
            var token: string = $(this).contents().find(input).val();
            if (token) {
                dfd.resolve(token);
            } else {
                dfd.reject();
            }

            $iframe.remove();
        })
        .appendTo("body");

        setTimeout(function (): void {
            if (dfd.state() === "pending") {
                dfd.reject();
                $iframe.remove();
            }
        }, delay);

        promise = tokenCache[url] = dfd.promise()
        .fail(function (): void {
            tokenCache[url] = null;
            //TODO: Trigger an handler event
        })
        .always(function (): void {
            if (!(--$.active)) {//from jquery/src/ajax.js //Will this call multiple times?
                $.event.trigger("ajaxStop");
            }
        });

        return promise;
    };

    function getTokenMethod(verb: string): (url: string, data?: any, settings?: any) => JQueryPromise<{}> {
        return function (url: string, data?: any, settings?: any): JQueryPromise<{}> {
            var dfd: JQueryDeferred<{}> = $.Deferred();

            getToken("/token")
            .always(function (token: string): void {
                var opts: any = $.extend({}, { data: data }, settings, { type: verb, headers: { "X-CSRF-Token": token } });
                App.Ajax(url, opts)
                .done(function (): void {
                    dfd.resolveWith(this, arguments);
                })
                .fail(function (): void {
                    dfd.rejectWith(this, arguments);
                });
            });
            return dfd.promise();
        };
    };

    App.Ajax = function (url: string, settings?: any): JQueryXHR {
        var defaults: any = {
            type: "POST",
            data: null,
            dataType: "json",
            messageHandler: toastr.error,
            statusCode: {
                201: handle201,
                400: function (jqXHR: JQueryXHR, textStatus: string, errorThrown: string): void { handle400(jqXHR, errorThrown, this.messageHandler); },
                401: function (jqXHR: JQueryXHR, textStatus: string, errorThrown: string): void { handle401(jqXHR, this.messageHandler); },
                404: function (jqXHR: JQueryXHR, textStatus: string, errorThrown: string): void { handle404(jqXHR, errorThrown, this.messageHandler); },
                500: function (jqXHR: JQueryXHR, textStatus: string, errorThrown: string): void { handle500(jqXHR, this.messageHandler); }
            }
        };

        var opts: any = $.extend({}, defaults, settings);
        return $.ajax(url, opts);
    };

    App.Get = function (url: string, data?: any, settings?: any): JQueryXHR {
        var opts: any = $.extend({}, { data: data }, settings, { type: "GET" });
        return App.Ajax(url, opts);
    };
    App.Delete = getTokenMethod("DELETE");
    App.Patch = getTokenMethod("PATCH");
    App.Post = getTokenMethod("POST");
    App.Put = getTokenMethod("PUT");

    //#endregion Ajax Handlers

    $(document)
    .ajaxStart(function (): void {
        $("html").addClass("busy");
        NProgress.start();
    })
    .ajaxStop(function (): void {
        $("html").removeClass("busy");
        NProgress.done();
        });

    window.App = App;
} (window, window.jQuery, window.ko, window.NProgress, window.toastr));