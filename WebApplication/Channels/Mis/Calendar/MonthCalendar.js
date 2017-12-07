//Create By Castle 胡方扬 2012-12-14
//封装月份控件
var date = new Date();
var d = date.getDate();
var m = date.getMonth() + 1;
var y = date.getFullYear();
var _ArrayWeek = null, vMonthDate = null;
var CurrDateTemp = "", preDate = "", nextDate = "", CurrDate = "", CellDate = "", NowDate = "";
_ArrayWeek = ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'];
var tr = "", td = "";
var bolEndTable = false, bolStartTable = false, bolStopTable = false; //表格结束标志、日期开始标志、日期结束标志

NowDate = y + "-" + m + "-" + d; //当月
NowDate = new Date(NowDate.replace(/-/g, '/'));
$(document).ready(function () {
    MonthLoadShow();
})
    function SetDateValue() {
        preDate = CurrDateTemp;
        preDate = new Date(preDate.setMonth(preDate.getMonth() - 1)); //上一月
        nextDate = CurrDateTemp;
        nextDate = new Date(nextDate.setMonth(nextDate.getMonth() + 2));    //下一月

        CellDate = CurrDate; //每一格中的日期
    }
    function RunDate() {
        bolEndTable = false, bolStartTable = false, bolStopTable = false;
        var strData = new Date();
        y = strData.getFullYear();
        m = strData.getMonth() + 1;
        d = strData.getDate();
        CurrDateTemp = strData.getFullYear() + '-' + (strData.getMonth() + 1) + '-1';
        CurrDateTemp = new Date(CurrDateTemp.replace(/-/g, '/'));

        CurrDate = strData.getFullYear() + '-' + (strData.getMonth() + 1) + '-1';
        CurrDate = new Date(CurrDate.replace(/-/g, '/'));
    }

    function MonthLoadShow() {
        //初始化当月
        RunDate();

        //计算并生成本月日历
        //移除原来生成的日期
        var tab = $('#MonthHeader');       //获取table的对象
        tab.find("tr").each(function () {
            $(this).remove();
        })
        SetDateValue();
        DrawTableHeader();
        DrawMonthDetail();
        binBtnEvn();
    }
    function DrawTableHeader() {
        tr = '<tr align="center" style="height:30px; " >';
        tr += '<td style="width:50px"><input type="button"  id="PreMonth" value="上月" class="l-button l-button-submit"/></td>';
        tr += '<td colspan="5"  style="width:300px;font-weight:bold;font-size:15px">' + y + '年' + m + '月</td>';
        tr += '<td style="width:50px"><input   type="button"  id="NextMonth" value="下月" class="l-button l-button-submit" /></td>';

        tr += '</tr>';
        $(tr).appendTo(MonthHeader);
        //画日期
        tr = '<tr align="center" style="height:50px; background-color:#F4F9FC"  >';
        for (var i = 0; i < _ArrayWeek.length; i++) {
            if (i == 0 || i == 6) {
                tr += '<td  style="width:100px; font-weight:bold;font-size:14px" ><font color="ff0000">' + _ArrayWeek[i] + '</font></td>';
            }
            else {
                tr += '<td  style="width:100px; font-weight:bold;font-size:14px" >' + _ArrayWeek[i] + '</td>';
            }
        }
        tr += '</tr>';
        $(tr).appendTo(MonthHeader);
    }


    /*
    * 	获得指定日期的星期数，1-6为星期一到星期六，0为星期天
    *	@y 年份
    *	@m 月份
    *	@d 日
    */
    function getWeek(y, m, d) {
        var _int = parseInt,
		c = _int(y / 100);
        y = y.toString().substring(2, 4);
        y = _int(y, 10);
        if (m === 1) {
            m = 13;
            y--;
        } else if (m === 2) {
            m = 14;
            y--;
        };

        var w = y + _int(y / 4) + _int(c / 4) - 2 * c + _int(26 * (m + 1) / 10) + d - 1;
        w = w % 7;

        return w >= 0 ? w : w + 7;
    }

    function DrawMonthDetail() {
        var yy = CurrDate.getFullYear();
        var mm = CurrDate.getMonth() + 1;
        var dd = CurrDate.getDate();
        var stweek = getWeek(yy, mm, dd);
        var tdId = 0;
        while (bolEndTable == false) {
            tr = '<tr align="center" style="height:100px;" >';
            for (var i = 0; i < _ArrayWeek.length; i++) {
                tdId += 1;
                if (bolStartTable == false) {
                    //说明正好是第一天
                    if (stweek == i) {
                        bolStartTable = true;
                    }
                }
                //如果当月日期还没画完
                if (bolStopTable == false) {
                    if (CellDate >= nextDate) {
                        bolStopTable = true;
                    }
                }
                //如果已经画到第一天而且当月日期还没画完
                if (bolStartTable == true && bolStopTable != true) {
                    //如果是周日或周六
                    var strclickValue = TogetDateForMonth(CellDate);
                    if (i == 0 || i == 6) {
                        tr += '<td id="td' + tdId + '" valign="top"  align="right" ';
                        //判断循环日期 是否相等，如果相等 则当前为突出显示
                        if (TogetDateForMonth(NowDate) == TogetDateForMonth(CellDate)) {
                            tr += 'style="width:100px; background-color:#FFB86A;" >';
                        }
                        else {
                            tr += 'style="width:100px; " >';
                        }
                        tr += '<font color="ff0000">' + CellDate.getDate() + '日</font>';
                    }
                    else {
                        tr += '<td id="td' + tdId + '"  valign="top" align="right"   ';
                        if (TogetDateForMonth(NowDate) == TogetDateForMonth(CellDate)) {
                            tr += 'style="width:100px; background-color:#FFB86A;" >';
                        }
                        else {
                            tr += 'style="width:100px; " >';
                        }
                        tr += '<font color="4A6884">' + CellDate.getDate() + '日</font>';
                    }
                    //---------------------------------------------------------------------------------------------------------------------------------
                    //---------------------------------------------------------------------------------------------------------------------------------
                    //此处加载业务数据
                    GetMonthPlan(TogetDateForMonth(CellDate));
                    if (vMonthDate != null) {
                        if (vMonthDate[0].SAMPINGGROUP != '0') {
                            tr += '<br><div align="left">已分配:<br/><font color="Red">' + vMonthDate[0].SAMPINGGROUP + '</font>组采样人员;<br/>计划采样<font color="Red">' + vMonthDate[0].CONTRACTGROUP + '</font>家企业</div>';
                        } 
                    }
                    //---------------------------------------------------------------------------------------------------------------------------------
                    //---------------------------------------------------------------------------------------------------------------------------------
                }
                else {
                    tr += '<td  style="width:100px" >';
                }
                tr += "</td>";
                //如果画到第一天
                if (bolStartTable == true) {
                    //加一天
                    CellDate = new Date(CellDate.setDate(CellDate.getDate() + 1));
                }
            }
            tr += '</tr>';
            $(tr).appendTo(MonthHeader);


            if (CellDate >= nextDate)//如果已经超出本月则跳出循环
            {
                bolEndTable = true;
            }
        }
    }


    //将时间转换为字符串日期
    function TogetDateForMonth(date) {
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
    //上一月事件
    function binBtnEvn() {
        $("#PreMonth").bind("click", function () {
            bolEndTable = false, bolStartTable = false, bolStopTable = false;
            CurrDateTemp = preDate;
            y = CurrDateTemp.getFullYear();
            m = CurrDateTemp.getMonth() + 1;
            d = CurrDateTemp.getDate();

            CurrDate = y + "-" + m + "-1"; //当月
            CurrDate = new Date(CurrDate.replace(/-/g, '/'));

            SetDateValue();
            //移除原来生成的日期
            var tab = $('#MonthHeader');       //获取table的对象
            tab.find("tr").each(function () {
                $(this).remove();
            })
            //画表头
            DrawTableHeader();
            //画日期
            DrawMonthDetail();
            //绑定按钮事件
            binBtnEvn();
        })
        //下一月事件
        $("#NextMonth").bind("click", function () {
            bolEndTable = false, bolStartTable = false, bolStopTable = false;
            CurrDateTemp = nextDate
            y = CurrDateTemp.getFullYear();
            m = CurrDateTemp.getMonth() + 1;
            d = CurrDateTemp.getDate();

            CurrDate = y + "-" + m + "-1"; //当月
            CurrDate = new Date(CurrDate.replace(/-/g, '/'));

            SetDateValue();
            //移除原来生成的日期
            var tab = $('#MonthHeader');       //获取table的对象
            tab.find("tr").each(function () {
                $(this).remove();
            })
            //画表头
            DrawTableHeader();
            //画日期
            DrawMonthDetail();
            //绑定按钮事件
            binBtnEvn();
        })
    }
    //以下是业务数据处理函数
    //--------------------------------------------------------------------
    //获取月度监测计划
    function GetMonthPlan(strDate) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetMonthPlan&strDate="+strDate,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vMonthDate = data.Rows;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    //---------------------------------------------------------------------