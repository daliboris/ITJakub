﻿$(document).ready(() => {
    var favoriteManager = new FavoriteManager(StorageManager.getInstance().getStorage());
    var favoriteManagement = new FavoriteManagement(favoriteManager);
    favoriteManagement.init();
});

class FavoriteManagement {
    private favoriteManager: FavoriteManager;
    private activeLabelId: number;
    private activeLabelForEditing: JQuery;
    private labelColorInput: ColorInput;
    private newFavoriteLabelDialog: FavoriteManagementDialog;
    private removeDialog: FavoriteManagementDialog;

    constructor(favoriteManager: FavoriteManager) {
        this.favoriteManager = favoriteManager;
        this.activeLabelForEditing = null;
        this.activeLabelId = null;

        this.newFavoriteLabelDialog = new FavoriteManagementDialog($("#new-favorite-label-dialog"));
        this.removeDialog = new FavoriteManagementDialog($("#remove-dialog"));
    }

    public init() {
        this.labelColorInput = new ColorInput($("#favorite-label-color"));
        this.labelColorInput.make();

        $(".favorite-label-management").each((index, element) => {
            var backgroundColor = $(element).data("color");
            var fontColor = FavoriteHelper.getFontColor(backgroundColor);
            $("a", element).css("color", fontColor);
        });
        
        $("#add-new-label").click(() => {
            this.showAddLabelDialog();
        });

        $("#new-favorite-label-dialog .save-button").click(() => {
            this.saveFavoriteLabel();
        });

        $(".favorite-label-management .favorite-label-link").click((event) => {
            var activeElement = $(event.target).closest(".favorite-label-management");
            this.setActiveLabel(activeElement);
        });

        $(".favorite-label-management .favorite-label-remove-link").click((event) => {
            var item = $(event.target).closest(".favorite-label-management");
            this.showRemoveDialog(item);
        });

        $(".favorite-label-management .favorite-label-edit-link").click((event) => {
            var item = $(event.target).closest(".favorite-label-management");
            var name = item.data("name");
            var color = item.data("color");
            this.showEditLabelDialog(name, color, item);
        });

        $("#sort-select").change(this.loadFavoriteItems.bind(this));
        $("#type-filter-select").change(this.loadFavoriteItems.bind(this));
        $("#name-filter").change(this.loadFavoriteItems.bind(this));
        //$("#name-filter-button").click(this.loadFavoriteItems.bind(this));

        $("#show-all-link").click(() => {
            this.setActiveLabel(null);
        });

        this.loadFavoriteItems();
    }

    private initFavoriteLabel(item: JQuery) {
        var backgroundColor = item.data("color");
        var fontColor = FavoriteHelper.getFontColor(backgroundColor);
        $("a", item).css("color", fontColor);

        $(".favorite-label-link", item).click(() => {
            this.setActiveLabel(item);
        });

        $(".favorite-label-management .favorite-label-remove-link").click(() => {
            this.showRemoveDialog(item);
        });

        $(".favorite-label-management .favorite-label-edit-link").click(() => {
            var name = item.data("name");
            var color = item.data("color");
            this.showEditLabelDialog(name, color, item);
        });
    }

    private loadFavoriteItems() {
        var sortOrder = $("#sort-select").val();
        var typeFilter = $("#type-filter-select").val();
        var nameFilter = $("#name-filter").val();
        
        var container = $("#favorite-item-container");
        container.empty();
        container.addClass("loader");

        this.favoriteManager.getFavorites(this.activeLabelId, typeFilter, nameFilter, sortOrder, (favorites) => {
            container.removeClass("loader");
            for (let i = 0; i < favorites.length; i++) {
                var favoriteItem = favorites[i];
                var item = new FavoriteManagementItem(container, favoriteItem.FavoriteType, favoriteItem.Id, favoriteItem.Title, favoriteItem.CreateTime, this.favoriteManager);
                item.make();
            }
        });
    }

    private setActiveLabel(item: JQuery) {
        $(".favorite-label-management").removeClass("active");

        if (item != null) {
            item.addClass("active");
            this.activeLabelId = item.data("id");
        } else {
            this.activeLabelId = null;
        }
        
        this.loadFavoriteItems();
    }

    private showRemoveDialog(item: JQuery) {
        var labelName = item.data("name");

        $("#remove-dialog .modal-body")
            .text("Opravdu chcete smazat vybraný štítek (" + labelName + ")? Štítek bude smazán včetně všech přiřazených oblíbených položek.");

        $("#remove-dialog .remove-button")
            .off("click")
            .click(() => {
                this.removeLabel(item);
            });

        this.removeDialog.show();
    }

    private showAddLabelDialog() {
        this.showEditLabelDialog("", "", null);
    }

    private showEditLabelDialog(name: string, color: string, item: JQuery) {
        this.activeLabelForEditing = item;
        this.labelColorInput.setValue(color);
        $("#favorite-label-name").val(name);
        this.newFavoriteLabelDialog.show();
    }

