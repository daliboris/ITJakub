﻿
class BibliographyModule {
    private static _instance: BibliographyModule = null;
    bibliographyModulControllerUrl: string = "";

    constructor() {
        if (BibliographyModule._instance) {
            throw new Error("Cannot instantiate...Use getInstance method instead");
        }
        BibliographyModule._instance = this;
    }

    public static getInstance(): BibliographyModule {
        if (BibliographyModule._instance === null) {
            BibliographyModule._instance = new BibliographyModule();
        }
        return BibliographyModule._instance;
    }


    public showBooks(books: IBookInfo[], container: string) {
        $(container).empty();
        var rootElement: HTMLUListElement = document.createElement('ul');
        $(rootElement).addClass('listing');
        $.each(books, (index, book: IBookInfo) => {
            var bibliographyHtml = this.makeBibliography(book);
            rootElement.appendChild(bibliographyHtml);
        });
        $(container).append(rootElement);
    }

    private makeBibliography(bibItem: IBookInfo): HTMLLIElement {
        var liElement: HTMLLIElement = document.createElement('li');
        $(liElement).addClass('list-item');
        $(liElement).attr("data-bookId", bibItem.BookId);

        var visibleContent: HTMLDivElement = document.createElement('div');
        $(visibleContent).addClass('visible-content');

        var bibFactory: IBibliographyFactory = BibliographyFactoryResolver.getInstance().getFactoryForType(bibItem.BookType);

        var panel = bibFactory.makeLeftPanel(bibItem);
        if (panel != null) visibleContent.appendChild(panel);

        panel = bibFactory.makeRightPanel(bibItem);
        if (panel != null) visibleContent.appendChild(panel);

        panel = bibFactory.makeMiddlePanel(bibItem);
        if (panel != null) visibleContent.appendChild(panel);

        $(liElement).append(visibleContent);

        var hiddenContent: HTMLDivElement = document.createElement('div');
        $(hiddenContent).addClass('hidden-content');

        panel = bibFactory.makeBottomPanel(bibItem);
        if (panel != null) hiddenContent.appendChild(panel);

        $(liElement).append(hiddenContent);

        return liElement;

    }
}

class OldCzechTextBankFactory implements IBibliographyFactory {
    makeLeftPanel(bookInfo: IBookInfo): HTMLDivElement {
        var leftPanel: HTMLDivElement = document.createElement('div');
        $(leftPanel).addClass('left-panel');
        return leftPanel;
    }

    makeRightPanel(bookInfo: IBookInfo): HTMLDivElement {
        var rightPanel: HTMLDivElement = document.createElement('div');
        $(rightPanel).addClass('right-panel');

        var infoButton: HTMLButtonElement = document.createElement('button');
        infoButton.type = 'button';
        $(infoButton).addClass('btn btn-sm information-button');
        var spanInfo: HTMLSpanElement = document.createElement('span');
        $(spanInfo).addClass('glyphicon glyphicon-info-sign');
        infoButton.appendChild(spanInfo);
        $(infoButton).click((event) => {

        }); //TODO fill click action
        rightPanel.appendChild(infoButton);

        var showContentButton: HTMLButtonElement = document.createElement('button');
        showContentButton.type = 'button';
        $(showContentButton).addClass('btn btn-sm show-button');
        var spanChevrDown: HTMLSpanElement = document.createElement('span');
        $(spanChevrDown).addClass('glyphicon glyphicon-chevron-down');
        showContentButton.appendChild(spanChevrDown);
        $(showContentButton).click(function (event) {
            $(this).parents('li.list-item').first().find('.hidden-content').show("slow");
            $(this).siblings('.hide-button').show();
            $(this).hide();
        });
        rightPanel.appendChild(showContentButton);

        var hideContentButton: HTMLButtonElement = document.createElement('button');
        hideContentButton.type = 'button';
        $(hideContentButton).addClass('btn btn-sm hide-button');
        var spanChevrUp: HTMLSpanElement = document.createElement('span');
        $(spanChevrUp).addClass('glyphicon glyphicon-chevron-up');
        hideContentButton.appendChild(spanChevrUp);
        $(hideContentButton).click(function (event) {
            $(this).parents('li.list-item').first().find('.hidden-content').hide("slow");
            $(this).siblings('.show-button').show();
            $(this).hide();
        });
        rightPanel.appendChild(hideContentButton);

        return rightPanel;
    }

