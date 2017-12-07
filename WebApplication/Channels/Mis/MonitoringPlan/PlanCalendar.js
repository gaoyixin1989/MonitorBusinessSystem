var isDay = true, isWeek = false, isMonth = false, PageType = "", strDate = "";
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
PageType = $.getUrlVar("type");
strDate = $.getUrlVar("strDate");
$(document).ready(function () {
    $("#txtDate").ligerDateEditor({ initValue: getInitDate(), onChangeDate: function (value) {
        var date = new Date();
        date = new Date(value.replace(/-/g, '/'));
        $("#MonthDiv").attr("style", "display:none");
        $("#WeekDiv").attr("style", "display:none");
        LoadShow(date, date);
        $("#DayDiv").attr("style", "display:");
    }
    });
    function getInitDate() {
        var strinitDate = "";
        if (!strDate) {
            strinitDate = TogetDate(new Date());
        } else {
            strinitDate = strDate;
        }
        return strinitDate;
    }
    $("#DayDiv").attr("style", "display:");

    $("#btnMonth").bind("click", function () {
        isMonth = true;
        isWeek = false;
        isDay = false;
        MonthLoadShow();
        $("#MonthDiv").attr("style", "display:");
        $("#WeekDiv").attr("style", "display:none");
        $("#DayDiv").attr("style", "display:none");
    })

    $("#btnWeek").bind("click", function () {
        isMonth = false;
        isWeek = true;
        isDay = false;
        WeekLoadShow(new Date(), new Date());
        $("#MonthDiv").attr("style", "display:none");
        $("#WeekDiv").attr("style", "display:");
        $("#DayDiv").attr("style", "display:none");
    })

    $("#btnDay").bind("click", function () {
        isMonth = false;
        isWeek = false;
        isDay = true;
        LoadShow(new Date(), new Date());
        $("#MonthDiv").attr("style", "display:none");
        $("#WeekDiv").attr("style", "display:none");
        $("#DayDiv").attr("style", "display:");
    })

    $("#btnToday").bind("click", function () {

        if (isDay) {
            $("#txtDate").val(TogetDate(new Date()));
            LoadShow(new Date(), new Date());
            $("#MonthDiv").attr("style", "display:none");
            $("#WeekDiv").attr("style", "display:none");
            $("#DayDiv").attr("style", "display:");
        }
        if (isWeek) {
            $("#MonthDiv").attr("style", "display:none");
            $("#WeekDiv").attr("style", "display:");
            $("#DayDiv").attr("style", "display:none");
            WeekLoadShow(new Date(), new Date());
        }
        if (isMonth) {
            $("#MonthDiv").attr("style", "display:");
            $("#WeekDiv").attr("style", "display:none");
            $("#DayDiv").attr("style", "display:none");
            MonthLoadShow();
        }
    })
    function TogetDate(date) {
        var strD = "";
        var thisYear = date.getYear();
        thisYear = (thisYear < 1900) ? (1900 + thisYear) : thisYear;
        var thisMonth = date.getMonth() + 1;
        //如果月份长度是一位则前面补0    
        if (thisMonth < 10) thisMonth = "0" + thisMonth;
        var thisDay = date.getDate();
        //如果天的长度是一位则前面补0    
        if (thisDay < 10) thisDay = "0" + thisDay;
        {

            strD = thisYear + "-" + thisMonth + "-" + thisDay;
        }
        return strD;
    }
})

function GetMonitorInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vMonitorType = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

function GetMonitorDutyInfor() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorDutyInfor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vMonitorType = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}
function GetMointorName(strMointorIdAr) {
    var strMointorName = "";
    var strArr = strMointorIdAr.split(';');
    if (strArr != null) {
        for (var i = 0; i < strArr.length; i++) {
            for (var n = 0; n < vMonitorType.length; n++) {
                if (vMonitorType[n].ID == strArr[i]) {
                    strMointorName += vMonitorType[n].MONITOR_TYPE_NAME + ';';
                }
            }
        }

        return strMointorName.substring(0, strMointorName.length - 1);
    }

    return strMointorName;
}