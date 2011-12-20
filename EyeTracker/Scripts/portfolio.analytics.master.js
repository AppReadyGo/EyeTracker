
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
    $('#date_btn').click(function () {
        $('#date_panel').show();
    });
    $("#datepicker_from").datepicker({
        defaultDate: portfolio.dateFrom,
        dateFormat: 'dd M yy',
        onSelect: onSelect,
        beforeShow: beforeShow,
        maxDate: portfolio.dateFromMax,
        minDate: portfolio.dateFromMin
    });
    $("#datepicker_to").datepicker({
        defaultDate: portfolio.dateTo,
        dateFormat: 'dd M yy',
        onSelect: onSelect,
        beforeShow: beforeShow,
        maxDate: portfolio.dateToMax,
        minDate: portfolio.dateToMin
    });
    $('#apply_btn').click(function () {
        var from = $("#datepicker_from").datepicker("getDate");
        var to = $("#datepicker_to").datepicker("getDate");
        $('#date_btn').text($.datepicker.formatDate('dd M yy', from) + ' - ' + $.datepicker.formatDate('dd M yy', to));
        $('#date_panel').hide();
    });
});
