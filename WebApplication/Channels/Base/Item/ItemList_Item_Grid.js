// Create by 潘德军 2012.11.05  "项目管理的项目列表"功能

//监测项目grid 的Toolbar click事件
var ItemCataLogTable = null;
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'add':
            showDetail(null, true);
            break;
        case 'modify':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetail({
                MONITOR_ID: selected.MONITOR_ID,
                MONITOR_NAME: selected.MONITOR_NAME,
                ITEM_NAME: selected.ITEM_NAME,
                ORDER_NUM: selected.ORDER_NUM,
                ITEM_NUM: selected.ITEM_NUM,
                LAB_CERTIFICATE: selected.LAB_CERTIFICATE,
                MEASURE_CERTIFICATE: selected.MEASURE_CERTIFICATE,
                IS_SAMPLEDEPT: selected.IS_SAMPLEDEPT,
                IS_ANYSCENE_ITEM: selected.IS_ANYSCENE_ITEM,
                ORI_CATALOG_TABLEID:selected.ORI_CATALOG_TABLEID,
                ItemID: selected.ID
    
            }, false);

            break;
        case 'del':
            var rows = manager.getCheckedRows();
            var strDelID = "";
            $(rows).each(function () {
                strDelID += (strDelID.length > 0 ? "," : "") + this.ID;
            });

            if (strDelID.length == 0) {
                $.ligerDialog.warn('请先选择要删除的记录！');
            }
            else {
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delItem(strDelID);
                });
            }

            break;
        case 'srh':
            showDetailSrh();
            break;
        case 'toBag':
            var surl = '../Channels/Base/Item/ItemBag.aspx?MenuNo=toBag';
            top.f_addTab('toBag', '项目包设置', surl);
            break;
        case 'toQC':
            var surl = '../Channels/Base/Item/ItemQCSet.aspx?MenuNo=toQC';
            top.f_addTab('toQC', '质控标准设置', surl);
            break;
        case 'toFee':
            var surl = '../Channels/Base/TestPrice/ItemPrice.aspx?MenuNo=toFee';
            top.f_addTab('toFee', '监测单价设置', surl);
            break;
        default:
            break;
    }
}

//监测项目grid 的右键菜单
function itemclick_OfMenu_UnderItem(item) {
    switch (item.id) {
        case 'menumodify':
            showDetail({
                MONITOR_ID: actionMONITOR_ID,
                MONITOR_NAME: actionMONITOR_NAME,
                ITEM_NAME: actionITEM_NAME,
                ORDER_NUM: actionORDER_NUM,
                ITEM_NUM: actionITEM_NUM,
                LAB_CERTIFICATE: actionLAB_CERTIFICATE,
                MEASURE_CERTIFICATE: actionMEASURE_CERTIFICATE,
                IS_SAMPLEDEPT: actionIS_SAMPLEDEPT,
                IS_ANYSCENE_ITEM: actionIS_ANYSCENE_ITEM,
                ORI_CATALOG_TABLEID: selected.ORI_CATALOG_TABLEID,
                ItemID: actionItemID
            }, false);

            break;
        case 'menudel':
            var strDelID = actionItemID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delItem(strDelID);
            });

            break;
        default:

    }
}

$.ajax({
    cache: false,
    async: false, //设置是否为异步加载,此处必须
    type: "POST",
    url: "ItemList.aspx?Action=GetDefineTable&strWhere=%DUSTATTRIBUTE%",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        if (data.Total != "0") {
            ItemCataLogTable = data.Rows;
        }
        else {
            $.ligerDialog.warn('获取数据失败！');
        }
    },
    error: function (msg) {
        $.ligerDialog.warn('Ajax加载数据失败！' + msg);
    }
});

//监测项目grid 的删除函数
function delItem(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "ItemList.aspx/deleteItem",
        data: "{'strDelIDs':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                manager.loadData();
            }
            else {
                $.ligerDialog.warn('删除监测项目数据失败！');
            }
        }
    });
}

