var package = {
    clientId: '',
    startDate: new Date(),
    parts: new Array(),
    mouseEvents: new Array()
};
addPart();
document.onscroll = function () {
    var curDate = new Date();
    var lastPart = package.parts[package.parts.length - 1];
    if ((curDate.getSeconds() - lastPart.startDate.getSeconds()) > 2) {
        lastPart.finishDate = curDate;
        addPart();
    }
    show();
};
document.onclick = function (e) {
    addMouseEvent(e);
    show();
};
window.onunload = function () {
    //alert("Window is closed.");
};
window.onbeforeunload = function () {
    sendData();
}


function addPart() {
    var size = getWinSize();
    package.parts.push({
        startDate: new Date(),
        windowWidth: size.w,
        windowHeight: size.h,
        clientWidth: f_clientWidth(),
        clientHeight: f_clientHeight(),
        scrollLeft: f_scrollLeft(),
        scrollTop: f_scrollTop(),
        finishDate: new Date()
    });
}

function addMouseEvent(e) {
    package.mouseEvents.push({
        startDate: new Date(),
        clientX: e.clientX,
        clientY: e.clientY,
        screenX: e.screenX,
        screenY: e.screenY
    });
}

function prepareData() {
    var strData = '{p:[';
    for (var i = 0; i < package.parts.length; i++) {
        var curPart = package.parts[i];
        strData += '{sd:' + curPart.startDate;
        strData += ',ww:' + curPart.windowWidth;
        strData += ',wh:' + curPart.windowH;
        strData += ',cw:' + curPart.clientWidth;
        strData += ',ch:' + curPart.clientHeight;
        strData += ',sl:' + curPart.scrollLeft;
        strData += ',st:' + curPart.scrollTop;
        strData += ',fd:' + curPart.finishDate;
        strData += '},';
    }
    strData += '],m:[';
    for (var i = 0; i < package.mouseEvents.length; i++) {
        var curME = package.mouseEvents[i];
        strData += '{sd:' + curME.startDate;
        strData += ',cx:' + curME.clientX;
        strData += ',cy:' + curME.clientY;
        strData += ',sx:' + curME.screenX;
        strData += ',sy:' + curME.screenY;
        strData += '},';
    }
    strData += ']}';
    return strData;
}

function getWinSize() {
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
}

function show() {
    if (package.mouseEvents.length > 10 || package.parts.length > 10) {
        sendData();
    }
    var el = document.getElementById('output');
    el.innerHTML = 'Events = ' + package.mouseEvents.length + ' Parts = ' + package.parts.length;
}
document.writeln('<div id="output"></div>');
show();



function f_clientWidth() {
    return f_filterResults(
		window.innerWidth ? window.innerWidth : 0,
		document.documentElement ? document.documentElement.clientWidth : 0,
		document.body ? document.body.clientWidth : 0
	);
}
function f_clientHeight() {
    return f_filterResults(
		window.innerHeight ? window.innerHeight : 0,
		document.documentElement ? document.documentElement.clientHeight : 0,
		document.body ? document.body.clientHeight : 0
	);
}
function f_scrollLeft() {
    return f_filterResults(
		window.pageXOffset ? window.pageXOffset : 0,
		document.documentElement ? document.documentElement.scrollLeft : 0,
		document.body ? document.body.scrollLeft : 0
	);
}
function f_scrollTop() {
    return f_filterResults(
		window.pageYOffset ? window.pageYOffset : 0,
		document.documentElement ? document.documentElement.scrollTop : 0,
		document.body ? document.body.scrollTop : 0
	);
}
function f_filterResults(n_win, n_docel, n_body) {
    var n_result = n_win ? n_win : 0;
    if (n_docel && (!n_result || (n_result > n_docel)))
        n_result = n_docel;
    return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
}

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

function sendData() {
    var xmlhttp;
    if (package.parts.length == 0 && package.mouseEvents.length == 0) {
        return;
    }
    var data = prepareData();
    //Put data to cookies
    setCookie('cachedData', data, 1);

    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            setCookie('cachedData', null, -365);
            package.mouseEvents = new Array();
            package.parts = new Array();
        }
    }
    xmlhttp.open("GET", "gethint.asp?q=" + data, true);
    xmlhttp.send();
}
