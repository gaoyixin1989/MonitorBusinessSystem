var manager;
var menu;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });

    //grid
    window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '任务单号/委托单号', name: 'WF_SERVICE_CODE', width: 150, align: 'left', isSort: false },
        { display: '任务名称', name: 'WF_SERVICE_NAME', width: 300, align: 'left', isSort: false },
        { display: '当前流程', name: 'WF_CAPTION', width: 100, align: 'left', isSort: false },
        { display: '当前环节', name: 'lblWF_TASK_CAPTION', width: 100, align: 'left', isSort: false, render: function (record) {
            return GetTaskName(record.WF_TASK_ID);
        }
        },
        { display: '环节状态', name: 'INST_TASK_STATE', width: 50, align: 'left', isSort: false, render: function (record) {
            return GetStepStateName(record.INST_TASK_STATE);
        }
        },
        { display: '办理人', name: 'OBJECT_USER', width: 60, align: 'left', isSort: false, render: function (record) {
            return GetUserName(record.OBJECT_USER);
        }
        },
        { display: '确认时间', name: 'CFM_TIME', width: 150, align: 'left', isSort: false },
        { display: '发送人', name: 'SRC_USER', width: 60, align: 'left', isSort: false, render: function (record) {
            return GetUserName(record.SRC_USER);
        }
        },
        { display: '发送时间', name: 'INST_TASK_STARTTIME', width: 150, align: 'left', isSort: false }

        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
    url: 'WfTaskListAllForJS.aspx?Action=GetTask' + "&isAllOrDeptOrSelf=" + request('isAllOrDeptOrSelf'),
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                //{ id: 'modify', text: '详细', click: TaskView, icon: 'modify' },
                //{ line: true },
                { id: 'srh', text: '查询', click: TaskSrh, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            TaskOpen();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //任务办理
    function TaskView() {
        if (manager.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录');
            return;
        }
        var strValue = manager.getSelectedRow().ID;
        //弹出对话框即可，地址为:WFTaskDetailView.aspx?ID=strValue;

        $.ligerDialog.open({ title: "该环节信息概述", width: 500, height: 320, buttons:
        [
        { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "WFTaskDetailView.aspx?ID=" + strValue
        });
    }
});

//弹出查询对话框
var detailWinSrh = null;
function TaskSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "任务名称", name: "SrhWF_SERVICE_NAME", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "发送人", name: "SrhSRC_USER", newline: false, type: "text"},
                      { display: "发送时间", name: "SrhINST_TASK_STARTTIME_from", newline: true, type: "date", format: "yyyy-MM-dd", showTime: "false" },
                      { display: "至", name: "SrhINST_TASK_STARTTIME_to", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 650, height: 200, top: 90, title: "查询任务",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhWF_SERVICE_NAME = escape($("#SrhWF_SERVICE_NAME").val());
        var SrhSRC_USER = escape($("#SrhSRC_USER").val());
        var SrhINST_TASK_STARTTIME_from = $("#SrhINST_TASK_STARTTIME_from").val();
        var SrhINST_TASK_STARTTIME_to = $("#SrhINST_TASK_STARTTIME_to").val();

        manager.set('url', "WfTaskListAllForJS.aspx?Action=GetTask&SrhWF_SERVICE_NAME=" + SrhWF_SERVICE_NAME + "&SrhSRC_USER=" + SrhSRC_USER + "&SrhINST_TASK_STARTTIME_from=" + SrhINST_TASK_STARTTIME_from + "&SrhINST_TASK_STARTTIME_to=" + SrhINST_TASK_STARTTIME_to+"&isAllOrDeptOrSelf="+request('isAllOrDeptOrSelf'));
    }
}

function clearSearchDialogValue() {
    $("#SrhWF_SERVICE_NAME").val("");
    $("#SrhSRC_USER").val("");
    $("#SrhINST_TASK_STARTTIME_from").val("");
    $("#SrhINST_TASK_STARTTIME_to").val("");
}

//发送人
function GetTaskName(strID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WfTaskListAllForJS.aspx/GetTaskName",
        data: "{'strValue':'" + strID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//当前环节
function GetUserName(strID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WfTaskListAllForJS.aspx/GetUserName",
        data: "{'strValue':'" + strID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//当前环节
function GetStepStateName(strID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WfTaskListAllForJS.aspx/GetStepStateName",
        data: "{'strStepState':'" + strID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}