// Create by 潘德军 2013.7.6  "结果数据可追溯性"功能
var sampleGrid, taskGrid, resultGrid;
var menuTask;
var pathUrl = "../";
var groupicon = pathUrl + "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var webServiceUrl = pathUrl + "TotalSearch.aspx";
var wfStepUrl = pathUrl + "../../../Sys/WF/WFShowStepDetail.aspx";
var AcceptanceCreateUrl = pathUrl + "../../Mis/Contract/Acceptance/AcceptanceCreate.aspx";
var FileEditUrl = pathUrl + "../../Rpt/Template/FileEdit.aspx";
var strUrl = "ResultLog.aspx";

var topHeight = $(window).height() / 2;
var gridHeight = $(window).height() / 2;
var gridHeightEx = $(window).height() / 2;

var reportId; //报告ID

$(document).ready(function () {
    $("#layout1").ligerLayout({ width: '98%', leftWidth: '50%', rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: '100%', topHeight: topHeight });

    //菜单
    menuTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'report', text: '报告', click: showReport, icon: 'pager' }
            ]
    });

    //监测任务grid
    taskGrid = $("#taskGrid").ligerGrid({
        columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 200, align: 'left', isSort: false },
        { display: '委托年度', name: 'CONTRACT_YEAR', width: 80, align: 'left', isSort: false },
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left', isSort: false },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getTaskCompanyNameForSearch(data.TESTED_COMPANY_ID);
        }
        },
        { display: '要求完成日期', name: 'ASKING_DATE', width: 80, align: 'left', isSort: false },
        { display: '完成日期', name: 'FINISH_DATE', width: 80, align: 'left', isSort: false },
        { display: '样品来源', name: 'SAMPLE_SOURCE', width: 80, align: 'left', isSort: false },
        { display: '项目负责人', name: 'PROJECT_ID', width: 100, align: 'left', isSort: false, render: function (data) {
            return getUserName(data.PROJECT_ID);
        }
        },
        { display: '任务状态', name: 'TASK_STATUS', width: 80, align: 'left', isSort: false, render: function (data) {
            return getTaskStatusName(data.TASK_STATUS, data.QC_STATUS);
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "任务信息",
        url: 'ResultLog.aspx?Action=selectTask',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: showAll, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'report', text: '报告', click: showReport, icon: 'pager' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            var selectedConItem = taskGrid.getSelectedRow();
            showSampleGrid(selectedConItem.ID);
            if (resultGrid)
                resultGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");

            menuTask.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            showSampleGrid(rowdata.ID);

            if (resultGrid)
                resultGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");
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
        var mainform = $("#searchTaskForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                      { display: "委托年度", name: "srhYear", newline: true, group: "查询信息", groupicon: groupicon, width: 150, type: "select", comboboxName: "dropYear", options: { isMultiSelect: false, valueFieldID: "srhYear", valueField: "ID", textField: "YEAR", url: "../../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetContratYear"} },
                      { display: "任务单号", name: "srhTICKET_NUM", newline: false, type: "text" },
                      { display: "委托类型", name: "srhContractType", newline: true, width: 150, type: "select", comboboxName: "srhContractTypeID", resize: false, options: { valueFieldID: "srhContractType", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "../TotalSearch.aspx?type=getDict&typeid=Contract_Type"} },
                      { display: "项目名称", name: "srhProjectName", newline: false, type: "text" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#searchTaskForm"),
            width: 600, height: 200,  title: "查询委托信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhContractType = $("#srhContractType").val();
        var SrhTICKET_NUM = $("#srhTICKET_NUM").val();
        var SrhProjectName = escape($("#srhProjectName").val());
        var SrhYear = $("#srhYear").val();

        taskGrid.set('url', "ResultLog.aspx?Action=selectTask&SrhContractType=" + SrhContractType + "&SrhTICKET_NUM=" + SrhTICKET_NUM + "&SrhProjectName=" + SrhProjectName + "&SrhYear=" + SrhYear);
    }

    //grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#srhContractType").val("");
        $("#srhTICKET_NUM").val("");
        $("#srhProjectName").val("");
        $("#srhYear").val("");

        $("#srhContractTypeID").ligerGetComboBoxManager().setValue("");
        $("#dropYear").ligerGetComboBoxManager().setValue("");
    }
}

function showAll() {
    taskGrid.set('url', "ResultLog.aspx?Action=selectTask");
}

function showSampleGrid(strTaskId) {
    //监测任务grid
    sampleGrid = $("#sampleGrid").ligerGrid({
        columns: [
        { display: '监测类别', name: 'MONITOR_TYPE_NAME', width: 100, align: 'left', isSort: false},
        { display: '样品号', name: 'SAMPLE_CODE', width: 100, align: 'left', isSort: false },
        { display: '样品名', name: 'SAMPLE_NAME', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: 'ResultLog.aspx?action=selectSample&task_id=' + strTaskId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: showAllSample, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: showSampleSrh, icon: 'search' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showResultGrid(rowdata.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
}

//grid 的查询对话框
var SampleSrh = null;
function showSampleSrh() {
    if (SampleSrh) {
        SampleSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchSampleForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", resize: true, comboboxName: "MONITOR_Type_ID", group: "查询条件", groupicon: groupicon, options: { valueFieldID: "MONITOR_ID", url: "../../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} }
                    ]
        });

        SampleSrh = $.ligerDialog.open({
            target: $("#searchSampleForm"),
            width: 300, height: 150, title: "查询信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); SampleSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); SampleSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhItemType = $("#MONITOR_ID").val();

        var selectedConItem = taskGrid.getSelectedRow();
        if (selectedConItem)
            sampleGrid.set('url', 'ResultLog.aspx?Action=selectSample&task_id=' + selectedConItem.ID + '&type_id=' + SrhItemType);
        else {
            $.ligerDialog.warn('请先选择一条监测任务！');
            return;
        }
    }

    //grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#MONITOR_ID").val("");
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue("");
    }
}

function showAllSample() {
    var selectedConItem = taskGrid.getSelectedRow();
    if (selectedConItem)
        sampleGrid.set('url', 'ResultLog.aspx?Action=selectTask&task_id=' + selectedConItem.ID);
    else {
        $.ligerDialog.warn('请先选择一条监测任务！');
        return;
    }
}

function showResultGrid(strSampleId) {
    //监测任务grid
    resultGrid = $("#resultGrid").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_ID', align: 'left', width: 200, minWidth: 60, frozen: true, render: function (record) {
            return getItemInfoName(record.ITEM_ID, "ITEM_NAME");
        }
        },
        { display: '结果', name: 'ITEM_RESULT', align: 'left', width: 100, minWidth: 60 },
        { display: '原始单号', name: 'REMARK_2', align: 'left', width: 150, minWidth: 60 },
        { display: '分析方法', name: 'ANALYSIS_NAME', align: 'left', width: 200, minWidth: 60 },
        { display: '检出限', name: 'LOWER_CHECKOUT', align: 'left', width: 100, minWidth: 60 },
        { display: '单位', name: 'UNIT', align: 'left', width: 100, minWidth: 60 },
        { display: '仪器', name: 'APPARATUS_NAME', align: 'left', width: 200, minWidth: 60 }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: 'ResultLog.aspx?action=selectResult&sample_id=' + strSampleId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srh', text: '历史记录', click: showHistory, icon: 'search' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
}

function showHistory(strSampleId) {
    var selectedConItem = resultGrid.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }

    var surl = 'ResultLogInfo.aspx?resultid=' + selectedConItem.ID;
    $(document).ready(function () { $.ligerDialog.open({ title: '历史记录', url: surl, width: 800, height: 600, modal: false }); });
}