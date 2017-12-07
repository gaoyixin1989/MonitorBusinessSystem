/// 日历控件--日
/// 创建时间：2012-12-22
/// 创建人：胡方扬
var dcurrDt, dNowDt, preDay, nextDay, showDay;
var tr = "", task_id = "", strMonitorName = "", plan_id = "", strSampeTaskUrl = "";
var straryDay = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
var strWeekName = "";
var Today = new Date();
var vDatePlanList = null, vMonitorType = null, vDutyUserList = null, vMonitorArrList = null;
$(document).ready(function () {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetConfigSetting&strConfigKey=SampeTaskUrl",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strSampeTaskUrl = data;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    if (strDate) {
        var tempDate = strDate;
        tempDate = tempDate.replace('-', '/');
        var ddDate = new Date(tempDate);
        LoadShow(ddDate, ddDate);
    } else {
        LoadShow(new Date(), new Date());
    }
})
function LoadShow(srrdcurrDt, srrNewDt) {
    //初始化页面
    dcurrDt = srrdcurrDt;
    dNowDt = srrdcurrDt;
    var tab = $('#tbDay');       //获取table的对象
    tab.find("tr").each(function () {
        $(this).remove();
    })
    ini();
    //绑定按钮事件
    btnClickEnv();
    function ini() {
        showDate();
    }
}
function addDay(action) {
    if (action == '-') {
        var PreDt = dcurrDt;
        preDay = new Date(PreDt.setDate(PreDt.getDate() - 1));
        dcurrDt = preDay;
        dNowDt = preDay;
    }
    if (action == '+') {
        var NextDt = dcurrDt;
        nextDay = new Date(NextDt.setDate(NextDt.getDate() + 1));
        dcurrDt = nextDay;
        dNowDt = nextDay;
    }
}
function btnClickEnv() {
    $("#PreDay").bind("click", function () {
        addDay('-');
        var tab = $('#tbDay');       //获取table的对象
        tab.find("tr").each(function () {
            $(this).remove();
        })
        showDate();
        btnClickEnv();
    });

    $("#NextDay").bind("click", function () {
        addDay('+');
        var tab = $('#tbDay');       //获取table的对象
        tab.find("tr").each(function () {
            $(this).remove();
        });
        showDate();
        btnClickEnv();
    });
    $("#btnadd").bind("click", function () {
        if (PageType != "1") {
            $.ligerDialog.open({ title: '监测任务预约', name: 'winaddtor', width: 700, height: 500, top: 0, url: '../MonitoringPlan/PlanAdd.aspx?strIfPlan=0&strDate=' + TogetDateForDay(dNowDt, false), buttons: [
                { text: '确定', onclick: f_SaveDate },
                { text: '返回', onclick: f_Cancel }
            ]
            });
        } else {
            $.ligerDialog.open({ title: '自定义监测任务新增', name: 'winaddtor', width: 700, height: 540, top: 0, url: '../MonitoringPlan/Contract_NewPlanAdd.aspx?strPlanId=0&strDate=' + TogetDateForDay(dNowDt, false), buttons: [
                { text: '确定', onclick: f_SaveDate },
                { text: '取消', onclick: f_Cancel }
            ]
            });
        }
    })

}


