// Create by 潘德军 2013.7.1  "综合查询--委托书查询"功能

var contractGrid, taskGrid, subTaskGrid;
var menuContract, menuTask, menuSubTask;
var pathUrl = "../";
var groupicon = pathUrl + "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var webServiceUrl = pathUrl + "TotalSearch.aspx";
var wfStepUrl = pathUrl + "../../../Sys/WF/WFShowStepDetail.aspx";
var AcceptanceCreateUrl = pathUrl + "../../Mis/Contract/Acceptance/AcceptanceCreate.aspx";
var FileEditUrl = pathUrl + "../../Rpt/Template/FileEdit.aspx";

var topHeight = $(window).height() / 5 * 3;
var gridHeight = $(window).height() / 5 * 3;
var gridHeightEx = $(window).height() / 5 * 2;

var reportId; //报告ID

$(document).ready(function () {
    $("#layout1").ligerLayout({ width: '98%', leftWidth: '50%', rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: '100%', topHeight: topHeight });

    //委托书grid菜单
    menuContract = $.ligerMenu({ width: 120, items:
            [
            { id: 'contract', text: '委托书信息', click: showContract, icon: 'archives' },
            { id: 'point', text: '点位', click: showPoint, icon: 'database' },
             { id: 'task', text: '执行情况', click: showContractStep, icon: 'role' }
            ]
    });
    //委托书grid
    contractGrid = $("#contractGrid").ligerGrid({
        columns: [
        { display: '委托年度', name: 'CONTRACT_YEAR', width: 60, align: 'left', isSort: false },
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left', isSort: false },
        { display: '委托单号', name: 'CONTRACT_CODE', width: 150, align: 'left', isSort: false },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
        { display: '委托客户', name: 'CLIENT_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameForSearch(data.CLIENT_COMPANY_ID);
        }
        },
        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameForSearch(data.TESTED_COMPANY_ID);
        }
        },
        { display: '项目负责人', name: 'PROJECT_ID', width: 100, align: 'left', isSort: false, render: function (data) {
            return getUserName(data.PROJECT_ID);
        }
        },
        { display: '办理状态', name: 'CONTRACT_SATAUS', width: 100, align: 'left', isSort: false, render: function (data) {
            if (data.CONTRACT_STATUS == '0') {
                return "<a style='color:Red'>委托书未提交</a>";
            }
            else if (data.CONTRACT_STATUS == "1") {
                return "委托书流转中";
            }
            else if (data.CONTRACT_STATUS == '9') {
                return "委托书已办结";
            }
            return data.CONTRACT_STATUS;
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "委托书信息",
        url: 'ContractSrh.aspx?Action=SelectContract',
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
                { id: 'contract', text: '委托书信息', click: showContract, icon: 'archives' },
                { id: 'point', text: '点位', click: showPoint, icon: 'database' },
                { id: 'task', text: '执行情况', click: showContractStep, icon: 'role' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showContract();
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            var selectedConItem = contractGrid.getSelectedRow();
            showTaskGrid(selectedConItem.ID);
            if (subTaskGrid)
                subTaskGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");

            menuContract.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            showTaskGrid(rowdata.ID);
            if (subTaskGrid)
                subTaskGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");
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
                      { display: "委托年度", name: "srhYear", newline: true, group: "查询信息", groupicon: groupicon, width: 150, type: "select", comboboxName: "dropYear", options: { isMultiSelect: false, valueFieldID: "srhYear", valueField: "ID", textField: "YEAR", url: "../../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetContratYear"} },
                      { display: "委托单号", name: "srhContractCode", newline: false, type: "text" },
                      { display: "委托类型", name: "srhContractType", newline: true, width: 150, type: "select", comboboxName: "srhContractTypeID", resize: false, options: { valueFieldID: "srhContractType", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "../TotalSearch.aspx?type=getDict&typeid=Contract_Type"} },
                      { display: "项目名称", name: "srhProjectName", newline: false, type: "text" }
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
        var SrhContractType = $("#srhContractType").val();
        var SrhContractCode = $("#srhContractCode").val();
        var SrhProjectName = escape($("#srhProjectName").val());
        var SrhYear = $("#srhYear").val();

        contractGrid.set('url', "ContractSrh.aspx?Action=SelectContract&SrhContractType=" + SrhContractType + "&SrhContractCode=" + SrhContractCode + "&SrhProjectName=" + SrhProjectName + "&SrhYear=" + SrhYear);
    }

    //grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#srhContractType").val("");
        $("#srhContractCode").val("");
        $("#srhProjectName").val("");
        $("#srhYear").val("");
    }
}

function showAll() {
    contractGrid.set('url', "ContractSrh.aspx?Action=SelectContract");
}

function showTaskGrid(strContractID) {
    //菜单
    menuTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'sample', text: '样品信息', click: showSample, icon: 'process' },
            { id: 'report', text: '报告', click: showReport, icon: 'pager' },
            { id: 'task', text: '任务追踪', click: showTaskStep, icon: 'role' }
            ]
    });

    //监测任务grid
    taskGrid = $("#taskGrid").ligerGrid({
        columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 200, align: 'left', isSort: false },
        { display: '要求完成日期', name: 'ASKING_DATE', width: 80, align: 'left', isSort: false },
        { display: '完成日期', name: 'FINISH_DATE', width: 80, align: 'left', isSort: false },
        { display: '样品来源', name: 'SAMPLE_SOURCE', width: 80, align: 'left', isSort: false },
        { display: '任务状态', name: 'TASK_STATUS', width: 80, align: 'left', isSort: false, render: function (data) {
            return getTaskStatusName(data.TASK_STATUS, data.QC_STATUS);
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: 'ContractSrh.aspx?action=selectTask&contract_id=' + strContractID,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'sample', text: '样品信息', click: showSample, icon: 'process' },
                { id: 'report', text: '报告', click: showReport, icon: 'pager' },
                { id: 'task', text: '执行情况', click: showTaskStep, icon: 'role' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            var selectedConItem = taskGrid.getSelectedRow();
            showSubTaskGrid(selectedConItem.ID);
            menuTask.show({ top: e.pageY, left: e.pageX });
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
}

function showSubTaskGrid(strTaskId) {
    //菜单
    menuSubTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'result', text: '结果信息', click: showResult, icon: 'process' },
            { id: 'subtask', text: '任务追踪', click: showTaskStep, icon: 'role' }
            ]
    });

    //监测任务grid
    subTaskGrid = $("#subTaskGrid").ligerGrid({
        columns: [
        { display: '监测类别', name: 'MONITOR_NAME', width: 100, align: 'left', isSort: false, render: function (data) {
            return getMonitorName(data.MONITOR_ID);
        }
        },
        { display: '采样人', name: 'SAMPLING_MAN', width: 80, align: 'left', isSort: false },
        { display: '采样日期', name: 'SAMPLE_ASK_DATE', width: 80, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: 'ContractSrh.aspx?action=selectsubTask&task_id=' + strTaskId,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'result', text: '结果信息', click: showResult, icon: 'process' },
                { id: 'subtask', text: '执行情况', click: showSubTaskStep, icon: 'role' }
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
}
