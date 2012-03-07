
function onSelect(selectedDate) {
    var option = this.id == "date_from" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
    $('#date_from_lnk,#datepicker_from').not(this).datepicker("option", option, date);
    if (this.id == "date_from") {
        $('#date_from_lnk').text(selectedDate);
        $('#date_from').val(selectedDate);
    } else {
        $('#date_to_lnk').text(selectedDate);
        $('#date_to').val(selectedDate);
    }
}

function beforeShow(input, inst) {
    var pos = $('#date_from_lnk').offset();
    $.datepicker._pos = $.datepicker._findPos(input);
    $.datepicker._pos[0] = pos.left;
    $.datepicker._pos[1] = pos.top + $('#date_from_lnk').height() + 10;
}

$(function () {
    $("#datepicker_from").datepicker({
        defaultDate: analytics.dateFrom,
        dateFormat: 'dd M yy',
        onSelect: onSelect,
        beforeShow: beforeShow,
        maxDate: analytics.dateFromMax,
        minDate: analytics.dateFromMin
    });
    $("#datepicker_to").datepicker({
        defaultDate: analytics.dateTo,
        dateFormat: 'dd M yy',
        onSelect: onSelect,
        beforeShow: beforeShow,
        maxDate: analytics.dateToMax,
        minDate: analytics.dateToMin
    });
    $('#apply_btn').click(function () {
        var from = $("#datepicker_from").datepicker("getDate");
        var to = $("#datepicker_to").datepicker("getDate");
        $('#date_btn').text($.datepicker.formatDate('dd M yy', from) + ' - ' + $.datepicker.formatDate('dd M yy', to));
        $('#date_panel').hide();
        document.location.href = '/Analytics/' + analytics.type + '/Dashboard/' + analytics.id + '/' + $.datepicker.formatDate('dd-M-yy', from) + '/' + $.datepicker.formatDate('dd-M-yy', to)
    });
});
