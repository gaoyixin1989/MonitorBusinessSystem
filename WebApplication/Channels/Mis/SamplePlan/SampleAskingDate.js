//创建原因：设置自送样 任务要求完成时间
//创建人：胡方扬
//创建时间：2013-07-02
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
var strDate =TogetDate(new Date());
var CONTRACT_TYPE = "", strWorkTask_id = "", Contact_Name = ""; PHONE = ""; TICKET_NUM = "";
$(document).ready(function () {
    strWorkTask_id = $.getUrlVar('strWorkTask_id');
    CONTRACT_TYPE = $.getUrlVar('CONTRACT_TYPE'); //委托类型
    TICKET_NUM = $.getUrlVar('TICKET_NUM'); //任务单号
    strDate = $.getUrlVar('ASKING_DATE'); //要求完成日期
    $("#txtDate").ligerDateEditor({ width: 200, initValue: strDate, onChangeDate: function (value) {
        strDate = $("#txtDate").val();
    }
    });
    //获取联系人和电话
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetPhoneInfo&strWorkTask_id=" + strWorkTask_id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                Contact_Name = data[0].Contact_Name; PHONE = data[0].link_phone; ;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    $("#Contact_Name").val(Contact_Name);
    var ASKING_DATE = strDate.split(' ')[0];
//    $("#txtDate").ligerDateEditor({ initValue: ASK });
    $("#txtDate").val(ASKING_DATE);
    $("#PHONE").val(PHONE);
    $("#txtTASK_CODE").val(TICKET_NUM);
    //任务单号查询
    $("#BtnSearch").click(function () {
        $.ligerDialog.open({ title: '任务单号查询', name: 'winaddtor', width: 320, height: 280, top: 10, url: '../MonitoringPlan/PendingDoTask_Search.aspx?CONTRACT_TYPE=' + CONTRACT_TYPE, buttons: [
                { text: '关闭', onclick: f_Cancel }
            ]
        });
    });

    function f_Cancel(item, dialog) {
        dialog.close();
    }

})
  
//获取当前日期 后7天的日期
function TogetDate(date) {
    var strD = "";
    var thisYear = date.getYear();
    thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
    var thisMonth = date.getMonth() + 1;
    //如果月份长度是一位则前面补0    
    if (thisMonth < 10) thisMonth = "0" + thisMonth;
    var thisDay = date.getDate() + 7;
    //如果天的长度是一位则前面补0    
    if (thisDay < 10) thisDay = "0" + thisDay;
    {

        strD = thisYear + "-" + thisMonth + "-" + thisDay;
    }
    return strD;
}

//获取要求完成时间
function GetFinishDate() {
    strDate = "";
    strDate = $("#txtDate").val();

    return strDate;
}

//得到任务单号
function getTASK_CODE() {

    var strData1 = $("#txtTASK_CODE").val();

    return strData1;
}

//得到联系人
function getContact_Name() {

    var strData1 = escape($("#Contact_Name").val());

    return strData1;
}

//得到联系电话
function getContact_PHONE() {

    var strData1 = $("#PHONE").val();

    return strData1;
}