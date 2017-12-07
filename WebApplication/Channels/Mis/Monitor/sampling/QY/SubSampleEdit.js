var groupicon = "../../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strSampleId = "", strSubSampleId = "", strActionDate = "", strSampleCode = "";
$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    }
});
$(document).ready(function () {
    strSubSampleId = request('strSubSampleId');
    strSampleId = request('strSampleId');
    strActionDate = request('strActionDate');
    strSampleCode = request('strSampleCode');



    //创建表单结构 --点位基本信息
    $("#divEdit").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 50, labelAlign: 'right',
        fields: [
                 { display: "采样时间", name: "ACTIONDATE", newline: true, type: "date", options: { initValue: TogetDate(new Date()), showTime: true }, group: "基本信息", groupicon: groupicon },
                 { display: "样品编号", name: "SUBSAMPLE_NAME", newline: false, type: "text" }

                ]
    });
//    $("#ACTIONDATE").ligerDateEditor({ initValue: TogetDate(new Date()) ,showTime: true});
    if (strSubSampleId) {
        $("#SUBSAMPLE_NAME").val(strSampleCode);
        $("#ACTIONDATE").val(TogetDate(new Date(Date.parse(strActionDate))))
    }
})

//得到标准保存参数
function gerRequestStr() {
    var strData = "";
    strData += "&strSampleId=" + strSampleId;
    if (strSubSampleId) {
        strData += "&strSubSampleId=" + strSubSampleId;
    }
    strData += "&strSampleCode=" + $("#SUBSAMPLE_NAME").val();
    strData += "&strActionDate=" + $("#ACTIONDATE").val();
    return strData;
}

function TogetDate(date) {
    var strD = "";
    var thisYear = date.getYear();
    var thisHour = date.getHours();
    if (thisHour < 10) thisHour = "0" + thisHour;
    var thisMinu = date.getMinutes();
    if (thisMinu < 10) thisMinu = "0" + thisMinu;
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate();
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        strD = thisYear + "-" + thisMonth + "-" + thisDay + " " + thisHour + ":" + thisMinu;
    }
    return strD;
}