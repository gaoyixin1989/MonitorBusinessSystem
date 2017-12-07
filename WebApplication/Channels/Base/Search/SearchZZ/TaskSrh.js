// Create by 潘德军 2013.7.1  "综合查询--监测任务查询"功能
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

var contract_type = ""; //黄进军添加20141028

//黄进军添加20141028
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
    $("#layout1").ligerLayout({ width: '98%', leftWidth: '50%', rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: '100%', topHeight: topHeight });

    //菜单
    menuTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'sample', text: '样品信息', click: showSample, icon: 'process' },
            { id: 'report', text: '报告', click: showReport, icon: 'pager' },
            { id: 'excel', text: '数据汇总表', click: excelExportForTask, icon: 'excel' },
            { id: 'task', text: '任务追踪', click: showTask, icon: 'role' }
            ]
    });

    //监测任务grid
    taskGrid = $("#taskGrid").ligerGrid({
        columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 200, align: 'left', isSort: false },
        //{ display: '委托年度', name: 'CONTRACT_YEAR', width: 80, align: 'left', isSort: false },
        { display: '项目名称', name: 'PROJECT_NAME', width: 400, align: 'left', isSort: false },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
//        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
//            return getTaskCompanyNameForSearch(data.TESTED_COMPANY_ID);
//        }
//        },
        { display: '要求完成日期', name: 'ASKING_DATE', width: 100, align: 'left', isSort: false },
        //{ display: '完成日期', name: 'FINISH_DATE', width: 80, align: 'left', isSort: false },
        {display: '样品来源', name: 'SAMPLE_SOURCE', width: 100, align: 'left', isSort: false },
       { display: '流程状态', name: 'CCFlowStatus', width: 100, align: 'left', isSort: false }
        //{ display: '报告编制人', name: 'PROJECT_ID', width: 100, align: 'left', isSort: false, render: function (data) {
        //    return getReportUserName(data.ID, "");
        //}
       // },
//        { display: '任务状态', name: 'TASK_STATUS', width: 80, align: 'left', isSort: false, render: function (data) {
//            return getTaskStatusName(data.TASK_STATUS, data.QC_STATUS);
//        }
//        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "任务信息",
        url: "TaskSrh.aspx?Action=selectTask",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                //{ id: 'srhAll', text: '所有记录', click: showAll, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'sample', text: '样品信息', click: showSample, icon: 'process' },
                //{ id: 'report', text: '报告', click: showReport, icon: 'pager' },
                //{ id: 'excel', text: '数据汇总表', click: excelExportForTask, icon: 'excel' },
                { id: 'task', text: '任务信息轨迹', click: showTask, icon: 'role'}//,
            //{ id: 'tasklist', text: '执行情况列表', click: showTotalFlowList, icon: 'role' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            var selectedConItem = taskGrid.getSelectedRow();
            showSubTaskGrid(selectedConItem.ID);
            //showContractGrid(selectedConItem.CONTRACT_ID);
            menuTask.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            showSubTaskGrid(rowdata.ID);
            //showContractGrid(rowdata.CONTRACT_ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//监测任务流程追踪
function showTask() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }

    if (selectedTaskItem.CCFlowStatus == '未走CCFLOW') {

        $.ligerDialog.warn('未走CCFlow！');
        return;
    }
   
       
  
    if (selectedTaskItem.CCFLOW_ID1 != null && selectedTaskItem.CCFLOW_ID1 != '') {
        $.ligerDialog.open({ title: '轨迹图', width: 900, height: 600, isHidden: false,
            url: '../../../../WF/WorkOpt/OneWork/ChartTrack.aspx?FID=' + selectedTaskItem.FID + '&FK_Flow=' + selectedTaskItem.FK_Flow + '&WorkID=' + selectedTaskItem.CCFLOW_ID1 +'&FK_Node=' + selectedTaskItem.FK_Node
        });
    }
    else {
        $.ligerDialog.warn('未走CCFlow！');
    }
}

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
                      //{ display: "监测年度", name: "srhYear", newline: true, group: "查询信息", groupicon: groupicon, width: 150, type: "select", comboboxName: "dropYear", options: { isMultiSelect: false, valueFieldID: "srhYear", valueField: "ID", textField: "YEAR", url: "../../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetContratYear"} },
                      {display: "任务单号", name: "srhTICKET_NUM", newline: true, type: "text" },
                      //{ display: "委托类型", name: "srhContractType", newline: true, width: 150, type: "select", comboboxName: "srhContractTypeID", resize: false, options: { valueFieldID: "srhContractType", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "../TotalSearch.aspx?type=getDict&typeid=Contract_Type"} },
                      {display: "项目名称", name: "srhProjectName", newline: false, type: "text" }
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
        //var SrhContractType = $("#srhContractType").val();
        var SrhTICKET_NUM = $("#srhTICKET_NUM").val();
        var SrhProjectName = escape($("#srhProjectName").val());
        //var SrhYear = $("#srhYear").val();

        taskGrid.set('url', "TaskSrh.aspx?Action=selectTask&SrhTICKET_NUM=" + SrhTICKET_NUM + "&SrhProjectName=" + SrhProjectName);
    }

    //grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        //$("#srhContractType").val("");
        $("#srhTICKET_NUM").val("");
        $("#srhProjectName").val("");
        //$("#srhYear").val("");
    }
}