    private removeLabel(item: JQuery) {
        var labelId = item.data("id");
        this.removeDialog.showSaving();
        this.favoriteManager.deleteFavoriteLabel(labelId, (error) => {
            if (error) {
                this.removeDialog.showError("Chyba při odstraňování štítku");
                return;
            }

            if (item.hasClass("active")) {
                this.setActiveLabel(null);
            }

            item.remove();
            this.removeDialog.hide();
        });
    }

    private saveNewFavoriteLabel(name: string, color: string) {
        this.newFavoriteLabelDialog.showSaving();
        this.favoriteManager.createFavoriteLabel(name, color, (id, error) => {
            if (error) {
                this.newFavoriteLabelDialog.showError("Chyba při ukládání štítku");
                return;
            }

            this.newFavoriteLabelDialog.hide();

            var labelDiv = document.createElement("div");
            $("#favorite-labels").append(labelDiv);

            var url = getBaseUrl() + "Favorite/GetFavoriteLabelManagementPartial?";
            var urlParams = {
                id: id,
                name: name,
                color: color
            }
            url = url + $.param(urlParams);

            $(labelDiv).load(url, null, () => {
                this.initFavoriteLabel($(labelDiv).children());
            });
        });
    }

    private saveEditedFavoriteLabel(labelItem: JQuery, name: string, color: string) {
        var labelId = labelItem.data("id");
        this.newFavoriteLabelDialog.showSaving();
        this.favoriteManager.updateFavoriteLabel(labelId, name, color, (error) => {
            if (error) {
                this.newFavoriteLabelDialog.showError("Chyba při ukládání štítku");
                return;
            }

            this.newFavoriteLabelDialog.hide();

            $(".favorite-label-name", labelItem).text(name);
            labelItem.css("background-color", color);
            labelItem.css("color", FavoriteHelper.getFontColor(color));
            labelItem.data("name", name);
            labelItem.data("color", color);
        });
    }

    private saveFavoriteLabel() {
        var name = $("#favorite-label-name").val();
        var color = this.labelColorInput.getValue();

        var error = "";
        if (!name) {
            error = "Nebylo zadáno jméno.";
        }
        if (!FavoriteHelper.isValidHexColor(color)) {
            error += " Nesprávný formát barvy (požadovaný formát: #000000).";
        }
        if (error.length > 0) {
            this.newFavoriteLabelDialog.showError(error);
            return;
        }

        if (this.activeLabelForEditing == null) {
            this.saveNewFavoriteLabel(name, color);
        } else {
            this.saveEditedFavoriteLabel(this.activeLabelForEditing, name, color);
        }
    }
}

class FavoriteManagementDialog {
    private dialogJQuery: JQuery;

    constructor(dialogJQuery: JQuery) {
        this.dialogJQuery = dialogJQuery;
    }

    public show() {
        $(".error, .saving-icon").addClass("hidden");
        this.dialogJQuery.modal("show");
    }

    public showSaving() {
        $(".saving-icon", this.dialogJQuery)
            .removeClass("hidden");
        $(".error", this.dialogJQuery)
            .addClass("hidden");
    }

    public showError(text: string) {
        $(".saving-icon", this.dialogJQuery)
            .addClass("hidden");
        $(".error", this.dialogJQuery)
            .text(text)
            .removeClass("hidden");
    }

    public hide() {
        this.dialogJQuery.modal("hide");
    }
}

class FavoriteManagementItem {
    private favoriteManager: FavoriteManager;
    private createTime: string;
    private name: string;
    private id: number;
    private type: FavoriteType;
    private container: JQuery;
    private innerContainerDiv: HTMLDivElement;
    private separatorHr: HTMLHRElement;
    private editFavoriteDialog: FavoriteManagementDialog;
    private removeDialog: FavoriteManagementDialog;

    constructor(container: JQuery, type: FavoriteType, id: number, name: string, createTime: string, favoriteManager: FavoriteManager) {
        this.favoriteManager = favoriteManager;
        this.createTime = createTime;
        this.name = name;
        this.id = id;
        this.type = type;
        this.container = container;

        this.editFavoriteDialog = new FavoriteManagementDialog($("#edit-favorite-dialog"));
        this.removeDialog = new FavoriteManagementDialog($("#remove-dialog"));
    }

