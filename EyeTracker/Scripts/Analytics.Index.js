
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
    $('.table ul li.portfolio > div.nav').click(function () {
        var parent = $(this).parent();
        var pid = parent.attr('pid');
        window.location.href = dashboardUrl.replace('{0}', pid).replace('{1}', '');
    });
    $('.table ul li.app > div.nav').click(function () {
        var parent = $(this);
        var pid = parent.attr('pid');
        var aid = parent.attr('aid');
        window.location.href = dashboardUrl.replace('{0}', pid).replace('{1}', aid);
    });
});