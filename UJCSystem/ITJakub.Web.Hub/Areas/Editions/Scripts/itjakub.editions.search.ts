﻿
$(document).ready(() => {

    var bookType = $("#listResults").data("book-type");
    var bibliographyModule = new BibliographyModule("#listResults", "#listResultsHeader", bookType);

    $('#searchButton').click(() => {
        var text = $('#searchbox').val();

        $.ajax({
            type: "GET",
            traditional: true,
            url: getBaseUrl() + "Editions/Editions/SearchEditions",
            data: { term: text },
            dataType: 'json',
            contentType: 'application/json',
            success: response => {
                bibliographyModule.showBooks(response.books);

            }
        });
    });

    var searchBox = new SearchBox("#searchbox", "Editions/Editions");
    searchBox.addDataSet("Title", "Názvy");
    searchBox.addDataSet("Author", "Autoři");
    searchBox.create();
});