﻿

var search: Search;

$(document).ready(() => {
    var booksCountOnPage = 5;

    var bookIds = new Array();
    var categoryIds = new Array();

    function sortOrderChanged() {
        search.processSearch();
    }

    function hideTypeahead() {
        $(".twitter-typeahead").find(".tt-menu").hide();
    };

    var bibliographyModule = new BibliographyModule("#listResults", "#listResultsHeader", sortOrderChanged, BookTypeEnum.Edition);

    function audioAdvancedSearchPaged(json: string, pageNumber: number) {
        hideTypeahead();
        if (typeof json === "undefined" || json === null || json === "") return;

        var start = (pageNumber - 1) * bibliographyModule.getBooksCountOnPage();
        var count = bibliographyModule.getBooksCountOnPage();
        var sortAsc = bibliographyModule.isSortedAsc();
        var sortingEnum = bibliographyModule.getSortCriteria();

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "AudioBooks/AudioBooks/AdvancedSearchPaged",
            data: { json: json, start: start, count: count, sortingEnum: sortingEnum, sortAsc: sortAsc, selectedBookIds: bookIds, selectedCategoryIds: categoryIds },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.showBooks(response.books);
            }
        });
    }

    function audioBasicSearchPaged(text: string, pageNumber: number) {
        hideTypeahead();
        if (typeof text === "undefined" || text === null || text === "") return;

        var start = (pageNumber - 1) * bibliographyModule.getBooksCountOnPage();
        var count = bibliographyModule.getBooksCountOnPage();
        var sortAsc = bibliographyModule.isSortedAsc();
        var sortingEnum = bibliographyModule.getSortCriteria();

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "AudioBooks/AudioBooks/TextSearchPaged",
            data: { text: text, start: start, count: count, sortingEnum: sortingEnum, sortAsc: sortAsc, selectedBookIds: bookIds, selectedCategoryIds: categoryIds },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.showBooks(response.books);
            }
        });
    }

    function pageClickCallbackForBiblModule(pageNumber: number) {

        if (search.isLastQueryJson()) {
            audioAdvancedSearchPaged(search.getLastQuery(), pageNumber);
        } else {
            audioBasicSearchPaged(search.getLastQuery(), pageNumber);
        }
    }

    function audioBasicSearch(text: string) {
        hideTypeahead();
        if (typeof text === "undefined" || text === null || text === "") return;

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "AudioBooks/AudioBooks/TextSearchCount",
            data: { text: text, selectedBookIds: bookIds, selectedCategoryIds: categoryIds },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.createPagination(booksCountOnPage, pageClickCallbackForBiblModule, response["count"]); //enable pagination
            }
        });
    }

    function audioAdvancedSearch(json: string) {
        hideTypeahead();
        if (typeof json === "undefined" || json === null || json === "") return;

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "AudioBooks/AudioBooks/AdvancedSearchResultsCount",
            data: { json: json, selectedBookIds: bookIds, selectedCategoryIds: categoryIds },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.createPagination(booksCountOnPage, pageClickCallbackForBiblModule, response["count"]); //enable pagination
            }
        });
    }


    var disabledOptions = new Array<SearchTypeEnum>();
    disabledOptions.push(SearchTypeEnum.Fulltext);
    disabledOptions.push(SearchTypeEnum.Heading);
    disabledOptions.push(SearchTypeEnum.Headword);
    disabledOptions.push(SearchTypeEnum.HeadwordDescription);
    disabledOptions.push(SearchTypeEnum.HeadwordDescriptionTokenDistance);
    disabledOptions.push(SearchTypeEnum.Sentence);
    disabledOptions.push(SearchTypeEnum.TokenDistance);

    search = new Search(<any>$("#listSearchDiv")[0], audioAdvancedSearch, audioBasicSearch);
    search.makeSearch(disabledOptions);

    var typeaheadSearchBox = new SearchBox(".searchbar-input", "AudioBooks/AudioBooks");
    typeaheadSearchBox.addDataSet("Title", "Název");
    typeaheadSearchBox.create();
    typeaheadSearchBox.value($(".searchbar-input.tt-input").val());


    var callbackDelegate = new DropDownSelectCallbackDelegate();
    callbackDelegate.selectedChangedCallback = (state: State) => {
        bookIds = new Array();

        for (var i = 0; i < state.SelectedItems.length; i++) {
            bookIds.push(state.SelectedItems[i].Id);
        }

        categoryIds = new Array();

        for (var i = 0; i < state.SelectedCategories.length; i++) {
            categoryIds.push(state.SelectedCategories[i].Id);
        }

        var parametersUrl = DropDownSelect2.getUrlStringFromState(state);
        typeaheadSearchBox.clearAndDestroy();
        typeaheadSearchBox.addDataSet("Title", "Název", parametersUrl);
        typeaheadSearchBox.create();
        typeaheadSearchBox.value($(".searchbar-input.tt-input").val());
    };

    var editionsSelector = new DropDownSelect2("#dropdownSelectDiv", getBaseUrl() + "AudioBooks/AudioBooks/GetAudioWithCategories", true, callbackDelegate);
    editionsSelector.makeDropdown();


    $(".searchbar-input.tt-input").change(() => {        //prevent clearing input value on blur() 
        typeaheadSearchBox.value($(".searchbar-input.tt-input").val());
    });

    search.processSearchQuery("%"); //search for all by default criteria (title)
    search.writeTextToTextField("");
});






