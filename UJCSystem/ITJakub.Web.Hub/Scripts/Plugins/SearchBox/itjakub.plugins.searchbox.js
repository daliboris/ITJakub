var SearchBox = (function () {
    function SearchBox(inputFieldElement, controllerName) {
        this.inputField = inputFieldElement;
        this.controllerName = controllerName;
        this.urlWithController = getBaseUrl() + controllerName;
        this.datasets = [];
        this.options = {
            hint: true,
            highlight: true,
            minLength: 2
        };
    }
    SearchBox.prototype.create = function () {
        $(this.inputField).typeahead(this.options, this.datasets);
    };
    SearchBox.prototype.destroy = function () {
        $(this.inputField).typeahead("destroy");
    };
    SearchBox.prototype.addDataSet = function (name, groupHeader) {
        var prefetchUrl = this.urlWithController + "/GetTypeahead" + name;
        var remoteUrl = this.urlWithController + "/GetTypeahead" + name + "ForQuery?query=%QUERY";
        var remoteOptions = {
            url: remoteUrl,
            wildcard: "%QUERY"
        };
        var bloodhound = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            prefetch: prefetchUrl,
            remote: remoteOptions
        });
        var dataset = {
            name: this.controllerName,
            limit: 5,
            source: bloodhound,
            templates: {
                header: "<div class=\"tt-suggestions-header\">" + groupHeader + "</div>"
            }
        };
        this.datasets.push(dataset);
    };
    return SearchBox;
})();
//# sourceMappingURL=itjakub.plugins.searchbox.js.map