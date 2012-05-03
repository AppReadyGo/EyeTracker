
$(document).ready(function () {
    $('div.expand').click(function () {
        if (!$(this).hasClass('disabled')) {
            var li = $(this).parent().parent();
            var sub = $('.portfolio-'+li.attr('pid'));
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
        window.location.href = $(this).parent().attr('url');
    });
});