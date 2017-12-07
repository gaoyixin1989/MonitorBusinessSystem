// Create by 潘德军 2013.7.1  "综合查询--委托书查询"功能

var contractGrid, taskGrid, subTaskGrid;
var contractGrid1, taskGrid1, subTaskGrid1;
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
var objToolbar = null;

var step_type = null; //黄进军添加20140901

var gridName = "0";

//黄进军添加20140901
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

    step_type = $.getUrlVar('step_type'); //黄进军添加20140901

    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            navtab = $("#navtab1").ligerGetTabManager();
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid == "home") {
                gridName = "0";
                contractGrid.loadData();
                if (taskGrid != null)
                    taskGrid.set('url', "ContractSrh.aspx?action=selectTask&contract_id=null");
                if (subTaskGrid != null)
                    subTaskGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");
            }
            if (tabid == 'tabitem1') {
                gridName = "1";
                contractGrid1.loadData();
                if (taskGrid != null)
                    taskGrid.set('url', "ContractSrh.aspx?action=selectTask&contract_id=null");
                if (subTaskGrid != null)
                    subTaskGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");
            }
        }
    });

    $("#layout1").ligerLayout({ width: '98%', leftWidth: '50%', rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: '100%', topHeight: topHeight });
    $("#layout2").ligerLayout({ width: '98%', leftWidth: '50%', rightWidth: "50%", allowLeftCollapse: false, allowRightCollapse: false, height: '100%', topHeight: topHeight });

    //委托书grid菜单
    menuContract = $.ligerMenu({ width: 120, items:
            [
            { id: 'contract', text: '委托书信息', click: showContract, icon: 'archives' },
            { id: 'point', text: '点位', click: showPoint, icon: 'database' }
            // { id: 'task', text: '执行情况', click: showContractStep, icon: 'role' }
            ]
    });
    if (request("step_type") == "QY") {
        objToolbar = { items: [
                 { id: 'srhAll', text: '所有记录', click: showAll, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'contract', text: '委托书信息', click: showContract, icon: 'archives' },
                { id: 'point', text: '点位', click: showPoint, icon: 'database' },
               // { id: 'task', text: '执行情况', click: showContractStep, icon: 'role' },
                { id: 'supervise', text: '任务督办', click: showSuperDialog, icon: 'role' }
                ]
        };
    }
    else {
        objToolbar = { items: [
                 { id: 'srhAll', text: '所有记录', click: showAll, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'contract', text: '委托书信息', click: showContract, icon: 'archives' },
                { id: 'point', text: '点位', click: showPoint, icon: 'database' },
               // { id: 'task', text: '执行情况', click: showContractStep, icon: 'role' },
               // { id: 'tasklist', text: '执行情况列表', click: showTotalFlowList, icon: 'role' },
            //huangjinjun 20140509
                {id: 'print', text: '打印', click: btnPrint, icon: 'add' },
            //end
                {id: 'uploadFA', text: '方案下载', click: upLoadFA, icon: 'add' }, { line: true },
                { id: 'loadCCFlowTrack', text: '任务信息轨迹', click: loadCCFlowTrack, icon: 'add' }
                ]
        };
    }
    //未办结委托书grid
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
        { display: '报告编制人', name: 'PROJECT_ID', width: 100, align: 'left', isSort: false, render: function (data) {
            return getReportUserName("", data.ID);
        }
        },
        { display: '办理状态', name: 'CONTRACT_SATAUS', width: 100, align: 'left', isSort: false, render: function (data) {
            if (data.CONTRACT_STATUS == '0' || data.CONTRACT_STATUS == '00') {
                return "<a style='color:Red'>委托书未提交</a>";
            }
            else if (data.CONTRACT_STATUS == "1") {
                return "委托书流转中";
            }
            else if (data.CONTRACT_STATUS == '9') {
                if (request("step_type") == "QY")
                    return "委托书任务已编制";
                else
                    return "委托书已办结";
            }
            return data.CONTRACT_STATUS;
        }
        },
         { display: '流程状态', name: 'CCFlowStatus', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "委托书信息",
        url: 'ContractSrh.aspx?Action=SelectContract&Status=0',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: objToolbar,
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

            if (subTaskGrid) {
                //subTaskGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=" + taskGrid.rows[0].ID);
            }

        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });

    //已办结委托书grid
    contractGrid1 = $("#contractGrid1").ligerGrid({
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
        { display: '报告编制人', name: 'PROJECT_ID', width: 100, align: 'left', isSort: false, render: function (data) {
            return getReportUserName("", data.ID);
        }
        },
        { display: '办理状态', name: 'CONTRACT_SATAUS', width: 100, align: 'left', isSort: false, render: function (data) {
            if (data.CONTRACT_STATUS == '0' || data.CONTRACT_STATUS == '00') {
                return "<a style='color:Red'>委托书未提交</a>";
            }
            else if (data.CONTRACT_STATUS == "1") {
                return "委托书流转中";
            }
            else if (data.CONTRACT_STATUS == '9') {
                if (request("step_type") == "QY")
                    return "委托书任务已编制";
                else
                    return "委托书已办结";
            }
            return data.CONTRACT_STATUS;
        }
    },
         { display: '流程状态', name: 'CCFlowStatus', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "委托书信息",
        url: 'ContractSrh.aspx?Action=SelectContract&Status=1',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: objToolbar,
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
            //            if (subTaskGrid)
            //                subTaskGrid.set('url', "ContractSrh.aspx?Action=selectsubTask&task_id=null");
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
        if (gridName == "0")
            contractGrid.set('url', "ContractSrh.aspx?Action=SelectContract&Status=0&SrhContractType=" + SrhContractType + "&SrhContractCode=" + SrhContractCode + "&SrhProjectName=" + SrhProjectName + "&SrhYear=" + SrhYear);
        else
            contractGrid1.set('url', "ContractSrh.aspx?Action=SelectContract&Status=1&SrhContractType=" + SrhContractType + "&SrhContractCode=" + SrhContractCode + "&SrhProjectName=" + SrhProjectName + "&SrhYear=" + SrhYear);
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
    if (gridName == "0")
        contractGrid.set('url', "ContractSrh.aspx?Action=SelectContract&Status=0");
    else
        contractGrid1.set('url', "ContractSrh.aspx?Action=SelectContract&Status=1");
}

function showTaskGrid(strContractID) {
    var divName = "";
    //菜单
    menuTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'sample', text: '样品信息', click: showSample, icon: 'process' },
            { id: 'report', text: '报告', click: showReport, icon: 'pager' },
            { id: 'excel', text: '数据汇总表', click: excelExportForTask, icon: 'excel' },
            { id: 'task', text: '任务追踪', click: showTaskStep, icon: 'role' }
            ]
    });

    //监测任务grid 黄进军修改20140901
    if (step_type == "QY") {
        if (gridName == "0")
            divName = "taskGrid";
        else
            divName = "taskGrid1";

        taskGrid = $("#" + divName).ligerGrid({
            columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 150, align: 'left', isSort: false },
        { display: '任务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
            //{ display: '要求完成日期', name: 'ASKING_DATE', width: 80, align: 'left', isSort: false },//黄进军注释
        {display: '完成日期', name: 'FINISH_DATE', width: 80, align: 'left', isSort: false },
        { display: '样品来源', name: 'SAMPLE_SOURCE', width: 80, align: 'left', isSort: false },
        { display: '任务状态', name: 'TASK_STATUS', width: 140, align: 'left', isSort: false, render: function (data) {
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
                { id: 'excel', text: '数据汇总表', click: excelExportForTask, icon: 'excel' }
               // { id: 'task', text: '执行情况', click: showTaskStep, icon: 'role' }
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
            onBeforeCheckAllRow: function (checked, grid, element) { return false; },
            isChecked: function (r) {
                if (r.__index == 0) {
                    showSubTaskGrid(r.ID); return true;
                }
            }
        });
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    } else {
        if (gridName == "0")
            divName = "taskGrid";
        else
            divName = "taskGrid1";

        taskGrid = $("#" + divName).ligerGrid({
            columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 150, align: 'left', isSort: false },
        { display: '任务类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
        { display: '要求完成日期', name: 'ASKING_DATE', width: 80, align: 'left', isSort: false },
        { display: '完成日期', name: 'FINISH_DATE', width: 80, align: 'left', isSort: false },
        { display: '样品来源', name: 'SAMPLE_SOURCE', width: 80, align: 'left', isSort: false },
        { display: '任务状态', name: 'TASK_STATUS', width: 140, align: 'left', isSort: false, render: function (data) {
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
                { id: 'excel', text: '数据汇总表', click: excelExportForTask, icon: 'excel' }
                //{ id: 'task', text: '执行情况', click: showTaskStep, icon: 'role' }
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
}

function showSubTaskGrid(strTaskId) {
    var divName = "";
    //菜单
    menuSubTask = $.ligerMenu({ width: 120, items:
            [
            { id: 'result', text: '结果信息', click: showResult, icon: 'process' },
            { id: 'excel', text: '数据汇总表', click: excelExportForSubTask, icon: 'excel' },
            { id: 'subtask', text: '任务追踪', click: showTaskStep, icon: 'role' }
            ]
    });
    if (gridName == "0")
        divName = "subTaskGrid";
    else
        divName = "subTaskGrid1";

    //监测任务grid
    subTaskGrid = $("#" + divName).ligerGrid({
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
                { id: 'excel', text: '数据汇总表', click: excelExportForSubTask, icon: 'excel' }
               // { id: 'subtask', text: '执行情况', click: showSubTaskStep, icon: 'role' }
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

//huangjinun 20140509
//打印
function btnPrint() {
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();

    if (selectedConItem == null) {
        $.ligerDialog.warn("请先选择一条数据！");
        return;
    } else {
        $("#cphData_hiddid").val(selectedConItem.ID);
        $("#cphData_btnExport").click();
    }

}

function upLoadFA() {
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();

    if (selectedConItem == null) {
        $.ligerDialog.warn("请先选择一条数据！");
        return;
    }
    if (rowID.CONTRACT_TYPE == "05") {
        $.ligerDialog.open({ title: '方案下载', width: 800, height: 350, isHidden: false,
            buttons: [
             { text: '直接下载', onclick: function (item, dialog) {
                 dialog.frame.aa(); //调用下载按钮
             }
             },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttMoreFileDownLoad.aspx?filetype=Contract&id=' + selectedConItem.ID
        });
    } else {
        $.ligerDialog.warn("只有验收监测才有方案下载！");
        return;
    }
}

//任务执行情况列表信息
function showTotalFlowList() {
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();

    if (selectedConItem == null) {
        $.ligerDialog.warn('请选择一条记录进行查看');
        return;
    }
    $.ligerDialog.open({ title: "执行情况列表查看", width: 740, height: 530, isHidden: false, buttons:
        [
        { text:
        '返回', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../TrackInfo.aspx?ContractID=" + contractGrid.getSelectedRow().ID + "&TaskID="
    });
}
//督办任务对话框
function showSuperDialog() {
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();

    if (selectedConItem == null) {
        $.ligerDialog.warn('请选择一条任务进行督办');
        return;
    }


    $.ligerDialog.open({ title: '任务督办', top: 0, width: 580, height: 480, buttons:
        [{ text: '督办', onclick: SendMsg },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: '../../../OA/Message/MessageInfo.aspx?supercontent=' + selectedConItem.PROJECT_NAME
    });
}
function SendMsg(item, dialog) {
    var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
    var strdata = fn();

    $.ajax({
        cache: false,
        type: "POST",
        url: "../../../OA/Message/MessageSendList.aspx/SendMsg",
        data: strdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                $.ligerDialog.success('督办成功！');
                dialog.close();
            }
            else {
                $.ligerDialog.warn('督办失败！');
            }
        }
    });

}

 
function loadCCFlowTrack() {

    var selectedTaskItem = contractGrid.getSelectedRow();
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
            url: '../../../../WF/WorkOpt/OneWork/ChartTrack.aspx?FID=' + selectedTaskItem.FID + '&FK_Flow=' + selectedTaskItem.FK_Flow + '&WorkID=' + selectedTaskItem.CCFLOW_ID1 + '&FK_Node=' + selectedTaskItem.FK_Node
        });
    }
    else {
        $.ligerDialog.warn('未走CCFlow！');
    }

}

