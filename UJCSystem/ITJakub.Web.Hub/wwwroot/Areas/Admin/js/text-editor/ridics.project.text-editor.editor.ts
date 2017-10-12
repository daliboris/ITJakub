﻿class Editor {
    private currentPageNumber = 0; //default initialisation
    private editingMode = false;
    private simplemde: SimpleMDE;
    private readonly commentInput: CommentInput;
    private readonly util: Util;

    constructor(commentInput: CommentInput, util: Util) {
        this.commentInput = commentInput;
        this.util = util;
    }

    getCurrentPageNumber() {
        return this.currentPageNumber;
    }

    getIsEditingMode() {
        return this.editingMode;
    }

    addCommentFromEditor = (editor: SimpleMDE) => {
        const currentPageNumber = this.getCurrentPageNumber();
        const time = Date.now();
        const nested = false;
        const nestedCommentOrder = 0;
        const ajax = (this.commentInput).addCommentSignsAndReturnCommentNumber(editor);
        ajax.done((data: string) => {
            const commentId = data;
            this.commentInput.processCommentSendClick(nested,
                currentPageNumber,
                commentId,
                nestedCommentOrder,
                time);
            this.commentInput.toggleCommentInputPanel();
        });
    }

    processAreaSwitch = () => {
        $(document.documentElement).on("click",
            ".editor",
            (e: JQueryEventObject) => { //dynamically instantiating SimpleMDE editor on textarea
                if (this.editingMode) {
                    let pageDiffers = false;
                    const jElSelected = e.target as HTMLElement;
                    const jEl = $(jElSelected).closest(".page-row");
                    const pageNumber = jEl.data("page") as number;
                    if (pageNumber !== this.currentPageNumber) {
                        pageDiffers = true;
                    }
                    this.currentPageNumber = jEl.data("page") as number;
                    const page = $(`[data-page=${pageNumber}]`);
                    const editor = page.find(".editor");
                    const pageDom = $(editor).children("textarea");
                    const editorExists = $(editor).children(".CodeMirror").length;
                    if (!editorExists) {
                        if (typeof this.simplemde !== "undefined" && this.simplemde !== null) {
                            this.simplemde.toTextArea();
                            this.simplemde = null;
                        }
                        const simpleMdeOptions: SimpleMDE.Options = {
                            element: pageDom[0],
                            autoDownloadFontAwesome: false,
                            spellChecker: false,
                            mode: "gfm",
                            toolbar: [
                                "bold", "italic", "heading", "|", "quote", "preview", {
                                    name: "comment",
                                    action: this.addCommentFromEditor,
                                    className: "fa fa-comment",
                                    title: "Add comment"
                                }
                            ]
                        };
                        this.simplemde = new SimpleMDE(simpleMdeOptions);
                        this.simplemde.defineMode("comment",
                            () => ({
                                token(stream: any) {
                                    if (stream.match(
                                        /(\$([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\%)/)
                                    ) {
                                        return "comment-start";
                                    }
                                    if (stream.match(
                                        /(\%([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\$)/)
                                    ) {
                                        return "comment-end";
                                    }
                                    while (stream.next() != null &&
                                        !stream.match(
                                            /(([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12})/,
                                            false)) {
                                        return null;
                                    }

                                }
                            }));

                        this.simplemde.codemirror.addOverlay("comment");

                    }
                    if (editorExists && pageDiffers) {
                        this.simplemde.toTextArea();
                        this.simplemde = null;
                    }
                }
            });
    }

    processPageModeSwitch = () => {
        $("#project-resource-preview").on("click",
            ".editing-mode-button",
            () => {
                this.editingMode = !this.editingMode;
                if (typeof this.simplemde !== "undefined" && !this.editingMode && this.simplemde !== null) {
                    this.simplemde.toTextArea();
                    this.simplemde = null;
                }
                this.toggleDivAndTextarea();
            });
    }

    toggleDivAndTextarea = () => {
        var pageRow = $(".lazyloaded");
        if (this.editingMode) { // changing div to textarea here
            pageRow.each((index: number, child: Element) => {
                const pageNumber = $(child).data("page") as number;
                const page = $(child).children(".composition-area").children(".page");
                const placeholderSpinner = $(child).children(".image-placeholder");
                placeholderSpinner.show();
                const plainTextAjax = this.util.loadPlainText(pageNumber);
                const viewerElement = $(page).children(".viewer");
                viewerElement.remove();
                this.createEditorAreaBody(page[0], plainTextAjax);
            });
        } else { // changing textarea to div here
            pageRow.each((index: number, child: Element) => {
                const pageNumber = $(child).data("page") as number;
                const page = $(child).children(".composition-area").children(".page");
                const placeholderSpinner = $(child).children(".image-placeholder");
                placeholderSpinner.show();
                const renderedTextAjax = this.util.loadRenderedText(pageNumber);
                const editorElement = $(page).children(".editor");
                editorElement.remove();
                this.createViewerAreaBody(page[0], renderedTextAjax);
            });
        }
    }

    private createEditorAreaBody(child: Element, ajax: JQueryXHR) {
        ajax.done((data: string) => {
            const placeHolderSpinner = $(child).parent(".composition-area").siblings(".image-placeholder");
            const plainText = data;
            const elm = `<div class="editor"><textarea>${plainText}</textarea></div>`;
            $(child).append(elm);
            placeHolderSpinner.hide();
        });
    }

    private createViewerAreaBody(child: Element, ajax: JQueryXHR) {
        ajax.done((data: IPageText) => {
            const placeHolderSpinner = $(child).parent(".composition-area").siblings(".image-placeholder");
            const renderedText = data.text;
            const elm = `<div class="viewer">${renderedText}</div>`;
            $(child).append(elm);
            placeHolderSpinner.hide();
        });
    }
}