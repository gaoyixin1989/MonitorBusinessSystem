// Create by 邵世卓 2012.11.28  "项目查询"功能
var firstManager;
var reportId; //报告ID
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height();
    var gridHeight = $(window).height();

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //监测任务grid
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
        { display: '任务单号', name: 'TICKET_NUM', width: 100, align: 'left', isSort: false },
        { display: '委托书编号', name: 'CONTRACT_CODE', width: 150, align: 'left', isSort: false },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
        { display: '委托客户', name: 'CLIENT_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameByTask(data.CLIENT_COMPANY_ID);
        }
        },
        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameByTask(data.TESTED_COMPANY_ID);
        }
        },
        { display: '计划制定人', name: 'CREATOR_ID', width: 80, align: 'left', isSort: false, render: function (data) {
            return getUserName(data.CREATOR_ID);
        }
        },
        { display: '计划制定日期', name: 'CREATE_DATE', width: 80, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: '100%', heightDiff: -5,
        title: "任务信息",
        url: 'TaskTraking.aspx?action=GetTaskInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: clickForTask, icon: 'refresh' },
                { line: true },
                { id: 'task', text: '任务追踪', click: clickForTask, icon: 'role' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showTaskInfo();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectedTaskItem = firstManager.getSelectedRow();
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});


// Create by 邵世卓 2012.11.28  "项目查询、查看"功能
//监测任务grid 的Toolbar click事件
function clickForTask(item) {
    switch (item.id) {
        case 'srhAll':
            firstManager.set('url', "TaskTraking.aspx?action=GetTaskInfo");
            break;
        case 'task':
            showTaskInfo();
            break;
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
        var mainform = $("#searchFirstForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                      { display: "委托类型", name: "srhContractType", newline: true, width: 150, type: "select", comboboxName: "srhContractTypeID", resize: false, group: "查询信息", groupicon: groupicon, options: { valueFieldID: "srhContractType", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "TotalSearch.aspx?type=getDict&typeid=Contract_Type"} },
                      { display: "任务单号", name: "srhReportCode", newline: false, type: "text" },
                      { display: "年度", name: "srhYear", newline: true, width: 150, type: "select", comboboxName: "dropYear", options: { isMultiSelect: false, valueFieldID: "srhYear", valueField: "ID", textField: "YEAR", url: "../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetContratYear"} },
                      { display: "项目名称", name: "srhProjectName", newline: false, type: "text" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 600, height: 200, left: 30, top: 90, title: "查询任务信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhContractType = $("#srhContractType").val();
        var SrhReportCode = $("#srhReportCode").val();
        var SrhProjectName = escape($("#srhProjectName").val());
        var SrhYear = $("#srhYear").val();

        firstManager.set('url', "TaskTraking.aspx?Action=GetTaskInfo&SrhContractType=" + SrhContractType + "&SrhReportCode=" + SrhReportCode + "&SrhProjectName=" + SrhProjectName + "&SrhYear=" + SrhYear);
    }
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#srhContractType").val("");
    $("#srhReportCode").val("");
    $("#srhProjectName").val("");
    $("#srhYear").val("");
}

//获取委托类型名称
function getContractTypeForSearch(type) {
    var strReturn;
    $.ajax({
        async: false,
        type: "POST",
        url: "TotalSearch.aspx/getContractType",
        data: "{'strValue':'" + type + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}

//获取企业名称
function getCompanyNameForSearch(client_id) {
    var strReturn = "";
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearch.aspx/getCompanyName",
        data: "{'strValue':'" + client_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}
//获取任务企业名称
function getCompanyNameByTask(client_id) {
    var strReturn = "";
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearch.aspx/getCompanyNameByTask",
        data: "{'strValue':'" + client_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}
//获取质控手段
function getQcType(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../Mis/Report/ReportSchedule.aspx/getQcType",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}
//获取姓名
function getUserName(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearch.aspx/getUserName",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}

//监测任务流程追踪
function showTaskInfo() {
    var selectedTaskItem = firstManager.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var flow_id;
    var winHeight;
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalTraking.aspx?type=getFlowInfo&business_id=" + selectedTaskItem.ID,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                var strList = data.split('|');
                if (strList.length > 1) {
                    flow_id = strList[0]; //流程ID
                    winHeight = strList[1]; //窗体高度
                }
            }
        }
    });
    var strUrl = ("../../../Sys/WF/WFShowStepDetail.aspx?ID=" + flow_id);
    $(document).ready(function () { $.ligerDialog.open({ title: '任务追踪', url: strUrl, width: 375, height: winHeight, modal: false }); });
}



