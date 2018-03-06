﻿class BookHeader {
    private innerHtml: HTMLDivElement;
    private parentReader: ReaderLayout;
    private bookId: string;
    private versionId: string;
    private sc: ServerCommunication;

    constructor(parentReader: ReaderLayout, sc: ServerCommunication, headerDiv: HTMLDivElement, bookTitle: string) {
        this.parentReader = parentReader;
        this.sc = sc;
        this.bookId = parentReader.getBookId();
        this.versionId = parentReader.getVersionId();

        this.innerHtml = this.createHeaderDiv(bookTitle);
    }

    public getInnerHtml(): HTMLDivElement {
        return this.innerHtml;
    }

    private createHeaderDiv(bookTitle: string): HTMLDivElement {
        var headerDiv = document.createElement("div");
        headerDiv.appendChild(this.informationDiv(bookTitle));

        var controlsDiv = document.createElement("div");
        $(controlsDiv).addClass("reader-controls content-container");
        controlsDiv.appendChild(this.makeSlider());
        controlsDiv.appendChild(this.makeViewButtons());
        controlsDiv.appendChild(this.makeToolButtons());
        controlsDiv.appendChild(this.makePageNavigation());
        headerDiv.appendChild(controlsDiv);

        return headerDiv;
    }

    private informationDiv(bookTitle: string): HTMLDivElement {
        var bookInfoDiv: HTMLDivElement = document.createElement("div");
        $(bookInfoDiv).addClass("book-details");

        var title = document.createElement("span");
        $(title).addClass("title");
        title.innerHTML = bookTitle;
        bookInfoDiv.appendChild(title);

        var fullscreenButton = document.createElement("button");
        $(fullscreenButton).addClass("fullscreen-button");

        var fullscreenSpan = document.createElement("span");
        $(fullscreenSpan).addClass("glyphicon glyphicon-fullscreen");
        $(fullscreenButton).append(fullscreenSpan);
        $(fullscreenButton).click(() => {
            if ($(fullscreenSpan).hasClass("glyphicon-fullscreen")) {
                $(this.innerHtml).addClass("fullscreen");
                $(fullscreenSpan).removeClass("glyphicon-fullscreen");
                $(fullscreenSpan).addClass("glyphicon-remove");
                this.parentReader.readerLayout.updateSize();
            } else {
                $(this.innerHtml).removeClass("fullscreen");
                $(fullscreenSpan).removeClass("glyphicon-remove");
                $(fullscreenSpan).addClass("glyphicon-fullscreen");
                this.parentReader.readerLayout.updateSize();
            }

        });
        bookInfoDiv.appendChild(fullscreenButton);

        var detailsButton = document.createElement("button");
        $(detailsButton).addClass("more-button");
        var detailsSpan = document.createElement("span");
        $(detailsSpan).addClass("glyphicon glyphicon-chevron-down");
        detailsButton.appendChild(detailsSpan);
        $(detailsButton).click((event) => {
            var target: JQuery = $(event.target);

            var title = target.parents(".book-details").find(".title");
            title.toggleClass("full");

            var details = target.parents(".book-details").find(".hidden-content");
            if (!details.hasClass("visible")) {
                $(target).removeClass("glyphicon-chevron-down");
                $(target).addClass("glyphicon-chevron-up");
                details.addClass("visible");
            } else {
                $(target).removeClass("glyphicon-chevron-up");
                $(target).addClass("glyphicon-chevron-down");
                details.removeClass("visible");
            }
        });
        bookInfoDiv.appendChild(detailsButton);

        var hiddenDiv = document.createElement("div");
        $(hiddenDiv).addClass("hidden-content");

        var editionNoteDiv = this.getEditionNote();
        hiddenDiv.appendChild(editionNoteDiv);

        var bookDetailDiv = this.getBookDetail();
        hiddenDiv.appendChild(bookDetailDiv);

        bookInfoDiv.appendChild(hiddenDiv);

        return bookInfoDiv;
    }

    private getEditionNote(): HTMLDivElement {
        var editionNoteDiv = document.createElement("div");
        $(editionNoteDiv).addClass("loading");
        $(editionNoteDiv).addClass("edition-note-wrapper");
        var editionNoteHeader = document.createElement("h3");
        $(editionNoteHeader).append("Ediční poznámka");
        $(editionNoteDiv).append(editionNoteHeader);

        var editionNote: JQueryXHR = this.sc.getEditionNote(this.bookId);
        editionNote.done((response: { editionNote: string }) => {
            var editionNoteText = document.createElement("div");
            $(editionNoteText).addClass("edition-note-text");
            if (response.editionNote == "") {
                $(editionNoteText).append("Toto dílo nemá ediční poznámku");
            } else {
                $(editionNoteText).append(response.editionNote);
            }
            editionNoteDiv.appendChild(editionNoteText);
            $(editionNoteDiv).removeClass("loading");
        });
        editionNote.fail(() => {
            $(editionNoteDiv).append("Toto dílo nemá ediční poznámku");
        });

        return editionNoteDiv;
    }

    private getBookDetail(): HTMLDivElement {
        var bookDetailDiv = document.createElement("div");
        $(bookDetailDiv).addClass("book-detail-wrapper");
        var bookDetailHeader = document.createElement("h3");
        $(bookDetailHeader).append("Informace o díle");
        bookDetailDiv.appendChild(bookDetailHeader);

        var bookDetail: JQueryXHR = this.sc.getBookDetail(this.bookId);
        bookDetail.done((response) => {
            var detailData = response["detail"];
            var detailTable = new TableBuilder();
            var editors: string = "";
            for (var i = 0; i < detailData.Editors.length; i++) {
                var editor = detailData.Editors[i];
                editors += editor.FirstName + " " + editor.LastName;
                if (i + 1 != detailData.Editors.length) {
                    editors += ", ";
                }
            }

            detailTable.makeTableRow("Editor", editors);
            detailTable.makeTableRow("Předloha", detailData.LiteraryOriginal);
            detailTable.makeTableRow("Zkratka památky", detailData.RelicAbbreviation);
            detailTable.makeTableRow("Zkratka pramene", detailData.SourceAbbreviation);
            detailTable.makeTableRow("Literární druh", detailData.LiteraryKinds);
            detailTable.makeTableRow("Literární žánr", detailData.LiteraryGenre);
            detailTable.makeTableRow("Poslední úprava edice	", detailData.CreateTimeString);

            $(detailTable.build()).find(".bib-table-cell").each(function () {
                if (this.innerHTML === "" || this.innerHTML === "undefined") {
                    this.innerHTML = "&lt;Nezadáno&gt;";
                }
            });

            $(bookDetailDiv).append(detailTable.build());


            if (detailData.Authors.length != 0) {
                var authors: string = "";
                for (var i = 0; i < detailData.Authors.length; i++) {
                    var author = detailData.Authors[i];
                    authors += author.FirstName + " " + author.LastName;
                    if (i + 1 != detailData.Authors.length) {
                        authors += ", ";
                    }
                }
                $(".title").prepend(authors + ": ");
            }

        });
        bookDetail.fail(() => {
            $(bookDetailDiv).append("Nepodařilo se načíst detaily o díle");
        });

        return bookDetailDiv;
    }

    private makeSlider(): HTMLDivElement {
        var slider: HTMLDivElement = document.createElement("div");
        $(slider).addClass("slider");
        $(slider).slider({
            min: 0,
            max: this.parentReader.pages.length - 1,
            value: 0,
            start: (event, ui) => {
                $(event.target).find(".ui-slider-handle").find(".slider-tip").show();
            },
            stop: (event, ui) => {
                $(event.target).find(".ui-slider-handle").find(".slider-tip").fadeOut(1000);
            },
            slide: (event, ui) => {
                $(event.target).find(".ui-slider-handle").find(".slider-tip").stop(true, true);
                $(event.target).find(".ui-slider-handle").find(".slider-tip").show();
                if (this.parentReader.pages[ui.value] !== undefined) {
                    $(event.target).find(".ui-slider-handle").find(".tooltip-inner").html("Strana: " + this.parentReader.pages[ui.value].text);
                } else {
                    console.error("missing page " + ui.value);
                }
            },
            change: (event: Event, ui: JQueryUI.SliderUIParams) => {
                if (this.parentReader.actualPageIndex !== ui.value) {
                    this.parentReader.moveToPageNumber(<any>ui.value, true);
                }
            }
        });

        var sliderTooltip: HTMLDivElement = document.createElement("div");
        sliderTooltip.classList.add("tooltip", "top", "slider-tip");
        var arrowTooltip: HTMLDivElement = document.createElement("div");
        arrowTooltip.classList.add("tooltip-arrow");
        sliderTooltip.appendChild(arrowTooltip);

        var innerTooltip: HTMLDivElement = document.createElement("div");
        $(innerTooltip).addClass("tooltip-inner");
        if (this.parentReader.pages[0] !== undefined) {
            $(innerTooltip).html("Strana: " + this.parentReader.pages[0].text);
        }
        else {
            console.error("missing page " + 0);
        }
        sliderTooltip.appendChild(innerTooltip);
        $(sliderTooltip).hide();

        var sliderHandle: JQuery = $(slider).find(".ui-slider-handle");
        sliderHandle.append(sliderTooltip);
        sliderHandle.hover((event) => {
            $(event.target).find(".slider-tip").stop(true, true);
            $(event.target).find(".slider-tip").show();
        });
        sliderHandle.mouseout((event) => {
            $(event.target).find(".slider-tip").fadeOut(1000);
        });
        return slider;
    }

    private makePageNavigation(): HTMLDivElement {
        var paginationUl: HTMLUListElement = document.createElement("ul");
        paginationUl.classList.add("pagination", "pagination-sm");

        var toLeft = document.createElement("ul");
        toLeft.classList.add("page-navigation-container", "page-navigation-container-left");

        var liElement: HTMLLIElement = document.createElement("li");
        $(liElement).addClass("page-navigation page-navigation-left");
        var anchor: HTMLAnchorElement = document.createElement("a");
        anchor.href = "#";
        anchor.innerHTML = "|<";
        $(anchor).click((event: Event) => {
            event.stopPropagation();
            this.parentReader.moveToPageNumber(0, true);
            return false;
        });
        liElement.appendChild(anchor);
        toLeft.appendChild(liElement);

        liElement = document.createElement("li");
        $(liElement).addClass("page-navigation page-navigation-left");
        anchor = document.createElement("a");
        anchor.href = "#";
        anchor.innerHTML = "<<";
        $(anchor).click((event: Event) => {
            event.stopPropagation();
            this.parentReader.moveToPageNumber(this.parentReader.actualPageIndex - 5, true);
            return false;
        });
        liElement.appendChild(anchor);
        toLeft.appendChild(liElement);

        liElement = document.createElement("li");
        $(liElement).addClass("page-navigation page-navigation-left");
        anchor = document.createElement("a");
        anchor.href = "#";
        anchor.innerHTML = "<";
        $(anchor).click((event: Event) => {
            event.stopPropagation();
            this.parentReader.moveToPageNumber(this.parentReader.actualPageIndex - 1, true);
            return false;
        });
        liElement.appendChild(anchor);
        toLeft.appendChild(liElement);

        var toRight = document.createElement("ul");
        toRight.classList.add("page-navigation-container", "page-navigation-container-right");

        liElement = document.createElement("li");
        $(liElement).addClass("page-navigation page-navigation-right");
        anchor = document.createElement("a");
        anchor.href = "#";
        anchor.innerHTML = ">";
        $(anchor).click((event: Event) => {
            event.stopPropagation();
            this.parentReader.moveToPageNumber(this.parentReader.actualPageIndex + 1, true);
            return false;
        });
        liElement.appendChild(anchor);
        toRight.appendChild(liElement);

        liElement = document.createElement("li");
        $(liElement).addClass("page-navigation page-navigation-right");
        anchor = document.createElement("a");
        anchor.href = "#";
        anchor.innerHTML = ">>";
        $(anchor).click((event: Event) => {
            event.stopPropagation();
            this.parentReader.moveToPageNumber(this.parentReader.actualPageIndex + 5, true);
            return false;
        });
        liElement.appendChild(anchor);
        toRight.appendChild(liElement);

        liElement = document.createElement("li");
        $(liElement).addClass("page-navigation page-navigation-right");
        anchor = document.createElement("a");
        anchor.href = "#";
        anchor.innerHTML = ">|";
        $(anchor).click((event: Event) => {
            event.stopPropagation();
            this.parentReader.moveToPageNumber(this.parentReader.pages.length - 1, true);
            return false;
        });
        liElement.appendChild(anchor);
        toRight.appendChild(liElement);

        liElement = document.createElement("li");
        $(liElement).addClass("more-pages more-pages-left");
        liElement.innerHTML = "...";
        paginationUl.appendChild(liElement);

        $.each(this.parentReader.pages, (index, page) => {
            liElement = document.createElement("li");
            $(liElement).addClass("page");
            $(liElement).data("page-index", index);
            anchor = document.createElement("a");
            anchor.href = "#";
            anchor.innerHTML = page.text;
            $(anchor).click((event: Event) => {
                event.stopPropagation();
                this.parentReader.moveToPage(page.pageId, true);
                return false;
            });
            liElement.appendChild(anchor);
            paginationUl.appendChild(liElement);
        });

        liElement = document.createElement("li");
        $(liElement).addClass("more-pages more-pages-right");
        liElement.innerHTML = "...";
        paginationUl.appendChild(liElement);

        var listingContainer: HTMLDivElement = document.createElement("div");
        listingContainer.classList.add("page-navigation-container-helper");
        listingContainer.classList.add("hidden");
        listingContainer.appendChild(toLeft);
        listingContainer.appendChild(paginationUl);
        listingContainer.appendChild(toRight);
        return listingContainer;
    }

    private makeToolButtons(): HTMLDivElement {
        var readerLayout = this;
        var toolButtons: HTMLDivElement = document.createElement("div");
        $(toolButtons).addClass("buttons left");

        var addBookmarksButton = new Button(this.parentReader).createAddBookmarkButton("bookmark",
            "Přidat záložku",
            this.parentReader.bookmarksPanelId
        );
        toolButtons.appendChild(addBookmarksButton);


        var bookmarksButton =
            new Button(this.parentReader).createToolButton("bookmark",
                this.parentReader.bookmarksPanelLabel,
                this.parentReader.bookmarksPanelId);
        toolButtons.appendChild(bookmarksButton);

        var contentButton = new Button(this.parentReader).createToolButton("book",
            this.parentReader.contentPanelLabel,
            this.parentReader.contentPanelId);
        toolButtons.appendChild(contentButton);

        var searchResultButton = new Button(this.parentReader).createToolButton("search",
            this.parentReader.searchPanelLabel,
            this.parentReader.searchPanelId);
        toolButtons.appendChild(searchResultButton);

        var termsButton = new Button(this.parentReader).createToolButton("list-alt",
            this.parentReader.termsPanelLabel,
            this.parentReader.termsPanelId);
        toolButtons.appendChild(termsButton);

        return toolButtons;
    }

    private makeViewButtons(): HTMLDivElement {
        var viewControl: HTMLDivElement = document.createElement("div");
        $(viewControl).addClass("view-control");

        var viewButtons = document.createElement("div");
        $(viewButtons).addClass("buttons");
        var hasBookText: boolean = false;
        var hasBookImage: boolean = false;
        this.parentReader.hasBookPage(this.bookId, this.versionId, () => {
            var textButton = new Button(this.parentReader).createViewButton("font",
                this.parentReader.textPanelLabel,
                this.parentReader.textPanelId);
            hasBookText = true;
            textButton.click();
            $(".page-navigation-container-helper").removeClass("hidden");
            viewButtons.appendChild(textButton);

            var checkboxDiv = this.createCheckboxDiv();
            viewControl.appendChild(checkboxDiv);
        });

        this.parentReader.hasBookImage(this.bookId, this.versionId, () => {
            var imageButton = new Button(this.parentReader).createViewButton("picture",
                this.parentReader.imagePanelLabel,
                this.parentReader.imagePanelId);
            hasBookImage = true;
            if(!hasBookText) {
                imageButton.click();
                $(".page-navigation-container-helper").removeClass("hidden");
            }
            viewButtons.appendChild(imageButton);
        });

        this.parentReader.hasBookAudio(this.bookId, this.versionId, () => {
            var audioButton = new Button(this.parentReader).createViewButton("music",
                this.parentReader.audioPanelLabel,
                this.parentReader.audioPanelId);

            if (!hasBookText && !hasBookImage) {
                audioButton.click();
                $(".page-navigation-container-helper").removeClass("hidden");
            }
            viewButtons.appendChild(audioButton);
        });;
        viewControl.appendChild(viewButtons);
        return viewControl;
    }

    private createCheckboxDiv(): HTMLDivElement {
        var checkboxesDiv = window.document.createElement("div");
        $(checkboxesDiv).addClass("reader-settings-checkboxes-area");

        var showPageCheckboxDiv: HTMLDivElement = window.document.createElement("div");
        var showPageNameCheckbox: HTMLInputElement = window.document.createElement("input");
        showPageNameCheckbox.type = "checkbox";

        $(showPageNameCheckbox).change((eventData: Event) => {
            var readerText: JQuery = $("#" + this.parentReader.textPanelId).find(".reader-text");
            var currentTarget: HTMLInputElement = <HTMLInputElement>(eventData.currentTarget);
            if (currentTarget.checked) {
                readerText.addClass("reader-text-show-page-names");
            } else {
                readerText.removeClass("reader-text-show-page-names");
            }
        });

        var pageNameSlider = document.createElement("label");
        $(pageNameSlider).addClass("switch");

        var showPageNameLabel: HTMLLabelElement = window.document.createElement("label");
        showPageNameLabel.innerHTML = "Číslování stránek";
        showPageCheckboxDiv.appendChild(showPageNameCheckbox);
        showPageCheckboxDiv.appendChild(pageNameSlider);
        showPageCheckboxDiv.appendChild(showPageNameLabel);
        showPageNameCheckbox.id = "checkbox-show-page-numbers";
        showPageNameLabel.setAttribute("for", showPageNameCheckbox.id);
        pageNameSlider.setAttribute("for", showPageNameCheckbox.id);
        checkboxesDiv.appendChild(showPageCheckboxDiv);

        var showPageOnNewLineDiv: HTMLDivElement = window.document.createElement("div");
        var showPageOnNewLineCheckbox: HTMLInputElement = window.document.createElement("input");
        showPageOnNewLineCheckbox.type = "checkbox";

        $(showPageOnNewLineCheckbox).change((eventData: Event) => {
            var readerText = $("#" + this.parentReader.textPanelId).find(".reader-text");
            var currentTarget: HTMLInputElement = <HTMLInputElement>(eventData.currentTarget);
            if (currentTarget.checked) {
                $(readerText).addClass("reader-text-page-new-line");
            } else {
                $(readerText).removeClass("reader-text-page-new-line");
            }
        });

        var pageOnNewLineSlider = document.createElement("label");
        $(pageOnNewLineSlider).addClass("switch");

        var showPageOnNewLineLabel: HTMLLabelElement = window.document.createElement("label");
        showPageOnNewLineLabel.innerHTML = "Zalamovat stránky";
        showPageOnNewLineDiv.appendChild(showPageOnNewLineCheckbox);
        showPageOnNewLineDiv.appendChild(pageOnNewLineSlider);
        showPageOnNewLineDiv.appendChild(showPageOnNewLineLabel);
        showPageOnNewLineCheckbox.id = "checkbox-page-breaks";
        showPageOnNewLineLabel.setAttribute("for", showPageOnNewLineCheckbox.id);
        pageOnNewLineSlider.setAttribute("for", showPageOnNewLineCheckbox.id);
        checkboxesDiv.appendChild(showPageOnNewLineDiv);

        var showCommentCheckboxDiv: HTMLDivElement = window.document.createElement("div");
        var showCommentCheckbox: HTMLInputElement = window.document.createElement("input");
        showCommentCheckbox.type = "checkbox";

        $(showCommentCheckbox).change((eventData: Event) => {
            var readerText = $("#" + this.parentReader.textPanelId).find(".reader-text");
            var currentTarget: HTMLInputElement = <HTMLInputElement>(eventData.currentTarget);
            if (currentTarget.checked) {
                $(readerText).addClass("show-notes");
            } else {
                $(readerText).removeClass("show-notes");
            }
        });

        var commentSlider = document.createElement("label");
        $(commentSlider).addClass("switch");

        var showCommentLabel: HTMLLabelElement = window.document.createElement("label");
        showCommentLabel.innerHTML = "Komentáře";
        showCommentCheckboxDiv.appendChild(showCommentCheckbox);
        showCommentCheckboxDiv.appendChild(commentSlider);
        showCommentCheckboxDiv.appendChild(showCommentLabel);
        showCommentCheckbox.id = "checkbox-show-comment";
        showCommentLabel.setAttribute("for", showCommentCheckbox.id);
        commentSlider.setAttribute("for", showCommentCheckbox.id);
        checkboxesDiv.appendChild(showCommentCheckboxDiv);

        return checkboxesDiv;
    }
}

