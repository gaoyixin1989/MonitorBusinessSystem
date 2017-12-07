// Create by 潘德军 2012.11.05  "项目管理的指定项目的分析方法设置列表"功能

//分析方法grid的toolbar click事件
function itemclick_OfToolbar_UnderItemMethod(item) {
    switch (item.id) {
        case 'add':
            showDetailMthod(null, true);
            break;
        case 'modify':
            var selected = managerMethod.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetailMthod({
                Method_ID: selected.ID,
                Method_ItemID: selected.ITEM_ID,
                Method_ItemMethodID: selected.METHOD_ID,
                Method_ItemANALYSISID: selected.ANALYSIS_METHOD_ID,
                Method_ItemINSTRUMENTID: selected.INSTRUMENT_ID,
                Method_ItemMETHOD: selected.METHOD,
                Method_ItemANALYSIS_METHOD: selected.ANALYSIS_METHOD,
                Method_ItemINSTRUMENT: selected.INSTRUMENT,
                Method_UnitCode: selected.Unitcode,
                Method_PRECISION: selected.PRECISION,
                Method_UPPER_LIMIT: selected.UPPER_LIMIT,
                Method_LOWER_LIMIT: selected.LOWER_LIMIT,
                Method_LOWER_CHECKOUT: selected.LOWER_CHECKOUT,
                Method_IS_DEFAULT: selected.IS_DEFAULT
            }, false);

            break;
        case 'del':
            var selectedItem = manager.getSelectedRow();
            var selectedMethod = managerMethod.getSelectedRow();

            if (!selectedItem || selectedItem.ID.length == 0) {
                $.ligerDialog.warn('请先选择监测项目！');
            }
            else if (!selectedMethod || selectedMethod.ID.length == 0) {
                $.ligerDialog.warn('请先选择分析方法！');
            }
            else {
                var strDefItemID = selectedItem.ID;
                var strDefMethodID = selectedMethod.ID;
                jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                    if (confirm)
                        delMethod(strDefItemID, strDefMethodID);
                });
            }

            break;
        case 'def':
            var selectedItem = manager.getSelectedRow();
            var selectedMethod = managerMethod.getSelectedRow();

            if (!selectedItem || selectedItem.ID.length == 0) {
                $.ligerDialog.warn('请先选择监测项目！');
            }
            else if (!selectedMethod || selectedMethod.ID.length == 0) {
                $.ligerDialog.warn('请先选择分析方法！');
            }
            else {
                var strDefItemID = selectedItem.ID;
                var strDefMethodID = selectedMethod.ID;
                defMethod(strDefItemID, strDefMethodID);
            }

            break;
        default:
            break;
    }
}

//分析方法grid的右键菜单
function itemclick_OfMenu_UnderItemMethod(item) {
    var selectedItem = manager.getSelectedRow();
    var strDefItemID = selectedItem.ID;

    switch (item.id) {
        case 'menumodify':
            showDetailMthod({
                Method_ID: actionMethod_ID,
                Method_ItemID: actionMethod_ItemID,
                Method_ItemMethodID: actionMethod_ItemMethodID,
                Method_ItemANALYSISID: actionMethod_ItemANALYSISID,
                Method_ItemINSTRUMENTID: actionMethod_ItemINSTRUMENTID,
                Method_ItemMETHOD: actionMethod_ItemMethod,
                Method_ItemANALYSIS_METHOD: actionMethod_ItemANALYSIS,
                Method_ItemINSTRUMENT: actionMethod_ItemINSTRUMENT,
                Method_UnitCode: actionMethod_UnitCode,
                Method_PRECISION: actionMethod_PRECISION,
                Method_UPPER_LIMIT: actionMethod_UPPER_LIMIT,
                Method_LOWER_LIMIT: actionMethod_LOWER_LIMIT,
                Method_LOWER_CHECKOUT: actionMethod_LOWER_CHECKOUT,
                Method_IS_DEFAULT: actionMethod_IS_DEFAULT
            }, false);

            break;
        case 'menudel':
            var strDelID = actionMethod_ID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delMethod(strDefItemID, strDelID);
            });

            break;
        case 'menudef':
            var strDefID = actionMethod_ID;

            defMethod(strDefItemID, strDefID);

            break;
        default:
            break;
    }
}

//分析方法grid的删除函数
function delMethod(strDefItemID, strDefMethodID) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "ItemList.aspx/delMethod",
        data: "{'strDelIDs':'" + strDefMethodID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                var selectedItem = manager.getSelectedRow();
                managerMethod.set('url', "ItemList.aspx?Action=GetMethods&selItemID=" + strDefItemID);
            }
            else {
                $.ligerDialog.warn('删除分析方法数据失败！');
            }
        }
    });
}

//分析方法grid的默认分析方法函数
function defMethod(strItemID, strMethodID) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "ItemList.aspx/defMethod",
        data: "{'strItemID':'" + strItemID + "','strMethodID':'" + strMethodID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                managerMethod.set('url', "ItemList.aspx?Action=GetMethods&selItemID=" + strItemID);
            }
            else {
                $.ligerDialog.warn('设置常用分析方法失败！');
            }
        }
    });
}

