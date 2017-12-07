// Create by 潘德军 2013.01.07  "工作流管理"功能
var objGrid = null;

//工作流管理
$(document).ready(function () {
    //菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改流程', click: updateData, icon: 'modify' },
            { line: true },
            { text: '删除流程', click: delData, icon: 'delete' },
            { line: true },
            { text: '环节设置', click: stepData, icon: 'settings' }
            ]
    });

    objGrid = $("#objGrid").ligerGrid({
        title: '流程模板',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        width: '100%',
        height: '100%',
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "WF_CLASS_ID",
        pageSizeOptions: [10, 15, 20, 50],
        url: 'WFSettingFlowListForJS.aspx?type=getData',
        columns: [
                 { display: '流程代码', name: 'WF_ID', align: 'left', isSort: false, width: 200 },
                 { display: '流程名称', name: 'WF_CAPTION', align: 'left', isSort: false, width: 200 },
                 { display: '流程分类', name: 'WF_CLASS_ID', align: 'left', isSort: false, width: 200, render: function (record) {
                     return GetClassName(record.WF_CLASS_ID);
                 }
                 },
                 { display: '操作时间', name: 'CREATE_DATE', align: 'left', isSort: false, width: 200, render: function (record) {
                     return GetLastTime(record.CREATE_DATE, record.DEAL_DATE);
                 }
                 }
                ],

        toolbar: { items: [
                { text: '添加流程', click: addData, icon: 'add' },
                { line: true },
                { text: '修改流程', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除流程', click: delData, icon: 'delete' },
                { line: true },
                { text: '环节设置', click: stepData, icon: 'settings' }
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

    //添加流程
    function addData() {
        $.ligerDialog.open({ title: '流程信息增加', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'WFSettingFlowInputForJS.aspx'
        });
    }

    //修改流程
    function updateData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行修改！');
            return;
        }

        var strId = objGrid.getSelectedRow().ID;

        $.ligerDialog.open({ title: '流程信息编辑', top: 0, width: 700, height: 300, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'WFSettingFlowInputForJS.aspx?strid=' + strId 
        });
    }

    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "WFSettingFlowListForJS.aspx/SaveData",
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

    //删除流程
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
                    url: "WFSettingFlowListForJS.aspx/deleteData",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objGrid.loadData();
                            $.ligerDialog.success('删除数据成功')
                        }
                        else if (data.d == "2"){
                            $.ligerDialog.success('该流程已执行过，不可删除！')
                        }
                        else {
                            $.ligerDialog.warn('删除数据失败');
                        }
                    }
                });
            }
        });
    }

    //环节设置
    function stepData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行环节设置！');
            return;
        }

        var strId = objGrid.getSelectedRow().ID;
        var strWF_CAPTION = objGrid.getSelectedRow().WF_CAPTION;
        var strWF_ID = objGrid.getSelectedRow().WF_ID;

        var surl = '../SYS/WF/WFSettingTaskInputForJS.aspx?action=|type=true&ID=' + strId + '&WF_ID=' + strWF_ID;
        top.f_overTab('环节设置：' + strWF_CAPTION, surl);
    }
});

//获取流程分类信息
function GetClassName(strWF_CLASS_ID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "WFSettingFlowListForJS.aspx/GetClassName",
        data: "{'strValue':'" + strWF_CLASS_ID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取操作时间信息
function GetLastTime(CREATE_DATE,DEAL_DATE) {
    var strResult="";
    if (CREATE_DATE.length == 0)
        strResult = "";
    else if (DEAL_DATE.length == 0)
        strResult = CREATE_DATE;
    else
        strResult = DEAL_DATE;
    return strResult;
}

