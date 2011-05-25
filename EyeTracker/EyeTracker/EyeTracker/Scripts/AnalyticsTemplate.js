var mobillify = {
    dataHandlerUrl: '{HANDLER_URL}',
    clientId: '{CLIENT_ID}',
    clicksData: new Array(),
    viewPartsData: new Array(),
    init: function () {
        mobillify.viewPartsData.push(mobillify.getViewPart(new Date()));

        document.onscroll = mobillify.onscroll;
        document.onclick = mobillify.onclick;
        window.onbeforeunload = function () {
            if (mobillify.clicksData.length > 0 || mobillify.viewPartsData.length > 0) {
                mobillify.setCookie('mobillifyData', mobillify.serializeData(), 30);
            }
        }

        var winSize = mobillify.getWinSize();
        var data = '{';
        data += '"cid":"' + mobillify.clientId + '",';
        data += '"sw":' + winSize.w + ',';
        data += '"sh":' + winSize.h + ',';
        data += '"cw":' + mobillify.clientWidth() + ',';
        data += '"ch":' + mobillify.clientHeight();
        data += '}';
        mobillify.sendData('init', data);
        data = mobillify.getCookie('mobillifyData');
        if (data) {
            mobillify.sendData('pack', data);
            mobillify.setCookie('mobillifyData', null, -365);
        }
    },
    serializeData: function () {
        var data = '{"cid":"' + mobillify.clientId + '"';
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
        if (mobillify.clicksData.length + mobillify.viewPartsData.length > 10) {
            mobillify.sendData('pack', mobillify.serializeData(), function () { mobillify.viewPartsData = new Array(); mobillify.clicksData = new Array(); });
        }
    },
    sendData: function (action, data, onreadystatechange) {
        var xmlhttp;
        //Put data to cookies
        //setCookie('cachedData', data, 1);

        if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
            xmlhttp = new XMLHttpRequest();
        }
        else {// code for IE6, IE5
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
        xmlhttp.onreadystatechange = onreadystatechange;
        /*function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
        //setCookie('cachedData', null, -365);
        package.mouseEvents = new Array();
        package.parts = new Array();
        }
        }*/
        xmlhttp.open("GET", mobillify.dataHandlerUrl + "?a=" + action + "&d=" + data, true);
        xmlhttp.send();
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
        return mobillify.filterResults(
                    window.innerWidth ? window.innerWidth : 0,
                    document.documentElement ? document.documentElement.clientWidth : 0,
                    document.body ? document.body.clientWidth : 0
                );
    },
    clientHeight: function () {
        return mobillify.filterResults(
                    window.innerHeight ? window.innerHeight : 0,
                    document.documentElement ? document.documentElement.clientHeight : 0,
                    document.body ? document.body.clientHeight : 0
                );
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
        var lastPart = mobillify.viewPartsData.length > 0 ? mobillify.viewPartsData[mobillify.viewPartsData.length - 1] : mobillify.getViewPart(new Date());
        if ((curDate.getSeconds() - lastPart.startDate.getSeconds()) > 2) {
            lastPart.finishDate = curDate;
            mobillify.viewPartsData.push(mobillify.getViewPart(curDate));
            mobillify.sendPackage();
        }
    },
    onclick: function (e) {
        mobillify.clicksData.push({
            date: new Date(),
            clientX: e.clientX,
            clientY: e.clientY
        });
        mobillify.sendPackage();
    },
    getViewPart: function (curDate) {
        return {
            startDate: curDate,
            scrollLeft: mobillify.scrollLeft(),
            scrollTop: mobillify.scrollTop(),
            finishDate: new Date()
        };
    }
};
mobillify.init();