class Button {  
    private readerLayout: ReaderLayout;

    constructor(readerLayout: ReaderLayout) {
        this.readerLayout = readerLayout;
    }

    public createViewButton(iconName: string, label: string, buttonId: string): HTMLButtonElement {
        var button: HTMLButtonElement = document.createElement("button");
        $(button).addClass(buttonId+"-button");
        var span = document.createElement("span");
        $(span).addClass("glyphicon glyphicon-"+iconName);
        $(button).append(span);

        var spanText = document.createElement("span");
        $(spanText).addClass("button-text");
        $(spanText).append(label);
        $(button).append(spanText);

        $(button).click(() => {
            this.readerLayout.createViewPanel(buttonId, spanText.innerHTML);
        });
        return button;
    }

    public createToolButton(iconName: string, label: string, buttonId: string): HTMLButtonElement {
        var button: HTMLButtonElement = document.createElement("button");
        $(button).addClass(buttonId + "-button");
        var span = document.createElement("span");
        $(span).addClass("glyphicon glyphicon-" + iconName);
        $(button).append(span);

        var spanText = document.createElement("span");
        $(spanText).addClass("button-text");
        $(spanText).append(label);
        $(button).append(spanText);

        $(button).click(() => {
            this.readerLayout.createToolPanel(buttonId, spanText.innerHTML);
        });
        return button;
    }

    public createAddBookmarkButton(iconName: string, label: string, buttonId: string): HTMLButtonElement {
        var button: HTMLButtonElement = document.createElement("button");
        $(button).addClass(buttonId + "-button");
        var span = document.createElement("span");
        $(span).addClass("glyphicon glyphicon-" + iconName);
        $(button).append(span);

        var spanText = document.createElement("span");
        $(spanText).addClass("button-text");
        $(spanText).append(label);
        $(button).append(spanText);


        $(button).click(() => {
            var actualPageName = this.readerLayout.getActualPage().text;
            this.readerLayout.getNewFavoriteDialog().show(actualPageName);
        });

        return button;
    }
}