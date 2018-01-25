﻿function initSearchNew() {
    const searchClass = new BohemianTextBankNew();
    searchClass.initSearch();
}

class BohemianTextBankNew {
    private approximateNumberOfResultsPerPage = 4; //preferred number of results to be displayed in page
    private resultsPerPage = 3; //corresponds to amount of results per page that server can return

    private hitBookIds = [];
    private transientResults: ICorpusSearchResult[] = [];

    private paginator: IndefinitePagination;

    private compositionResultListStart = -1;
    private compositionsPerPage = 10;
    private compositionPageIsLast = false;

    private currentBookId = -1;
    private currentBookIndex = 0;
    private currentResultStart = -1;
    private currentViewPage = 1;

    private contextLength = -1;

    private resultsCountOnPage = 20;

    private paginationMaxVisibleElements = 5;

    private defaultErrorMessage =
        "Vyhledávání se nezdařilo. Ujistěte se, zda máte zadáno alespoň jedno kritérium na vyhledávání v textu.";

    private urlSearchKey = "search";
    private urlStartKey = "start"; //TODO in process of changing to start
    private urlSelectionKey = "selected";
    private urlSortAscKey = "sortAsc";
    private urlSortCriteriaKey = "sortCriteria";
    private urlContextSizeKey = "contextSize";

    private readyForInit = false;
    private notInitialized = true;

    private bookIdsInQuery = new Array();
    private categoryIdsInQuery = new Array();

    private booksSelector: DropDownSelect2;
    private sortBar: SortBar;

    private enabledOptions = new Array<SearchTypeEnum>();

    private search: Search;

    initSearch() {
        const paginationContainerEl = $("#paginationContainer");
        const paginator = new IndefinitePagination({
            container: paginationContainerEl,
            nextPageCallback: this.formNextPage.bind(this),
            previousPageCallback: this.loadPreviousPage.bind(this),
            loadAllPagesButton: true,
            loadAllPagesCallback: this.loadAllPages.bind(this),
            loadPageCallBack: this.goToPage.bind(this),
            showSlider: true,
            showInput: true
        });
        this.paginator = paginator;
        paginator.make();

        $("#wordCheckbox").change(() => {
            var checkbox = $("#wordCheckbox");
            var mainDiv = $("#corpus-search-div");

            if (checkbox.is(":checked")) {
                mainDiv.addClass("show-word");
            } else {
                mainDiv.removeClass("show-word");
            }
        });

        $("#commentCheckbox").change(() => {
            var checkbox = $("#commentCheckbox");
            var mainDiv = $("#corpus-search-div");

            if (checkbox.is(":checked")) {
                mainDiv.addClass("show-notes");
            } else {
                mainDiv.removeClass("show-notes");
            }
        });


        $("#languageCheckbox").change(() => {
            var checkbox = $("#languageCheckbox");
            var mainDiv = $("#corpus-search-div");

            if (checkbox.is(":checked")) {
                mainDiv.addClass("show-language");
            } else {
                mainDiv.removeClass("show-language");
            }
        });


        $("#structureCheckbox").change(() => {
            var checkbox = $("#structureCheckbox");
            var mainDiv = $("#corpus-search-div");

            if (checkbox.is(":checked")) {
                mainDiv.addClass("show-structure");
            } else {
                mainDiv.removeClass("show-structure");
            }
        });


        $("#paragraphCheckbox").change(() => {
            var checkbox = $("#paragraphCheckbox");
            var mainDiv = $("#corpus-search-div");

            if (checkbox.is(":checked")) {
                mainDiv.addClass("show-paragraph");
            } else {
                mainDiv.removeClass("show-paragraph");
            }
        });

        $("#resultsTableBody").on("click",
            ".search-result",
            (event: JQuery.Event) => {
                var clickedRow = $(event.target as Node as Element).closest(".search-result");

                $(".search-result").not(clickedRow).removeClass("clicked");
                clickedRow.addClass("clicked");

                this.printDetailInfo(clickedRow);
            });

        $("#contextPositionsSelect").change(() => {
            const contextLength = parseInt($("#contextPositionsSelect").val() as string);
            updateQueryStringParameter(this.urlContextSizeKey, contextLength);
            //this.searchForBook(this.currentPage);TODO reload page on context change?
        });

        $("#corpus-search-results-table-div").scroll((event) => {
            $("#corpus-search-results-abbrev-table-div").scrollTop($(event.target as Node as Element).scrollTop());
        });

        $("#corpus-search-results-abbrev-table-div").scroll((event) => {
            $("#corpus-search-results-table-div").scrollTop($(event.target as Node as Element).scrollTop());
        });

        this.initializeFromUrlParams();

        const sortBarContainer = "#listResultsHeader";

        const sortBarContainerEl = $(sortBarContainer);
        sortBarContainerEl.empty();
        this.sortBar = new SortBar(this.sortOrderChanged);
        const sortBarHtml = this.sortBar.makeSortBar(sortBarContainer);
        sortBarContainerEl.append(sortBarHtml);

        this.enabledOptions.push(SearchTypeEnum.Title);
        this.enabledOptions.push(SearchTypeEnum.Author);
        this.enabledOptions.push(SearchTypeEnum.Editor);
        this.enabledOptions.push(SearchTypeEnum.Dating);
        this.enabledOptions.push(SearchTypeEnum.Fulltext);
        this.enabledOptions.push(SearchTypeEnum.Heading);
        this.enabledOptions.push(SearchTypeEnum.Sentence);
        this.enabledOptions.push(SearchTypeEnum.Term);
        this.enabledOptions.push(SearchTypeEnum.TokenDistance);

        const favoritesQueriesConfig: IModulInicializatorConfigurationSearchFavorites = {
            bookType: BookTypeEnum.TextBank,
            queryType: QueryTypeEnum.Search
        };
        this.search = new Search($("#listSearchDiv")[0] as Node as HTMLDivElement,
            this.corpusAdvancedSearchBookHits.bind(this),
            this.corpusBasicSearchBookHits.bind(this),
            favoritesQueriesConfig);
        this.search.limitFullTextSearchToOne();
        this.search.makeSearch(this.enabledOptions);

        const callbackDelegate = new DropDownSelectCallbackDelegate();
        callbackDelegate.selectedChangedCallback = (state: State) => {

        };
        callbackDelegate.dataLoadedCallback = () => {
            this.initializeFromUrlParams();
        };

        this.booksSelector = new DropDownSelect2("#dropdownSelectDiv",
            getBaseUrl() + "BohemianTextBank/BohemianTextBank/GetCorpusWithCategories",
            BookTypeEnum.TextBank,
            true,
            callbackDelegate);
        this.booksSelector.makeDropdown();
    }

