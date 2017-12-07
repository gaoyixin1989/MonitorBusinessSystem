var objGrid = null, vEnvTypesItems = null;
var strPlanId = "", strPointId = "", strPointItems = "", strPointItemsName = "", strKeyColumns = "", strTableName = "", strProjectName = "", strEnvType = "", TICKET_NUM = "", CONTRACT_TYPE = "";
var strSampleUrl = "", gridName = "0", strQcSetting = "", strWorkTask_Id = "", strTaskType = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var strMangerAuditSetting = "", strQcStatus = "";

$(document).ready(function () {
    var objToolbar = { items: [
                { id: 'view', text: '查看', click: f_ViewData, icon: 'archives' },
                { line: true },
                { id: 'add', text: '增加', click: f_Add, icon: 'add' },
                { line: true },
                { id: 'doTask', text: '办理', click: f_doTask, icon: 'TRUE' },
                { line: true },
                 { id: 'dele', text: '删除', click: f_delete, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
    };
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
                GetFinishedDate();
            }
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
                    if (strMangerAuditSetting == "1") {
                        strQcStatus = "8";
                    }
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

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=EnvTypes",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vEnvTypesItems = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    if (strMangerAuditSetting == "1") {
        objToolbar = {
            items: [
                { id: 'view', text: '查看', click: f_ViewData, icon: 'archives' },
                { line: true },
                { id: 'add', text: '增加', click: f_Add, icon: 'add' },
                { line: true },
                 { id: 'dele', text: '删除', click: f_delete, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
        }
    }

    objGrid = $("#maingrid").ligerGrid({
        columns: [
                    { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 400 },
                    { display: '任务单号', name: 'TICKET_NUM', align: 'center', width: 200 },
                    { display: '任务下达日期', name: 'PLAN_DATE', align: 'left', width: 120, render: function (items) {
                        return items.PLAN_YEAR + "年" + items.PLAN_MONTH + "月" + items.PLAN_DAY + "日";
                    }
                    },
                    { display: '环境类别', name: 'PLAN_TYPE', align: 'left', width: 250, minWidth: 80, render: function (items) {
                        var strPalnType = items.PLAN_TYPE.split(';');
                        var strPlanTypeName = "";
                        if (strPalnType.length > 0) {
                            for (var n = 0; n < strPalnType.length; n++) {
                                for (var i = 0; i < vEnvTypesItems.length; i++) {
                                    if (vEnvTypesItems[i].DICT_CODE == strPalnType[n]) {
                                        strPlanTypeName += vEnvTypesItems[i].DICT_TEXT + ";";
                                    }
                                }
                            }
                            if (strPlanTypeName != "") {
                                strPlanTypeName = strPlanTypeName.substring(0, strPlanTypeName.length - 1);
                            }
                            return strPlanTypeName;
                        }
                        return items.PLAN_TYPE;
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
          }
                ],
        width: '100%',
        height: '99%',
        toolbar: objToolbar,
        pageSizeOptions: [5, 10, 15, 20, 25, 30],
        pageSize: 25,
        url: 'MonitoringPlan.ashx?action=GetEnvPlanTaskAbList',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
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
    function GetFinishedDate() {
        objGrid1 = $("#maingrid1").ligerGrid({
            columns: [
                    { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 400 },
                    { display: '任务单号', name: 'TICKET_NUM', align: 'center', width: 200 },
                    { display: '任务下达日期', name: 'PLAN_DATE', align: 'left', width: 120, render: function (items) {
                        return items.PLAN_YEAR + "年" + items.PLAN_MONTH + "月" + items.PLAN_DAY + "日";
                    }
                    },
                    { display: '环境类别', name: 'PLAN_TYPE', align: 'left', width: 250, minWidth: 80, render: function (items) {
                        var strPalnType = items.PLAN_TYPE.split(';');
                        var strPlanTypeName = "";
                        if (strPalnType.length > 0) {
                            for (var n = 0; n < strPalnType.length; n++) {
                                for (var i = 0; i < vEnvTypesItems.length; i++) {
                                    if (vEnvTypesItems[i].DICT_CODE == strPalnType[n]) {
                                        strPlanTypeName += vEnvTypesItems[i].DICT_TEXT + ";";
                                    }
                                }
                            }
                            if (strPlanTypeName != "") {
                                strPlanTypeName = strPlanTypeName.substring(0, strPlanTypeName.length - 1);
                            }
                            return strPlanTypeName;
                        }
                        return items.PLAN_TYPE;
                    }
                    }, { display: '要求完成日期', name: 'ASKING_DATE', width: 120, minWidth: 60, align: 'left', render: function (items) {
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
                    }
                ],
            width: '100%',
            height: '99%',
            toolbar: { items: [
                { id: 'view', text: '查看', click: f_ViewData, icon: 'archives' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh1, icon: 'search' }
            ]
            },
            pageSizeOptions: [5, 10, 15, 20, 25, 30],
            pageSize: 25,
            url: 'MonitoringPlan.ashx?action=GetEnvPlanTaskAbList&strSendStatus=1&strQcStatus=' + strQcStatus,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            rownumbers: true,
            checkbox: true,
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

    function f_delete() {
        var rowSelect = null, grid = null;
        if (gridName == "0") {
            rowSelect = objGrid.getSelectedRow()
            grid = objGrid;
        }
        if (gridName == "1") {
            rowSelect = objGrid1.getSelectedRow()
            grid = objGrid1;
        }
        if (rowSelect != null) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须
                type: "POST",
                url: "MonitoringPlan.ashx?action=DelEnvTaskPlan&strPlanId=" + rowSelect.ID,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        objGrid.loadData();
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
            $.ligerDialog.warn('请选择一行！');
            return;
        }
    }

    function f_doTask() {
        var rowSelected = null, grid = null;
        rowSelected = objGrid.getSelectedRow()
        if (rowSelected == null) {
            $.ligerDialog.warn('请选择一条记录！');
        } else {
            strWorkTask_Id = rowSelected.TASK_ID;
            strPlanId = rowSelected.ID;
            strTaskType = rowSelected.TASK_TYPE;
            //如果需要设置是否质控 和全程质控信息 则 发送到采样前质控
            if (strQcSetting == "1") {
                //                $.ligerDialog.open({ title: "质控设置", name: 'winselectorQc', width: 440, height: 150, top: 30, url: 'EnvPendingDoTask.aspx?strWorkTask_id=' + strWorkTask_Id, buttons: [
                //                { text: '确定', onclick: f_QCSetOK },
                //                { text: '返回', onclick: f_ListCancel }
                //            ]
                //                });
                f_QCSetOK();
            } else {//如果不需要设置是否质控 全程质控 则发送到采样任务分配
                if (strSampleUrl != "") {
                    top.f_addTab("tabSample_EnvAny", '采样任务分配', "" + strSampleUrl + "?planid=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + "&strTaskType=" + strTaskType + "&type=ModifTaskSampleDutyUser");
                } else {
                    top.f_addTab("tabSample_EnvAny", '采样任务分配', "../Channels/Mis/Monitor/sampling/SamplingTaskAllocation.aspx?planid=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + "&strTaskType=" + strTaskType + "&type=ModifTaskSampleDutyUser");
                }
            }
        }
    }

    function f_QCSetOK() {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=SetTaskQcStatus&strWorkTask_Id=" + strWorkTask_Id + "&strPlanId=" + strPlanId + "&strSendStatus=1&strQcStatus=1&strAllQCStatus=",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                if (data == "True") {
                    $.ligerDialog.success('任务已发送至【<a style="color:Red;font-weight:bold">采样前质控</a>】环节！');
                    objGrid.loadData();
                    //if (strSampleUrl != "") {
                    //    top.f_addTab("tabSample_EnvAny", '采样任务分配', "" + strSampleUrl + "?planid=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + "&strTaskType=" + strTaskType + "&strQCStatus=2&strAllQCStatus=&type=ModifTaskSampleDutyUser");
                    //} else {
                    //    top.f_addTab("tabSample_EnvAny", '采样任务分配', "../Channels/Mis/Monitor/sampling/SamplingTaskAllocation.aspx?planid=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + "&strTaskType=" + strTaskType + "strQCStatus=2&strAllQCStatus=&type=ModifTaskSampleDutyUser");
                    //}
                }
            },
            error: function (msg) {
                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
            }
        });
    }

    //    function f_QCSetOK(item, dialog) {
    //        var fn_iframQC = dialog.frame.GetQcStatus || dialog.frame.window.GetQcStatus;
    //        var data_iframQC = fn_iframQC();
    //        var fn_iframAQC = dialog.frame.GetQcStatus || dialog.frame.window.GetQcStatus;
    //        var data_iframAQC = fn_iframAQC();
    //        $.ajax({
    //            cache: false,
    //            async: false, //设置是否为异步加载,此处必须
    //            type: "POST",
    //            url: "MonitoringPlan.ashx?action=SetTaskQcStatus&strWorkTask_Id=" + strWorkTask_Id + "&strPlanId=" + strPlanId + "&strSendStatus=1&strQcStatus=" + data_iframQC + "&strAllQcStatus=" + data_iframAQC,
    //            contentType: "application/text; charset=utf-8",
    //            dataType: "text",
    //            success: function (data) {
    //                if (data == "True") {
    //                    if (strSampleUrl != "") {
    //                        top.f_addTab("tabSample_EnvAny", '采样任务分配', "" + strSampleUrl + "?planid=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + "&strTaskType=" + strTaskType + "&strQCStatus=" + data_iframQC + "&strAllQCStatus=" + data_iframAQC + "&type=ModifTaskSampleDutyUser");
    //                    } else {
    //                        top.f_addTab("tabSample_EnvAny", '采样任务分配', "../Channels/Mis/Monitor/sampling/SamplingTaskAllocation.aspx?planid=" + strPlanId + "&strWorkTask_Id=" + strWorkTask_Id + "&strTaskType=" + strTaskType + "strQCStatus=" + data_iframQC + "&strAllQCStatus=" + data_iframAQC + "&type=ModifTaskSampleDutyUser");
    //                    }
    //                }
    //            },
    //            error: function (msg) {
    //                $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    //            }
    //        });
    //    }

    function f_ViewData() {
        var rowSelect = null, grid = null;
        if (gridName == "0") {
            rowSelect = objGrid.getSelectedRow()
            grid = objGrid;
        }
        if (gridName == "1") {
            rowSelect = objGrid1.getSelectedRow()
            grid = objGrid1;
        }
        if (rowSelect != null) {
            $.ligerDialog.open({ title: "环境质量类预约点位查看", name: 'winselector', width: 700, height: 400, top: 30, url: 'EnvPointPlanView.aspx?strPlanId=' + rowSelect.ID, buttons: [
                { text: '返回', onclick: f_ListCancel }
            ]
            });
        }
        else {
            $.ligerDialog.warn('请选择一行！');
            return;
        }
    }

    function f_Add() {
        TICKET_NUM = "";
        CONTRACT_TYPE = "";
        $.ligerDialog.open({ title: "新增环境质量类预约", name: 'winselector', width: 800, height: 650, top: 30, url: 'EnvPlanAdd.aspx?TICKET_NUM=' + TICKET_NUM + '&CONTRACT_TYPE=' + CONTRACT_TYPE + '&strMangerAuditSetting=' + strMangerAuditSetting, buttons: [
                { text: '确定', onclick: f_ListOK },
                { text: '返回', onclick: f_ListCancel }
            ]
        });
    }

    function f_ListOK(item, dialog) {
        
        var fn_ifram = dialog.frame.GetIfram || dialog.frame.window.GetIfram;
        var data_ifram = fn_ifram();
        if (data_ifram == "") {
            $.ligerDialog.warn('请选择监测类别，并生成初始化数据！');
            return;
        }



        var fn_row = dialog.frame.GetDataRow || dialog.frame.window.GetDataRow;
        var data_row = fn_row();

        if (data_row != "") {
            $.ligerDialog.warn('请选择【<a style="color:Red;font-weight:bold">' + data_row + '</a>】监测类别的点位!');
            return;
        }

        strProjectName = "";
        var fn_ProjectName = dialog.frame.GetProjectName || dialog.frame.window.GetProjectName;
        var data_ProjectName = fn_ProjectName();
        if (data_ProjectName == "") {
            return;
        }
        strProjectName = data_ProjectName;

        strEnvType = "";
        var fn_EnvType = dialog.frame.GetEnvType || dialog.frame.window.GetEnvType;
        var data_EnvType = fn_EnvType();
        if (data_EnvType == "") {
            return;
        }
        strEnvType = data_EnvType;

        strPlanId = "";
        var fn_PlanId = dialog.frame.GetPlanId || dialog.frame.window.GetPlanId;
        var data_PlanId = fn_PlanId();
        if (data_PlanId == "") {
            return;
        }
        strPlanId = data_PlanId;

        var strAskingDate = "";
        var fn_AskingDate = dialog.frame.GetAskingDate || dialog.frame.window.GetAskingDate;
        var data_AskingDate = fn_AskingDate();
        if (data_AskingDate == "") {
            return;
        }
        strAskingDate = data_AskingDate;

        //潘德军 2013-12-23 任务单号可改，且不自动生成
        var fn_TaskCode = dialog.frame.getTASK_CODE || dialog.frame.window.getTASK_CODE;
        var data_TaskCode = fn_TaskCode();
        if (data_TaskCode == "") {
            $.ligerDialog.warn('请先录入任务单号！');
            return;
        }

        var strSendDept = "";
        if (strMangerAuditSetting == "1") {
            var fn_SendDept = dialog.frame.getSendDept || dialog.frame.window.getSendDept;
            strSendDept = fn_SendDept();
        }

        var fn_save = dialog.frame.SaveDataRowPoint || dialog.frame.window.SaveDataRowPoint;
        var data_save = fn_save();
        var iBC = dialog.frame.window.GetBcInfo();
        if (data_save) {
            $.ajax({
                cache: false,
                async: false, //设置是否为异步加载,此处必须strTaskNum
                type: "POST",
                //潘德军 2013-12-23 任务单号可改，且不自动生成
                //                url: "MonitoringPlan.ashx?action=doPlanTask&strEnvTypeId=" + strEnvType + "&strProjectName=" + encodeURI(strProjectName) + "&strAskingDate=" + strAskingDate + "&strQcStatus=" + strQcStatus + "&strPlanId=" + strPlanId,
                url: "MonitoringPlan.ashx?action=doPlanTask&strTaskNum=" + encodeURI(data_TaskCode) + "&strEnvTypeId=" + strEnvType + "&strProjectName=" + encodeURI(strProjectName) + "&strAskingDate=" + strAskingDate + "&strQcStatus=" + strQcStatus + "&strPlanId=" + strPlanId + "&strSendDept=" + strSendDept + "&strState=" + iBC,
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    if (data == "True") {
                        dialog.close();
                        objGrid.loadData();
                        var msg = "数据保存成功";
                        if (strMangerAuditSetting == "1") {
                            msg = "采样前质控";
                            $.ligerDialog.success('任务已发送至【<a style="color:Red;font-weight:bold">' + msg + '</a>】环节！');
                        } else {
                            $.ligerDialog.success(msg + "!");
                        }
                        return;
                    }
                },
                error: function (msg) {
                    $.ligerDialog.warn('Ajax加载数据失败！' + msg);
                    return;
                }
            });
        }
    }

    function f_ListCancel(item, dialog) {
        dialog.close();
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
                     { display: "环境质量类型", name: "SEA_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vEnvTypesItems} },
                     { display: "任务下达日期", name: "SEA_PLANDATE", newline: true, type: "date" },
                     { display: "任务单号", name: "SEA_TICKET_NUM", newline: false, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 660, height: 240, top: 90, title: "待发送任务查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_PLANDATE = $("#SEA_PLANDATE").val();
            var SEA_TICKET_NUM = $("#SEA_TICKET_NUM").val();
            var url = "MonitoringPlan.ashx?action=GetEnvPlanTaskAbList&strProjectName=" + SEA_PROJECT_NAME + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=" + SEA_PLANDATE;
            objGrid.set('url', url)
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_PROJECT_NAME").val("");
        $("#SEA_PLANDATE").val("");
        $("#SEA_TICKET_NUM").val("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    }


    //设置grid 的弹出查询对话框
    var detailWinSrh1 = null;
    function showDetailSrh1() {
        if (detailWinSrh1) {
            detailWinSrh1.show();
        }
        else {
            //创建表单结构

            var mainform1 = $("#SrhForm1");
            mainform1.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "项目名称", name: "SEA_PROJECT_NAME1", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "环境质量类型", name: "SEA_CONTRACT_TYPE1", newline: false, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX1", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP1", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vEnvTypesItems} },
                     { display: "任务下达日期", name: "SEA_PLANDATE1", newline: true, type: "date" },
                     { display: "任务单号", name: "SEA_TICKET_NUM1", newline: false, type: "text" }
                    ]
            });

            detailWinSrh1 = $.ligerDialog.open({
                target: $("#detailSrh1"),
                width: 660, height: 240, top: 90, title: "已发送任务查询",
                buttons: [
                  { text: '确定', onclick: function () { search1(); clearSearchDialogValue1(); detailWinSrh1.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue1(); detailWinSrh1.hide(); } }
                  ]
            });
        }

        function search1() {
            var SEA_PROJECT_NAME = encodeURI($("#SEA_PROJECT_NAME1").val());
            var SEA_CONTRACT_TYPE_OP = $("#SEA_CONTRACT_TYPE_OP1").val();
            var SEA_PLANDATE = $("#SEA_PLANDATE1").val();
            var SEA_TICKET_NUM = $("#SEA_TICKET_NUM1").val();
            var url = "MonitoringPlan.ashx?action=GetEnvPlanTaskAbList&strSendStatus=1&strProjectName=" + SEA_PROJECT_NAME + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=" + SEA_PLANDATE;
            objGrid1.set('url', url)
        }
    }

    function clearSearchDialogValue1() {
        $("#SEA_PROJECT_NAME1").val("");
        $("#SEA_PLANDATE1").val("");
        $("#SEA_TICKET_NUM1").val("");
        $("#SEA_CONTRACT_TYPE_BOX1").ligerGetComboBoxManager().setValue("");
    }

});