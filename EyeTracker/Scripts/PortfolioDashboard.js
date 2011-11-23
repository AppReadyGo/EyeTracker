


$(document).ready(function () {

    var placeholder = $("#usage_charts_place_holder");
    var plot = $.plot(placeholder, usageChartData, {
        xaxis: { mode: "time", timeformat: '%d %b %y' },
        yaxis: { autoscaleMargin: 0.02/*, tickFormatter: currencyFormatter */},
        series: {
            lines: { show: true },
            points: { show: true }
        },
        grid: { hoverable: true, clickable: true }
    });
});