    private formNextPage() {
        this.currentViewPage = this.paginator.getCurrentPage();
        const hasBeenWrapped = this.paginator.hasBeenWrapped();
        if (hasBeenWrapped) {
            bootbox.alert({
                title: "Attention",
                message: "Page does not exist",
                buttons: {
                    ok: {
                        className: "btn-default"
                    }
                }
            });
            return;
        }
        const historyContainerEl = $(".page-history-constainer");
        const previousPage = this.currentViewPage - 1;
        const viewingPageEl = historyContainerEl.children(`[data-viewing-page-number=${previousPage}]`);
        if (viewingPageEl.length) {
            this.goToPage(this.currentViewPage);
        }else{
            this.generateViewingPage();
        }
    }

    private initializeFromUrlParams() {
        if (this.readyForInit && this.notInitialized) {

            this.notInitialized = false;

            const contextSize = getQueryStringParameterByName(this.urlContextSizeKey);
            if (contextSize) {
                $("#contextPositionsSelect").val(contextSize);
            }

            const sortedAsc = getQueryStringParameterByName(this.urlSortAscKey);
            const sortCriteria = parseInt(getQueryStringParameterByName(this.urlSortCriteriaKey));

            if (sortedAsc && sortCriteria) {
                this.sortBar.setSortedAsc(sortedAsc === "true");
                this.sortBar.setSortCriteria(sortCriteria as SortEnum);
            }

            const selected = getQueryStringParameterByName(this.urlSelectionKey);

            const searched = getQueryStringParameterByName(this.urlSearchKey);
            this.search.writeTextToTextField(searched);

            if (selected) {
                this.booksSelector.restoreFromSerializedState(selected);
            }

        } else if (!this.notInitialized) {
            this.search.processSearch();
        } else {
            this.readyForInit = true;
        }

    }

    private actualizeSelectedBooksAndCategoriesInQuery() {
        const selectedIds = this.booksSelector.getSelectedIds();
        this.bookIdsInQuery = selectedIds.selectedBookIds;
        this.categoryIdsInQuery = selectedIds.selectedCategoryIds;
    }

