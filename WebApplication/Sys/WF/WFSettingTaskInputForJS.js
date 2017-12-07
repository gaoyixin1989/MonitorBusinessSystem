// Create by 潘德军 2013.01.09  "工作流管理"功能
var objGrid = null;

//工作流管理
$(document).ready(function () {
    //菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改环节', click: updateData, icon: 'modify' },
            { line: true },
            { text: '删除环节', click: delData, icon: 'delete' },
            { line: true },
            { text: '提前一步', click: upData, icon: 'bullet_wrench' },
            { line: true },
            { text: '后退一步', click: downData, icon: 'bullet_wrench' }
            ]
    });

    var strID = request("ID");

    objGrid = $("#objGrid").ligerGrid({
        title: '环节列表',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        width: '100%',
        height: '100%',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        pageSizeOptions: [10, 15, 20, 50],
        url: 'WFSettingTaskInputForJS.aspx?type=getData&ID=' + strID,
        columns: [
                 { display: '流程名称', name: 'WF_ID', align: 'left', isSort: false, width: 200, render: function (record) {
                     return GetWFName(record.WF_ID);
                 }
                 },
                 { display: '节点名称', name: 'TASK_CAPTION', align: 'left', isSort: false, width: 200 },
                 { display: '命令集', name: 'COMMAND_NAME', align: 'left', isSort: false, width: 200, render: function (record) {
                     return GetCMDName(record.COMMAND_NAME);
                 }
                 },
                 { display: '附加功能', name: 'FUNCTION_LIST', align: 'left', isSort: false, width: 200, render: function (record) {
                     return GetFUNCName(record.FUNCTION_LIST);
                 }
                 },
                 { display: '执行顺序', name: 'TASK_ORDER', align: 'left', isSort: false, width: 200 }
                ],

        toolbar: { items: [
                { text: '添加环节', click: addData, icon: 'add' },
                { line: true },
                { text: '修改环节', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除环节', click: delData, icon: 'delete' },
                { line: true },
                { text: '提前一步', click: upData, icon: 'bullet_wrench' },
                { line: true },
                { text: '后退一步', click: downData, icon: 'bullet_wrench' },
                { line: true },
                { text: '返回流程列表', click: backUrl, icon: 'back' }
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

            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //添加环节
    function addData() {
        var strWF_ID = request("WF_ID");

        $.ligerDialog.open({ title: '环节信息增加', top: 0, width: 750, height: 500, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'WFSettingTaskInputDetailForJS.aspx?WF_ID=' + strWF_ID
        });
    }

    //修改环节
    function updateData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行修改！');
            return;
        }

        var strWF_ID = request("WF_ID");
        var strId = objGrid.getSelectedRow().WF_TASK_ID;

        $.ligerDialog.open({ title: '环节信息编辑', top: 0, width: 750, height: 500, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'WFSettingTaskInputDetailForJS.aspx?WF_ID=' + strWF_ID + '&WF_TASK_ID=' + strId
        });
    }

    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "WFSettingTaskInputForJS.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    //删除环节
    function delData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除！');
            return;
        }

        $.ligerDialog.confirm('确认删除吗？', function (yes) {
            if (yes == true) {
                var strValue = objGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "WFSettingTaskInputForJS.aspx/deleteData",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objGrid.loadData();
                            $.ligerDialog.success('删除数据成功')
                        }
                        else {
                            $.ligerDialog.warn('删除数据失败');
                        }
                    }
                });
            }
        });
    }

    //提前一步
    function upData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行排序！');
            return;
        }

        var strID = objGrid.getSelectedRow().ID;
        var strWFID = objGrid.getSelectedRow().WF_ID;
        $.ajax({
            cache: false,
            type: "POST",
            url: "WFSettingTaskInputForJS.aspx/upData",
            data: "{'strID':'" + strID + "','strWFID':'" + strWFID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('排序失败');
                }
            }
        });
    }

    //后退一步
    function downData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行排序！');
            return;
        }

        var strID = objGrid.getSelectedRow().ID;
        var strWFID = objGrid.getSelectedRow().WF_ID;
        $.ajax({
            cache: false,
            type: "POST",
            url: "WFSettingTaskInputForJS.aspx/downData",
            data: "{'strID':'" + strID + "','strWFID':'" + strWFID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('排序失败');
                }
            }
        });
    }

    //    //环节排序
    //    function orderData() {
    //        var strID = request("ID");
    //        var strWF_ID = request("WF_ID");
    //        var strWF_CAPTION = GetWFName(strWF_ID);

    //        var surl = '../SYS/WF/WFSettingTaskOrderDetailForJS.aspx?WF_ID=' + strWF_ID + '&ID=' + strID;
    //        top.f_overTab('环节排序：' + strWF_CAPTION, surl);
    //    }

    //返回流程列表
    function backUrl() {
        var surl = '../SYS/WF/WFSettingFlowListForJS.aspx';
        top.f_overTab('工作流管理', surl);
    }
});

//获取流程名称
function GetWFName(strWF_ID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WFSettingTaskInputForJS.aspx/GetWFName",
        data: "{'strValue':'" + strWF_ID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取命令集
function GetCMDName(strCOMMAND_NAME) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WFSettingTaskInputForJS.aspx/GetCMDName",
        data: "{'strValue':'" + strCOMMAND_NAME + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取附加功能
function GetFUNCName(strFUNCTION_LIST) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WFSettingTaskInputForJS.aspx/GetFUNCName",
        data: "{'strValue':'" + strFUNCTION_LIST + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}