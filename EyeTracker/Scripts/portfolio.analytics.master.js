

$(function () {
    var dates = $("#date_from, #date_to").datepicker({
        defaultDate: portfolio.dateFrom,
        dateFormat: 'dd M yy',
        onSelect: function (selectedDate) {
            var option = this.id == "date_from" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
            if (this.id == "date_from") {
                $('#date_from_lnk').text(selectedDate);
                $('#date_from').val(selectedDate);
            } else {
                $('#date_to_lnk').text(selectedDate);
                $('#date_to').val(selectedDate);
            }
        },
        beforeShow: function (input, inst) {
            var pos = $('#date_from_lnk').offset();
            $.datepicker._pos = $.datepicker._findPos(input);
            $.datepicker._pos[0] = pos.left;
            $.datepicker._pos[1] = pos.top + $('#date_from_lnk').height() + 10;
        }
    });
    $("#date_from").datepicker("option", "maxDate", portfolio.dateFromMax);
    $("#date_to").datepicker("option", "minDate", portfolio.dateFromMin);
    $('#date_from_lnk').click(function () {
        $('#date_from').datepicker('show');
    });
    $('#date_to_lnk').click(function () {
        $('#date_to').datepicker('show');
    });
});
