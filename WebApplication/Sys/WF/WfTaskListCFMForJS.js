var manager;
var menu;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });

    //质控设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '认领', click: TaskOpen, icon: 'modify' }
            ]
    });

    //grid
    window['g'] =
    manager = $("#maingrid").ligerGrid({
        columns: [
        { display: '任务单号', name: 'WF_SERVICE_CODE', width: 200, align: 'left', isSort: false },
        { display: '任务名称', name: 'WF_SERVICE_NAME', width: 500, align: 'left', isSort: false },
        { display: '当前环节', name: 'lblWF_TASK_CAPTION', width: 200, align: 'left', isSort: false, render: function (record) {
            return GetTaskName(record.WF_TASK_ID);
        }
        },
        { display: '发送人', name: 'SRC_USER', width: 150, align: 'left', isSort: false, render: function (record) {
            return GetUserName(record.SRC_USER);
        }
        },
        { display: '发送时间', name: 'INST_TASK_STARTTIME', width: 150, align: 'left', isSort: false }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
        url: 'WfTaskListCFMForJS.aspx?Action=GetTask',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'modify', text: '确认', click: TaskOpen, icon: 'modify' },
                { line: true },
                { id: 'srh', text: '查询', click: TaskSrh, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
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
    function TaskOpen() {
        if (manager.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行操作');
            return;
        }
        var strValue = manager.getSelectedRow().ID;

        $.ligerDialog.confirm('是否认领此任务?\r\n', function (yes) {
            if (yes == true) {
                $.ajax({
                //弹出一个窗口即可
                    cache: false,
                    type: "POST",
                    url: "WfTaskListCFMForJS.aspx/SetTaskToConfirm",
                    data: "{'strValue':'" + strValue + "','bIsUnConfirm':'false'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == true) {
                            $.ligerDialog.success('数据操成功！');
                            manager.loadData();
                        }
                        else {
                            $.ligerDialog.warn('数据操作失败！');
                            return;
                        }
                    },
                    error: function (msg) {
                        $.ligerDialog.warn('AJAX数据加载失败!');
                    }
                });
            }

        })
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

        manager.set('url', "WfTaskListCFMForJS.aspx.aspx?Action=GetTask&SrhWF_SERVICE_NAME=" + SrhWF_SERVICE_NAME + "&SrhSRC_USER=" + SrhSRC_USER + "&SrhINST_TASK_STARTTIME_from=" + SrhINST_TASK_STARTTIME_from + "&SrhINST_TASK_STARTTIME_to=" + SrhINST_TASK_STARTTIME_to);
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
        url: "WfTaskListCFMForJS.aspx.aspx/GetTaskName",
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
        url: "WfTaskListCFMForJS.aspx/GetUserName",
        data: "{'strValue':'" + strID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}