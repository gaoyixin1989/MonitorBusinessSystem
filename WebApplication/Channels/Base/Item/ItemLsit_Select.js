// Create by 潘德军 2012.11.05  "下拉框弹出grid进行选择"功能

//cancel按钮
function selectCancel(item, dialog) {
    dialog.close();
}

//弹出分析方法grid
function Method_select() {
    $.ligerDialog.open({ title: '选择分析方法', name: 'winselector', width: 700, height: 370, url: 'SelectMethod.aspx?MethodSel_ItemID='+$("#hidMethodSel_ItemID").val(), buttons: [
                { text: '确定', onclick: Method_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}

//分析方法弹出grid ok按钮
function Method_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择分析方法!');
        return;
    }
    $("#Method_ItemANALYSISID").val(data.METHOD_CODE + "," + data.ANALYSIS_NAME);
    $("#hidMethod_ItemANALYSISID").val(data.ID);
    dialog.close();
}

//弹出仪器grid
function Apparatus_select() {
    $.ligerDialog.open({ title: '选择分析仪器', name: 'winselector', width: 700, height: 370, url: 'SelectApparatus.aspx', buttons: [
                { text: '确定', onclick: Apparatus_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}

//仪器弹出grid ok按钮
function Apparatus_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择分析仪器!');
        return;
    }
    $("#APPARATUS_ID").val(data.NAME);
    $("#hidAPPARATUS_ID").val(data.ID);
    dialog.close();
}

//弹出监测项目grid
function Item_select() {
    var selectedBag = managerBag.getSelectedRow();
    var strMonitorID = selectedBag.MONITOR_ID;

    $.ligerDialog.open({ title: '属性信息编辑', name: 'winselector', width: 500, height: 400, buttons: [
                { text: '确定', onclick: Item_selectOK },
                { text: '取消', onclick: selectCancel }
               ], url: 'SelectItem.aspx?monitorId=' + strMonitorID
    });

    return false;
}

//监测项目弹出grid ok按钮
function Item_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择监测项目!');
        return;
    }
    $("#ItemID").val(data.ITEM_NAME);
    $("#hidItemID").val(data.ID);
    dialog.close();
}
