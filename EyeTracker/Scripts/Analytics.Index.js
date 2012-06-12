
$(document).ready(function () {
    $('div.expand').click(function () {
        var li = $(this).parent().parent();
        if (!li.hasClass('disabled')) {
            var sub = $('.portfolio-' + li.attr('pid'));
            if (sub.is(':visible')) {
                sub.hide();
                li.removeClass('expanded');
            } else {
                sub.show();
                li.addClass('expanded');
            }
        }
    });
    $('.table ul li.portfolio > div.nav, .table ul li.app > div.nav').click(function () {
        var li = $(this).parent();
        if (!li.hasClass('disabled')) {
            window.location.href = li.attr('url');
        }
    });
});