    makeMiddlePanel(bookInfo: IBookInfo): HTMLDivElement {
        var middlePanel: HTMLDivElement = document.createElement('div');
        $(middlePanel).addClass('middle-panel');
        var middlePanelHeading: HTMLDivElement = document.createElement('div');
        $(middlePanelHeading).addClass('heading');
        middlePanelHeading.innerHTML = bookInfo.Name;
        middlePanel.appendChild(middlePanelHeading);
        var middlePanelBody: HTMLDivElement = document.createElement('div');
        $(middlePanelBody).addClass('body');
        middlePanelBody.innerHTML = bookInfo.Body;
        middlePanel.appendChild(middlePanelBody);
        return middlePanel;
    }

    makeBottomPanel(bookInfo: IBookInfo): HTMLDivElement {
        var tableDiv: HTMLDivElement = document.createElement('div');
        $(tableDiv).addClass('table');
        this.appendTableRow("Editor", bookInfo.Editor, tableDiv);
        this.appendTableRow("Předloha", bookInfo.Pattern, tableDiv);
        this.appendTableRow("Zkratka památky", bookInfo.RelicAbbreviation, tableDiv);
        this.appendTableRow("Zkratka pramene", bookInfo.SourceAbbreviation, tableDiv);
        this.appendTableRow("Literární druh", bookInfo.LiteraryType, tableDiv);
        this.appendTableRow("Literární žánr", bookInfo.LiteraryGenre, tableDiv);
        this.appendTableRow("Poslední úprava edice", bookInfo.LastEditation, tableDiv);

        //TODO add Edicni poznamka anchor and copyright to hiddenContent here

        return tableDiv;
    }

    //TODO make makeTable Helper or table builder
    private appendTableRow(label: string, value: string, tableDiv: HTMLDivElement) {
        var rowDiv: HTMLDivElement = document.createElement('div');
        $(rowDiv).addClass('row');
        var labelDiv: HTMLDivElement = document.createElement('div');
        $(labelDiv).addClass('cell label');
        labelDiv.innerHTML = label;
        rowDiv.appendChild(labelDiv);
        var valueDiv: HTMLDivElement = document.createElement('div');
        $(valueDiv).addClass('cell');
        if (!value || value.length === 0) {
            valueDiv.innerHTML = "&lt;nezadáno&gt;";
        } else {
            valueDiv.innerHTML = value;
        }
        rowDiv.appendChild(valueDiv);
        tableDiv.appendChild(rowDiv);
    }
}

class DictionaryFactory implements IBibliographyFactory {

    makeLeftPanel(bookInfo: IBookInfo): HTMLDivElement {
        var leftPanel: HTMLDivElement = document.createElement('div');
        $(leftPanel).addClass('left-panel');

        var inputCheckbox: HTMLInputElement = document.createElement('input');
        inputCheckbox.type = "checkbox";
        $(inputCheckbox).addClass('checkbox');
        leftPanel.appendChild(inputCheckbox);

        var starEmptyButton: HTMLButtonElement = document.createElement('button');
        starEmptyButton.type = 'button';
        $(starEmptyButton).addClass('btn btn-xs star-empty-button');
        var spanEmptyStar: HTMLSpanElement = document.createElement('span');
        $(spanEmptyStar).addClass('glyphicon glyphicon-star-empty');
        starEmptyButton.appendChild(spanEmptyStar);
        $(starEmptyButton).click(function (event) {
            $(this).siblings('.star-button').show();
            $(this).hide();
        }); //TODO fill click action
        leftPanel.appendChild(starEmptyButton);

        var starButton: HTMLButtonElement = document.createElement('button');
        starButton.type = 'button';
        $(starButton).addClass('btn btn-xs star-button');
        $(starButton).css('display', 'none');
        var spanStar: HTMLSpanElement = document.createElement('span');
        $(spanStar).addClass('glyphicon glyphicon-star');
        starButton.appendChild(spanStar);
        $(starButton).click(function (event) {
            $(this).siblings('.star-empty-button').show();
            $(this).hide();
        }); //TODO fill click action
        leftPanel.appendChild(starButton);

        return leftPanel;
    }

