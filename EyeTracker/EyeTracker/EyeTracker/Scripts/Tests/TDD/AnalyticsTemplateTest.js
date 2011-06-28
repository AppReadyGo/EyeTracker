
function fakeSendData(dataHandlerUrl, data, onreadystatechange) {
}

function resetMobillify() {
    mobillify.visitId = -1;
    mobillify.winSize = null;
    mobillify.clicksData = new Array();
    mobillify.viewPartsData = new Array();
    document.onscroll = null;
    document.onclick = null;
    window.onbeforeunload = null;
}


var orgSendData = mobillify.sendData;
mobillify.sendData = fakeSendData;

//init
test('init()', function () {
    //Check creating a new view part
    mobillify.init();
    equals(mobillify.viewPartsData.length, 1, 'The length of viewPartsData must be 1');

    //Check if cookies has previous data
    resetMobillify();
    //setup coockies
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + 2);
    var cookieData = '{"vid":"1","vpd":[{"sd":"' + new Date().toUTCString() + '","sl":0,"st":0,"fd":"' + new Date().toUTCString() + '"},],"cd":[{"d":"' + new Date().toUTCString() + '", "cx": 200, "cy":300},]}';
    document.cookie = 'mobillifyData=' + cookieData + '; expires=' + exdate.toUTCString();
    var isSendDataCalled = false;
    //attach fake send data function
    mobillify.sendData = function (dataHandlerUrl, data, onreadystatechange) {
        equals(data, cookieData, 'Correct Data');
        isSendDataCalled = true;
        mobillify.sendData = fakeSendData;
    };
    mobillify.init();
    equals(isSendDataCalled, true, 'Send data function was called');
    //Check if coockies was deleted
    ok(document.cookie.indexOf('mobillifyData') == -1, 'Cookies was deleted');

    //Check creating and sending of visit info object
    resetMobillify();
    mobillify.sendData = function (dataHandlerUrl, data, onreadystatechange) {
        var res = { responseText: '{WasError:false, Value:100}' };
        setTimeout(function () {
            onreadystatechange(res);
            start();
            equals(mobillify.visitId, 100, 'Visit Id is set');
        }, 1);
    };
    stop();
    mobillify.init();
});

//addDataToCookie
test('addDataToCookie()', function () {
    resetMobillify();
    //If visitId was not recived
    mobillify.addDataToCookie();
    ok(document.cookie.indexOf('mobillifyData') == -1, 'Cookies was not set');

    //If there is no collected click data or view parts data
    mobillify.visitId = 100;
    mobillify.addDataToCookie();
    ok(document.cookie.indexOf('mobillifyData') == -1, 'Cookies was not set');

    //Check success adding cookies
    mobillify.visitId = 100;
    mobillify.clicksData.push({date: new Date(),clientX: 100, clientY: 200 });
    mobillify.viewPartsData.push({ startDate: new Date(), scrollLeft: 0, scrollTop: 100, finishDate: new Date() });
    mobillify.addDataToCookie();
    ok(document.cookie.indexOf('mobillifyData') > -1, 'Cookies was set');

    //Check last part finish date
    ok(document.cookie.indexOf(escape(new Date().toUTCString().substring(0,23))) > -1, 'Finish date was set correctly');

    //Clear coockies
    var exdate = new Date();
    exdate.setDate(exdate.getDate() - 365);//-6
    document.cookie = 'mobillifyData=' + null + '; expires=' + exdate.toUTCString();
});

//serializeData
test('serializeData()', function () {
    resetMobillify();
    //Check visitId was not recived
    var resData = mobillify.serializeData();
    equals(resData, null, 'Visit id is less or equale to zero');

    //Check success serilazation
    var date = new Date();
    mobillify.visitId = 100;
    mobillify.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    mobillify.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    var resData = mobillify.serializeData();
    var compData = '{"vid":"100","vpd":[{"sd":"' + date.toUTCString() + '","sl":0,"st":100,"fd":"' + date.toUTCString() + '"},],"cd":[{"d":"' + date.toUTCString() + '","cx":100,"cy":200},]}';
    equals(resData, compData, 'Serialization is working');
});

//sendPackage
test('sendPackage()', function () {
    resetMobillify();
    //Check visitId
    var isSendDataCalled = false;
    mobillify.sendData = function (dataHandlerUrl, data, onreadystatechange) {
        isSendDataCalled = true;
    };
    mobillify.sendPackage();
    equals(isSendDataCalled, false, 'Send data function was not called');

    //Check sum of clicks and viewparts more than 3
    isSendDataCalled = false;
    mobillify.visitId = 100;
    mobillify.sendPackage();
    equals(isSendDataCalled, false, 'Send data function was not called');

    //Check clicks and view parts arrays was reset
    var date = new Date();
    mobillify.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    mobillify.clicksData.push({ date: date, clientX: 100, clientY: 200 });
    mobillify.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    mobillify.viewPartsData.push({ startDate: date, scrollLeft: 0, scrollTop: 100, finishDate: date });
    mobillify.sendPackage();
    equals(isSendDataCalled, true, 'Send data function was called');
    equals(mobillify.clicksData.length, 0, 'clicksData was reset');
    equals(mobillify.viewPartsData.length, 0, 'viewPartsData was reset');

    mobillify.sendData = fakeSendData;

});

//sendData for visit info
test('sendData(Visit Info)', function () {
    resetMobillify();
};

//sendData for package info
test('sendData(Package Info)', function () {
    resetMobillify();
};

//getWinSize
test('getWinSize()', function () {
    resetMobillify();
};


//clientWidth
//clientHeight
//filterResults
//setCookie
//getCookie
//scrollLeft
//scrollTop
//onscroll
//onclick
//getViewPart
//dateDiffInSec
