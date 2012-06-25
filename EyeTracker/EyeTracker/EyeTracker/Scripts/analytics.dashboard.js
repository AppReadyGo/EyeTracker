
$(document).ready(function () {
    var placeholder = $("#usage_charts_place_holder");
    var plot = $.plot(placeholder, usageChartData, {
        xaxis: { mode: "time", timeformat: '%d %b %y' },
        yaxis: { min: 0, tickDecimals: 0 },
        series: {
            lines: { show: true },
            points: { show: true }
        },
        grid: { hoverable: true, clickable: true }
    });
});