function showDate() {

    showDay = TogetDateForDay(dNowDt, true);
    var yy, mm, dd;
    yy = dNowDt.getFullYear();
    mm = dNowDt.getMonth() + 1;
    dd = dNowDt.getDate();
    var stweek = getWeek(yy, mm, dd);
    for (var i = 0; i < straryDay.length; i++) {
        if (stweek == i) {
            strWeekName = straryDay[i];
        }
    }
    tr = '<tr align="center" style="height:30px; background-color:#F4F9FC;" >';
    tr += '<td style="width:50px"><input type="button"  id="PreDay" value="上一天" class="l-button l-button-submit"/></td>';
    if (showDay == TogetDateForDay(Today, true)) {
        tr += '<td style="width:300px;font-weight:bold;font-size:15px"><font color="ff0000">' + showDay + '  ' + strWeekName + '</font></td>';
    }
    else {
        tr += '<td style="width:300px;font-weight:bold;font-size:15px">' + showDay + '  ' + strWeekName + '</td>';
    }
    tr += '<td style="width:40px;"><input type="button"  id="btnadd" value="新 增" class="l-button l-button-submit"/></td>';
    tr += '<td style="width:50px"><input   type="button"  id="NextDay" value="下一天" class="l-button l-button-submit" /></td>';

    tr += '</tr>';
    $(tr).appendTo(tbDay);
    //---------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------------------
    //此处加载业务数据
    vMonitorType = null;
//    GetMonitorInfor();
    vDatePlanList = null;
    vMonitorArrList = null;
    strMonitorName = "";
    GetPlanListForDate();
    if (vDatePlanList == null) {
        tr = '<tr align="center" style="height:50px;" >';
        tr += '<td  colspan="4" style="width:700px;"  align="center" >当天无预约记录</td>'
        tr += '</tr>';
        $(tr).appendTo(tbDay);
    }
    else {
        for (var i = 0; i < vDatePlanList.length; i++) {
            strMonitorName = "";
            task_id = vDatePlanList[i].CONTRACT_ID;
            plan_id = vDatePlanList[i].ID;
            GetContractMonitorType(task_id,plan_id);
            tr = '<tr align="center" style="height:50px;" >';
            tr += '<td align="left"  colspan="3" style="width:600px;">';
            tr += '<br/>';
            tr += (i + 1) + '、<B>项目名称</B>：' + vDatePlanList[i].PROJECT_NAME + "；";
            tr += '<br/>';
            tr += '<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>委托单号</B>：' + vDatePlanList[i].CONTRACT_CODE + '；&nbsp;'
            tr += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>监测类型</B>：' + strMonitorName ;
            tr += '<br/>';
            tr += '<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<B>受检单位</B>：' + vDatePlanList[i].COMPANY_NAME + '；&nbsp;';
            tr += '&nbsp;&nbsp;&nbsp;<B>所属区域</B>：' + vDatePlanList[i].AREA_NAME + '；';
            tr += '<br/>';
            tr += '<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + GetDutyUser(plan_id);
            tr += '</td>'
            tr += '<td align="right" style="border-right-style:none" >';
            //引用父页面URL参数变量
            if (PageType == "1") {
                tr += '<br/><input   type="button"  id="btnAccept' + (i + 1) + '" value="办 理"  onclick="AcceptPlan(\'' + vDatePlanList[i].ID + '\');"  class="l-button l-button-submit" />';
            } else {
                tr += '&nbsp;&nbsp;<input   type="button"  id="btnEdit' + (i + 1) + '" value="编 辑"  onclick="EditPlan(\'' + vDatePlanList[i].ID + '\');" class="l-button l-button-submit" />';
            }
            tr += '&nbsp;&nbsp;<input   type="button"  id="btnDel' + (i + 1) + '" value="删 除" onclick="DelPlan(\'' + vDatePlanList[i].ID + '\')"  class="l-button l-button-submit" />';
            tr += '<br/>';
            tr += '</td>'
            tr += '</tr>';
            $(tr).appendTo(tbDay);
        }

    }
    //---------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------------------
}

//计算星期
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

