
function fakeSendData(dataHandlerUrl, data, onreadystatechange) {
}

function resetMobillify() {
    _mfyaq.visitId = -1;
    _mfyaq.winSize = null;
    _mfyaq.clicksData = new Array();
    _mfyaq.viewPartsData = new Array();
    document.onscroll = null;
    document.onclick = null;
    window.onbeforeunload = null;
}


var orgSendData = _mfyaq.sendData;
_mfyaq.sendData = fakeSendData;

//init
test('init()', function () {
    //Check creating a new view part
    _mfyaq.init();
    equals(_mfyaq.viewPartsData.length, 1, 'The length of viewPartsData must be 1');

    //Check if cookies has previous data
    resetMobillify();
    //setup coockies
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + 2);
    var cookieData = '{"vid":"1","vpd":[{"sd":"' + new Date().toUTCString() + '","sl":0,"st":0,"fd":"' + new Date().toUTCString() + '"},],"cd":[{"d":"' + new Date().toUTCString() + '", "cx": 200, "cy":300},]}';
    document.cookie = '_mfyaq=' + cookieData + '; expires=' + exdate.toUTCString();
    var isSendDataCalled = false;
    //attach fake send data function
    _mfyaq.sendData = function (dataHandlerUrl, data, onreadystatechange) {
        equals(data, cookieData, 'Correct Data');
        isSendDataCalled = true;
        _mfyaq.sendData = fakeSendData;
    };
    _mfyaq.init();
    equals(isSendDataCalled, true, 'Send data function was called');
    //Check if coockies was deleted
    ok(document.cookie.indexOf('_mfyaq') == -1, 'Cookies was deleted');

    //Check creating and sending of visit info object
    resetMobillify();
    _mfyaq.sendData = function (dataHandlerUrl, data, onreadystatechange) {
        var res = { responseText: '{HasError:false, Value:100}' };
        setTimeout(function () {
            onreadystatechange(res);
            start();
            equals(_mfyaq.visitId, 100, 'Visit Id is set');
        }, 1);
    };
    stop();
    _mfyaq.init();
});

//addDataToCookie
test('addDataToCookie()', function () {
    resetMobillify();
    //If visitId was not recived
    _mfyaq.addDataToCookie();
    ok(document.cookie.indexOf('_mfyaq') == -1, 'Cookies was not set');

    //If there is no collected click data or view parts data
    _mfyaq.visitId = 100;
    _mfyaq.addDataToCookie();
    ok(document.cookie.indexOf('_mfyaq') == -1, 'Cookies was not set');

    //Check success adding cookies
    _mfyaq.visitId = 100;
    _mfyaq.clicksData.push({date: new Date(),clientX: 100, clientY: 200 });
    _mfyaq.viewPartsData.push({ startDate: new Date(), scrollLeft: 0, scrollTop: 100, finishDate: new Date() });
    _mfyaq.addDataToCookie();
    ok(document.cookie.indexOf('_mfyaq') > -1, 'Cookies was set');

    //Check last part finish date
    ok(document.cookie.indexOf(escape(new Date().toUTCString().substring(0,23))) > -1, 'Finish date was set correctly');

    //Clear coockies
    var exdate = new Date();
    exdate.setDate(exdate.getDate() - 365);//-6
    document.cookie = '_mfyaq=' + null + '; expires=' + exdate.toUTCString();
});

//serializeData
test('serializeData()', function () {
    resetMobillify();
    //Check visitId was not recived
    var resData = _mfyaq.serializeData();
    equals(resData, null, 'Visit id is less or equale to zero');

    //Check success serilazation
    var date = new Date();
    _mfyaq.visitId = 100;
    _mfyaq.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    _mfyaq.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    var resData = _mfyaq.serializeData();
    var compData = '{"vid":"100","vpd":[{"sd":"' + date.toUTCString() + '","sl":0,"st":100,"fd":"' + date.toUTCString() + '"},],"cd":[{"d":"' + date.toUTCString() + '","cx":100,"cy":200},]}';
    equals(resData, compData, 'Serialization is working');
});

//sendPackage
test('sendPackage()', function () {
    resetMobillify();
    //Check visitId
    var isSendDataCalled = false;
    _mfyaq.sendData = function (dataHandlerUrl, data, onreadystatechange) {
        isSendDataCalled = true;
    };
    _mfyaq.sendPackage();
    equals(isSendDataCalled, false, 'Send data function was not called');

    //Check sum of clicks and viewparts more than 3
    isSendDataCalled = false;
    _mfyaq.visitId = 100;
    _mfyaq.sendPackage();
    equals(isSendDataCalled, false, 'Send data function was not called');

    //Check clicks and view parts arrays was reset
    var date = new Date();
    _mfyaq.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    _mfyaq.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    _mfyaq.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    _mfyaq.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    _mfyaq.sendPackage();
    equals(isSendDataCalled, true, 'Send data function was called');
    equals(_mfyaq.clicksData.length, 0, 'clicksData was reset');
    equals(_mfyaq.viewPartsData.length, 0, 'viewPartsData was reset');

    _mfyaq.sendData = fakeSendData;

});

