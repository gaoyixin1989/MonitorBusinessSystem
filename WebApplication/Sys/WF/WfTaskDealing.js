// Create by 潘德军 2013.01.07  "任务追踪"功能

var objGrid = null;

//任务追踪功能
$(document).ready(function () {

    //菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '追踪', click: dealData, icon: 'modify' }
            ]
    });

    objGrid = $("#objGrid").ligerGrid({
        title: '任务列表',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        width: '100%',
        height: '100%',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        pageSizeOptions: [10, 15, 20,50],
        url: 'WfTaskDealingForJS.aspx?type=getData',
        columns: [
                 { display: '开始执行时间', name: 'WF_STARTTIME', align: 'left', isSort: false, width: 200 },
                 { display: '项目名称', name: 'WF_SERVICE_NAME', align: 'left', isSort: false, width: 600 },
                 { display: '业务流程', name: 'WF_ID', align: 'left', isSort: false, width: 200, render: function (record) {
                     return getWFName(record.WF_ID);
                 } 
                 },
                 { display: '当前环节', name: 'WF_TASK_ID', align: 'left', isSort: false, width: 200, render: function (record) {
                     return getTaskName(record.WF_TASK_ID);
                 }
                 },
                 { display: '当前执行人', name: 'OBJECT_USER', align: 'left', isSort: false, width: 200, render: function (record) {
                     return getUserName(record.OBJECT_USER);
                 }
                 }
                ],
        toolbar: { items: [
                { text: '办理情况', click: dealData, icon: 'modify' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            objmenu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            dealData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //追踪
    function dealData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行追踪！');
            return;
        }

        var strId = objGrid.getSelectedRow().ID;
        var strWFId = objGrid.getSelectedRow().WF_ID;
        var strHeight = $(window).height();

        var strUrl = ("WFShowStepDetail.aspx?ID=" + strId);
        $(document).ready(function () { $.ligerDialog.open({ title: '任务追踪', url: strUrl, width: 400, height: strHeight, modal: false }); });
    }
});

//获取业务流程信息
function getWFName(strWfID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WfTaskDealingForJS.aspx/getWFName",
        data: "{'strValue':'" + strWfID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取当前环节信息
function getTaskName(strTaskID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WfTaskDealingForJS.aspx/getTaskName",
        data: "{'strValue':'" + strTaskID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取用户信息
function getUserName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WfTaskDealingForJS.aspx/getUserName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}