﻿//Create By 胡方扬 
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vExamTypeItems = null, vEStatusItems = null, vEmployeList = null, vEmployeDept = null, vContratTypeItem = null;
var vEmployeList = null, vSubEmploye = null, strEmployePostName = "";
var isFisrt = "", gridName = "0";
var strExamType = "", strEmployeNames = "";
var strPlanId = "", strTask_id = "", strWorkTask_id = "", strAskFinishDate = ""; TICKET_NUM = ""; CONTRACT_TYPE = "";
var strTest_CompanyID = "";
var strSampleUrl = "", strQcSetting = "", strMangerAuditSetting = "";
var strType = "";

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

    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            navtab = $("#navtab1").ligerGetTabManager();
            if (tabid == "home") {
                gridName = "0";
            }
            if (tabid == "tabitem1") {
                gridName = "1";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                GetFinishedDate();
            }
            else {
                isFisrt = true;
            }
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
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
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
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
        url: "../Contract/MethodHander.ashx?action=GetContratYearHistory",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAutoYearItem = data.Rows;
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
        url: "../Contract/MethodHander.ashx?action=GetWebConfigValue&strKey=SampeTaskUrl",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            if (data != "") {
                strSampleUrl = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //获取是否启用预约质控设置环节
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
    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 320 },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 150, minWidth: 60, render: function (item) {
                    if (vContratTypeItem.length > 0) {
                        for (var n = 0; n < vContratTypeItem.length; n++) {
                            if (vContratTypeItem[n].DICT_CODE == item.CONTRACT_TYPE) {
                                var strContractTypeName = vContratTypeItem[n].DICT_TEXT;
                                return strContractTypeName;
                            }
                        }
                    }
                    return item.CONTRACT_TYPE;
                }
                },
            { display: '监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', render: function (items) {
                return GetContractMonitorType(items.CONTRACT_ID, items.ID);
            }
            },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 150, minWidth: 60 }
                ],
        width: '100%',
        height: '99%',
        pageSizeOptions: [5, 10, 15, 20, 25, 30],
        pageSize: 20,
        url: 'MonitoringPlan.ashx?action=GetPendingPlanList&strType=false',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        toolbar: { items: [
                { id: 'doenve', text: '任务下达', click: DoTaskPlan, icon: 'customers' },
                { line: true },
                { id: 'add', text: '新增', click: AddData, icon: 'add' },
                { line: true },
                { id: 'del', text: '删除', click: DeleteData, icon: 'delete' },
                { line: true },
            //                { id: 'excel', text: '导出任务通知单', click: f_ExportTaskExcle, icon: 'excel' },
            //                { line: true },
                {id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        whenRClickToSelect: true,
        onDblClickRow: function (data, rowindex, rowobj) {
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //================================================
    function GetFinishedDate() {
        maingrid1 = $("#maingrid1").ligerGrid({
            columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 220 },
                { display: '任务下达日期', name: 'PLANDATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                    if (items.PLAN_YEAR != "") {
                        return items.PLAN_YEAR + '年' + items.PLAN_MONTH + '月' + items.PLAN_DAY + '日';
                    }
                }
                },
             { display: '要求完成日期', name: 'ASKING_DATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
                 var strDate = "";
                 var v = items.ASKING_DATE;
                 if (v != "") {
                     var contractData = new Date(Date.parse(v.replace(/-/g, '/')))
                     strDate += contractData.getFullYear() + "年";
                     strDate += (contractData.getMonth() + 1) + "月";
                     strDate += contractData.getDate() + "日";
                     return strDate;
                 }
                 return items.ASKING_DATE;
             }
             },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '已选监测类型', name: 'TYPES', width: 160, minWidth: 60, align: 'left', render: function (items) {
                    var strValue = GetContractMonitorType(items.CONTRACT_ID, items.ID);
                    if (strValue.length > 20) {
                        return "<a title=" + strValue + ">" + strValue.substring(0, 20) + "......</a>"
                    }
                    return strValue;
                }
                },
                { display: '受检单位', name: 'COMPANY_NAME', align: 'left', width: 180, minWidth: 60 },
                { display: '所属区域', name: 'AREA_NAME', width: 80, minWidth: 60 }
            //                { display: '已选监测项目负责人', name: 'DUTYUSER', width: 400, minWidth: 60, align: 'left', render: function (items) {
            //                    var strValue = GetDutyUser(items.CONTRACT_ID, items.ID);
            //                    if (strValue.length > 30) {
            //                        return "<a title=" + strValue + ">" + strValue.substring(0, 30) + "......</a>"
            //                    }
            //                    return strValue;
            //                }
            //            }
                ],
            width: '100%',
            height: '99%',
            pageSizeOptions: [5, 10, 15, 20, 25, 30],
            pageSize: 20,
            url: 'MonitoringPlan.ashx?action=GetPendingPlanList&strType=true',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            rownumbers: true,
            checkbox: true,
            toolbar: { items: [
                //                { id: 'view', text: '查看', click: ViewData, icon: 'archives' },
                //                { line: true },
                //               { id: 'sample', text: '采样任务分配', click: TopOpen, icon: 'up' },
                //                { line: true },
                {id: 'excel', text: '导出任务通知单', click: f_ExportTaskExcle, icon: 'excel' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'del', text: '删除', click: DeleteTask, icon: 'delete' }
                ]
            },
            whenRClickToSelect: true,
            onDblClickRow: function (data, rowindex, rowobj) {
            },
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
        $("#pageloading").hide();
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    }

    /**
    *功能描述:删除无委托书的监测任务计划
    */
    function DeleteData() {
        var rowSelected = maingrid.getSelectedRow();
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一行进行操作'); return;
        }
        if (rowSelected.CONTRACT_ID == "") {
            $.ligerDialog.confirm('确定要删除该条记录吗？\r\n', function (result) {
                if (result == true) {
                    $.ajax({
                        cache: false,
                        async: false, //设置是否为异步加载,此处必须
                        type: "POST",
                        url: "MonitoringPlan.ashx?action=DelContractPlan&strPlanId=" + rowSelected.ID,
                        contentType: "application/text; charset=utf-8",
                        dataType: "text",
                        success: function (data) {
                            if (data != "") {
                                maingrid.loadData();
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
                } else {
                    return;
                }
            });
        } else {
            $.ligerDialog.warn("无法删除由委托合同书生成的任务\r\n,如有疑问请联系管理员！");
        }
    }

    function TopOpen() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            if (strSampleUrl != "") {
                top.f_addTab("tabSample", '采样任务分配', "" + strSampleUrl + "?planid=" + rowSelected.ID + "&strTask_Id=" + rowSelected.CONTRACT_ID + "&strWorkTask_Id=" + rowSelected.TASK_ID);
            } else {
                top.f_addTab("tabSample", '采样任务分配', "../Channels/Mis/Monitor/sampling/SamplingTaskAllocation.aspx?planid=" + rowSelected.ID + "&strTask_Id=" + rowSelected.CONTRACT_ID + "&strWorkTask_Id=" + rowSelected.TASK_ID);
            }
        }
    }
    function AddData() {
        $.ligerDialog.open({ title: '新增预约任务', name: 'winaddtor', width: 820, height: 550, top: 30, url: '../MonitoringPlan/TempTaskPlanAdd.aspx', buttons: [
                { text: '确定', onclick: f_AddSaveDate },
            //                { text: '办理', onclick: f_DoTaskData },
                {text: '取消', onclick: f_AddCancel }
            ]
        });
    }
    /**
    *功能描述:临时性委托预约任务生成
    *item 数据对象
    *dialog 窗体
    */
    function f_AddSaveDate(item, dialog) {
        var fn_PlanPoint = dialog.frame.SavePlanPoint || dialog.frame.window.SavePlanPoint;
        var data_PlanPoint = fn_PlanPoint();
        if (data_PlanPoint == "1") {
            var fn = dialog.frame.getReturnPageValue || dialog.frame.window.getReturnPageValue;
            var data = fn();
            if (data != "") {
                $.post("../MonitoringPlan/TempTaskPlanAdd.aspx?action=doPlanTask" + data, function (data) {
                    if (data == "1") {
                        dialog.close();
                        maingrid.loadData();
                        $.ligerDialog.success('数据保存成功！'); return;
                    }
                    else {
                        maingrid.loadData();
                        $.ligerDialog.warn('数据保存失败！'); return;
                    }
                });
            }
            else {
                maingrid.loadData();
                $.ligerDialog.warn('数据保存失败！');
                return;
            }

        }

    }

    function f_DoTaskData() {

    }

    function f_AddCancel(item, dialog) {
        dialog.close();
    }

    function DoTaskPlan() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            strPlanId = rowSelected.ID;
            strTask_id = rowSelected.CONTRACT_ID;
            strWorkTask_id = rowSelected.TASK_ID;
            strAskFinishDate = rowSelected.ASKING_DATE;
            TICKET_NUM = rowSelected.TICKET_NUM;
            CONTRACT_TYPE = rowSelected.CONTRACT_TYPE;
            strTest_CompanyID = rowSelected.TEST_COMPANY_ID;
            $.ligerDialog.open({ title: '任务下达', name: 'winaddtor', width: 600, height: 400, top: 90, url: '../MonitoringPlan/PendingDoTask.aspx?strIfPlan=0&strDate=&strPlanId=' + strPlanId + '&strTaskId=' + strTask_id + '&strWorkTask_id=' + strWorkTask_id + '&strAskFinishDate=' + strAskFinishDate + '&strProjectNmae=' + encodeURI(rowSelected.PROJECT_NAME) + '&TICKET_NUM=' + TICKET_NUM + '&CONTRACT_TYPE=' + CONTRACT_TYPE + '&TEST_COMPANY_ID=' + strTest_CompanyID, buttons: [
                { text: '确定', onclick: f_SaveData },
                { text: '取消', onclick: f_Cancel }
            ]
            });
        }
    }

    function f_ExportTaskExcle() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            strPlanId = rowSelected.ID;
            strTask_id = rowSelected.CONTRACT_ID;
            strWorkTask_id = rowSelected.TASK_ID;
            strType = $.getUrlVar('type');
            $("#hidTaskId").val(strTask_id);
            $("#hidPlanId").val(strPlanId);
            $("#hidWorkTaskId").val(strWorkTask_id);
            $("#hidType").val(strType);
            $("#btnExport").click();
        }
    }

    function f_SaveData(item, dialog) {
        //发送完成后，询问是否打印任务单 潘德军 2014-01-07
        var rowSelected = null;
        rowSelected = maingrid.getSelectedRow();
        var strPrintWorkTask_id = rowSelected.TASK_ID;
        var strPrintPlanId = rowSelected.ID;
        var strPrintTask_id = rowSelected.CONTRACT_ID;

        var fn1 = dialog.frame.getTASK_CODE || dialog.frame.window.getTASK_CODE;
        var data1 = fn1();
        if (data1 == "" || data1 == "未编号") {
            $.ligerDialog.warn('请先设置任务单号！');
            return;
        }

        var fn = dialog.frame.SaveData || dialog.frame.window.SaveData;
        var data = fn();
        if (data == "1") {
            dialog.close();
            maingrid.loadData();
            var msg = "采样任务分配";
            if (strQcSetting == "1") {
                msg = "采样前质控";
            }
            else {
                if (strMangerAuditSetting != "" && strMangerAuditSetting == "1") {
                    msg = "技术室主管审核";
                }
            }
            $.ligerDialog.success('任务已发送至【<a style="color:Red;font-weight:bold">' + msg + '</a>】环节！');
            //发送完成后，询问是否打印任务单 潘德军 2014-01-07
            if_PrintTaskExcel(strPrintWorkTask_id, strPrintPlanId, strPrintTask_id);
            return;
        } else {
            maingrid.loadData();
            $.ligerDialog.warn('数据保存失败！');
            return;
        }
    }

    //发送完成后，询问是否打印任务单 潘德军 2014-01-07
    function if_PrintTaskExcel(strPrintWorkTask_id, strPrintPlanId, strPrintTask_id) {
        $.ligerDialog.confirm('是否打印任务通知单？\r\n', function (result) {
            if (result == true) {
                $("#hidTaskId").val(strPrintTask_id);
                $("#hidPlanId").val(strPrintPlanId);
                $("#hidWorkTaskId").val(strPrintWorkTask_id);
                strType = $.getUrlVar('type');
                $("#hidType").val(strType); //huangjinjun add
                $("#btnExport").click();
            } else {
                return;
            }
        });
    }

    function f_Cancel(item, dialog) {
        dialog.close();
    }
    function ViewData() {
        var rowSelected = null, grid = null;
        if (gridName == "0") {
            rowSelected = maingrid.getSelectedRow()
            grid = maingrid;
        }
        if (gridName == "1") {
            rowSelected = maingrid1.getSelectedRow()
            grid = maingrid1;
        }
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            var sDate = rowSelected.PLAN_YEAR + '-' + rowSelected.PLAN_MONTH + '-' + rowSelected.PLAN_DAY;
            top.f_addTab("tabSample", '查看预约任务', "../Channels/Mis/MonitoringPlan/PlanCalendar.aspx?strDate=" + sDate);
        }
    };

    function GetContractMonitorType(strtask_id, strplan_id) {
        vMonitorArrList = null;
        strMonitorName = "";
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: 'MonitoringPlan.ashx?action=GetPointMonitorInfor&task_id=' + strtask_id + '&strPlanId=' + strplan_id + '&strIfPlan=1',
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

        if (strMonitorName != "") {
            strMonitorName = strMonitorName.substring(0, strMonitorName.length - 1)
        }
        return strMonitorName;
    }


    function GetDutyUser(strtask_id, strplan_id) {
        var stDutyUser = "";
        if (vMonitorArrList != null) {
            for (var i = 0; i < vMonitorArrList.length; i++) {
                GetContractDutyUser(strtask_id, vMonitorArrList[i].ID, strplan_id);
                if (vDutyUserList != null) {
                    stDutyUser += vDutyUserList[0].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '；';
                }
            }
        }
        return stDutyUser.substring(0, stDutyUser.length - 1);
    }
    //获取监测计划责任人
    function GetContractDutyUser(strtask_id, mointorid, strplan_id) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&strPlanId=" + strplan_id + "&task_id=" + strtask_id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data.Total != 0) {
                    vDutyUserList = data.Rows;
                } else {
                    vDutyUserList = null;
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "项目名称", name: "SEA_PROJECT_NAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "委托单号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "合同年度", name: "SEA_CONTRACT_YEAR", newline: true, type: "select", comboboxName: "SEA_CONTRACT_YEAR_BOX", options: { valueFieldID: "SEA_CONTRACT_YEAR_OP", valueField: "ID", textField: "YEAR", data: vAutoYearItem} },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME", newline: true, type: "text" },
                     { display: "所属区域", name: "SEA_AREA", newline: false, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "SEA_AREA_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem} },
                     { display: "任务单号", name: "SEA_TICKET_NUM", newline: true, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "待预约任务查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME").val());
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE").val());
            var SEA_CONTRACT_YEAR_OP = encodeURI($("#SEA_CONTRACT_YEAR_BOX").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_TEST_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME").val());
            var SEA_AREA = $("#SEA_AREA_OP").val();
            var SEA_TICKET_NUM = $("#SEA_TICKET_NUM").val();
            if (gridName == "0") {
                var url = "MonitoringPlan.ashx?action=GetPendingPlanList&strType=false&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=";
                maingrid.set('url', url)
            }
            if (gridName == "1") {
                var url = "MonitoringPlan.ashx?action=GetPendingPlanList&strType=true&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=";
                maingrid1.set('url', url)
            }
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_PROJECT_NAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_TEST_COMPANYNAME").val("");
        $("#SEA_TICKET_NUM").val("");
        $("#SEA_CONTRACT_YEAR_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
    }
})

/**
*功能描述:删除无委托书的监测任务计划
*/
function DeleteTask() {
    var rowSelected = maingrid1.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一行进行操作'); return;
    }
    
    if (rowSelected.CONTRACT_ID == "") {
        $.ligerDialog.confirm('该任务已经下达，确定要删除吗？\r\n删除后无法修复', function (result) {
            if (result == true) {
                $.ajax({
                    cache: false,
                    async: false, //设置是否为异步加载,此处必须
                    type: "POST",
                    url: "MonitoringPlan.ashx?action=DelTaskPlan&strPlanId=" + rowSelected.ID,
                    contentType: "application/text; charset=utf-8",
                    dataType: "text",
                    success: function (data) {
                        if (data != "") {
                            maingrid1.loadData();
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
            } else {
                return;
            }
        });
    } else {
        $.ligerDialog.warn("无法删除由委托合同书生成的任务\r\n,如有疑问请联系管理员！");
    }
}