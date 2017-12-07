// Create by 潘德军 2013.7.1  "综合查询--监测任务查询"功能
var taskGrid, subTaskGrid;
var menuTask, menuSubTask;
var pathUrl = "../";
var groupicon = pathUrl + "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var webServiceUrl = pathUrl + "TotalSearch.aspx";
var strMyPage = "SummarySheet_Local.aspx";

var topHeight = $(window).height() / 5 * 3;
var gridHeight = $(window).height() / 5 * 3;
var gridHeightEx = $(window).height() / 5 * 2;

var reportId; //报告ID

$(document).ready(function () {
    $("#layout1").ligerLayout({ width: '98%', height: '100%', topHeight: topHeight });

    //菜单
    menuTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'excel', text: '数据汇总表', click: excelExportForTask_Summary, icon: 'excel' }
            ]
    });

    //监测任务grid
    taskGrid = $("#taskGrid").ligerGrid({
        columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 200, align: 'left', isSort: false },
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left', isSort: false },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getTaskCompanyNameForSearch(data.TESTED_COMPANY_ID);
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "任务信息",
        url: strMyPage + '?Action=selectTask',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: showAll, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            var selectedConItem = taskGrid.getSelectedRow();
            showSubTaskGrid(selectedConItem.ID);
            //menuTask.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            showSubTaskGrid(rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                      { display: "任务单号", name: "srhTICKET_NUM", newline: false, type: "text", group: "查询信息", groupicon: groupicon }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 600, height: 200, left: 30, top: 90, title: "查询委托信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhTICKET_NUM = $("#srhTICKET_NUM").val();

        taskGrid.set('url', strMyPage + "?Action=selectTask&&SrhTICKET_NUM=" + SrhTICKET_NUM);
    }

    //grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#srhTICKET_NUM").val("");
    }
}

function showAll() {
    taskGrid.set('url', strMyPage + "?Action=selectTask");
}

function showSubTaskGrid(strTaskId) {
    //菜单
    menuSubTask = $.ligerMenu({ width: 120, items:
            [

            { id: 'excel', text: '数据汇总表', click: showSheetTypeDialog, icon: 'excel' }
            ]
    });

    //监测任务grid
    subTaskGrid = $("#subTaskGrid").ligerGrid({
        columns: [
        { display: '监测类别', name: 'MONITOR_NAME', width: 100, align: 'left', isSort: false, render: function (data) {
            return getMonitorName(data.MONITOR_ID);
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: strMyPage + '?action=selectsubTask&task_id=' + strTaskId,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [

                { id: 'excel', text: '数据汇总表', click: showSheetTypeDialog, icon: 'excel' },
                { id: 'excel', text: '上传数据汇总表', click: upLoadSummaryTable, icon: 'excel' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menuSubTask.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //上传汇总表
    function upLoadSummaryTable() {
        if (subTaskGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('上传附件之前请先选择一条记录');
            return;
        }
        $.ligerDialog.open({ title: '汇总表上传', width: 500, height: 270,
            buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileUpload.aspx?filetype=samplingSummaryTable&id=' + subTaskGrid.getSelectedRow().ID
        });
    }
}

var detailExcelWin = null;
function excelExportForTask_Summary() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条任务数据！');
        return;
    }
    detailExcelWin = $.ligerDialog.open({ url: "../SearchFunction/print_Summary_Local.aspx?isLocal=1&task_id=" + selectedTaskItem.ID });
}

function excelExportForSubTask_Summary() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    var selectedsubTaskItem = subTaskGrid.getSelectedRow();
    if (!selectedsubTaskItem) {
        $.ligerDialog.warn('请先选择一条监测类别数据！');
        return;
    }
    detailExcelWin = $.ligerDialog.open({ url: "../SearchFunction/print_Summary_Local.aspx?isLocal=1&task_id=" + selectedTaskItem.ID + "&subtask_id=" + selectedsubTaskItem.ID });
}

function showSheetTypeDialog() {
    var vSheetType = null

    var selectedTaskItem = taskGrid.getSelectedRow();
    var selectedsubTaskItem = subTaskGrid.getSelectedRow();
    if (!selectedsubTaskItem) {
        $.ligerDialog.warn('请先选择一条监测类别数据！');
        return;
    }

    //获取可用的汇总表名称
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "SummarySheet_Local.aspx?action=GetSheetType&subTaskID=" + selectedsubTaskItem.ID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vSheetType = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //弹出窗口，供客户选择汇总表
    var sheetTypeForm = $("#sheetTypeForm");
    sheetTypeForm.ligerForm({
        inputWidth: 320, labelWidth: 90, space: 0, labelAlign: 'right',
        fields: [
                      { display: "报表", name: "ddlSheetType", comboboxName: "ddlSheetType_OP", newline: false, type: "select", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "ddlSheetType", data: vSheetType} }
                    ]
    });
    $("#ddlSheetType_OP").ligerGetComboBoxManager().setData(vSheetType);

    var showSheetTypeWin = null;
    showSheetTypeWin = $.ligerDialog.open({
        target: $("#sheetType"),
        width: 450, height: 150, left: 30, top: 90, title: "请选择报表",
        buttons: [
                  { text: '确定', onclick: function () { excelExportFor_SubTask_Summary(); showSheetTypeWin.hide(); } },
                  { text: '取消', onclick: function () { showSheetTypeWin.hide(); } }
                  ]
    });

    //根据客户选择的汇总表导出Excel
    function excelExportFor_SubTask_Summary() {
        var strSheetType = $("#ddlSheetType").val();
        if (strSheetType != "") {
            var selectedTaskItem = taskGrid.getSelectedRow();
            var selectedsubTaskItem = subTaskGrid.getSelectedRow();
            if (!selectedsubTaskItem) {
                $.ligerDialog.warn('请先选择一条监测类别数据！');
                return;
            }
            detailExcelWin = $.ligerDialog.open({ url: "../SearchFunction/print_Summary_Local.aspx?isLocal=1&task_id=" + selectedTaskItem.ID + "&subtask_id=" + selectedsubTaskItem.ID + "&sheetType=" + strSheetType });
        }
    }
}