// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {
    var form = $('#search-form')[0];

    if (form == undefined) {
        return;
    }

    form.reset();
});