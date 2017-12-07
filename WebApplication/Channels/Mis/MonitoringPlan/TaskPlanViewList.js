//Create By 胡方扬 
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

var maingrid = null, maingrid1 = null, maingrid2 = null, vTypeItems = null, vExamTypeItems = null, vEStatusItems = null, vEmployeList = null, vEmployeDept = null;
var vEmployeList = null, vSubEmploye = null, strEmployePostName = "";
var isFisrt = "", gridName = "0";
var strExamType = "", strEmployeNames = "";
var strPlanId = "", strTask_id = "", strWorkTask_id = "", strAskFinishDate = "";
var strSampleUrl = "";
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

    maingrid = $("#maingrid").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', align: 'left', width: 320 },
                { display: '委托单号', name: 'CONTRACT_CODE', width: 200, minWidth: 60 },
                { display: '任务单号', name: 'TICKET_NUM', width: 200, minWidth: 60 },
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 150, minWidth: 60, render: function (item) {
                    if (vContratTypeItem.length > 0) {
                        for (var i = 0; i < vContratTypeItem.length; i++) {
                            if (vContratTypeItem[i].DICT_CODE == item.CONTRACT_TYPE) {
                                return vContratTypeItem[i].DICT_TEXT;
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
                { display: '所属区域', name: 'AREA_NAME', width: 80, minWidth: 60 }
                ],
        width: '100%',
        height: '99%',
        pageSizeOptions: [5, 10, 15, 20,25,30],
        pageSize: 20,
        url: 'MonitoringPlan.ashx?action=GetPendingPlanList&strType=false',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        rownumbers: true,
        checkbox: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: ShowSrch, icon: 'search' }
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
                { display: '所属区域', name: 'AREA_NAME', width: 80, minWidth: 60 },
                { display: '已选监测项目负责人', name: 'DUTYUSER', width: 400, minWidth: 60, align: 'left', render: function (items) {
                    var strValue = GetDutyUser(items.CONTRACT_ID);
                    if (strValue.length > 30) {
                        return "<a title=" + strValue + ">" + strValue.substring(0, 30) + "......</a>"
                    }
                    return strValue;
                }
                }
                ],
            width: '100%',
            height: '99%',
            pageSizeOptions: [5, 10, 15, 20,25,30],
            pageSize: 20,
            url: 'MonitoringPlan.ashx?action=GetPendingPlanListDoTask&strType=true',
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            rownumbers: true,
            checkbox: true,
            toolbar: { items: [
                { id: 'srh', text: '查询', click: ShowSrch, icon: 'search' }
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
        $.ligerDialog.open({ title: '预约任务新增', name: 'winaddtor', width: 700, height: 540, top: 0, url: '../MonitoringPlan/ContractPlanAdd.aspx', buttons: [
                { text: '确定', onclick: f_AddSaveDate },
                { text: '取消', onclick: f_AddCancel }
            ]
        });
    }
    function f_AddSaveDate(item, dialog) {
        var fn = dialog.frame.SaveReturn || dialog.frame.window.SaveReturn;
        var data = fn();
        if (data == "1") {
            maingrid.loadData();
            $.ligerDialog.success('数据保存成功！');
            return;
        } else {
            maingrid.loadData();
            $.ligerDialog.warn('数据保存失败！');
            return;
        }
    }
    function f_AddCancel(item, dialog) {
        dialog.close();
    }
    function ShowSrch() {
        if (gridName == "0") {
            showDetailSrh();
        } else {
            showDetailSrh1();
        }
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
            $.ligerDialog.open({ title: '监测任务预约', name: 'winaddtor', width: 700, height: 340, top: 90, url: '../MonitoringPlan/PendingDoTask.aspx?strIfPlan=0&strDate=&strPlanId=' + strPlanId + '&strTaskId=' + strTask_id + '&strWorkTask_id=' + strWorkTask_id + '&strAskFinishDate=' + strAskFinishDate + '&strProjectNmae=' + encodeURI(rowSelected.PROJECT_NAME), buttons: [
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
            $("#hidTaskId").val(strTask_id);
            $("#hidPlanId").val(strPlanId);
            $("#btnExport").click();
        }
    }

    function f_SaveData(item, dialog) {
        var fn = dialog.frame.SaveData || dialog.frame.window.SaveData;
        var data = fn();
        if (data == "1") {
            dialog.close();
            maingrid.loadData();
            $.ligerDialog.success('数据保存成功！');
            return;
        } else {
            maingrid.loadData();
            $.ligerDialog.warn('数据保存失败！');
            return;
        }
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

        return strMonitorName;
    }


    function GetDutyUser(strtask_id) {
        var stDutyUser = "";
        if (vMonitorArrList != null) {
            for (var i = 0; i < vMonitorArrList.length; i++) {
                GetContractDutyUser(strtask_id, vMonitorArrList[i].ID);
                if (vDutyUserList != null) {
                    stDutyUser += vDutyUserList[0].MONITOR_TYPE_NAME + '类:' + vDutyUserList[0].REAL_NAME + '；';
                }
            }
        }
        return stDutyUser.substring(0, stDutyUser.length - 1);
    }
    //获取监测计划责任人
    function GetContractDutyUser(strtask_id, mointorid) {
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "MonitoringPlan.ashx?action=GetContractDutyUser&strMonitorId=" + mointorid + "&task_id=" + strtask_id,
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
                     { display: "合同编号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
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
                var url = "MonitoringPlan.ashx?action=GetPendingPlanListDoTask&strType=true&strProjectName=" + SEA_PROJECT_NAME + "&strContractCode=" + SEA_CONTRACT_CODE + "&strContratYear=" + SEA_CONTRACT_YEAR_OP + "&strContractType=" + SEA_CONTRACT_TYPE_OP + "&strCompanyNameFrim=" + SEA_TEST_COMPANYNAME + "&strAreaIdFrim=" + SEA_AREA + "&strTickeNum=" + encodeURI(SEA_TICKET_NUM) + "&strDate=" + SEA_PLANDATE;
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