    public make() {
        var innerContainerDiv = document.createElement("div");
        var separatorHr = document.createElement("hr");

        var iconColumn = document.createElement("div");
        var iconContainer = document.createElement("div");
        var icon = this.createIconElement();
        $(iconContainer)
            .attr("style", "text-align: center; font-size: 120%;")
            .append(icon);
        $(iconColumn)
            .addClass("col-md-1 col-xs-2")
            .append(iconContainer);

        var nameColumn = document.createElement("div");
        var nameLink = document.createElement("a");
        var nameDiv = document.createElement("div");
        $(nameDiv)
            .text(this.name)
            .addClass("favorite-item-name");
        $(nameLink)
            .attr("href", "#")
            .append(nameDiv);
        $(nameColumn)
            .addClass("col-md-10 col-xs-8")
            .append(nameLink);

        var removeColumn = document.createElement("div");
        var removeLink = document.createElement("a");
        var removeIconContainer = document.createElement("div");
        var removeIcon = document.createElement("span");
        var editLink = document.createElement("a");
        var editIconContainer = document.createElement("div");
        var editIcon = document.createElement("span");
        $(removeIcon)
            .addClass("glyphicon")
            .addClass("glyphicon-trash");
        $(editIcon)
            .addClass("glyphicon")
            .addClass("glyphicon-pencil");
        $(removeIconContainer)
            .addClass("text-center")
            .attr("style", "float: right; width: 45%;")
            .append(removeIcon);
        $(editIconContainer)
            .addClass("text-center")
            .append(editIcon);
        $(removeLink)
            .attr("href", "#")
            .attr("title", "Smazat oblíbenou položku")
            .append(removeIconContainer)
            .click(() => {
                $("#remove-dialog .modal-body")
                    .text("Opravdu chcete smazat vybranou oblíbenou položku (" + this.name + ")?");

                $("#remove-dialog .remove-button")
                    .off("click")
                    .click(this.remove.bind(this));

                this.removeDialog.show();
                
            });
        $(editLink)
            .attr("href", "#")
            .attr("title", "Upravit oblíbenou položku")
            .append(editIconContainer)
            .click(() => {
                $("#favorite-item-name").val(this.name);

                $("#edit-favorite-dialog .save-button")
                    .off("click")
                    .click(this.edit.bind(this));

                this.editFavoriteDialog.show();
            });

        $(removeColumn)
            .addClass("col-md-1 col-xs-2")
            .append(removeLink)
            .append(editLink);
        
        $(innerContainerDiv)
            .addClass("row")
            .append(iconColumn)
            .append(nameColumn)
            .append(removeColumn);

        this.container
            .append(innerContainerDiv)
            .append(separatorHr);

        this.innerContainerDiv = innerContainerDiv;
        this.separatorHr = separatorHr;
    }

    private remove() {
        this.removeDialog.showSaving();
        this.favoriteManager.deleteFavoriteItem(this.id, (error) => {
            if (error) {
                this.removeDialog.showError("Chyba při odstraňování položky");
                return;
            }

            this.removeDialog.hide();

            $(this.innerContainerDiv).remove();
            $(this.separatorHr).remove();
        });
    }

    private edit() {
        var newName = $("#favorite-item-name").val();

        this.editFavoriteDialog.showSaving();
        this.favoriteManager.updateFavoriteItem(this.id, newName, (error) => {
            if (error) {
                this.editFavoriteDialog.showError("Chyba při ukládání položky");
                return;
            }

            this.editFavoriteDialog.hide();

            this.name = newName;
            $(".favorite-item-name", this.innerContainerDiv).text(newName);
        });
    }

    private createIconElement(): HTMLSpanElement {
        var icon = document.createElement("span");
        $(icon).addClass("glyphicon");

        switch (this.type) {
            case FavoriteType.Book:
                $(icon).addClass("glyphicon-book")
                    .attr("title", "Kniha");
                break;
            case FavoriteType.PageBookmark:
                $(icon).addClass("glyphicon-bookmark")
                    .attr("title", "Záložka na stránku v knize");
                break;
            case FavoriteType.Category:
                $(icon).addClass("glyphicon-list")
                    .attr("title", "Kategorie");
                break;
            case FavoriteType.Query:
                $(icon).addClass("glyphicon-search")
                    .attr("title", "Vyhledávací dotaz");
                break;
            case FavoriteType.BookVersion:
                $(icon).addClass("glyphicon-tags")
                    .attr("title", "Verze knihy");
                break;
            default:
                $(icon).addClass("glyphicon-question-sign")
                    .attr("title", "Neznámý typ oblíbené položky");
                break;
        }
        return icon;
    }
}

class ColorInput {
    private inputElement: JQuery;

    constructor(inputElement: JQuery) {
        this.inputElement = inputElement;
    }

    public make() {
        this.inputElement.colorpickerplus();
        this.inputElement.on("changeColor", (event, color) => {
            if (color == null) {
                color = "#FFFFFF";
            }

            this.setValue(color);
        });

        this.inputElement.change(() => this.updateBackground());

        // disable saving custom colors:
        $(".colorpickerplus-container button").remove();
        if (window.localStorage) {
            window.localStorage.removeItem("colorpickerplus_custom_colors");
        }
    }

    public setValue(value: string) {
        this.inputElement.val(value);
        this.updateBackground();
    }

    public getValue(): string {
        return this.inputElement.val();
    }

    private updateBackground() {
        var value = this.inputElement.val();
        if (value.length !== 7) {
            value = "#FFFFFF";
        }

        this.inputElement.css("background-color", value);
        this.inputElement.css("color", FavoriteHelper.getFontColor(value));
    }
}