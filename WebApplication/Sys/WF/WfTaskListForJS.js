// Create by 石磊   2012.11.05  "任务列表"功能
// update by 潘德军 2012.12.20  改为ligerui

var manager;
var menu;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });

    //质控设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '办理', click: TaskOpen, icon: 'modify' }
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
        //        { display: '认领时间', name: 'CFM_TIME', width: 150, align: 'left', isSort: false }
        ],
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        //        url: 'WfTaskListForJS.aspx?Action=GetTask&isQueding=' + request('isQueding'), 
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'modify', text: '办理', click: TaskOpen, icon: 'modify' },
                { line: true },
            //                { id: 'unconfirm', text: '撤销', click: Unconfirm, icon: 'back' },
            //                { line: true },
                {id: 'srh', text: '查询', click: TaskSrh, icon: 'search' }
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
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    function Unconfirm() {
        if (manager.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行确认');
            return;
        }
        var strValue = manager.getSelectedRow().ID;

        $.ligerDialog.confirm('是否确定撤销?\r\n', function (yes) {
            if (yes == true) {
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "WfTaskListCFMForJS.aspx/SetTaskToConfirm",
                    data: "{'strValue':'" + strValue + "','bIsUnConfirm':'true'}",
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

    //任务办理
    function TaskOpen() {
        if (manager.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行办理');
            return;
        }
        var strValue = manager.getSelectedRow().ID;

        var surl = "", urltitle = "", tabid = "";
        //tabid = "tabidTaskOpenNew";
        surl = "WFDealPage.aspx";
        surl += "?ID=" + strValue + "";
        //urltitle = GetTaskName(manager.getSelectedRow().WF_TASK_ID);

        //self.location = surl;
        surl = "../Sys/WF/" + surl;
        top.f_overTab(GetTaskName(manager.getSelectedRow().WF_TASK_ID), surl);
        //        top.f_addTab("TaskOpen_" + strValue, GetTaskName(manager.getSelectedRow().WF_TASK_ID), surl);
    }
    
    GetTaskInfo();
    WaitingTask();
    ReturnTask();
    FW_Wait();
    FW_Return();
});
//加载数据
function GetTaskInfo() {
    if (request('isQueding') != null) {
        manager.set('url', "WfTaskListForJS.aspx?Action=GetTask&isQueding=" + request('isQueding'));
    }
    else {
        manager.set('url', "WfTaskListForJS.aspx?Action=GetTask&isQueding=");
    }
}
  //待办任务(郑州需求)
  function WaitingTask() {
      var status = getQueryString("waiting");
      if (status != null) {
          manager.set('url', "WfTaskListForJS.aspx?Action=WaitingTask&status=" + status);
      }
  }
  //退回任务(郑州需求)
  function ReturnTask() {
      var back = getQueryString("Return");
      if (back != null) {
          manager.set('url', "WfTaskListForJS.aspx?Action=BackTask&back=" + back);
      }
  }
  //待办任务(郑州需求)
  function FW_Wait() {
      var fw_status = getQueryString("fw_wait");
      if (fw_status != null) {
          manager.set('url', "WfTaskListForJS.aspx?Action=FW_WaitingTask&fw_status=" + fw_status);
      }
  }
  //退回任务(郑州需求)
  function FW_Return() {
      var fw_back = getQueryString("fw_return");
      if (fw_back != null) {
          manager.set('url', "WfTaskListForJS.aspx?Action=FW_BackTask&fw_back=" + fw_back);
      }
  }
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

        manager.set('url', "WfTaskListForJS.aspx?Action=GetTask&SrhWF_SERVICE_NAME=" + SrhWF_SERVICE_NAME + "&SrhSRC_USER=" + SrhSRC_USER + "&SrhINST_TASK_STARTTIME_from=" + SrhINST_TASK_STARTTIME_from + "&SrhINST_TASK_STARTTIME_to=" + SrhINST_TASK_STARTTIME_to+"&isQueding="+request('isQueding'));
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
        url: "WfTaskListForJS.aspx/GetTaskName",
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
        url: "WfTaskListForJS.aspx/GetUserName",
        data: "{'strValue':'" + strID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}
