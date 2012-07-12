

$(document).ready(function () {
    $('#user-search-input').keypress(function (e) {
        if (e.which == 13) {
            $('#search-submit').click();
        }
    });
});