    makeRightPanel(bookInfo: IBookInfo): HTMLDivElement {
        var rightPanel: HTMLDivElement = document.createElement('div');
        $(rightPanel).addClass('right-panel');

        var bookButton: HTMLButtonElement = document.createElement('button');
        bookButton.type = 'button';
        $(bookButton).addClass('btn btn-sm book-button');
        var spanBook: HTMLSpanElement = document.createElement('span');
        $(spanBook).addClass('glyphicon glyphicon-book');
        bookButton.appendChild(spanBook);
        $(bookButton).click((event) => {

        }); //TODO fill click action
        rightPanel.appendChild(bookButton);

        var infoButton: HTMLButtonElement = document.createElement('button');
        infoButton.type = 'button';
        $(infoButton).addClass('btn btn-sm information-button');
        var spanInfo: HTMLSpanElement = document.createElement('span');
        $(spanInfo).addClass('glyphicon glyphicon-info-sign');
        infoButton.appendChild(spanInfo);
        $(infoButton).click((event) => {

        }); //TODO fill click action
        rightPanel.appendChild(infoButton);

        return rightPanel;
    }

    makeMiddlePanel(bookInfo: IBookInfo): HTMLDivElement {
        var middlePanel: HTMLDivElement = document.createElement('div');
        $(middlePanel).addClass('middle-panel');
        var middlePanelHeading: HTMLDivElement = document.createElement('div');
        $(middlePanelHeading).addClass('heading');
        middlePanelHeading.innerHTML = bookInfo.Name;
        middlePanel.appendChild(middlePanelHeading);
        var middlePanelBody: HTMLDivElement = document.createElement('div');
        $(middlePanelBody).addClass('body');
        middlePanelBody.innerHTML = bookInfo.Body;
        middlePanel.appendChild(middlePanelBody);
        return middlePanel;
    }

    makeBottomPanel(bookInfo: IBookInfo): HTMLDivElement {
        return null;
    }
}

class EditionFactory implements IBibliographyFactory {

    makeLeftPanel(bookInfo: IBookInfo): HTMLDivElement {
        var leftPanel: HTMLDivElement = document.createElement('div');
        $(leftPanel).addClass('left-panel');
        return leftPanel;
    }

    makeRightPanel(bookInfo: IBookInfo): HTMLDivElement {
        var rightPanel: HTMLDivElement = document.createElement('div');
        $(rightPanel).addClass('right-panel');

        var bookButton: HTMLButtonElement = document.createElement('button');
        bookButton.type = 'button';
        $(bookButton).addClass('btn btn-sm book-button');
        var spanBook: HTMLSpanElement = document.createElement('span');
        $(spanBook).addClass('glyphicon glyphicon-book');
        bookButton.appendChild(spanBook);
        $(bookButton).click((event) => {

        }); //TODO fill click action
        rightPanel.appendChild(bookButton);

        var infoButton: HTMLButtonElement = document.createElement('button');
        infoButton.type = 'button';
        $(infoButton).addClass('btn btn-sm information-button');
        var spanInfo: HTMLSpanElement = document.createElement('span');
        $(spanInfo).addClass('glyphicon glyphicon-info-sign');
        infoButton.appendChild(spanInfo);
        $(infoButton).click((event) => {

        }); //TODO fill click action
        rightPanel.appendChild(infoButton);

        var showContentButton: HTMLButtonElement = document.createElement('button');
        showContentButton.type = 'button';
        $(showContentButton).addClass('btn btn-sm show-button');
        var spanChevrDown: HTMLSpanElement = document.createElement('span');
        $(spanChevrDown).addClass('glyphicon glyphicon-chevron-down');
        showContentButton.appendChild(spanChevrDown);
        $(showContentButton).click(function(event) {
            $(this).parents('li.list-item').first().find('.hidden-content').show("slow");
            $(this).siblings('.hide-button').show();
            $(this).hide();
        });
        rightPanel.appendChild(showContentButton);

        var hideContentButton: HTMLButtonElement = document.createElement('button');
        hideContentButton.type = 'button';
        $(hideContentButton).addClass('btn btn-sm hide-button');
        var spanChevrUp: HTMLSpanElement = document.createElement('span');
        $(spanChevrUp).addClass('glyphicon glyphicon-chevron-up');
        hideContentButton.appendChild(spanChevrUp);
        $(hideContentButton).click(function(event) {
            $(this).parents('li.list-item').first().find('.hidden-content').hide("slow");
            $(this).siblings('.show-button').show();
            $(this).hide();
        });
        rightPanel.appendChild(hideContentButton);

        return rightPanel;
    }