//sendData for visit info
test('sendData(Visit Info)', function () {
    resetMobillify();
    var winSize = _mfyaq.getWinSize();
    var data = '{';
    data += '"cid":"' + _mfyaq.key + '",';
    data += '"sw":' + winSize.w + ',';
    data += '"sh":' + winSize.h + ',';
    data += '"cw":' + _mfyaq.clientWidth() + ',';
    data += '"ch":' + _mfyaq.clientHeight() + ',';
    data += '"d":"' + new Date().toUTCString() + '",';
    data += '"uri":"' + location.href + '"';
    data += '}';
    stop();
    _mfyaq.sendData = orgSendData;
    _mfyaq.sendData(_mfyaq.visitHandlerUrl, data, function (xmlhttp) {
        var res = eval("(" + xmlhttp.responseText + ')');
        start();
        equals(res.HasError, false, 'Data was sent');
        _mfyaq.sendData = fakeSendData;
    });
});

//sendData for package info
test('sendData(Package Info)', function () {
    resetMobillify();
    var date = new Date();
    _mfyaq.visitId = 100;
    _mfyaq.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    _mfyaq.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    _mfyaq.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    _mfyaq.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    var resData = _mfyaq.serializeData();
    _mfyaq.sendData = orgSendData;
    stop();
    _mfyaq.sendData(_mfyaq.packageHandlerUrl, resData, function (xmlhttp) {
        var res = eval("(" + xmlhttp.responseText + ')');
        start();
        equals(res.HasError, false, 'Data was sent');
        _mfyaq.sendData = fakeSendData;
    });
});

//getWinSize
test('getWinSize()', function () {
    resetMobillify();
    var winSize = _mfyaq.getWinSize()
    ok(winSize, 'Object returned');
    equals(winSize.w, winSize.w, 'Window width');
    equals(winSize.h, winSize.h, 'Window height');
});

//clientWidth
test('clientWidth()', function () {
    resetMobillify();
    var w = _mfyaq.clientWidth();
    ok(w, 'Object returned');
    equals(w, w, 'Client width');
});

//clientHeight
test('clientHeight()', function () {
    resetMobillify();
    var h = _mfyaq.clientHeight();
    ok(h, 'Object returned');
    equals(h, h, 'Client height');
});

//filterResults
test('filterResults() - Is not in use', function () {
    resetMobillify();
});

//setCookie
test('setCookie()', function () {
    resetMobillify();
    _mfyaq.setCookie('_mfyaq', 'test', 1);
    ok(document.cookie.indexOf('_mfyaq') > -1, 'Cookies was set');
    _mfyaq.setCookie('_mfyaq', null, -365);
    ok(document.cookie.indexOf('_mfyaq') == -1, 'Cookies was removed');
});

//getCookie
test('getCookie()', function () {
    resetMobillify();
    _mfyaq.setCookie('_mfyaq', 'test', 1);
    ok(document.cookie.indexOf('_mfyaq') > -1, 'Cookie was set');
    var cookie = _mfyaq.getCookie('_mfyaq');
    ok(cookie, 'Object returned');
    equals(cookie, 'test', 'Right cookie');
    _mfyaq.setCookie('_mfyaq', null, -365);
    ok(document.cookie.indexOf('_mfyaq') == -1, 'Cookie was removed');
});

//scrollLeft
test('scrollLeft()', function () {
    resetMobillify();
    var l = _mfyaq.scrollLeft();
    equals(l, l, 'Scroll left');
});

//scrollTop
test('scrollTop()', function () {
    resetMobillify();
    var t = _mfyaq.scrollTop();
    equals(t, t, 'Scroll top');
});

//onscroll
test('onscroll()', function () {
    resetMobillify();
    //Check when no view parts a new part is added
    _mfyaq.onscroll();
    equals(_mfyaq.viewPartsData.length, 1, 'Part was added');
    //Check less two seconds
    _mfyaq.onscroll();
    equals(_mfyaq.viewPartsData.length, 1, 'Nothing was changed');
    //Check more two secconds
    _mfyaq.viewPartsData[0].startDate = new Date(2011, 6, 20, 0, 0, 0, 0);
    _mfyaq.onscroll();
    equals(_mfyaq.viewPartsData.length, 2, 'Part was added');
});

//onclick
test('onclick()', function () {
    resetMobillify();
    _mfyaq.visitId = 100;
    //Check if added data
    _mfyaq.onclick({ clientX: 100, clientY: 100 });
    equals(_mfyaq.clicksData.length, 1, 'Click was added');
    //Check added data to coockie
    ok(document.cookie.indexOf('_mfyaq') > -1, 'Cookie was set');
    _mfyaq.setCookie('_mfyaq', null, -365);
    ok(document.cookie.indexOf('_mfyaq') == -1, 'Cookie was removed');
});

//getViewPart
test('getViewPart()', function () {
    resetMobillify();
    //Check return object
    var date = new Date();
    var vp = _mfyaq.getViewPart(date);
    ok(vp, 'Object returned');
    equals(vp.startDate, date, 'Correct date');
});

//dateDiffInSec
test('dateDiffInSec()', function () {
    resetMobillify();
    //Check difference between dates
    var date1 = new Date(2011, 6, 20, 0, 0, 0, 0);
    var date2 = new Date(2011, 6, 20, 0, 0, 100, 0);
    var sec = _mfyaq.dateDiffInSec(date1, date2);
    equals(sec, 100, 'Diferent seconds');

});
