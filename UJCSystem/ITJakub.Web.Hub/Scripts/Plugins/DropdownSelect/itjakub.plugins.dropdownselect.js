﻿/// <reference path="../../typings/jqueryui/jqueryui.d.ts" />
var DropDownSelect = (function () {
    function DropDownSelect(dropDownSelectContainer, showStar) {
        this.dropDownSelectContainer = dropDownSelectContainer;
        this.showStar = showStar;
        this.makeDropdown();
    }
    DropDownSelect.prototype.makeDropdown = function () {
        var dropDownDiv = document.createElement("div");
        $(dropDownDiv).addClass("dropdown-select");

        this.makeHeader(dropDownDiv);
        this.makeBody(dropDownDiv);

        $(this.dropDownSelectContainer).append(dropDownDiv);
    };

    DropDownSelect.prototype.makeHeader = function (dropDownDiv) {
        var dropDownHeadDiv = document.createElement("div");
        $(dropDownHeadDiv).addClass("dropdown-select-header");

        var checkBoxSpan = document.createElement("span");
        $(checkBoxSpan).addClass("dropdown-select-checkbox");

        var checkbox = document.createElement("input");
        checkbox.type = "checkbox";

        checkBoxSpan.appendChild(checkbox);

        dropDownHeadDiv.appendChild(checkBoxSpan);

        var textSpan = document.createElement("span");
        $(textSpan).addClass("dropdown-select-text");
        textSpan.innerText = "Slovnínky o staré češtině"; //TODO read from parameter

        dropDownHeadDiv.appendChild(textSpan);

        var moreSpan = document.createElement("span");
        $(moreSpan).addClass("dropdown-select-more");

        $(moreSpan).click(function () {
            var body = $(this).parents(".dropdown-select").children(".dropdown-select-body");
            if (body.is(":hidden")) {
                $(this).children().removeClass("glyphicon-chevron-down");
                $(this).children().addClass("glyphicon-chevron-up");
                body.slideDown();
            } else {
                $(this).children().removeClass("glyphicon-chevron-up");
                $(this).children().addClass("glyphicon-chevron-down");
                body.slideUp();
            }
        });

        var iconSpan = document.createElement("span");
        $(iconSpan).addClass("glyphicon glyphicon-chevron-down");

        moreSpan.appendChild(iconSpan);

        dropDownHeadDiv.appendChild(moreSpan);

        dropDownDiv.appendChild(dropDownHeadDiv);
    };

    DropDownSelect.prototype.makeBody = function (dropDownDiv) {
        var dropDownBodyDiv = document.createElement("div");
        $(dropDownBodyDiv).addClass("dropdown-select-body");

        var filterDiv = document.createElement("div");
        $(filterDiv).addClass("dropdown-filter");

        var filterInput = document.createElement("input");
        $(filterInput).addClass("dropdown-filter-input");
        filterInput.placeholder = "Filtrovat podle názvu..";

        $(filterInput).keyup(function () {
            $(this).change();
        });

        $(filterInput).change(function () {
            if ($(this).val() == '') {
                $(this).parents(".dropdown-select-body").children(".concrete-item").show();
            } else {
                $(this).parents(".dropdown-select-body").children(".concrete-item").hide().filter(':contains(' + $(this).val() + ')').show();
            }
        });

        filterDiv.appendChild(filterInput);

        var filterClearSpan = document.createElement("span");
        $(filterClearSpan).addClass("dropdown-clear-filter glyphicon glyphicon glyphicon-remove-circle");

        $(filterClearSpan).click(function () {
            $(this).siblings(".dropdown-filter-input").val('').change();
        });

        filterDiv.appendChild(filterClearSpan);

        dropDownBodyDiv.appendChild(filterDiv);

        //TODO load cascades of childrens
        this.makeItem(dropDownBodyDiv, "slovn9k asaa", false); //TODO read from parameter
        this.makeItem(dropDownBodyDiv, "slovn9k asbb", true); //TODO read from parameter
        this.makeItem(dropDownBodyDiv, "slovn9k ccc", false); //TODO read from parameter

        dropDownDiv.appendChild(dropDownBodyDiv);
    };

    DropDownSelect.prototype.makeItem = function (dropDownBodyDiv, name, isLeaf) {
        var itemDiv = document.createElement("div");
        $(itemDiv).addClass("concrete-item"); //TODO add data-item-id, data-item-name, data-item-type, data-item-is-favorite

        var checkbox = document.createElement("input");
        $(checkbox).addClass("concrete-item-checkbox checkbox");
        checkbox.type = "checkbox";

        $(checkbox).click(function () {
            //TODO add item to search criteria
        });

        itemDiv.appendChild(checkbox);

        if (!isLeaf) {
            var moreSpan = document.createElement("span");
            $(moreSpan).addClass("concrete-item-more");

            $(moreSpan).click(function () {
                var children = $(this).closest(".concrete-item").children(".child-items");
                if (children.is(":hidden")) {
                    $(this).children().removeClass("glyphicon-chevron-down");
                    $(this).children().addClass("glyphicon-chevron-up");
                    children.slideDown();
                } else {
                    $(this).children().removeClass("glyphicon-chevron-up");
                    $(this).children().addClass("glyphicon-chevron-down");
                    children.slideUp();
                }
            });

            var iconSpan = document.createElement("span");
            $(iconSpan).addClass("glyphicon glyphicon-chevron-down");

            moreSpan.appendChild(iconSpan);
            itemDiv.appendChild(moreSpan);
        }

        if (this.showStar) {
            var saveStarSpan = document.createElement("span");
            $(saveStarSpan).addClass("save-item glyphicon glyphicon-star-empty");

            $(saveStarSpan).click(function () {
                $(this).siblings(".delete-item").show();
                $(this).hide();
                //TODO populate request on save to favorites
            });

            itemDiv.appendChild(saveStarSpan);

            var deleteStarSpan = document.createElement("span");
            $(deleteStarSpan).addClass("delete-item glyphicon glyphicon-star");

            $(deleteStarSpan).click(function () {
                $(this).siblings(".save-item").show();
                $(this).hide();
                //TODO populate request on delete from favorites
            });

            itemDiv.appendChild(deleteStarSpan);
        }

        var nameSpan = document.createElement("span");
        $(nameSpan).addClass("concrete-item-name");
        nameSpan.innerText = name;

        itemDiv.appendChild(nameSpan);

        if (!isLeaf) {
            var childrenDiv = document.createElement("div");
            $(childrenDiv).addClass("child-items");

            this.makeItem(childrenDiv, "option next", true);
            if (name !== "option next 2  oijonicvsj9shvg9nhs vmi9d90d90d90rvjegm0j0vgj") {
                this.makeItem(childrenDiv, "option next 2  oijonicvsj9shvg9nhs vmi9d90d90d90rvjegm0j0vgj", false);
            }

            itemDiv.appendChild(childrenDiv);
        }

        dropDownBodyDiv.appendChild(itemDiv);
    };
    return DropDownSelect;
})();
//# sourceMappingURL=itjakub.plugins.dropdownselect.js.map
