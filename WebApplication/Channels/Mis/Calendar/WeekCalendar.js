var currDT, NowDt, WeekDt, tdDT, draDataDt, lastDate;
var aryDay = new Array("日", "一", "二", "三", "四", "五", "六");
var tr = "";
var vWeekDate = null;
//初始页面


$(document).ready(function () {
    WeekLoadShow(new Date(), new Date());
})
function WeekLoadShow(srrcurrDt, srrNewDt) {
    //初始化页面
    currDT = srrcurrDt;
    NowDt = srrcurrDt;
    var tab = $('#tb1');       //获取table的对象
    tab.find("tr").each(function () {
        $(this).remove();
    })
    Weekini();
    //绑定按钮事件
    BtnWeekClickEvn();
    function Weekini() {
        showweekDate();
    }
}
function Weekini() {
        showweekDate();
    }
    //上一周 或 下一周
    function addWeek(ope) {
        var num = 0;
        if (ope == "-") {
            num = -7;
        }
        else if (ope == "+") {
            num = 7;
        }
        currDT = addDate(currDT, num);
        showweekDate();
    }
    function showweekDate() {
        var dw = currDT.getDay();
        //确定周一是那天
        if (dw == 0) {
            tdDT = addDate(currDT, -6);
            WeekDt = addDate(currDT, -6);
            draDataDt = addDate(currDT, -6);
            lastDate = addDate(currDT, -6);
        }
        else {
            tdDT = addDate(currDT, (1 - dw));
            WeekDt = addDate(currDT, (1 - dw));
            draDataDt = addDate(currDT, (1 - dw));
            lastDate = addDate(currDT, (1 - dw));
        }
        lastDate.setDate(lastDate.getDate() + 6);
        var strweekDt = TogetDateForWeek(new Date( WeekDt),true) + "~"+ TogetDateForWeek(lastDate,true);
        tr = '<tr align="center" style="height:30px; " >';
        tr += '<td style="width:50px"><input type="button"  id="PreWeek" value="上周" class="l-button l-button-submit"/></td>';
        tr += '<td colspan="5"  style="width:300px;font-weight:bold;font-size:15px">' + strweekDt + '</td>';
        tr += '<td style="width:50px"><input   type="button"  id="NextWeek" value="下周" class="l-button l-button-submit" /></td>';

        tr += '</tr>';
        $(tr).appendTo(tb1);
        //在表格中显示一周的日期
        tr = '<tr align="center" style="height:50px; background-color:#F4F9FC"  >';
        for (var i = 0; i < aryDay.length; i++) {
            dw = tdDT.getDay();
            if (tdDT.toLocaleDateString() == currDT.toLocaleDateString() && currDT.toLocaleDateString() == NowDt.toLocaleDateString()) {
                tr += '<td  style="width:100px; font-weight:bold;font-size:14px" ><font color="ff0000">' + (tdDT.getMonth() + 1) + "月" + tdDT.getDate() + "日 星期" + aryDay[dw] + '</font></td>';
            }
            else {
                tr += '<td  style="width:100px; font-weight:bold;font-size:14px" >' + (tdDT.getMonth() + 1) + "月" + tdDT.getDate() + "日 星期" + aryDay[dw] + '</td>';
            }
            tdDT = addDate(tdDT, 1);  //下一天
        }
        tr += '</tr>';
        $(tr).appendTo(tb1);

        //---------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------
        //此处加载业务数据
        tr = '<tr valign="top" align="left" style="height:600px;"  >';
        for (var i = 0; i < aryDay.length; i++) {
            var planDate = TogetDateForWeek(draDataDt, false);
            tr += '<td  style="width:100px;" >';
            GetWeekPlan(planDate);
            if (vWeekDate != null) {
                for (var n = 0; n < vWeekDate.length; n++) {
                    tr += '<br><div align="left">' + (n+ 1) + '、' + vWeekDate[n].REAL_NAME + '</br>' + vWeekDate[n].AREANAME +'<font color="Red">'+ vWeekDate[n].COMPANNUM + '</font>家企业<div>';
                }
                tr += "</td>";
            }

            draDataDt = addDate(draDataDt, 1);  //下一天
        }
        tr += '</tr>';
        $(tr).appendTo(tb1);
    }
    //---------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------------------
    function BtnWeekClickEvn() {
        $("#PreWeek").bind("click", function () {
            //移除原来生成的日期
            var tab = $('#tb1');       //获取table的对象
            tab.find("tr").each(function () {
                $(this).remove();
            })
            addWeek('-');
            BtnWeekClickEvn();
        })

        $("#NextWeek").bind("click", function () {
            //移除原来生成的日期
            var tab = $('#tb1');       //获取table的对象
            tab.find("tr").each(function () {
                $(this).remove();
            })
            addWeek('+');
            BtnWeekClickEvn();
        })
    }
    //增加或减少若干天，由 num 的正负决定，正为加，负为减
    function addDate(dt, num) {
        var ope = "+";
        if (num < 0) {
            ope = "-";
        }

        var reDT = dt;
        for (var i = 0; i < Math.abs(num); i++) {
            reDT = addOneDay(reDT, ope);
        }
        return reDT;
    }

    //增加或减少一天，由ope决定, + 为加，- 为减，否则不动
    function addOneDay(dt, ope) {
        var num = 0;
        if (ope == "-") {
            num = -1;
        }
        else if (ope == "+") {
            num = 1;
        }
        var y = dt.getYear();
        var m = dt.getMonth();
        var lastDay = getLastDay(y, m);
        var d = dt.getDate();
        d += num;
        if (d < 1) {
            m--;
            if (m < 0) {
                y--;
                m = 11;
            }
            d = getLastDay(y, m);
        }
        else if (d > lastDay) {
            m++;
            if (m > 11) {
                y++;
                m = 0;
            }
            d = 1;
        }

        var reDT = new Date();
        reDT.setYear(y);
        reDT.setMonth(m);
        reDT.setDate(d);
        return reDT;
    }

    //是否为闰年
    function isLeapYear(y) {
        var isLeap = false;
        if (y % 4 == 0 && y % 100 != 0 || y % 400 == 0) {
            isLeap = true;
        }
        return isLeap;
    }
    //将时间转换为字符串日期
    function TogetDateForWeek(date, formart) {
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

            if (formart) {
                strD = thisYear + "年" + thisMonth + "月" + thisDay + '日';
            }
            else {
                strD = thisYear + "-" + thisMonth + "-" + thisDay;
            }
        }
        return strD;
    }
    //每月最后一天
    function getLastDay(y, m) {
        var lastDay = 28;
        m++;
        if (m == 1 || m == 3 || m == 5 || m == 7 || m == 8 || m == 10 || m == 12) {
            lastDay = 31;
        }
        else if (m == 4 || m == 6 || m == 9 || m == 11) {
            lastDay = 30;
        }
        else if (isLeapYear(y) == true) {
            lastDay = 29;
        }
        return lastDay;
    }

    //以下是业务数据处理函数
    //--------------------------------------------------------------------
    //获取月度监测计划
    function GetWeekPlan(date) {
        vWeekDate = null;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetWeekPlan&strDate=" + date,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vWeekDate = data.Rows;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
    //--------------------------------------------------------------------