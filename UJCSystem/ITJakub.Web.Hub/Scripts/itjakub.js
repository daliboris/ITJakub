/// <reference path="Plugins/Bibliography/itjakub.plugins.bibliography.ts"/>
/// <reference path="Plugins/DropdownSelect/itjakub.plugins.dropdownselect.ts"/>
//sets state to main plugins menu
$(document).ready(function () {
    $('#main-plugins-menu').find('li').removeClass('active');
    var href = window.location.pathname;
    var liTargetingActualPage = $('#main-plugins-menu').find("a[href='" + href.toString() + "']").parent('li');
    $(liTargetingActualPage).addClass('active');
    $(liTargetingActualPage).parents('li').addClass('active');
});
function getQueryStringParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"), results = regex.exec(location.search);
    if (results === null)
        return "";
    var decoded = decodeURIComponent(results[1].replace(/\+/g, " "));
    decoded = replaceSpecialChars(decoded);
    return decoded;
}
function replaceSpecialChars(text) {
    var decoded = text.replace(/&amp;/g, '&'); //TODO make better replace
    decoded = decoded.replace(/&gt;/g, '>');
    decoded = decoded.replace(/&lt;/g, '<');
    decoded = decoded.replace(/&quot;/g, '"');
    decoded = decoded.replace(/&#39;/g, "'");
    return decoded;
}
function updateQueryStringParameter(key, value) {
    var uri = window.location.href;
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        history.pushState(null, null, uri.replace(re, '$1' + key + "=" + encodeURIComponent(value) + '$2'));
    }
    else {
        history.pushState(null, null, uri + separator + key + "=" + encodeURIComponent(value));
    }
}
function getBaseUrl() {
    var baseUrl = $("#baseUrl").data("path");
    return baseUrl;
}
// jQuery case-insensitive contains
jQuery.expr[':'].containsCI = function (a, i, m) { return (jQuery(a).text().toLowerCase()
    .indexOf(m[3].toLowerCase()) >= 0); };
//# sourceMappingURL=itjakub.js.map