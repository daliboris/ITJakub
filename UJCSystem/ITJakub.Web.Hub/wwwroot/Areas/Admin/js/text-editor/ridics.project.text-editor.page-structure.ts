﻿class PageStructure {
    private readonly commentArea: CommentArea;
    private readonly util: Util;
    private readonly main: TextEditorMain;
    private readonly editor: Editor;

    constructor(commentArea: CommentArea, util: Util, main: TextEditorMain, editor: Editor) {
        this.commentArea = commentArea;
        this.util = util;
        this.main = main;
        this.editor = editor;
    }

    createPage(pageNumber: number) {
        const isEditingMode = this.editor.getIsEditingMode();
        const showPageNumber = this.main.getShowPageNumbers();
        console.log("trying to create page " + pageNumber); //TODO debug
        let elm = "";
        const pageEl = $(`*[data-page="${pageNumber}"]`);
        elm += "<div class=\"col-xs-7 composition-area\">";
        elm += `<div class="page">`;
        let displayStyle;
        if (showPageNumber) {
            displayStyle = "display: block;";
        } else {
            displayStyle = "display: none;";
        }
        elm += `<div class="page-number" style="${displayStyle}">[${pageNumber}]</div>`;
        if (!isEditingMode) {
            elm += "<div class=\"viewer\">";
            elm += `<span class="rendered-text"></span>`;
            elm += "</div>";
        }
        if (isEditingMode) {
            elm += "<div class=\"editor\">";
            elm += `<textarea class="plain-text"></textarea>`;
            elm += "</div>";   
        }
        elm += "</div>";
        elm += "</div>";
        const html = $.parseHTML(elm);
        $(pageEl).append(html);
        if (!isEditingMode) {
            this.appendRenderedText(pageNumber, showPageNumber);
        }
        if (isEditingMode) {
            this.appendPlainText(pageNumber);
        }
        this.commentArea.asyncConstructCommentArea(pageNumber,
            true,
            true); //collapse section on page load, collapse nested comments on page load
        this.commentArea.toggleAreaSizeIconHide(pageNumber);
    }

    appendRenderedText(pageNumber: number, showPageNumber: boolean) {
        const renderedText = this.util.loadRenderedText(pageNumber);
        renderedText.done((data: string) => {
            if (data !== "error-no-file") {
                const pageEl = $(`*[data-page="${pageNumber}"]`);
                const compositionAreaDiv = pageEl.find(".rendered-text");
                const pageBody = data;
                $(compositionAreaDiv).append(pageBody);
                $(pageEl).children(".image-placeholder").remove();
            }
        });

    }

    appendPlainText(pageNumber: number) {
        const plainText = this.util.loadPlainText(pageNumber);
        plainText.done((data: string) => {
            if (data !== "error-no-file") {
                const pageEl = $(`*[data-page="${pageNumber}"]`);
                const textArea = pageEl.find(".plain-text");
                $(textArea).val(data);
                $(pageEl).children(".image-placeholder").remove();
            }
        });

    }
}