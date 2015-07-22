﻿$(document).ready(() => {
    var pageSize = 25;
    var dictionaryViewerHeadword = new DictionaryViewer("#headwords-list", "#headwords-pagination", "#headword-description", true);
    var dictionaryViewerFulltext = new DictionaryViewer("#headwords-list-fulltext", "#headwords-pagination-fulltext", "#headword-description", true);
    var dictionaryViewerAdvanced = new DictionaryViewer("#headwords-list-advanced", "#headwords-pagination-advanced", "#headword-description", true);
    var dictionaryWrapperBasic = new DictionaryViewerTextWrapper(dictionaryViewerHeadword, dictionaryViewerFulltext, pageSize);
    var dictionaryWrapperAdvanced = new DictionaryViewerJsonWrapper(dictionaryViewerAdvanced, pageSize);

    var processSearchJson = (json: string) => {
        $("#headword-description").empty();
        dictionaryWrapperAdvanced.loadCount(json);
    };

    var processSearchText = (text: string) => {
        $("#headword-description").empty();
        dictionaryWrapperBasic.loadCount(text);
    };

    var search = new Search($("#dictionarySearchDiv"), processSearchJson, processSearchText);
    var disabledOptions = new Array<SearchTypeEnum>();
    disabledOptions.push(SearchTypeEnum.Fulltext);
    disabledOptions.push(SearchTypeEnum.TokenDistance);
    disabledOptions.push(SearchTypeEnum.Sentence);
    disabledOptions.push(SearchTypeEnum.Heading);
    search.makeSearch(disabledOptions);

    var callbackDelegate = new DropDownSelectCallbackDelegate();
    callbackDelegate.selectedChangedCallback = (state) => {
        
    };

    var dictionarySelector = new DropDownSelect2("div.dictionary-selects", getBaseUrl() + "Dictionaries/Dictionaries/GetDictionariesWithCategories", true, callbackDelegate);
    dictionarySelector.makeDropdown();

    var tabs = new DictionarySearchTabs();
});

class DictionarySearchTabs {
    private searchTabs: Array<SearchTab>;

    constructor() {
        this.searchTabs = [
            new SearchTab("#tab-headwords", "#headwords", "#description-headwords"),
            new SearchTab("#tab-fulltext", "#fulltext", "#description-fulltext"),
            new SearchTab("#tab-advanced", "#advanced", "#description-advanced")
        ];
        
        $("#search-tabs a").click(e => {
            e.preventDefault();
            $(e.target).tab("show");
            this.show(e.target.getAttribute("href"));
        });
    }

    show(id: string) {
        var index = 0;
        switch (id) {
            case "#headwords":
                index = 0;
                break;
            case "#fulltext":
                index = 1;
                break;
            case "#advanced":
                index = 2;
                break;
        }

        var searchTab = this.searchTabs[index];
        $("#headword-description div").removeClass("active");
        $(searchTab.descriptionDiv).addClass("active");
    }

    showAdvanced() {
        var searchTab = this.searchTabs[2];
        $("#search-tabs li").addClass("hidden");
        $(searchTab.tabLi).removeClass("hidden");
    }

    showBasic() {
        var searchTab = this.searchTabs[2];
        $("#search-tabs li").removeClass("hidden");
        $(searchTab.tabLi).addClass("hidden");
    }
}

class SearchTab {
    listDiv: string;
    descriptionDiv: string;
    tabLi: string;

    constructor(tabLi: string, listDiv: string, descriptionDiv: string) {
        this.descriptionDiv = descriptionDiv;
        this.listDiv = listDiv;
        this.tabLi = tabLi;
    }
}

class DictionaryViewerJsonWrapper {
    private pageSize: number;
    private dictionaryViewer: DictionaryViewer;
    private json: string;

    constructor(dictionaryViewer: DictionaryViewer, pageSize: number) {
        this.pageSize = pageSize;
        this.dictionaryViewer = dictionaryViewer;
    }

    loadCount(json: string) {
        this.json = json;
        $.ajax({
            type: "POST",
            traditional: true,
            url: getBaseUrl() + "Dictionaries/Dictionaries/SearchCriteriaResultsCount",
            data: JSON.stringify({
                "json": json
            }),
            dataType: "json",
            contentType: "application/json",
            success: (response) => {
                var resultCount = response;
                this.dictionaryViewer.createViewer(resultCount, this.loadHeadwords.bind(this), this.pageSize, json);
            }
        });
    }

    loadHeadwords(pageNumber: number) {
        $.ajax({
            type: "POST",
            traditional: true,
            url: getBaseUrl() + "Dictionaries/Dictionaries/SearchCriteria",
            data: JSON.stringify({
                "json": this.json,
                "start": this.pageSize * (pageNumber - 1),
                "count": this.pageSize
            }),
            dataType: "json",
            contentType: "application/json",
            success: (response) => {
                this.dictionaryViewer.showHeadwords(response);
            }
        });
    }
}

class DictionaryViewerTextWrapper {
    private pageSize: number;
    private headwordViewer: DictionaryViewer;
    private fulltextViewer: DictionaryViewer;
    private text: string;

    constructor(headwordViewer: DictionaryViewer, fulltextViewer: DictionaryViewer, pageSize: number) {
        this.pageSize = pageSize;
        this.headwordViewer = headwordViewer;
        this.fulltextViewer = fulltextViewer;
    }

    loadCount(text: string) {
        this.text = text;
        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Dictionaries/Dictionaries/SearchBasicResultsCount",
            data: {
                text: text
            },
            dataType: "json",
            contentType: "application/json",
            success: (response: IHeadwordSearchResult) => {
                this.headwordViewer.createViewer(response.HeadwordCount, this.loadHeadwords.bind(this), this.pageSize, text);
                this.fulltextViewer.createViewer(response.FulltextCount, this.loadFulltextHeadwords.bind(this), this.pageSize, text);
            }
        });
    }

    loadHeadwords(pageNumber: number) {
        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Dictionaries/Dictionaries/SearchBasicHeadword",
            data: {
                text: this.text,
                start: this.pageSize * (pageNumber - 1),
                count: this.pageSize
            },
            dataType: "json",
            contentType: "application/json",
            success: (response) => {
                this.headwordViewer.showHeadwords(response);
            }
        });
    }

    loadFulltextHeadwords(pageNumber: number) {
        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Dictionaries/Dictionaries/SearchBasicFulltext",
            data: {
                text: this.text,
                start: this.pageSize * (pageNumber - 1),
                count: this.pageSize
            },
            dataType: "json",
            contentType: "application/json",
            success: (response) => {
                this.fulltextViewer.showHeadwords(response);
            }
        });
    }
}

interface IHeadwordSearchResult {
    HeadwordCount: number;
    FulltextCount: number;
}