function TogetDateForDay(date, formart) {
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
function f_Cancel(item, dialog) {
    dialog.close();
}
function f_SaveDate(item, dialog) {
    var fn = dialog.frame.SaveDate || dialog.frame.window.SaveDate;
    var data = fn();
    if (data == "1") {
       // if (strTaskDoModel != "1") {
            LoadShow(dNowDt, dNowDt);
            $.ligerDialog.success('数据保存成功！');
            dialog.close();
            return;
//        }
//        if (strTaskDoModel == "1") {
//            var fn1 = dialog.frame.DoPlan || dialog.frame.window.DoPlan;
//            var doPlan = fn1();
//            if (doPlan == "1") {
//                LoadShow(dNowDt, dNowDt);
//                $.ligerDialog.success('数据保存成功！');
//                dialog.close();
//                return;
//            } 
//else {
//            return;
//            }
        //}
    }
    else {
        $.ligerDialog.warn('数据保存失败！');return;
    }
          }

//以下是业务数据处理函数
//--------------------------------------------------------------------
//获取指定日期监测计划
function GetPlanListForDate() {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetPlanListForDate&strDate=" + TogetDateForDay(dNowDt, false),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vDatePlanList = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}

//以下是对指定日期的某条监测计划进行办理、编辑、删除操作
//办理操作
function AcceptPlan(strPlanId) {
//    if (strTaskDoModel != "1") {
//        $.post("../MonitoringPlan/PlanAdd.aspx?type=doPlan&strPlanId=" + strPlanId, function (data) {
//            if (data == "1") {
//                LoadShow(dNowDt, dNowDt);
//                top.f_addTab("tabSample", '采样任务分配', "../Channels/Mis/Monitor/sampling/QHD/SamplingTaskAllocation.aspx?strTaskId=" + task_id + "&planid=" + strPlanId);
//            }
//            else {
//                $.ligerDialog.warn('办理失败！');
//            }
//        });
//    } 
   // if (strTaskDoModel == "1") {
   if(strSampeTaskUrl!=""){
        top.f_addTab("tabSample_Day", '采样任务分配', ""+strSampeTaskUrl+"?strTaskId=" + task_id + "&planid=" + strPlanId);
        }else{
        top.f_addTab("tabSample_Day", '采样任务分配', "../Channels/Mis/Monitor/sampling/SamplingTaskAllocation.aspx?strTaskId=" + task_id + "&planid=" + strPlanId);
        }
   // }
}
//编辑操作
function EditPlan(strPlanId) {
        $.ligerDialog.open({ title: '监测任务预约编辑', name: 'winaddtor', width: 700, height: 540, top: 0, url: '../MonitoringPlan/PlanAdd.aspx?strPlanId=' + strPlanId + '&strDate=' + TogetDateForDay(dNowDt, false), buttons: [
                { text: '确定', onclick: f_SaveDate },
                { text: '取消', onclick: f_Cancel }
            ]
        });
}

// 删除操作
function DelPlan(strPlanId) {
    if (strPlanId != "") {
        $.ligerDialog.confirm('确定要删除该条记录吗？\r\n', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "MonitoringPlan.ashx?action=DelContractPlan&strPlanId=" + strPlanId,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            LoadShow(dNowDt, dNowDt);
                            $.ligerDialog.success('数据操作成功！');
                        }
                        else {
                            $.ligerDialog.warn('数据操作失败！');
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    }
                });
            }
            else {
                return;
            }
        })
    }
   
}

function GetContractMonitorType(strtask_id,strplan_id) {
    if (task_id != "") {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + strtask_id + '&strPlanId=' + strplan_id,
            //            url: "../Contract/MethodHander.ashx?action=GetContractMonitorType&strContratId=" + task_id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vMonitorArrList = data.Rows;
                    for (var i = 0; i < vMonitorArrList.length; i++) {
                        strMonitorName += vMonitorArrList[i].MONITOR_TYPE_NAME + ";";
                    }
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }
}

function GetDutyUser(strPlanId) {
    var stDutyUser = "";
    if (vMonitorArrList != null) {
        for (var i = 0; i < vMonitorArrList.length; i++) {
            GetContractDutyUser(vMonitorArrList[i].ID,strPlanId);
            if (vDutyUserList != null) {
                stDutyUser += '<B>' + vDutyUserList[0].MONITOR_TYPE_NAME + '类负责人:</B>' + vDutyUserList[0].REAL_NAME + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;';
            }
        }
    }
    return stDutyUser;
}
//获取监测计划责任人
function GetContractDutyUser(mointorid,plan_id) {
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&task_id=" + task_id+"&strPlanId="+plan_id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.Total != 0) {
                vDutyUserList = data.Rows;
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
}
//---------------------------------------------------------------------