function showAll() {
    taskGrid.set('url', "TaskSrh.aspx?Action=selectTask");
}

function showContractGrid(strContractID) {
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
        { display: '委托单号', name: 'CONTRACT_CODE', width: 100, align: 'left', isSort: false },
        { display: '委托客户', name: 'CLIENT_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameForSearch(data.CLIENT_COMPANY_ID);
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
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: 'TaskSrh.aspx?action=SelectContract&contract_id=' + strContractID,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'contract', text: '委托书信息', click: showContract, icon: 'archives' },
                { id: 'point', text: '点位', click: showPoint, icon: 'database' },
                { id: 'task', text: '执行情况', click: showContractStep, icon: 'role' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menuContract.show({ top: e.pageY, left: e.pageX });
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

function showSubTaskGrid(strTaskId) {
    //菜单
    menuSubTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'result', text: '结果信息', click: showResult, icon: 'process' },
            { id: 'excel', text: '数据汇总表', click: excelExportForSubTask, icon: 'excel' },
            { id: 'subtask', text: '任务追踪', click: showSubTaskStep, icon: 'role' }
            ]
    });

    //监测任务grid
    subTaskGrid = $("#subTaskGrid").ligerGrid({
        columns: [
        { display: '监测类别', name: 'MONITOR_NAME', width: 200, align: 'left', isSort: false, render: function (data) {
            return getMonitorName(data.MONITOR_ID);
        }
        },
        { display: '采样人', name: 'SAMPLING_MAN', width: 100, align: 'left', isSort: false },
        { display: '采样日期', name: 'SAMPLE_ASK_DATE', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeightEx, heightDiff: -30,
        title: "",
        url: 'TaskSrh.aspx?action=selectsubTask&task_id=' + strTaskId,
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
//        toolbar: { items: [
//                { id: 'result', text: '结果信息', click: showResult, icon: 'process' },
//                { id: 'excel', text: '数据汇总表', click: excelExportForSubTask, icon: 'excel' },
//                { id: 'subtask', text: '执行情况', click: showSubTaskStep, icon: 'role' }
//                ]
//        },
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

//任务执行情况列表信息
function showTotalFlowList() {
    if (taskGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一条记录进行查看');
        return;
    }
    $.ligerDialog.open({ title: "执行情况列表查看", width: 740, height: 530, isHidden: false, buttons:
        [
        { text:
        '返回', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../TrackInfo.aspx?ContractID=&TaskID=" + taskGrid.getSelectedRow().ID
    });
}