    private sortOrderChanged() {
        //if (paginator) {TODO
        //    paginator.goToPage(1);
        //}
    }

    private showLoading() {
        $("#result-abbrev-table").hide();
        $("#result-table").hide();
        $("#corpus-search-results-table-div-loader").empty();
        $("#corpus-search-results-table-div-loader").show();
        $("#corpus-search-results-table-div-loader").addClass("loader");
    }


    private hideLoading() {
        $("#corpus-search-results-table-div-loader").removeClass("loader");
        $("#corpus-search-results-table-div-loader").hide();
        $("#result-abbrev-table").show();
        $("#result-table").show();
    }

    private printErrorMessage(message: string) {
        this.hideLoading();
        const corpusErrorDiv = $("#corpus-search-results-table-div-loader");
        corpusErrorDiv.empty();
        corpusErrorDiv.html(message);
        corpusErrorDiv.show();
    }

    private corpusAdvancedSearchPaged(json: string, start: number, contextLength: number, bookId: number) {
        if (!json) return;

        const count = this.resultsPerPage;
        this.showLoading();

        const payload: ICorpusLookupAdvancedSearch = {
            json: json,
            start: start,
            count: this.resultsPerPage,
            contextLength: contextLength,
            snapshotId: bookId,
            selectedCategoryIds: this.categoryIdsInQuery,
            selectedBookIds: this.bookIdsInQuery
        };
        const advancedSearchPageAjax =
            $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/AdvancedSearchCorpusGetPage`, payload);

        advancedSearchPageAjax.done((response) => {
            this.hideLoading();
            const results: ICorpusSearchResult[] = response["results"];
            this.currentResultStart += this.resultsPerPage;
                const resultPage = (start / count) + 1;
                const compositionPage = (this.compositionResultListStart / this.compositionsPerPage);
                const viewingPage = this.paginator.getCurrentPage();
                this.makeHistoryEntry(bookId, resultPage, compositionPage, viewingPage);
            this.calculateAndFlushNumberOfResults(results);
        });
        advancedSearchPageAjax.fail(() => {
            this.printErrorMessage(this.defaultErrorMessage);
        });
    }

    private fillResultsIntoTable(results: ICorpusSearchResult[]) {
        var tableBody = $("#resultsTableBody");
        const abbrevTableBody = $("#resultsAbbrevTableBody");
        for (var i = 0; i < results.length; i++) {
            var result = results[i];
            var pageContext = result.pageResultContext;
            var verseContext = result.verseResultContext;
            var bibleVerseContext = result.bibleVerseResultContext;
            var contextStructure = pageContext.contextStructure;
            var bookId = result.bookId;
            var pageId = pageContext.id;
            var acronym = result.sourceAbbreviation;
            var notes = result.notes;

            var tr = $("<tr></tr>");
            tr.addClass("search-result abbr-result-row");
            tr.attr("data-bookId", bookId);
            tr.attr("data-author", result.author);
            tr.attr("data-title", result.title);
            tr.attr("data-dating", result.originDate);
            tr.attr("data-pageId", pageId);
            tr.attr("data-pageName", pageContext.name);
            tr.attr("data-acronym", acronym);


            if (verseContext) {
                tr.attr("data-verseXmlId", verseContext.verseXmlId);
                tr.attr("data-verseName", verseContext.verseName);
            }

            if (bibleVerseContext) {
                tr.attr("data-bibleBook", bibleVerseContext.bibleBook);
                tr.attr("data-bibleChapter", bibleVerseContext.bibleChapter);
                tr.attr("data-bibleVerse", bibleVerseContext.bibleVerse);
            }

            var tdBefore = $("<td></td>");
            tdBefore.html(contextStructure.before);

            var tdMatch = $(`<td class="text-center"></td>`);
            var matchSpanEl = $(`<span></span>`);
            matchSpanEl.addClass("match");
            matchSpanEl.html(contextStructure.match);
            tdMatch.append(matchSpanEl);

            var tdAfter = $("<td></td>");
            tdAfter.html(contextStructure.after);

            tr.append(tdBefore);
            tr.append(tdMatch);
            tr.append(tdAfter);

            tableBody.append(tr);

            if (notes) {

                var notesTr = $("<tr></tr>");
                notesTr.addClass("notes");

                var tdNotes = $("<td></td>");
                tdNotes.attr("colSpan", 2);


                for (var j = 0; j < notes.length; j++) {
                    var noteSpan = $("<span></span>");
                    noteSpan.html(notes[j]);
                    noteSpan.addClass("note");
                    tdNotes.append(noteSpan);
                }


                notesTr.append(tdNotes);

                var beforeNotesTr = $("<tr></tr>");
                beforeNotesTr.addClass("notes spacer");

                var afterNotesTr = $("<tr></tr>");
                afterNotesTr.addClass("notes spacer");

                tableBody.append(beforeNotesTr);
                tableBody.append(notesTr);
                tableBody.append(afterNotesTr);

            }

            //fill left table with abbrev of corpus name
            var abbrevTr = $("<tr></tr>");
            abbrevTr.addClass("abbr-result-row");
            //$(abbrevTr).data("bookXmlId", bookXmlId);
            //$(abbrevTr).data("pageXmlId", pageXmlId);
            var abbrevTd = $("<td></td>");

            var abbrevHref = $("<a></a>");
            abbrevHref.attr("href", `${getBaseUrl()}Editions/Editions/Listing?bookId=${bookId}&searchText=${this.search.getLastQuery()}&page=${pageId}`);
            abbrevHref.html(acronym);

            abbrevTd.append(abbrevHref);

            abbrevTr.append(abbrevTd);
            abbrevTableBody.append(abbrevTr);

            if (notes) {

                var abbRevNotesTr = $("<tr></tr>");
                abbRevNotesTr.addClass("notes");

                var abbrevTdNotes = $("<td></td>");

                abbRevNotesTr.append(abbrevTdNotes);

                var beforeAbbrevNotesTr = $("<tr></tr>");
                beforeAbbrevNotesTr.addClass("notes spacer");

                var afterAbbrevNotesTr = $("<tr></tr>");
                afterAbbrevNotesTr.addClass("notes spacer");

                abbrevTableBody.append(beforeAbbrevNotesTr);
                abbrevTableBody.append(abbRevNotesTr);
                abbrevTableBody.append(afterAbbrevNotesTr);

            }
        }


        //scroll from left to center match column in table
        var firstChildTdWidth = tableBody.children("tr").first().children("td").first().width();
        var tableContainer = tableBody.parents("#corpus-search-results-table-div");
        var tableContainerWidth = tableContainer.width();
        var scrollOffset = firstChildTdWidth - tableContainerWidth / 2;
        tableContainer.scrollLeft(scrollOffset);
    }

    private corpusBasicSearchPaged(text: string, start: number, contextLength: number, bookId: number) {
        if (!text) return;
        const count = this.resultsPerPage;

        this.showLoading();

        const payload: ICorpusLookupBasicSearch = {
            text: text,
            start: start,
            count: count,
            contextLength: contextLength,
            snapshotId: bookId,
            selectedCategoryIds: this.categoryIdsInQuery,
            selectedBookIds: this.bookIdsInQuery
        };

        console.log(`---PAGE ${this.paginator.getCurrentPage()} INDEX---`);
        console.log(`composition list start: ${this.compositionResultListStart - this.compositionsPerPage}`);
        console.log(`result list start: ${this.currentResultStart}`);
        console.log(`current bookID: ${this.currentBookId}`);
        console.log(`current viewing page: ${this.paginator.getCurrentPage()}`);
        console.log(`---PAGE INDEX END---`);

        const getPageAjax = $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/TextSearchFulltextGetBookPage`,
            payload);