//分析方法grid的编辑对话框及save函数
var detailWinMethod = null, curentDataMethod = null, currentIsAddNewMethod;
function showDetailMthod(data, isAddNew) {
    var selectedItem = manager.getSelectedRow();
    if (!selectedItem) {
        $.ligerDialog.warn('请先选择监测项目！');
        return;
    }
    var strItemId = selectedItem.ID;
    $("#hidMethodSel_ItemID").val(strItemId);

    curentDataMethod = data;
    currentIsAddNewMethod = isAddNew;
    if (detailWinMethod) {
        detailWinMethod.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editMethodForm");
        mainform.ligerForm({
            labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { name: "Method_ID", type: "hidden" },
                      { display: "监测项目", name: "Method_ItemID", width: 170, newline: true, type: "select", comboboxName: "Method_ItemID1", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "Method_ItemID", url: "../MonitorType/Select.ashx?view=T_BASE_ITEM_INFO&idfield=ID&textfield=ITEM_NAME&where=is_del|0"} },
                      { name: "hidMethod_ItemANALYSISID", type: "hidden" },
                      { display: "分析方法", name: "Method_ItemANALYSISID", width: 450, newline: true, type: "select" },
                      { name: "hidAPPARATUS_ID", type: "hidden" },
                      { display: "分析仪器", name: "APPARATUS_ID", width: 170, newline: true, type: "select" },
                      { display: "最低检出限", name: "LOWER_CHECKOUT", newline: false, type: "text" },
                      { display: "单位", name: "UnitCode", width: 170, newline: true, type: "select", comboboxName: "Unit", options: { valueFieldID: "UnitCode", url: "../MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|item_unit"} },
                      { display: "小数点位数", name: "PRECISION", newline: false, type: "spinner", options: { type: "int"} }
                    ]
        });
        //add validate by ssz
        $("#LOWER_CHECKOUT").attr("validate", "[{maxlength:16,msg:'最低检出限最大长度为16'}]")

        $("#Method_ItemANALYSISID").ligerComboBox({
            onBeforeOpen: Method_select, valueFieldID: 'hidMethod_ItemANALYSISID'
        });
        $("#APPARATUS_ID").ligerComboBox({
            onBeforeOpen: Apparatus_select, valueFieldID: 'hidAPPARATUS_ID'
        });

        detailWinMethod = $.ligerDialog.open({
            target: $("#detailMethod"),
            width: 650, height: 250, top: 90, title: "监测项目信息",
            buttons: [
                  { text: '确定', onclick: function () { saveMethod(); } },
                  { text: '取消', onclick: function () { clearDialogValue_Method(); detailWinMethod.hide(); } }
                  ]
        });
    }
    if (curentDataMethod) {
        $("#Method_ID").val(curentDataMethod.Method_ID);
        $("#hidMethod_ItemANALYSISID").val(curentDataMethod.Method_ItemANALYSISID);
        $("#Method_ItemANALYSISID").val(curentDataMethod.Method_ItemMETHOD + "," + curentDataMethod.Method_ItemANALYSIS_METHOD);
        $("#hidAPPARATUS_ID").val(curentDataMethod.Method_ItemINSTRUMENTID);
        $("#APPARATUS_ID").val(curentDataMethod.Method_ItemINSTRUMENT);
        $("#UnitCode").val(curentDataMethod.Method_UnitCode);
        $("#Unit").ligerGetComboBoxManager().setValue(curentDataMethod.Method_UnitCode);
        $("#LOWER_CHECKOUT").val(curentDataMethod.Method_LOWER_CHECKOUT);
        $("#PRECISION").val(curentDataMethod.Method_PRECISION);
    }
    else {
        $("#PRECISION").val("0");
        $("#UnitCode").val("");
        $("#Unit").ligerGetComboBoxManager().setValue(""); //初始化单位
    }

    $("#Method_ItemID").val(selectedItem.ID);
    $("#Method_ItemID1").ligerGetComboBoxManager().setValue(selectedItem.ID);
    $("#Method_ItemID1").ligerGetComboBoxManager().setDisabled();
    $("#Method_ItemID1").val(selectedItem.ITEM_NAME);
    function saveMethod() {
        //表单验证
        if (!$("#editMethodForm").validate())
            return false;
        if ($("#hidMethod_ItemANALYSISID").val().length == 0) {
            $.ligerDialog.warn('请选择分析方法！');
            return;
        }

        var strData = "{";
        strData += currentIsAddNewMethod ? "" : "'strID':'" + $("#Method_ID").val() + "',";
        strData += currentIsAddNewMethod ? "'strItemID':'" + $("#Method_ItemID").val() + "'," : ""; ;
        strData += "'strANALYSISID':'" + $("#hidMethod_ItemANALYSISID").val() + "',";
        strData += "'strAPPARATUS_ID':'" + $("#hidAPPARATUS_ID").val() + "',";
        strData += "'strLOWER_CHECKOUT':'" + $("#LOWER_CHECKOUT").val() + "',";
        strData += "'strUnitCode':'" + $("#UnitCode").val() + "',";
        strData += "'strPRECISION':'" + $("#PRECISION").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "ItemList.aspx/" + (currentIsAddNewMethod ? "AddMehod" : "EditMehod"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedItem1 = manager.getSelectedRow();
                    var strItemId1 = selectedItem1.ID;
                    detailWinMethod.hidden();
                    managerMethod.set('url', "ItemList.aspx?Action=GetMethods&selItemID=" + strItemId1);
                    clearDialogValue_Method();
                }
                else {
                    $.ligerDialog.warn('保存分析方法数据失败！');
                }
            }
        });
    }
}

//分析方法grid的编辑对话框 清空
function clearDialogValue_Method() {
    $("#Method_ID").val("");
    $("#hidAPPARATUS_ID").val("");
    $("#Method_ItemANALYSISID").val("");
    $("#hidMethod_ItemANALYSISID").val("");
    $("#APPARATUS_ID").val("");
    $("#LOWER_CHECKOUT").val("");
    $("#UnitCode").val("");
    $("#Unit").val("");

    $("#Method_ItemID").val("");
    $("#Method_ItemID1").ligerGetComboBoxManager().setValue("");
}

