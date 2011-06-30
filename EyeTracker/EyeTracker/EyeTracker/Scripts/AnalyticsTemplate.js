var _mfyaq = _mfyaq || {};

_mfyaq.visitHandlerUrl = '{VISIT_HANDLER_URL}',
_mfyaq.packageHandlerUrl = '{PACKAGE_HANDLER_URL}',
_mfyaq.visitId = -1,
_mfyaq.winSize = null,
_mfyaq.clicksData = new Array(),
_mfyaq.viewPartsData = new Array(),
_mfyaq.init = function () {
        //Create a new view part object
        _mfyaq.viewPartsData.push(_mfyaq.getViewPart(new Date()));
        //Attach events
        document.onscroll = _mfyaq.onscroll;
        document.onclick = _mfyaq.onclick;
        window.onbeforeunload = function () {
            _mfyaq.addDataToCookie();
        }
        //Check if cookies has previous view data
        data = _mfyaq.getCookie('_mfyaq');
        if (data) {
            //Send cookies data to server
            _mfyaq.sendData(_mfyaq.packageHandlerUrl, data);
            _mfyaq.setCookie('_mfyaq', null, -365);
        }
        //Create and send to server visit object
        _mfyaq.winSize = _mfyaq.getWinSize();
        var data = '{';
        data += '"cid":"' + _mfyaq.key + '",';
        data += '"sw":' + _mfyaq.winSize.w + ',';
        data += '"sh":' + _mfyaq.winSize.h + ',';
        data += '"cw":' + _mfyaq.clientWidth() + ',';
        data += '"ch":' + _mfyaq.clientHeight() + ',';
        data += '"d":"' + new Date().toUTCString() + '",';
        data += '"uri":"' + location.href + '"';
        data += '}';
        _mfyaq.sendData(_mfyaq.visitHandlerUrl, data, function (xmlhttp) {
            var res = eval("(" + xmlhttp.responseText + ')');
            if (!res.HasError) {
                _mfyaq.visitId = res.Value;
            }
        });
    };
_mfyaq.addDataToCookie = function () {
        if (_mfyaq.visitId > 0 && (_mfyaq.clicksData.length > 0 || _mfyaq.viewPartsData.length > 0)) {
            if (_mfyaq.viewPartsData.length > 0) {
                var lastPart = _mfyaq.viewPartsData[_mfyaq.viewPartsData.length - 1];
                lastPart.finishDate = new Date();
            }
            _mfyaq.setCookie('_mfyaq', _mfyaq.serializeData(), 30);
        }
    };
_mfyaq.serializeData = function () {
        var data = null;
        if (_mfyaq.visitId > 0) {
            data = '{"vid":"' + _mfyaq.visitId + '"';
            data += ',"vpd":[';
            for (var i = 0; i < _mfyaq.viewPartsData.length; i++) {
                var curPart = _mfyaq.viewPartsData[i];
                data += '{"sd":"' + curPart.startDate.toUTCString() + '"';
                data += ',"sl":' + curPart.scrollLeft;
                data += ',"st":' + curPart.scrollTop;
                data += ',"fd":"' + curPart.finishDate.toUTCString() + '"';
                data += '},';
            }
            data += '],"cd":[';
            for (var i = 0; i < _mfyaq.clicksData.length; i++) {
                var curClick = _mfyaq.clicksData[i];
                data += '{"d":"' + curClick.date.toUTCString() + '"';
                data += ',"cx":' + curClick.clientX;
                data += ',"cy":' + curClick.clientY;
                data += '},';
            }
            data += ']}';
        }
        return data;
    };
_mfyaq.sendPackage = function () {
        if (_mfyaq.visitId > 0 && (_mfyaq.clicksData.length + _mfyaq.viewPartsData.length > 3)) {
            var data = _mfyaq.serializeData();
            _mfyaq.viewPartsData = new Array();
            _mfyaq.clicksData = new Array();
            _mfyaq.sendData(_mfyaq.packageHandlerUrl, data);
        }
    };
