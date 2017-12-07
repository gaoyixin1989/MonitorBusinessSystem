// Create by 魏林 2013.07.16  "收文办理功能"

var objGrid = null;
var strUrl = "SWHandleList.aspx";
var strHandleUrl = "SWHandle.aspx";
var strHandleReadUrl = "SWHandleRead.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {

    //收文办理列表
    objGrid = $("#grid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?action=getGridInfo',
        columns: [
                { display: '任务编号', name: 'TASK_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '任务名称', name: 'TASK_NAME', align: 'left', width: 250, minWidth: 60 },
                { display: '当前环节', name: 'TASK_STATUS', align: 'left', width: 150, minWidth: 60, render: function (item) {
                    switch (item.TASK_STATUS) {
                        case "0":
                            return "未提交";
                            break;
                        case "1":
                            return "主任阅示";
                            break;
                        case "2":
                            return "站长阅示";
                            break;
                        case "3":
                            return "分管阅办";
                            break;
                        case "4":
                            return "科室办结";
                            break;
                        case "5":
                            return "办公室归档";
                            break;
                        case "6":
                            return "已办结";
                            break;
                    }
                }
                },
                { display: '发送人', name: 'SEND_USER', align: 'left', width: 150, minWidth: 60 },
                { display: '发送时间', name: 'SEND_DATE', align: 'left', width: 150, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '办理', click: Handle, icon: 'modify' },
                { text: '查询', click: ShowSearch, icon: 'search'}
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

            Handle();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//办理
function Handle() {
    var HandleTitle = "收文任务办理";
    if (!objGrid.getSelectedRow()) {
        $.ligerDialog.warn('请选择一条任务进行办理');
        return;
    }

    var strHandlePageUrl = strHandleUrl;
    switch (objGrid.getSelectedRow().TASK_STATUS) {
        case "0":
            HandleTitle = "未提交";
            break;
        case "1":
            HandleTitle = "主任阅示";
            break;
        case "2":
            HandleTitle = "站长阅示";
            break;
        case "3":
            HandleTitle = "分管阅办";
            break;
        case "4":
            HandleTitle = "科室办结";
            break;
        case "5":
            HandleTitle = "办公室归档";
            strHandlePageUrl = strHandleReadUrl;
            break;
        case "6":
            HandleTitle = "已办结";
            break;
    }
    var surl = '../Channels/OA/SW/ZZ/' + strHandlePageUrl + '?action=Handle&ID=' + objGrid.getSelectedRow().ID + '&Status=' + objGrid.getSelectedRow().TASK_STATUS;
    top.f_overTab(HandleTitle, surl);
}

//弹出查询对话框
var searchDialog = null;
function ShowSearch() {
    if (searchDialog) {
        searchDialog.show();
    }
    else {
        //创建表单结构
        var divDetail = $("#divDetail");
        divDetail.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "任务名称", name: "TASKNAME", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "发送人", name: "SENDUSER", newline: false, type: "text" },
                      { display: "发送时间", name: "SENDDATE_from", newline: true, type: "date", format: "yyyy-MM-dd", showTime: "false" },
                      { display: "至", name: "SENDDATE_to", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false" }
                    ]
        });

        searchDialog = $.ligerDialog.open({
            target: $("#divSearchForm"),
            width: 650, height: 200, top: 90, title: "查询任务",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); searchDialog.hide(); } }
                  ]
        });
    }


}

function search() {
    var TASKNAME = escape($("#TASKNAME").val());
    var SENDUSER = escape($("#SENDUSER").val());
    var SENDDATE_from = $("#SENDDATE_from").val();
    var SENDDATE_to = $("#SENDDATE_to").val();

    objGrid.set('url', strUrl + '?action=getGridInfo&TASKNAME=' + TASKNAME + '&SENDUSER=' + SENDUSER + '&SENDDATE_from=' + SENDDATE_from + '&SENDDATE_to=' + SENDDATE_to);
}

function clearSearchDialogValue() {
    $("#TASKNAME").val("");
    $("#SENDUSER").val("");
    $("#SENDDATE_from").val("");
    $("#SENDDATE_to").val("");
}