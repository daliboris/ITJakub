﻿declare var lazySizes: any;

class PageLazyLoading {
    private readonly pageStructure: PageStructure;
    private readonly editor: Editor;

    constructor(pageStructure: PageStructure, editor: Editor) {
        this.pageStructure = pageStructure;
        this.editor = editor;
    }

    init() {
        $(".pages-start").on("lazybeforeunveil",
            (event) => {
                var targetEl = $(event.target);
                if (targetEl.hasClass("page-row")) {
                    this.pageStructure.loadPage(targetEl);
                } else {
                    this.pageStructure.loadSection(targetEl);
                }
            });
        this.initConfig();
    }

    private initConfig() {
        const lazyConfig = (window as any).lazySizesConfig;
        lazyConfig.loadMode = 1; //only load visible elements
    }
}