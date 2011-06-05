var mobillify = {
    visitHandlerUrl: '{VISIT_HANDLER_URL}',
    packageHandlerUrl: '{PACKAGE_HANDLER_URL}',
    clientId: '{CLIENT_ID}',
    visitId: -1,
    winSize: null,
    clicksData: new Array(),
    viewPartsData: new Array(),
    init: function () {
        mobillify.viewPartsData.push(mobillify.getViewPart(new Date()));

        document.onscroll = mobillify.onscroll;
        document.onclick = mobillify.onclick;
        window.onbeforeunload = function () {
            mobillify.addDataToCookie();
        }
        data = mobillify.getCookie('mobillifyData');
        if (data) {
            mobillify.sendData(mobillify.packageHandlerUrl, data);
            mobillify.setCookie('mobillifyData', null, -365);
        }

        mobillify.winSize = mobillify.getWinSize();
        var data = '{';
        data += '"cid":"' + mobillify.clientId + '",';
        data += '"sw":' + mobillify.winSize.w + ',';
        data += '"sh":' + mobillify.winSize.h + ',';
        data += '"cw":' + mobillify.clientWidth() + ',';
        data += '"ch":' + mobillify.clientHeight() + ',';
        data += '"d":"' + new Date().toUTCString() + '",';
        data += '"uri":"' + location.href + '"';
        data += '}';
        mobillify.sendData(mobillify.visitHandlerUrl, data, function (xmlhttp) {
            var res = eval("(" + xmlhttp.responseText + ')');
            if (!res.WasError) {
                mobillify.visitId = res.Value;
            } else {
                alert(xmlhttp.responseText);
            }
        });
    },
    addDataToCookie: function () {
        if (mobillify.visitId > 0 && (mobillify.clicksData.length > 0 || mobillify.viewPartsData.length > 0)) {
            var lastPart = mobillify.viewPartsData[mobillify.viewPartsData.length - 1];
            lastPart.finishDate = new Date();
            mobillify.setCookie('mobillifyData', mobillify.serializeData(), 30);
        }
    },
    serializeData: function () {
        var data = '{"vid":"' + mobillify.visitId + '"';
        data += ',"vpd":[';
        for (var i = 0; i < mobillify.viewPartsData.length; i++) {
            var curPart = mobillify.viewPartsData[i];
            data += '{"sd":"' + curPart.startDate.toUTCString() + '"';
            data += ',"sl":' + curPart.scrollLeft;
            data += ',"st":' + curPart.scrollTop;
            data += ',"fd":"' + curPart.finishDate.toUTCString() + '"';
            data += '},';
        }
        data += '],"cd":[';
        for (var i = 0; i < mobillify.clicksData.length; i++) {
            var curClick = mobillify.clicksData[i];
            data += '{"d":"' + curClick.date.toUTCString() + '"';
            data += ',"cx":' + curClick.clientX;
            data += ',"cy":' + curClick.clientY;
            data += '},';
        }
        data += ']}';
        return data;
    },
    sendPackage: function () {
        if (mobillify.visitId > 0 && (mobillify.clicksData.length + mobillify.viewPartsData.length > 3)) {
            var data = mobillify.serializeData();
            mobillify.viewPartsData = new Array();
            mobillify.clicksData = new Array();
            mobillify.sendData(mobillify.packageHandlerUrl, data, function (xmlhttp) {
                var res = eval("(" + xmlhttp.responseText + ')');
                if (res.WasError) {
                    alert(xmlhttp.responseText);
                }
            });
            mobillify.clicksData = new Array();
            mobillify.viewPartsData = new Array();
        }
    },
    sendData: function (dataHandlerUrl, data, onreadystatechange) {
        var xmlhttp;
        if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
            xmlhttp = new XMLHttpRequest();
        }
        else {// code for IE6, IE5
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && (xmlhttp.status == 200 || xmlhttp.status == 0)) {
                if (onreadystatechange) onreadystatechange(xmlhttp);
            }
        }

        data = 'json=' + data;
        xmlhttp.open("POST", dataHandlerUrl, true);
        xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xmlhttp.setRequestHeader("Content-length", data.length);
        xmlhttp.setRequestHeader("Connection", "close");
        xmlhttp.send(data);

    },
    getWinSize: function () {
        var winW = 0, winH = 0;
        if (document.body && document.body.offsetWidth) {
            winW = document.body.offsetWidth;
            winH = document.body.offsetHeight;
        }
        if (document.compatMode == 'CSS1Compat' &&
        document.documentElement &&
        document.documentElement.offsetWidth) {
            winW = document.documentElement.offsetWidth;
            winH = document.documentElement.offsetHeight;
        }
        if (window.innerWidth && window.innerHeight) {
            winW = window.innerWidth;
            winH = window.innerHeight;
        }
        return { w: winW, h: winH };
    },
    clientWidth: function () {
        var db = document.body;
        var dde = document.documentElement;

        return Math.max(db.scrollWidth, dde.scrollWidth, db.offsetWidth, dde.offsetWidth, db.clientWidth, dde.clientWidth)
        //        return mobillify.filterResults(
        //                    window.innerWidth ? window.innerWidth : 0,
        //                    document.documentElement ? document.documentElement.clientWidth : 0,
        //                    document.body ? document.body.clientWidth : 0
        //                );
    },
    clientHeight: function () {
        var db = document.body;
        var dde = document.documentElement;

        return Math.max(db.scrollHeight, dde.scrollHeight, db.offsetHeight, dde.offsetHeight, db.clientHeight, dde.clientHeight)
        //        return mobillify.filterResults(
        //                    window.innerHeight ? window.innerHeight : 0,
        //                    document.documentElement ? document.documentElement.clientHeight : 0,
        //                    document.body ? document.body.clientHeight : 0
        //                );
    },
    filterResults: function (n_win, n_docel, n_body) {
        var n_result = n_win ? n_win : 0;
        if (n_docel && (!n_result || (n_result > n_docel)))
            n_result = n_docel;
        return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
    },
    setCookie: function (c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    },
    getCookie: function (c_name) {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    },
    scrollLeft: function () {
        return mobillify.filterResults(
        window.pageXOffset ? window.pageXOffset : 0,
        document.documentElement ? document.documentElement.scrollLeft : 0,
        document.body ? document.body.scrollLeft : 0
        );
    },
    scrollTop: function () {
        return mobillify.filterResults(
        window.pageYOffset ? window.pageYOffset : 0,
        document.documentElement ? document.documentElement.scrollTop : 0,
        document.body ? document.body.scrollTop : 0
        );
    },
    onscroll: function () {
        var curDate = new Date();
        if (mobillify.viewPartsData.length == 0) {
            mobillify.viewPartsData.push(mobillify.getViewPart(new Date()));
        }
        var lastPart = mobillify.viewPartsData[mobillify.viewPartsData.length - 1];
        var diff = mobillify.dateDiffInSec(curDate, lastPart.startDate)
        if (diff > 2) {
            lastPart.finishDate = curDate;
            mobillify.sendPackage();
            mobillify.viewPartsData.push(mobillify.getViewPart(curDate));
        } else {
            lastPart.scrollLeft = mobillify.scrollLeft();
            lastPart.scrollTop = mobillify.scrollTop();
        }
    },
    onclick: function (e) {
        mobillify.clicksData.push({
            date: new Date(),
            clientX: e.clientX,
            clientY: e.clientY
        });
        mobillify.addDataToCookie();
        mobillify.sendPackage();
    },
    getViewPart: function (curDate) {
        return {
            startDate: curDate,
            scrollLeft: mobillify.scrollLeft(),
            scrollTop: mobillify.scrollTop(),
            finishDate: new Date()
        };
    },
    dateDiffInSec: function (date1, date2) {
        var diff = new Date();
        diff.setTime(Math.abs(date1.getTime() - date2.getTime()));

        var timediff = diff.getTime();
        /*
        var weeks = Math.floor(timediff / (1000 * 60 * 60 * 24 * 7));
        timediff -= weeks * (1000 * 60 * 60 * 24 * 7);
 
        var days = Math.floor(timediff / (1000 * 60 * 60 * 24));
        timediff -= days * (1000 * 60 * 60 * 24);
 
        var hours = Math.floor(timediff / (1000 * 60 * 60));
        timediff -= hours * (1000 * 60 * 60);
 
        var mins = Math.floor(timediff / (1000 * 60));
        timediff -= mins * (1000 * 60);
        */
        var secs = Math.floor(timediff / 1000);
        timediff -= secs * 1000;
        return secs;
    }
};
mobillify.init();
