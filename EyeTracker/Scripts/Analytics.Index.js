
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
        var id = $(this).parent().attr('itemid');
        window.location.href = dashboardUrl.replace('{0}', type.Portfolio).replace('{1}', id);
    });
    $('tr.sub tr').click(function () {
        var id = $(this).attr('itemid');
        window.location.href = dashboardUrl.replace('{0}', type.Application).replace('{1}', id); ;
    });
});