        getPageAjax.done((response) => {
            this.hideLoading();
            const results: ICorpusSearchResult[] = response["results"];
            this.currentResultStart += this.resultsPerPage;
            const resultPage = (this.currentResultStart / count) + 1;
            const compositionPage = (this.compositionResultListStart / this.compositionsPerPage);
            const viewingPage = this.paginator.getCurrentPage();
            this.makeHistoryEntry(bookId, resultPage, compositionPage, viewingPage);
            this.calculateAndFlushNumberOfResults(results);
        });

        getPageAjax.fail(() => {
            this.printErrorMessage(this.defaultErrorMessage);
        });
    }

    private calculateAndFlushNumberOfResults(results: ICorpusSearchResult[]) {
        if (!results.length) {
            this.switchToNextBook();
            return;
        }
        this.transientResults = this.transientResults.concat(results);
        if (results.length < this.resultsPerPage && this.transientResults.length < this.approximateNumberOfResultsPerPage) {
            this.switchToNextBook();
            return;
        }

        if (this.transientResults.length < this.approximateNumberOfResultsPerPage) {
            this.loadBookResultPage(this.currentResultStart, this.currentBookId);
        } else {
            this.flushTransientResults();
        }
    }

    private switchToNextBook() {
        this.currentResultStart = 0;//internal list index reset
        this.currentBookIndex++;//external list index shift
        if (this.currentBookIndex > (this.hitBookIds.length - 1)) {
            if (this.compositionPageIsLast) {
                if (this.transientResults.length) {
                    this.flushTransientResults();
                }
                bootbox.alert({
                    title: "Attention",
                    message: "This is a last page",
                    buttons: {
                        ok: {
                            className: "btn-default"
                        }
                    }
                });
                return;
            }else{
            const search = getQueryStringParameterByName(this.urlSearchKey);
            if (this.search.isLastQueryJson()) {
                this.loadNextCompositionAdvancedResultPage(search);
            } else {
                this.loadNextCompositionResultPage(search);
            }
            return;
            }
        }
        this.currentBookId = this.hitBookIds[this.currentBookIndex];
        this.loadBookResultPage(this.currentResultStart, this.currentBookId);
    }

    private loadBookResultPage(start: number, bookId: number) {
        const contextLength = parseInt($("#contextPositionsSelect").val() as string);
        if (this.search.isLastQueryJson()) {
            this.corpusAdvancedSearchPaged(this.search.getLastQuery(), start, contextLength, bookId);
        } else {
            this.corpusBasicSearchPaged(this.search.getLastQuery(), start, contextLength, bookId);
        }
    }

    private resetIds() {
        this.compositionResultListStart = -1;
        this.currentResultStart = -1;
        this.currentBookIndex = 0;
        this.currentBookId = -1;
    }

    private corpusBasicSearchBookHits(text: string) {
        if (!text) return;
        const nextPageEl = $(".indefinite-pagination-next-page");
        nextPageEl.prop("disabled", false);
        this.resetIds();
        const firstPage = 1;
        this.currentViewPage = firstPage;
        this.emptyResultsTable();
        this.resetHistory();
        this.compositionPageIsLast = false;
        this.paginator.updatePage(this.currentViewPage);
        this.actualizeSelectedBooksAndCategoriesInQuery();
        this.showLoading();

        this.loadNextCompositionResultPage(text);
    }

    private emptyResultsTable() {
        const tableBody = $("#resultsTableBody");
        const abbrevTableBody = $("#resultsAbbrevTableBody");
        tableBody.empty();
        abbrevTableBody.empty();
    }

    private flushTransientResults() {
        this.emptyResultsTable();
        this.fillResultsIntoTable(this.transientResults);
        this.transientResults = [];
    }

    /**
     * Generates viewing page
     */
    private generateViewingPage() {
        if (this.currentBookId === -1) {
            this.currentBookId = this.hitBookIds[this.currentBookIndex];
        }
        if (this.currentResultStart === -1) {
            this.currentResultStart = 0;
        }
        this.loadBookResultPage(this.currentResultStart, this.currentBookId);
    }

    private resetHistory() {
        const historyContainerEl = $(".page-history-constainer");
        historyContainerEl.empty();
    }

    private goToPage(pageNumber: number) {
        this.loadPage(pageNumber);
        this.paginator.updatePage(pageNumber);
        console.log(pageNumber);
    }

    private loadPreviousPage() {
        const previousPage = this.paginator.getCurrentPage();
        this.loadPage(previousPage);
    }

    private loadPage(pageNumber: number) {
        const pageHasBeenWrapped = this.paginator.hasBeenWrapped();
        const previousPage = pageNumber - 1;
        if (previousPage === 0 && !pageHasBeenWrapped) {//to load page 1 it's needed to reset indexes
            this.resetIds();
            if (this.search.isLastQueryJson()) {
                this.loadNextCompositionAdvancedResultPage(this.search.getLastQuery(), true);
            } else {
                this.loadNextCompositionResultPage(this.search.getLastQuery(), true);
            }
            return;
        }

        const historyContainerEl = $(".page-history-constainer");
        //const prevPageButtonEl = $(".indefinite-pagination-prev-page");
        const viewingPageEl = historyContainerEl.children(`[data-viewing-page-number=${previousPage}]`);
        if (viewingPageEl.length && !pageHasBeenWrapped) {
            const entry: ICorpusSearchViewingPageHistoryEntry = JSON.parse(viewingPageEl.attr("data-viewing-page-structure"));
            this.compositionResultListStart = (entry.compositionPage - 1) * this.compositionsPerPage;
            this.currentResultStart = (entry.hitResultPage - 1) * this.resultsPerPage;
            this.currentBookIndex = entry.bookIndex;
            console.log(`---PAGE ${previousPage} LAST INDEX---`);
            console.log(`comosition list start: ${this.compositionResultListStart}`);
            console.log(`result list start: ${this.currentResultStart}`);
            console.log(`current bookID: ${this.hitBookIds[this.currentBookIndex]}`);
            console.log(`current viewing page: ${previousPage}`);
            console.log(`---PAGE INDEX END---`);
            if (this.search.isLastQueryJson()) {
                this.loadNextCompositionAdvancedResultPage(this.search.getLastQuery(), true);
            } else {
                this.loadNextCompositionResultPage(this.search.getLastQuery(), true);
            }
        } else {
            bootbox.alert({
                title: "Attention",
                message: "Page does not exist",
                buttons: {
                    ok: {
                        className: "btn-default"
                    }
                }
            });
        }
    }

    private makeHistoryEntry(bookId: number, resultPage: number, compositionPage: number, viewingPage: number) {
        const historyContainerEl = $(".page-history-constainer");
        const viewingPageEl = historyContainerEl.children(`[data-viewing-page-number=${viewingPage}]`);
        if (viewingPageEl.length) {
            const pageStructure: ICorpusSearchViewingPageHistoryEntry = {
                compositionPage: compositionPage,
                bookIndex: this.currentBookIndex,
                hitResultPage: resultPage
            };
            viewingPageEl.attr("data-viewing-page-structure", JSON.stringify(pageStructure));
        } else {
            const historyNewEntryEl = $(`<li></li>`);
            historyNewEntryEl.attr("data-viewing-page-number", viewingPage);
            const pageStructure: ICorpusSearchViewingPageHistoryEntry = {
                compositionPage: compositionPage,
                bookIndex: this.currentBookIndex,
                hitResultPage: resultPage
            };
            historyNewEntryEl.attr("data-viewing-page-structure", JSON.stringify(pageStructure));
            historyContainerEl.append(historyNewEntryEl);
        }
    }

    private corpusAdvancedSearchBookHits(json: string) {
        if (!json) return;
        const nextPageEl = $(".indefinite-pagination-next-page");
        nextPageEl.prop("disabled", false);
        this.compositionResultListStart = -1;
        this.currentViewPage = 1;
        this.emptyResultsTable();
        this.resetHistory();
        this.compositionPageIsLast = false;
        this.paginator.updatePage(this.currentViewPage);

        this.actualizeSelectedBooksAndCategoriesInQuery();
        this.showLoading();

        this.loadNextCompositionAdvancedResultPage(json);
    }

    /**
    * Pagination of external list - list of compositions with hits, basic search
    * @param text
    */
    private loadNextCompositionResultPage(text: string, noResetIndexes?: boolean) {
        if (this.compositionResultListStart === -1) {
            this.compositionResultListStart = 0;
        }
        const start = this.compositionResultListStart;
        const count = this.compositionsPerPage;
        const sortingEnum = this.sortBar.getSortCriteria();
        const sortAsc = this.sortBar.isSortedAsc();
        const sortingDirection = sortAsc ? SortDirection.Asc : SortDirection.Desc;

        const payload: JQuery.PlainObject = {
            text: text,
            start: start,
            count: count,
            selectedBookIds: this.bookIdsInQuery,
            selectedCategoryIds: this.categoryIdsInQuery,
            sortBooksBy: sortingEnum,
            sortDirection: sortingDirection 
        };

        updateQueryStringParameter(this.urlSearchKey, text);
        updateQueryStringParameter(this.urlSortAscKey, sortAsc);
        updateQueryStringParameter(this.urlSortCriteriaKey, sortingEnum);
        updateQueryStringParameter(this.urlSelectionKey, this.booksSelector.getSerializedState());

        $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/GetHitBookIdsPaged`, payload)
            .done((bookIds: IPagedResultArray<number>) => {
                const totalCount = bookIds.totalCount;
                const page = (start / count) + 1;
                const totalPages = Math.ceil(totalCount / count);
                $("#totalResultCountDiv").text(totalCount);
                this.hitBookIds = bookIds.list;
                if (this.hitBookIds.length < this.compositionsPerPage || page === totalPages) {
                    this.compositionPageIsLast = true;
                }
                if (!this.hitBookIds.length) {//TODO requires attention
                    if (this.transientResults.length) {
                        this.flushTransientResults();
                    }
                    bootbox.alert({
                        title: "Attention",
                        message: "Page does not exist",
                        buttons: {
                            ok: {
                                className: "btn-default"
                            }
                        }
                    });
                    $(".indefinite-pagination-next-page").prop("disabled", true);
                } else {
                    this.currentBookId = -1;//reset book id to get new
                    if(!noResetIndexes){//Do not reset book index when loading a page from history
                    this.currentBookIndex = 0;//reset book index as book array is new
                    }
                    this.compositionResultListStart += this.compositionsPerPage;
                    this.generateViewingPage();
                }
            }).fail(() => {
                this.printErrorMessage(this.defaultErrorMessage);
            });
    }
    /**
     * Pagination of external list - list of compositions with hits, advanced search
     * @param json Json request with search request
     */
    private loadNextCompositionAdvancedResultPage(json: string, noResetIndexes?: boolean) {
        if (this.compositionResultListStart === -1) {
            this.compositionResultListStart = 0;
        }
        const sortingEnum = this.sortBar.getSortCriteria();
        const sortAsc = this.sortBar.isSortedAsc();
        const sortingDirection = sortAsc ? SortDirection.Asc : SortDirection.Desc;
        const start = this.compositionResultListStart;
        const count = this.compositionsPerPage;

        updateQueryStringParameter(this.urlSearchKey, json);
        updateQueryStringParameter(this.urlSelectionKey, this.booksSelector.getSerializedState());
        updateQueryStringParameter(this.urlSortAscKey, sortAsc);
        updateQueryStringParameter(this.urlSortCriteriaKey, sortingEnum);

        const payload: JQuery.PlainObject = {
            json: json,
            start: start,
            count: count,
            selectedBookIds: this.bookIdsInQuery,
            selectedCategoryIds: this.categoryIdsInQuery,
            sortBooksBy: sortingEnum,
            sortDirection: sortingDirection 
        };

        $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/AdvancedSearchGetHitBookIdsPaged`, payload)
            .done((bookIds: IPagedResultArray<number>) => {
                const totalCount = bookIds.totalCount;
                const page = (start / count) + 1;
                const totalPages = Math.ceil(totalCount / count);
                $("#totalResultCountDiv").text(totalCount);
                this.hitBookIds = bookIds.list;
                if (this.hitBookIds.length < this.compositionsPerPage || page === totalPages) {
                    this.compositionPageIsLast = true;
                }
                if (!this.hitBookIds.length) {//TODO requires attention
                    if (this.transientResults.length) {
                        this.emptyResultsTable();
                        this.fillResultsIntoTable(this.transientResults);
                        this.transientResults = [];
                    }
                    bootbox.alert({
                        title: "Attention",
                        message: "Page does not exist",
                        buttons: {
                            ok: {
                                className: "btn-default"
                            }
                        }
                    });
                    $(".indefinite-pagination-next-page").prop("disabled", true);
                } else {
                    this.currentBookId = -1;//reset book id to get new
                    if (!noResetIndexes) { //Do not reset book index when loading a page from history
                        this.currentBookIndex = 0; //reset book index as book array is new
                    }
                    this.compositionResultListStart += this.compositionsPerPage;
                    this.generateViewingPage();
                }
            }).fail(() => {
                this.printErrorMessage(this.defaultErrorMessage);
            });
    }

    private printDetailInfo(tableRowEl: JQuery) {
        const undefinedReplaceString = "<Nezadáno>";

        $("#detail-author").text(tableRowEl.data("author") ? tableRowEl.data("author") : undefinedReplaceString);
        $("#detail-title").text(tableRowEl.data("title") ? tableRowEl.data("title") : undefinedReplaceString);
        $("#detail-dating").text(tableRowEl.data("dating") ? tableRowEl.data("dating") : undefinedReplaceString);
        $("#detail-dating-century").text(undefinedReplaceString); //TODO ask where is this info stored
        $("#detail-abbrev").text(tableRowEl.data("acronym") ? tableRowEl.data("acronym") : undefinedReplaceString);

        //Edition note
        const editionNoteAnchor = $("#detail-edition-note-href");
        editionNoteAnchor.prop("href", `/EditionNote/EditionNote?bookId=${tableRowEl.data("bookId")}`);

        const folioHref = $("<a></a>");
        folioHref.prop("href",
            `${getBaseUrl()}Editions/Editions/Listing?bookId=${tableRowEl.data("bookId")}&searchText=${
            this.search.getLastQuery()}&page=${tableRowEl.data("pageId")}`);
        folioHref.text(tableRowEl.data("pageName") ? tableRowEl.data("pageName") : undefinedReplaceString);

        $("#detail-folio").empty().append(folioHref);

        $("#detail-vers").text(tableRowEl.data("verseName") ? tableRowEl.data("verseName") : undefinedReplaceString);
        $("#detail-bible-vers-book")
            .text(tableRowEl.data("bibleBook") ? tableRowEl.data("bibleBook") : undefinedReplaceString);
        $("#detail-bible-vers-chapter")
            .text(tableRowEl.data("bibleChapter") ? tableRowEl.data("bibleChapter") : undefinedReplaceString);
        $("#detail-bible-vers-vers")
            .text(tableRowEl.data("bibleVerse") ? tableRowEl.data("bibleVerse") : undefinedReplaceString);
    }

    private loadAllPages() : JQuery.Deferred<any>{
        const searchText = this.search.getLastQuery();
        let ajax: JQuery.jqXHR;
        if (this.search.isLastQueryJson()) {
            ajax = $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/AdvancedGetAllPages`,
                {
                    text: searchText,
                    selectedBookIds: this.bookIdsInQuery,
                    selectedCategoryIds: this.categoryIdsInQuery
                });
        } else {
            ajax = $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/GetAllPages`,
                {
                    text: searchText,
                    selectedBookIds: this.bookIdsInQuery,
                    selectedCategoryIds: this.categoryIdsInQuery
                });
        }
        const deferred = $.Deferred();
        ajax.done((allPages) => {//TODO make interface
            const totalNumberOfPages = allPages.totalCount;
            const pages = allPages.pages;//TODO
            deferred.resolve(totalNumberOfPages);
        });
        ajax.fail(() => {
            deferred.reject();
        });
        return deferred;
    }

    private calculateNumberOfPages() { //TODO not used yet
        var pageStructure = [];
        const hitBookIdsAjax = $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/GetHitBookIds`,
            { text: "", selectedBookIds: null, selectedCategoryIds: null } as JQuery.PlainObject);
        hitBookIdsAjax.done((bookIds: number[]) => {
            bookIds.forEach((bookId: number) => {
                const bookHitPagesAjax =
                    $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/GetNumberOfPagesByBook`,
                        { bookId: bookId } as JQuery.PlainObject);
                bookHitPagesAjax.done((pages: number[]) => {
                    var transientResults = [];
                    var numberOfTransientPages = 0;
                    pages.forEach((page: number) => {
                        const numberOfHitsPerPageAjax =
                            $.get(`${getBaseUrl()}BohemianTextBank/BohemianTextBank/GetResultNumberByBookByPage`,
                                { bookId: bookId } as JQuery.PlainObject);
                        numberOfHitsPerPageAjax.done((resultNumber: number) => {
                            if (resultNumber < (this.approximateNumberOfResultsPerPage - 5)
                            ) { //if page has less than minimum results, hold in temporary array
                                transientResults.push({ bookId: bookId, page: page });
                                numberOfTransientPages += resultNumber;
                            } else { //more than minimum pages
                                if (transientResults) {
                                    transientResults.push({
                                        bookId: bookId,
                                        page: page
                                    }); //if more than min number of pages and temp array exist, flush them
                                    pageStructure.push(transientResults);
                                    transientResults.length = 0;
                                    numberOfTransientPages = 0;
                                } else { //more than min number of pages, no temp array
                                    pageStructure.push([{ bookId: bookId, page: page }]);
                                }
                            }
                            if (numberOfTransientPages >= (this.approximateNumberOfResultsPerPage - 5)
                            ) { //no page with more than min number of hits, but temp array is too big
                                pageStructure.push(transientResults);
                                transientResults.length = 0;
                                numberOfTransientPages = 0;
                            }
                        }).fail(() => {
                            //TODO
                        });
                    });
                }).fail(() => {
                    //TODO
                });
            });
        }).fail(() => {
            //TODO
        });
    }
}