    makeMiddlePanel(bookInfo: IBookInfo): HTMLDivElement {
        var middlePanel: HTMLDivElement = document.createElement('div');
        $(middlePanel).addClass('middle-panel');
        var middlePanelHeading: HTMLDivElement = document.createElement('div');
        $(middlePanelHeading).addClass('heading');
        middlePanelHeading.innerHTML = bookInfo.Name;
        middlePanel.appendChild(middlePanelHeading);
        var middlePanelBody: HTMLDivElement = document.createElement('div');
        $(middlePanelBody).addClass('body');
        middlePanelBody.innerHTML = bookInfo.Body;
        middlePanel.appendChild(middlePanelBody);
        return middlePanel;
    }

    makeBottomPanel(bookInfo: IBookInfo): HTMLDivElement {
        var tableDiv: HTMLDivElement = document.createElement('div');
        $(tableDiv).addClass('table');
        this.appendTableRow("Editor", bookInfo.Editor, tableDiv);
        this.appendTableRow("Předloha", bookInfo.Pattern, tableDiv);
        this.appendTableRow("Zkratka památky", bookInfo.RelicAbbreviation, tableDiv);
        this.appendTableRow("Zkratka pramene", bookInfo.SourceAbbreviation, tableDiv);
        this.appendTableRow("Literární druh", bookInfo.LiteraryType, tableDiv);
        this.appendTableRow("Literární žánr", bookInfo.LiteraryGenre, tableDiv);
        this.appendTableRow("Poslední úprava edice", bookInfo.LastEditation, tableDiv);

        //TODO add Edicni poznamka anchor and copyright to hiddenContent here

        return tableDiv;
    }

    //TODO make makeTable Helper
    private appendTableRow(label: string, value: string, tableDiv: HTMLDivElement) {
        var rowDiv: HTMLDivElement = document.createElement('div');
        $(rowDiv).addClass('row');
        var labelDiv: HTMLDivElement = document.createElement('div');
        $(labelDiv).addClass('cell label');
        labelDiv.innerHTML = label;
        rowDiv.appendChild(labelDiv);
        var valueDiv: HTMLDivElement = document.createElement('div');
        $(valueDiv).addClass('cell');
        if (!value || value.length === 0) {
            valueDiv.innerHTML = "&lt;nezadáno&gt;";
        } else {
            valueDiv.innerHTML = value;
        }
        rowDiv.appendChild(valueDiv);
        tableDiv.appendChild(rowDiv);
    }
}

class BibliographyFactoryResolver {
    private static _instance: BibliographyFactoryResolver = null;
    private m_factories: IBibliographyFactory[];


    constructor() {
        if (BibliographyFactoryResolver._instance) {
            throw new Error("Cannot instantiate...Use getInstance method instead");
        }
        BibliographyFactoryResolver._instance = this;
        this.m_factories = new Array();
        this.m_factories['Edition'] = new EditionFactory(); //TODO make enum bookType
        this.m_factories['Dictionary'] = new DictionaryFactory();
        this.m_factories['OldCzechTextBank'] = new OldCzechTextBankFactory();

    }

    public static getInstance(): BibliographyFactoryResolver {
        if (BibliographyFactoryResolver._instance === null) {
            BibliographyFactoryResolver._instance = new BibliographyFactoryResolver();
        }
        return BibliographyFactoryResolver._instance;
    }

    public getFactoryForType(bookType: string): IBibliographyFactory {
        return this.m_factories[bookType];
    }


}

interface IBibliographyFactory {

    makeLeftPanel(bookInfo: IBookInfo): HTMLDivElement;
    makeRightPanel(bookInfo: IBookInfo): HTMLDivElement;
    makeMiddlePanel(bookInfo: IBookInfo): HTMLDivElement;
    makeBottomPanel(bookInfo: IBookInfo): HTMLDivElement;
}

interface IBookInfo {
    BookId: string;
    BookType: string;
    Name: string;
    Body: string;
    Editor: string;
    Pattern: string;
    SourceAbbreviation: string;
    RelicAbbreviation: string;
    LiteraryType: string;
    LiteraryGenre: string;
    LastEditation: string;
    EditationNote: string; //anchor href?
    Copyright: string;

}