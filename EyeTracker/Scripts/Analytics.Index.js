
$(document).ready(function () {

    $('td.expand').click(function () {
        var sub = $(this).parent().next();
        if (sub.is(':visible')) {
            sub.hide();
            $(this).removeClass('expanded');
        } else {
            sub.show();
            $(this).addClass('expanded');
        }
    });
    $('tr.main td:not(.expand)').click(function () {
        var parent = $(this).parent();
        var pid = parent.attr('pid');
        window.location.href = dashboardUrl.replace('{0}', pid).replace('{1}', '');
    });
    $('tr.sub tr').click(function () {
        var parent = $(this);
        var pid = parent.attr('pid');
        var aid = parent.attr('aid');
        window.location.href = dashboardUrl.replace('{0}', pid).replace('{1}', aid);
    });
});