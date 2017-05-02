function getWebserviceStr(url, data, type) {
    var result = null;
    var callback = function (resultXml) {
        //alert('11' + resultXml);
        if (resultXml["Message"]) {
            result = resultXml["Message"];
        }
    };
    execute_xml(url, callback, data, type, false);
    return result;
}

function execute_xml(url, callback, data, type, async) {
    if (!type) type = "POST";
    if (async == undefined || async == null) async = true;
    $.ajax({
        type: type,
        async: async,
        cache: false,
        url: url,
        data: data,
        dataType: "json",
        success: callback,
        error: function (result, status, a) {
            alert(status + "ERROR: " + result.responseText);
        }
    });
}

function getWeekOfMonth(date_val, weekStart) {
    weekStart = (weekStart || 0) - 0;
    if (isNaN(weekStart) || weekStart > 6)
        weekStart = 0;

    var dayOfWeek = date_val.getDay();
    var day = date_val.getDate();
    return Math.ceil((day - dayOfWeek - 1) / 7) + ((dayOfWeek >= weekStart) ? 1 : 0);
}

function getCookeByName(cName) {
    var cStr = "";
    var c_start, c_end;
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(cName + "=")
        if (c_start != -1) {
            c_start = c_start + cName.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1)
                c_end = document.cookie.length
            cStr = unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return cStr;
}

function setCookieByFrequence(cName, cValue) {
    var cStr = getCookeByName(cName);
    if (cStr == "")
        cStr = cValue;
    else
        cStr = cValue + "|" + cStr;

    cStr = (cStr + "|").replace("|" + cValue + "|", "");
    var arr = cStr.split('|');
    var j, newStr;
    newStr = "";
    j = 1;
    for (var i = 0; i < arr.length; i++) {
        if (j > 10) break;
        if (arr[i] != "") {
            if (newStr == "")
                newStr += arr[i];
            else
                newStr += "|" + arr[i]
            j++;
        }
    }
    var dt = new Date();
    dt.setTime(dt.getTime() + 10 * 24 * 3600 * 1000);
    document.cookie = cName + "=" + escape(newStr) + ";expires=" + dt.toGMTString();
}

function checkDateStringFormat(RQ) {
    var date = RQ;
    var result = date.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);

    if (result == null)
        return false;
    var d = new Date(result[1], result[3] - 1, result[4]);
    return (d.getFullYear() == result[1] && (d.getMonth() + 1) == result[3] && d.getDate() == result[4]);

}

function checkTimeStringFormat(SJ) {
    var a = SJ.match(/^(\d{1,2})(:)?(\d{1,2})$/);
    if (a == null) { return false; }
    if (a[1] > 24 || a[3] > 60) {
        return false
    }
    return true;
}

function sleep(n) {
    var now = new Date();
    var exitTime = now.getTime() + n;
    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}


/** 
 * @说明 对Date的扩展，将 Date 转化为指定格式的String， 
 * 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，  * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)   *  
 * @param fmt  * @author LWZ  * @returns  */
Date.prototype.Format2String = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, // 月份   
        "d+": this.getDate(), // 日   
        "h+": this.getHours(), // 小时   
        "m+": this.getMinutes(), // 分   
        "s+": this.getSeconds(), // 秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), // 季度   
        "S": this.getMilliseconds()// 毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}




