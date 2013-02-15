
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
    $('#SelectedScreenSize, #SelectedPath').selectBoxIt({ theme: "jqueryui" });
    $('#SelectedScreenSize, #SelectedPath').change(function () { $('#advanced_filter_apply').button("option", "disabled", false); });
    $('#date_range_btn').button();
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
});

$(document).ready(function () {
    function updateFilter(source, target) {
        var sOptions = source.find('option');
        var tOptions = target.find('option');
        for (var i = 0; i < sOptions.length; i++) {
            tOptions[i].selected = sOptions[i].selected;
        }
        target.multiselect('refresh');
    }
    function advancedFilteApply() {
        var from = $("#datepicker_from").datepicker("getDate");
        var to = $("#datepicker_to").datepicker("getDate");
        //var portfolios = $('#SelectedPortfolioId').val();
        //var applications = $('#SelectedApplicationId').val();
        var screenSizes = $('#SelectedScreenSize').val();
        var paths = $('#SelectedPath').val();
        var url = '/Analytics/' + analytics.action +
                  '/?aid=' + analytics.aid +
                  '&fd=' + $.datepicker.formatDate('dd-M-yy', from) +
                  '&td=' + $.datepicker.formatDate('dd-M-yy', to);
        if (screenSizes) url += '&ss=' + screenSizes;
        if (paths) url += '&p=' + paths;

        document.location.href = url;

        // Not need for update will refresh page
        //updateFilter($('#advanced_portfolios,#advanced_applications'), $('#portfolios,#applications'));
        //$('#advanced_filter').hide();
        //$('#portfolios,#applications').multiselect('enable');
    }

    $('#advanced_filter_apply').button({ disabled: true });
    $('#date_range_pnl .actions a').button();

    $('#advanced_filter_btn').button({
        icons: { primary: 'ui-icon-plus' },
        text: false
    });

    // --- Data range buttons
    $('#date_range_btn').click(function () {
        $('#date_range_pnl').show();
    });

    $('#date_range_apply').click(function () {
        $('#date_range_btn').html('&nbsp;&nbsp;' + $('#date_from').val() + ' - ' + $('#date_to').val() + '&nbsp;&nbsp;');
        $('#date_range_pnl').hide();
        $('#advanced_filter_apply').button("option", "disabled", false);
    });

    $('#date_range_cancel').click(function () {
        $('#date_range_pnl').hide();
    });

    // --- advanced filter buttons
    $('#advanced_filter_btn').click(function () {
        $('#date_range_pnl').hide();
    });
    $('#advanced_filter_apply').click(advancedFilteApply);


    $('#SelectedPortfolioId').change(function () {
        var id = $(this).val();
        $('#SelectedApplicationId').empty();
        $('#SelectedApplicationId').append('<option value="0">All</option>');
        var data = analytics.pData[id];
        for (var i = 0; i < data.length; i++) {
            $('#SelectedApplicationId').append('<option value="' + data[i].id + '">' + data[i].desc + '</option>');
        }
    });

    $('#SelectedApplicationId').change(function () {
        var id = $(this).val();
        $('#SelectedScreenSize').empty();
        $('#SelectedScreenSize').append('<option value="">All</option>');
        $('#SelectedPath').empty();
        $('#SelectedPath').append('<option value="">All</option>');
        if (id != '0') {
            var data = analytics.aData[id];
            for (var i = 0; i < data.scr.length; i++) {
                $('#SelectedScreenSize').append('<option value="' + data.scr[i] + '">' + data.scr[i] + '</option>');
            }
            for (var i = 0; i < data.pth.length; i++) {
                $('#SelectedPath').append('<option value="' + data.pth[i] + '">' + data.pth[i] + '</option>');
            }
        }
    });
});

