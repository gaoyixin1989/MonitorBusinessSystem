// Create by 潘德军 2012.11.05  "下拉框弹出grid进行选择"功能

//cancel按钮
function selectCancel(item, dialog) {
    dialog.close();
}

//弹出国标grid
function NATIONAL_select() {
    if ($("#MONITOR_ID").val() == "") {
        $.ligerDialog.warn('请先选择监测类型!');
        return;
    }

    $.ligerDialog.open({ title: '选择国标条件项', name: 'winselector', width: 700, height: 370, url: 'SelectST.aspx?stType=01&MONITOR_ID=' + $("#MONITOR_ID").val() + "&selNode=" + $("#hidNATIONAL_ST_CON").val(), 
    buttons: [
                { text: '确定', onclick: NATIONAL_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}

//国标弹出grid ok按钮
function NATIONAL_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择国标条件项!');
        return;
    }
    $("#NATIONAL_ST_CONDITION_ID").val(data.split("|")[1]);
    $("#hidNATIONAL_ST_CON").val(data.split("|")[0]);
    dialog.close();
}

//弹出地标grid
function LOCAL_select() {
    if ($("#MONITOR_ID").val() == "") {
        $.ligerDialog.warn('请先选择监测类型!');
        return;
    }

    $.ligerDialog.open({ title: '选择地标条件项', name: 'winselector', width: 700, height: 370, url: 'SelectST.aspx?stType=02&MONITOR_ID=' + $("#MONITOR_ID").val() + "&selNode=" + $("#hidLOCAL_ST_CON").val(),
         buttons: [
                { text: '确定', onclick: LOCAL_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}

//地标弹出grid ok按钮
function LOCAL_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择地标条件项!');
        return;
    }
    $("#LOCAL_ST_CONDITION_ID").val(data.split("|")[1]);
    $("#hidLOCAL_ST_CON").val(data.split("|")[0]);
    dialog.close();
}

//弹出行标grid
function INDUSTRY_select() {
    if ($("#MONITOR_ID").val() == "") {
        $.ligerDialog.warn('请先选择监测类型!');
        return;
    }

    $.ligerDialog.open({ title: '选择行标条件项', name: 'winselector', width: 700, height: 370, url: 'SelectST.aspx?stType=03&MONITOR_ID=' + $("#MONITOR_ID").val() + "&selNode=" + $("#hidINDUSTRY_ST_CON").val(), 
    buttons: [
                { text: '确定', onclick: INDUSTRY_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
    });
    return false;
}

//行标弹出grid ok按钮
function INDUSTRY_selectOK(item, dialog) {
    var fn = dialog.frame.f_select || dialog.frame.window.f_select;
    var data = fn();
    if (!data) {
        $.ligerDialog.warn('请选择行标条件项!');
        return;
    }
    $("#INDUSTRY_ST_CONDITION_ID").val(data.split("|")[1]);
    $("#hidINDUSTRY_ST_CON").val(data.split("|")[0]);
    dialog.close();
}

//弹出监测项目grid
function Item_select() {
    $.ligerDialog.open({ title: '选择监测项目', name: 'winselector', width: 700, height: 370, url: '../Item/SelectItem.aspx?monitorId=' + strMonitorID,
        buttons: [
                { text: '确定', onclick: Item_selectOK },
                { text: '取消', onclick: selectCancel }
            ]
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
    $("#ITEM_ID").val(data.ITEM_NAME);
    $("#hidITEM_ID").val(data.ID);
    dialog.close();
}