_mfyaq.sendData = function (dataHandlerUrl, data, onreadystatechange) {
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

        //data = 'json=' + data;
        xmlhttp.open("POST", dataHandlerUrl, true);
        xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xmlhttp.setRequestHeader("Content-length", data.length);
        xmlhttp.setRequestHeader("Connection", "close");
        xmlhttp.send(data);

    };
_mfyaq.getWinSize = function () {
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
    };
_mfyaq.clientWidth = function () {
        var db = document.body;
        var dde = document.documentElement;

        return Math.max(db ? db.scrollWidth : 0, dde.scrollWidth, db ? db.offsetWidth : 0, dde.offsetWidth, db ? db.clientWidth : 0, dde.clientWidth)
        //        return _mfyaq.filterResults(
        //                    window.innerWidth ? window.innerWidth : 0,
        //                    document.documentElement ? document.documentElement.clientWidth : 0,
        //                    document.body ? document.body.clientWidth : 0
        //                );
    };
_mfyaq.clientHeight = function () {
        //TODO:Remove it
        if (_mfyaq.Key == 3) {
            return 837;
        }

        var db = document.body;
        var dde = document.documentElement;

        return Math.max(db ? db.scrollHeight : 0, dde.scrollHeight, db ? db.offsetHeight : 0, dde.offsetHeight, db ? db.clientHeight : 0, dde.clientHeight)
        //        return _mfyaq.filterResults(
        //                    window.innerHeight ? window.innerHeight : 0,
        //                    document.documentElement ? document.documentElement.clientHeight : 0,
        //                    document.body ? document.body.clientHeight : 0
        //                );
    };
_mfyaq.filterResults = function (n_win, n_docel, n_body) {
        var n_result = n_win ? n_win : 0;
        if (n_docel && (!n_result || (n_result > n_docel)))
            n_result = n_docel;
        return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
    };
_mfyaq.setCookie = function (c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    };
_mfyaq.getCookie = function (c_name) {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    };
_mfyaq.scrollLeft = function () {
        return _mfyaq.filterResults(
        window.pageXOffset ? window.pageXOffset : 0,
        document.documentElement ? document.documentElement.scrollLeft : 0,
        document.body ? document.body.scrollLeft : 0
        );
    };
_mfyaq.scrollTop = function () {
        return _mfyaq.filterResults(
        window.pageYOffset ? window.pageYOffset : 0,
        document.documentElement ? document.documentElement.scrollTop : 0,
        document.body ? document.body.scrollTop : 0
        );
    };
_mfyaq.onscroll = function () {
        var curDate = new Date();
        if (_mfyaq.viewPartsData.length == 0) {
            _mfyaq.viewPartsData.push(_mfyaq.getViewPart(new Date()));
        }
        var lastPart = _mfyaq.viewPartsData[_mfyaq.viewPartsData.length - 1];
        var diff = _mfyaq.dateDiffInSec(curDate, lastPart.startDate)
        if (diff > 2) {
            lastPart.finishDate = curDate;
            _mfyaq.sendPackage();
            _mfyaq.viewPartsData.push(_mfyaq.getViewPart(curDate));
        } else {
            lastPart.scrollLeft = _mfyaq.scrollLeft();
            lastPart.scrollTop = _mfyaq.scrollTop();
        }
    };
_mfyaq.onclick = function (e) {
        _mfyaq.clicksData.push({
            date: new Date(),
            clientX: e.clientX,
            clientY: e.clientY
        });
        _mfyaq.addDataToCookie();
        _mfyaq.sendPackage();
    };
_mfyaq.getViewPart = function (curDate) {
        return {
            startDate: curDate,
            scrollLeft: _mfyaq.scrollLeft(),
            scrollTop: _mfyaq.scrollTop(),
            finishDate: new Date()
        };
    };
_mfyaq.dateDiffInSec = function (date1, date2) {
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
    };

_mfyaq.init();
