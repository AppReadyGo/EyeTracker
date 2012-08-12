
$(document).ready(function () { 
    // Submit form
    $('input').keypress(function (e) {
        if (e.which == 13) {
            $(this).closest('form').submit();
        }
    });


    // Search
    $('input[type="search"]').keypress(function (e) {
        if (e.which == 13) {
            $(this).next().click();
        }
    });
});