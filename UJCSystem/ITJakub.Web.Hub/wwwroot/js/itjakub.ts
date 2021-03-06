﻿/// <reference path="Plugins/Bibliography/itjakub.plugins.bibliography.ts"/>
/// <reference path="Plugins/DropdownSelect/itjakub.plugins.dropdownselect.ts"/>


//sets state to main plugins menu
$(document).ready(() => {
    $('#main-plugins-menu').find('li').removeClass('active');
    var href = window.location.pathname;
    var liTargetingActualPage = $('#main-plugins-menu').find("a[href='" + href.toString() + "']").parent('li');
    $(liTargetingActualPage).addClass('active');
    $(liTargetingActualPage).parents('li').addClass('active');

    // Fix navigation menu behavior for touch devices
    $("#main-plugins-menu > ul > li > a").on("touchstart", (event) => {
        event.preventDefault();
        var $liElement = $(event.currentTarget).closest(".has-sub");
        $liElement.siblings().removeClass("hover");
        $liElement.toggleClass("hover");
    });
    $(".secondary-navbar-toggle").on("touchstart", (event) => {
        if ($(event.target).is("a")) {
            return;
        }
        var $buttonElement = $(event.currentTarget);
        $buttonElement.siblings(".secondary-navbar-toggle").removeClass("hover");
        $buttonElement.toggleClass("hover");
    });

    // Disable Dropzone auto-initializing
    Dropzone.autoDiscover = false;
});

function getQueryStringParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    if (results === null) return "";
    var decoded = decodeURIComponent(results[1].replace(/\+/g, " "));
    decoded = replaceSpecialChars(decoded);
    return decoded;
}

function replaceSpecialChars(text : string): string {
    var decoded = text.replace(/&amp;/g, '&');   //TODO make better replace
    decoded = decoded.replace(/&gt;/g, '>');
    decoded = decoded.replace(/&lt;/g, '<');
    decoded = decoded.replace(/&quot;/g, '"');
    decoded = decoded.replace(/&#39;/g, "'");
    return decoded;
}

function escapeHtmlChars(text: string): string {
    var map = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': "&quot;",
        "'": "&#039;"
    };
    return text.replace(/[&<>"']/g, char => map[char]);
}

function updateQueryStringParameter(key, value) {
    var uri = window.location.href;
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        history.replaceState(null,null,uri.replace(re, '$1' + key + "=" + encodeURIComponent(value) + '$2'));
    }
    else {
        history.replaceState(null, null,uri + separator + key + "=" + encodeURIComponent(value));
    }
}

function getBaseUrl() {
    var baseUrl = $("#baseUrl").data("path");
    return baseUrl;
}

function convertDate(date: string): Date {
    return new Date(parseInt(date.substr(6)));
}

enum RoleEnum {
    EditLemmatization = 0
}

function isUserLoggedIn() {
    return $("#permissions-div").data("is-authenticated") === "True";
}

function isUserInRole(role: RoleEnum) {
    var paramRoleString = RoleEnum[role].toString();
    var rolesString = $("#permissions-div").data("roles");
    return rolesString.indexOf(paramRoleString) >= 0;
}

function onClickHref(event:JQueryEventObject, targetUrl) {
    if (event.ctrlKey || event.which == 2) {
        event.preventDefault();

        window.open(targetUrl, '_blank');

        return false;
    } else {
        window.location.href = targetUrl;
    }
}

// jQuery case-insensitive contains
jQuery.expr[':'].containsCI = (a, i, m) => (jQuery(a).text().toLowerCase()
    .indexOf(m[3].toLowerCase()) >= 0);

function getImageResourcePath(): string {
    return getBaseUrl() + "images/";
}

// Automatic popover close, fix 2 clicks for reopening problem
$(document).on('click', (e) => {
    $('[data-toggle="popover"],[data-original-title]').each(function () {
        //the 'is' for buttons that trigger popups
        //the 'has' for icons within a button that triggers a popup
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            (($(this).popover('hide').data('bs.popover') || {}).inState || {}).click = false; // fix for BS 3.3.6
        }
    });
});
