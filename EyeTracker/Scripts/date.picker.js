
jQuery.fn.datePicker = function (options) {
    options = options || {};
    options.date = options.date || new Date();
    options.min = options.min || new Date();
    options.max = options.max || new Date();
    options.popup = options.popup || true;
    options.firstDayOfWeek = options.firstDayOfWeek || 'Mo'; //'Su','Mo'
    options.weekDays = options.weekDays || ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'];
    options.monthNames = options.monthNames || ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    options.onChangeDate = options.onChangeDate || function (date) { };
    options.onShow = options.onShow || function (picker) { };
    options.dateFormat = options.dateFormat || 'dd/MM/yyyy';

    var content = $('<div class="date-picker"' +
                    (options.popup ? 'style="display:none;"' : '') +
                    '>' +
                    '<div><a class="left"><</a><a class="zoom-out-btn"></a><a class="right">></a></div>' +
                    '<div class="wrapper"></div></div>');
    var dayHtml = '    <table class="date">' +
                    '        <tr class="week"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>' +
                    '    </table>';
    var monthHtml = '    <table class="month">' +
                    '        <tr><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td></tr>' +
                    '        <tr><td></td><td></td><td></td><td></td></tr>' +
                    '    </table>';

    var weekIdx = options.firstDayOfWeek == 'Su' ? 0 : 1;


    function exchangeFade(container, elem1, elem2) {
        elem2.hide();
        container.append(elem2);
        elem1.fadeOut(function () { elem1.remove(); });
        elem2.fadeIn();
    }
    function exchangeSlideRight(container, elem1, elem2) {
        elem1.css({ left: 0 });
        elem2.hide();
        container.append(elem2);
        elem2.css({ left: (elem2.width() + 30) });
        elem2.show();
        elem2.animate({ left: 0 });
        elem1.animate({ left: -(elem1.width() + 30) }, function () {
            elem1.remove();
        });
    }
    function exchangeSlideLeft(container, elem1, elem2) {
        elem1.css({ left: 0 });
        elem2.hide();
        container.append(elem2);
        elem2.css({ left: -(elem2.width() + 30) });
        elem2.show();
        elem2.animate({ left: 0 });
        elem1.animate({ left: (elem1.width()) }, function () {
            elem1.remove();
        });
    }

    function animate(container, elem1, elem2, animType) {
        if (animType == 0) {
            if (elem1) elem1.remove();
            container.append(elem2);
        } else if (animType == 1) {
            exchangeFade(container, elem1, elem2);
        } else if (animType == 2) {
            exchangeSlideLeft(container, elem1, elem2);
        } else if (animType == 3) {
            exchangeSlideRight(container, elem1, elem2);
        }
    }

    function populateDays(input, zoomBtn, curContent, date, animType) {
        var firstDay = new Date(date.getTime());
        firstDay.setDate(1);
        var curWeekDay = firstDay.getDay();
        //move first day to next row
        curWeekDay = curWeekDay == 0 ? 7 : (curWeekDay == 1 ? 8 : curWeekDay);
        firstDay.setDate(-(curWeekDay - 2));

        var curItemIdx = 0;
        var content = $(dayHtml);
        $('td', content).each(function () {
            if (curItemIdx < 7) {
                $(this).text(options.weekDays[weekIdx]);
                weekIdx = (weekIdx + 1) % options.weekDays.length;
            } else {
                var curDay = firstDay.getDate();
                $(this).html('<a day="' + curDay + '">' + curDay + '</a>');
                if (curDay == date.getDate() && firstDay.getMonth() == date.getMonth()) {
                    $(this).addClass('selected');
                }
                if (firstDay.getFullYear() < date.getFullYear() || (firstDay.getFullYear() == date.getFullYear() && firstDay.getMonth() < date.getMonth())) {
                    $(this).addClass('other');
                    $(this).addClass('prev');
                } else if (firstDay.getFullYear() > date.getFullYear() || (firstDay.getFullYear() == date.getFullYear() && firstDay.getMonth() > date.getMonth())) {
                    $(this).addClass('other');
                    $(this).addClass('next');
                }
                firstDay.setDate(firstDay.getDate() + 1);
            }
            curItemIdx++;
        });
        zoomBtn.text(options.monthNames[date.getMonth()] + ' ' + date.getFullYear());
        var wrapper = $('.wrapper', curContent);
        var table = $(wrapper.children()[0]);
        animate(wrapper, table, content, animType);
        $('a', content).click(function () {
            var newDate = new Date(date.getTime());
            var td = $(this).parent();
            if (td.hasClass('prev')) {
                newDate.setMonth(newDate.getMonth() - 1);
                newDate.setDate($(this).attr('day') * 1);
                curContent.attr('date', newDate.getTime());
                populateDays(input, zoomBtn, curContent, newDate, 2/*Left*/);
            } else if (td.hasClass('next')) {
                newDate.setMonth(newDate.getMonth() + 1);
                newDate.setDate($(this).attr('day') * 1);
                curContent.attr('date', newDate.getTime());
                populateDays(input, zoomBtn, curContent, newDate, 3/*right*/);
            } else {
                newDate.setDate($(this).attr('day') * 1);
                input.val(newDate.format(options.dateFormat));
                curContent.attr('date', newDate.getTime());
                options.onChangeDate(newDate);
                if (options.popup) content.parent().parent().hide();
            }
        });
    }

    function populateMonths(input, zoomBtn, curContent, date, animType) {
        var curMonth = 0;
        var content = $(monthHtml);
        $('td', content).each(function () {
            var mName = options.monthNames[curMonth];
            $(this).html('<a month="' + curMonth + '">' + mName.substring(0, 3) + '</a>');
            if (curMonth == date.getMonth()) {
                $(this).addClass('selected');
            }
            curMonth++;
        });
        zoomBtn.text(date.getFullYear());
        var wrapper = $('.wrapper', curContent);
        var table = $(wrapper.children()[0]);
        animate(wrapper, table, content, animType);
        $('a', content).click(function () {
            var month = $(this).attr('month');
            var newDate = new Date(date.getFullYear(), month, date.getDate());
            if (newDate.getMonth() != month) {
                newDate.setMonth(month);
            }
            if (newDate.getFullYear() != date.getFullYear()) {
                newDate.setYear(date.getFullYear());
            }
            curContent.attr('date', newDate.getTime());
            populateDays(input, zoomBtn, curContent, newDate, 1);
            curContent.attr('mode', 0);
        });
    }
    function populateYears(input, zoomBtn, curContent, date, animType) {
        var firstYear = (parseInt(date.getFullYear() / 10) * 10);
        var curYear = firstYear - 1;
        var content = $(monthHtml);
        $('td', content).each(function () {
            $(this).html('<a year="' + curYear + '">' + curYear + '</a>');
            if (curYear < firstYear) {
                $(this).addClass('other');
                $(this).addClass('prev');
            } else if (curYear > (firstYear + 9)) {
                $(this).addClass('other');
                $(this).addClass('next');
            }
            if (curYear == date.getFullYear()) {
                $(this).addClass('selected');
            }
            curYear++;
        });
        var fromYear = parseInt(date.getFullYear() / 100) * 100;
        zoomBtn.text(firstYear + '-' + (firstYear + 9));
        var wrapper = $('.wrapper', curContent);
        var table = $(wrapper.children()[0]);
        animate(wrapper, table, content, animType);
        $('a', content).click(function () {
            var td = $(this).parent();
            var year = $(this).attr('year');
            var newDate = new Date(year, date.getMonth(), date.getDate());
            if (newDate.getFullYear() != year) {
                newDate.setYear(year);
            }
            curContent.attr('date', newDate.getTime());
            if (td.hasClass('prev')) {
                populateYears(input, zoomBtn, curContent, newDate, 2/*Left*/);
            } else if (td.hasClass('next')) {
                populateYears(input, zoomBtn, curContent, newDate, 3/*Right*/);
            } else {
                populateMonths(input, zoomBtn, curContent, newDate, 1);
                curContent.attr('mode', 1);
            }
        });
    }
    function populateRangeYears(input, zoomBtn, curContent, date, animType) {
        var firstYear = parseInt(date.getFullYear() / 100) * 100;
        var curYear = firstYear - 10;
        var content = $(monthHtml);
        $('td', content).each(function () {
            $(this).html('<a year="' + curYear + '">' + curYear + '-<br/>' + (curYear + 9) + '</a>');
            if (curYear < firstYear) {
                $(this).addClass('other');
                $(this).addClass('prev');
            } else if (curYear >= (firstYear + 100)) {
                $(this).addClass('other');
                $(this).addClass('next');
            }
            if (curYear <= date.getFullYear() && (curYear + 9) > date.getFullYear()) {
                $(this).addClass('selected');
            }
            curYear += 10;
        });
        zoomBtn.text(firstYear + '-' + (firstYear + 99));
        var wrapper = $('.wrapper', curContent);
        var table = $(wrapper.children()[0]);
        animate(wrapper, table, content, animType);
        $('a', content).click(function () {
            var td = $(this).parent();
            var year = $(this).attr('year');
            var newDate = new Date(year, date.getMonth(), date.getDate());
            if (newDate.getFullYear() != year) {
                newDate.setYear(year);
            }
            curContent.attr('date', newDate.getTime());
            if (td.hasClass('prev')) {
                populateRangeYears(input, zoomBtn, curContent, newDate, 2/*Left*/);
            } else if (td.hasClass('next')) {
                populateRangeYears(input, zoomBtn, curContent, newDate, 3/*Right*/);
            } else {
                populateYears(input, zoomBtn, curContent, newDate, 1);
                curContent.attr('mode', 2);
            }
        });
    }

    function reset(input, zoomBtn, curContent) {
        var date = options.date || new Date(input.val());
        curContent.attr('date', date.getTime());
        curContent.attr('mode', 0);
        populateDays(input, zoomBtn, curContent, date, 0);
    }

    $(this).each(function () {
        var input = $(this);
        var date = options.date || new Date(input.val());
        //var date = new Date(options.date.getTime());
        var curContent = content.clone();
        curContent.attr('mode', 0); //Days
        curContent.attr('date', date.getTime());
        var zoomBtn = $('.zoom-out-btn', curContent)
        populateDays(input, zoomBtn, curContent, date, 0);
        input.after(curContent);
        zoomBtn.click(function () {
            var mode = curContent.attr('mode');
            var date = new Date(curContent.attr('date') * 1);
            if (mode == 0) {//Days
                populateMonths(input, zoomBtn, curContent, date, 1);
                curContent.attr('mode', 1);
            } else if (mode == 1) {//Months
                populateYears(input, zoomBtn, curContent, date, 1);
                curContent.attr('mode', 2);
            } else if (mode == 2) {//Years
                populateRangeYears(input, zoomBtn, curContent, date, 1);
                curContent.attr('mode', 3);
            }
        });
        $('.left', curContent).click(function () {
            var date = new Date(curContent.attr('date') * 1);
            var mode = curContent.attr('mode');
            if (mode == 0) {//Days
                date.setMonth(date.getMonth() - 1);
                curContent.attr('date', date.getTime());
                populateDays(input, zoomBtn, curContent, date, 2/*Left*/);
            } else if (mode == 1) {//Months
                date.setFullYear(date.getFullYear() - 1);
                curContent.attr('date', date.getTime());
                populateMonths(input, zoomBtn, curContent, date, 2/*Left*/);
            } else if (mode == 2) {//Years
                date.setFullYear(date.getFullYear() - 9);
                curContent.attr('date', date.getTime());
                populateYears(input, zoomBtn, curContent, date, 2/*Left*/);
            } else if (mode == 3) {//Range Years
                date.setFullYear(date.getFullYear() - 100);
                curContent.attr('date', date.getTime());
                populateRangeYears(input, zoomBtn, curContent, date, 2/*Right*/);
            }
        });
        $('.right', curContent).click(function () {
            var date = new Date(curContent.attr('date') * 1);
            var mode = curContent.attr('mode');
            if (mode == 0) {//Days
                date.setMonth(date.getMonth() + 1);
                curContent.attr('date', date.getTime());
                populateDays(input, zoomBtn, curContent, date, 3/*Right*/);
            } else if (mode == 1) {//Months
                date.setFullYear(date.getFullYear() + 1);
                curContent.attr('date', date.getTime());
                populateMonths(input, zoomBtn, curContent, date, 3/*Right*/);
            } else if (mode == 2) {//Years
                date.setFullYear(date.getFullYear() + 9);
                curContent.attr('date', date.getTime());
                populateYears(input, zoomBtn, curContent, date, 3/*Right*/);
            } else if (mode == 3) {//Range Years
                date.setFullYear(date.getFullYear() + 100);
                curContent.attr('date', date.getTime());
                populateRangeYears(input, zoomBtn, curContent, date, 3/*Right*/);
            }
        });
        input.click(function () {
            curContent.toggle();
            reset(input, zoomBtn, curContent)
            if (options.onShow) options.onShow(curContent);
        });
        curContent.bind("mouseleave", function () {
            curContent.hide();
            reset(input, zoomBtn, curContent)
        });
    });

};

