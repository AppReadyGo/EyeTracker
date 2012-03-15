
function onSelect(selectedDate) {
    $('#preset_range').val('custom');
    var option = this.id == "datepicker_from" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
    $('#date_from_lnk,#datepicker_from').not(this).datepicker("option", option, date);
    if (this.id == "datepicker_from") {
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
    $('#preset_range').change(function () {
        var val = $(this).val();
        var dFrom, dTo;
        if (val == 'custom') {
            return;
        }
        if (val == 'today') {
            dFrom = new Date();
            dTo = dFrom;
        } else if (val == 'yesterday') {
            dFrom = new Date();
            dFrom.setDate(dFrom.getDate() - 1);
            dTo = dFrom;
        } else if (val == 'lastweek') {
            dFrom = new Date();
            dFrom.setDate(dFrom.getDate() - 7);
            dTo = new Date();
        } else if (val == 'lastmonth') {
            dFrom = new Date();
            dFrom.setMonth(dFrom.getMonth() - 1);
            dTo = new Date();
        }

        var strFrom = $.datepicker.formatDate('dd M yy', dFrom);
        var strTo = $.datepicker.formatDate('dd M yy', dTo);
        $('#datepicker_from').datepicker("setDate", dFrom);
        $('#datepicker_to').datepicker("setDate", dTo);

        // $('#date_from_lnk').text(strFrom);
        $('#date_from').val(strFrom);
        // $('#date_to_lnk').text(strTo);
        $('#date_to').val(strTo);
    });
    $('#date_from').change(function () {
        var date = Date.parse($(this).val());
        if (isNaN(date)) {
            $(this).addClass("error");
        } else {
            $(this).removeClass("error");
        }
    });
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
        var portfolio = $('#portfolioId').val();
        var application = $('#applicationId').val();
        document.location.href = '/Analytics/' + analytics.action + '/' + portfolio + '/' + application + '/' + $.datepicker.formatDate('dd-M-yy', from) + '/' + $.datepicker.formatDate('dd-M-yy', to)
    });
    $('#cancel_btn').click(function () {
        $('#filter_body').hide();
    });
    $('#date_from_lnk,#date_to_lnk').click(function () {
        $('#filter_body').show();
    });
});
