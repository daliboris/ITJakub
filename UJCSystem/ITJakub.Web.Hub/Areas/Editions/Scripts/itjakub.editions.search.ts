﻿
var search: Search;
 
$(document).ready(() => {

    const urlSearchKey = "search";
    const urlPageKey = "page";
    const urlSelectionKey = "selected";
    const urlSortAscKey = "sortAsc";
    const urlSortCriteriaKey = "sortCriteria";

    var readyForInit = false;
    var notInitialized = true;

    var booksCountOnPage = 5;

    var bookIdsInQuery = new Array();
    var categoryIdsInQuery = new Array();

    var selectedBookIds = new Array();
    var selectedCategoryIds = new Array();

    var bibliographyModule: BibliographyModule;
    var editionsSelector: DropDownSelect2;

    var initPage: number = null;

    function initializeFromUrlParams() {
        if (readyForInit && notInitialized) {

            notInitialized = false;

            var page = getQueryStringParameterByName(urlPageKey);

            if (page) {
                initPage = parseInt(page);
            }

            var sortedAsc = getQueryStringParameterByName(urlSortAscKey);
            var sortCriteria = getQueryStringParameterByName(urlSortCriteriaKey);

            if (sortedAsc && sortCriteria) {
                bibliographyModule.setSortedAsc(sortedAsc === "true");
                bibliographyModule.setSortCriteria(<SortEnum>(<any>(sortCriteria)));
            }

            var selected = getQueryStringParameterByName(urlSelectionKey);

            var searched = getQueryStringParameterByName(urlSearchKey);
            search.writeTextToTextField(searched);

            if (selected) {
                editionsSelector.setStateFromUrlString(selected);
            }

        } else if (!notInitialized) {
            search.processSearch();
        } else {
            readyForInit = true;
        }

    }

    function actualizeSelectedBooksAndCategoriesInQuery() {
        bookIdsInQuery = selectedBookIds;
        categoryIdsInQuery = selectedCategoryIds;
    }

    var callbackDelegate = new DropDownSelectCallbackDelegate();
    callbackDelegate.selectedChangedCallback = (state: State) => {
        selectedBookIds = new Array();

        for (var i = 0; i < state.SelectedItems.length; i++) {
            selectedBookIds.push(state.SelectedItems[i].Id);
        }

        selectedCategoryIds = new Array();

        for (var i = 0; i < state.SelectedCategories.length; i++) {
            selectedCategoryIds.push(state.SelectedCategories[i].Id);
        }

        var parametersUrl = DropDownSelect2.getUrlStringFromState(state);
    };
    callbackDelegate.dataLoadedCallback = () => {
        var selectedIds = editionsSelector.getSelectedIds();
        selectedBookIds = selectedIds.selectedBookIds;
        selectedCategoryIds = selectedIds.selectedCategoryIds;
        initializeFromUrlParams();
    };

    editionsSelector = new DropDownSelect2("#dropdownSelectDiv", getBaseUrl() + "Editions/Editions/GetEditionsWithCategories", true, callbackDelegate);
    editionsSelector.makeDropdown();

    function editionAdvancedSearchPaged(json: string, pageNumber: number) {

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
            url: getBaseUrl() + "Editions/Editions/AdvancedSearchPaged",
            data: { json: json, start: start, count: count, sortingEnum: sortingEnum, sortAsc: sortAsc, selectedBookIds: bookIdsInQuery, selectedCategoryIds: categoryIdsInQuery },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.showBooks(response.books);
                updateQueryStringParameter(urlSearchKey, json);
                updateQueryStringParameter(urlPageKey, pageNumber);
                updateQueryStringParameter(urlSortAscKey, bibliographyModule.isSortedAsc());
                updateQueryStringParameter(urlSortCriteriaKey, bibliographyModule.getSortCriteria());
            }
        });
    }

    function editionBasicSearchPaged(text: string, pageNumber: number) {

        //if (typeof text === "undefined" || text === null || text === "") return;

        var start = (pageNumber - 1) * bibliographyModule.getBooksCountOnPage();
        var count = bibliographyModule.getBooksCountOnPage();
        var sortAsc = bibliographyModule.isSortedAsc();
        var sortingEnum = bibliographyModule.getSortCriteria();

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Editions/Editions/TextSearchFulltextPaged",
            data: { text: text, start: start, count: count, sortingEnum: sortingEnum, sortAsc: sortAsc, selectedBookIds: bookIdsInQuery, selectedCategoryIds: categoryIdsInQuery },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.showBooks(response.books);
                updateQueryStringParameter(urlSearchKey, text);
                updateQueryStringParameter(urlPageKey, pageNumber);
                updateQueryStringParameter(urlSortAscKey, bibliographyModule.isSortedAsc());
                updateQueryStringParameter(urlSortCriteriaKey, bibliographyModule.getSortCriteria());
            }
        });
    }

    function pageClickCallbackForBiblModule(pageNumber: number) {

        if (search.isLastQueryJson()) {
            editionAdvancedSearchPaged(search.getLastQuery(), pageNumber);
        } else {
            editionBasicSearchPaged(search.getLastQuery(), pageNumber);
        }
    }

    function sortOrderChanged() {
        bibliographyModule.showPage(1);
    }

    bibliographyModule = new BibliographyModule("#listResults", "#listResultsHeader", sortOrderChanged, BookTypeEnum.Edition, "Editions/Editions/GetSearchConfiguration");


    function createPagination(booksCount: number) {
        var pages = booksCount / booksCountOnPage;
        if (initPage && initPage <= pages) {
            bibliographyModule.createPagination(booksCountOnPage, pageClickCallbackForBiblModule, booksCount, initPage);
        } else {
            bibliographyModule.createPagination(booksCountOnPage, pageClickCallbackForBiblModule, booksCount);
        }
    }

    function editionBasicSearch(text: string) {
        actualizeSelectedBooksAndCategoriesInQuery();
        //if (typeof text === "undefined" || text === null || text === "") return;

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Editions/Editions/TextSearchFulltextCount",
            data: { text: text, selectedBookIds: bookIdsInQuery, selectedCategoryIds: categoryIdsInQuery },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                createPagination(response["count"]); //enable pagination
                updateQueryStringParameter(urlSearchKey, text);
                updateQueryStringParameter(urlSelectionKey, DropDownSelect2.getUrlStringFromState(editionsSelector.getState()));
                updateQueryStringParameter(urlSortAscKey, bibliographyModule.isSortedAsc());
                updateQueryStringParameter(urlSortCriteriaKey, bibliographyModule.getSortCriteria());
            }
        });
    }

    function editionAdvancedSearch(json: string) {

        if (typeof json === "undefined" || json === null || json === "") return;
        actualizeSelectedBooksAndCategoriesInQuery();

        bibliographyModule.clearBooks();
        bibliographyModule.showLoading();
        
        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Editions/Editions/AdvancedSearchResultsCount",
            data: { json: json, selectedBookIds: bookIdsInQuery, selectedCategoryIds: categoryIdsInQuery},
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                createPagination(response["count"]); //enable pagination
                updateQueryStringParameter(urlSearchKey, json);
                updateQueryStringParameter(urlSelectionKey, DropDownSelect2.getUrlStringFromState(editionsSelector.getState()));
                updateQueryStringParameter(urlSortAscKey, bibliographyModule.isSortedAsc());
                updateQueryStringParameter(urlSortCriteriaKey, bibliographyModule.getSortCriteria());
            }
        });
    }

    var enabledOptions = new Array<SearchTypeEnum>();
    enabledOptions.push(SearchTypeEnum.Title);
    enabledOptions.push(SearchTypeEnum.Author);
    enabledOptions.push(SearchTypeEnum.Editor);
    enabledOptions.push(SearchTypeEnum.Dating);
    enabledOptions.push(SearchTypeEnum.Fulltext);
    enabledOptions.push(SearchTypeEnum.TokenDistance);
    enabledOptions.push(SearchTypeEnum.Heading);
    enabledOptions.push(SearchTypeEnum.Sentence);

    search = new Search(<any>$("#listSearchDiv")[0], editionAdvancedSearch, editionBasicSearch);
    search.makeSearch(enabledOptions);

    initializeFromUrlParams();
});