//监测项目grid 的编辑对话框及save函数
var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editItemForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", resize: true, comboboxName: "MONITOR_Type_ID", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "MONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "监测项目", name: "ITEM_NAME", newline: false, type: "text", validate: { required: true, minlength: 3} },
                      { display: "现场监测", name: "IS_SAMPLEDEPT", newline: true, type: "select", resize: false, comboboxName: "IS_SAMPLEDEPT_1", options: { valueFieldID: "IS_SAMPLEDEPT", url: "../MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|IS_SAMPLEDEPT"} },
                      { display: "实验室认可", name: "LAB_CERTIFICATE", newline: false, type: "select", resize: false, comboboxName: "LAB_CERTIFICATE_1", options: { valueFieldID: "LAB_CERTIFICATE", url: "../MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|LAB_CERTIFICATE"} },
                      { display: "实验室认可", name: "MEASURE_CERTIFICATE", newline: true, type: "select", resize: false, comboboxName: "MEASURE_CERTIFICATE_1", options: { valueFieldID: "MEASURE_CERTIFICATE", url: "../MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|MEASURE_CERTIFICATE"} },
                      { display: "项目代码", name: "ITEM_NUM", newline: false, type: "text", options: { type: "int"} },
                      { display: "序号", name: "ORDER_NUM", newline: true, type: "spinner", options: { type: "int"} },
                      { display: "分析现场监测", name: "IS_ANYSCENE_ITEM", newline: false, type: "select", resize: false, comboboxName: "IS_ANYSCENE_ITEM_1", options: { valueFieldID: "IS_ANYSCENE_ITEM", url: "../MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|company_yesno"} },
                      { display: "原始记录表", name: "ORI_CATALOG_TABLEID", newline: true, type: "select", resize: false, comboboxName: "ORI_CATALOG_TABLEID_1", options: { data: ItemCataLogTable, valueFieldID: 'ORI_CATALOG_TABLEID_OP', valueField: 'table_name', textField: 'table_name'} }
                    ]
        });


    $("#IS_ANYSCENE_ITEM_1").ligerComboBox({isMultiSelect: false,
        onSelected: function (newvalue) {
            var strIsxc = $("#IS_SAMPLEDEPT").val();
            if (newvalue == "1" && strIsxc == "是") {
                $.ligerDialog.warn('分析类现场监测项目与现场监测项目不能同时为【<a style="color:Red">是</a>】');
                $("#IS_ANYSCENE_ITEM_1").ligerGetComboBoxManager().setValue("0");
                return;
            }
        }
    });
    $("#IS_SAMPLEDEPT_1").ligerComboBox({ isMultiSelect: false,
        onSelected: function (newvalue) {
            var strIsxc = $("#IS_ANYSCENE_ITEM").val();
            if (newvalue == "是" && strIsxc == "1") {
                $.ligerDialog.warn('现场监测项目与分析类现场监测项目不能同时为【<a style="color:Red">是</a>】');
                $("#IS_SAMPLEDEPT_1").ligerGetComboBoxManager().setValue("否");
                return;
            }
        }
    });
        detailWin = $.ligerDialog.open({
            target: $("#detail"),
            width: 650, height: 250, top: 90, title: "监测项目信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        //$("#Item_ID").val(curentData.ItemID);
        $("#MONITOR_ID").val(curentData.MONITOR_ID);
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue(curentData.MONITOR_ID);
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setDisabled();
        $("#ITEM_NAME").val(curentData.ITEM_NAME);
        $("#LAB_CERTIFICATE").val(curentData.LAB_CERTIFICATE);
        $("#LAB_CERTIFICATE_1").ligerGetComboBoxManager().setValue(curentData.LAB_CERTIFICATE);
        $("#MEASURE_CERTIFICATE").val(curentData.MEASURE_CERTIFICATE);
        $("#MEASURE_CERTIFICATE_1").ligerGetComboBoxManager().setValue(curentData.MEASURE_CERTIFICATE);
        $("#ORDER_NUM").val(curentData.ORDER_NUM);
        $("#ITEM_NUM").val(curentData.ITEM_NUM);
        $("#IS_SAMPLEDEPT").val(curentData.IS_SAMPLEDEPT);
        $("#IS_SAMPLEDEPT_1").ligerGetComboBoxManager().setValue(curentData.IS_SAMPLEDEPT);
        $("#IS_ANYSCENE_ITEM_1").ligerGetComboBoxManager().setValue(curentData.IS_ANYSCENE_ITEM);
        $("#ORI_CATALOG_TABLEID_1").ligerGetComboBoxManager().setValue(curentData.ORI_CATALOG_TABLEID);
        
    }
    else {
        $("#MONITOR_ID").val("");
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue("");
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setEnabled();
        $("#IS_SAMPLEDEPT").val("否");
        $("#IS_SAMPLEDEPT_1").ligerGetComboBoxManager().setValue("否");
        $("#LAB_CERTIFICATE").val("是");
        $("#LAB_CERTIFICATE_1").ligerGetComboBoxManager().setValue("是");
        $("#MEASURE_CERTIFICATE").val("是");
        $("#MEASURE_CERTIFICATE_1").ligerGetComboBoxManager().setValue("是");
        $("#ORDER_NUM").val("0");

        $("#MONITOR_ID").val("");
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue("");
        $("#IS_ANYSCENE_ITEM_1").ligerGetComboBoxManager().setValue("0");
        $("#ORI_CATALOG_TABLEID_1").ligerGetComboBoxManager().setValue("");
    }

    function save() {
        var strData = "{";
        strData += currentIsAddNew ? "" : "'strID':'" + curentData.ItemID + "',";
        strData += currentIsAddNew ? "'strMONITOR_ID':'" + $("#MONITOR_ID").val() + "'," : ""; ;
        strData += "'strITEM_NAME':'" + $("#ITEM_NAME").val() + "',";
        strData += "'strLAB_CERTIFICATE':'" + $("#LAB_CERTIFICATE").val() + "',";
        strData += "'strMEASURE_CERTIFICATE':'" + $("#MEASURE_CERTIFICATE").val() + "',";
        strData += "'strORDER_NUM':'" + $("#ORDER_NUM").val() + "',";
        strData += "'strITEM_NUM':'" + $("#ITEM_NUM").val() + "',";
        strData += "'strIS_SAMPLEDEPT':'" + $("#IS_SAMPLEDEPT").val() + "',";
        strData += "'strIS_ANYSCENE':'" + $("#IS_ANYSCENE_ITEM").val() + "',";
        strData += "'strORI_CATALOG_TABLEID':'" + $("#ORI_CATALOG_TABLEID_OP").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "ItemList.aspx/" + (currentIsAddNew ? "AddItem" : "EditItem"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWin.hidden();
                    manager.loadData();
                    clearDialogValue();
                }
                else {
                    $.ligerDialog.warn('保存监测项目数据失败！');
                }
            }
        });
    }
}

//监测项目grid 的编辑对话框元素的值 清除
function clearDialogValue() {
    $("#Item_ID").val("");
    $("#MONITOR_ID").val("");
    $("#ITEM_NAME").val("");
    $("#LAB_CERTIFICATE").val("");
    $("#MEASURE_CERTIFICATE").val("");
    $("#ORDER_NUM").val("");
    $("#IS_ANYSCENE_ITEM_1").ligerGetComboBoxManager().setValue("0");
}

//监测项目grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchItemForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "SrhMONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", resize: false, group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhMONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "监测项目", name: "SrhITEM_NAME", newline: true, type: "text", validate: { required: true, minlength: 3} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhMONITOR_ID = $("#SrhMONITOR_ID").val();
        var SrhITEM_NAME = escape($("#SrhITEM_NAME").val());

        manager.set('url', "ItemList.aspx?Action=GetItems&SrhMONITOR_ID=" + SrhMONITOR_ID + "&SrhITEM_NAME=" + SrhITEM_NAME);
    }
}

//监测项目grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SrhMONITOR_ID").val("");
    $("#SrhMONITOR_TYPE_ID").val("");
    $("#SrhITEM_NAME").val("");
}

