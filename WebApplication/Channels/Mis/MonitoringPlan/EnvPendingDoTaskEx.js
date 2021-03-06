﻿
/// 日历控件--监测计划办理
/// 创建时间：2013-4-1
/// 创建人：胡方扬
var objGrid = null, vMonitorType = null, vPointItem = null, vMonth = null, vContractInfor = null, vDutyInitUser = null;
var task_id = "", struser = "", strmonitorID = "", monitorTypeName = "";
var tr = "", MonitorArr = [];
var saveflag = "";
var vDutyList = null;
var strPlanId = "", strIfPlan = "", strDate = "", strCompanyId = "";
var strFreqId = "", strPointId = "", strInitValue = "", strWorkTask_id = "", strAskFinishDate = ""; TICKET_NUM = ""; CONTRACT_TYPE = ""; Contact_Name = "";PHONE = "";
var isEdit = false, GetSampleFreqItems = null;
var checkedCustomer = [], checkedMonitorArr = [], checkedPoint = [], moveCheckPoint = [], moveCheckCustomer = [];
var YesNoItems = null;
var strQcSetting = "", strQcStatus = "3", strAllQCStatus = "0"; //设置默认质控设置为已经设置完成
var strMangerAuditSetting = ""; // 秦皇岛 采样任务下达与采样任务分配前添加采样分配领导审核环节
//潘德军 2013-12-23 任务单号可改，且初始不生成
var strTaskNum = "";
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
    $("#layout1").ligerLayout({ topHeight: 100, leftWidth: "100%", allowLeftCollapse: false, allowRightCollapse: false, height: "98%" });
    strPlanId = $.getUrlVar('strPlanId');
    strIfPlan = $.getUrlVar('strIfPlan');
    strDate = $.getUrlVar('strDate');
    strProjectNmae = decodeURI($.getUrlVar('strProjectNmae'));
    task_id = $.getUrlVar('strTaskId');
    strWorkTask_id = $.getUrlVar('strWorkTask_id');
    strAskFinishDate = $.getUrlVar('strAskFinishDate');
    TICKET_NUM = $.getUrlVar('TICKET_NUM'); //任务单号
    CONTRACT_TYPE = $.getUrlVar('CONTRACT_TYPE'); //委托类型
    //strAskFinishDate = TogetDateAfter7(new Date());
    //获取是否启用预约质控设置环节
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=ManagerAdut",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data.length > 0) {
                    strMangerAuditSetting = data[0].DICT_CODE;
                }
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "MonitoringPlan.ashx?action=GetConfigSetting&strConfigKey=QCsetting",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strQcSetting = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //获取是否字典项
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=company_yesno",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                YesNoItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

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
    if (!strDate) {
        strDate = TogetDate(new Date());
    }
    
    //赋值
    $("#txtProjectName").val(strProjectNmae);
    $("#txtProjectName").ligerTextBox({ disabled: true });
    $("#txtDate").ligerDateEditor({ initValue: strDate, disabled: true });
    $("#txtFinishDate").ligerDateEditor({ initValue: strAskFinishDate.split(' ')[0], disabled: true });
    $("#txtTASK_CODE").val(TICKET_NUM);
    if (strQcSetting == "1") {
        strQcStatus = "1";
        strAllQCStatus = "1";
    }
    else {
        $("#tdQcLab").attr("style", "padding:3px;display:none");
        $("#txtQcSet").attr("style", "padding:3px;display:none");
        //        $("#tdAllQcLab").attr("style", "padding:3px;display:none");
        //        $("#txtAllQcSet").attr("style", "padding:3px;display:none");
        $("#trQcRow").attr("style", "display:none");
        strAllQCStatus = "0";
        strQcStatus = "3";
        if (strMangerAuditSetting != "" && strMangerAuditSetting == "1") {
            strQcStatus = "M11";
        }
    }
    if (task_id != "") {
        GetContractMonitorType();
        //CreateInputComboBox();
    }
    //任务单号查询
    $("#BtnSearch").click(function () {
        $.ligerDialog.open({ title: '任务单号查询', name: 'winaddtor', width: 320, height: 280, top: 10, url: '../MonitoringPlan/PendingDoTask_Search.aspx?CONTRACT_TYPE=' + CONTRACT_TYPE, buttons: [
                { text: '关闭', onclick: f_Cancel }
            ]
        });
    });
})
 function f_Cancel(item, dialog) {
        dialog.close();
    }

    function GetContractMonitorType() {
        if (task_id != "") {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + task_id + '&strPlanId='+strPlanId+'&strIfPlan=' + strIfPlan,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.Total != 0) {
                        MonitorArr = data.Rows;
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                }
            });
        }
    }

    function SaveData() {
        //潘德军 2013-12-23 任务单号可改，且初始不生成
        strTaskNum = $("#txtTASK_CODE").val();
        if (strTaskNum == "") {
            return;
        }

        strDate = $("#txtDate").val();
        strAskFinishDate = $("#txtFinishDate").val();
        SaveFinishData();
        return saveflag;
    }


    function SaveFinishData() {
        strAskFinishDate=$("#txtFinishDate").val();
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=SaveFinishData&strWorkTask_Id=" + strWorkTask_id + "&strAskFinishDate=" + strAskFinishDate + "&strQcStatus=" + strQcStatus + "&strAllQcStatus=" + strAllQCStatus  ,
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    saveflag = "1";
                    //SavePlanPeople();
                }
                else {
                    saveflag = "";
//                    return;
                }
            },
            error: function (msg) {
                saveflag = "";
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

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

    function TogetDateAfter7(date) {
        /*var strD = "";
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
        */
        var newDate = new Date(date);
        newDate = newDate.valueOf();
        newDate = newDate + 7 * 24 * 60 * 60 * 1000;
        newDate = new Date(newDate);

        var y = newDate.getFullYear();
        var m = newDate.getMonth() + 1;
        var d = newDate.getDate();
        if (m <= 9) m = "0" + m;
        if (d <= 9) d = "0" + d;
        var strD = y + "-" + m + "-" + d;
        return strD;
    }

    //潘德军 2013-12-23 任务单号可改，且初始不生成
    //任务单号可改，且初始不生成
    //得到任务单号
    function getTASK_CODE() {

        var strData1 = $("#txtTASK_CODE").val();

        return strData1;
    }