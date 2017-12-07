// Create by 潘德军 2012.11.05  "质控标准设置"功能

var manager;
var menu;
var actionID, actionItemName, actionTwinValue, actionAddMin, actionAddMax;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout1").ligerLayout({ leftWidth: 170, height: '100%' });

    //质控设置菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu, icon: 'modify' }
            ]
    });

    //质控设置grid
    window['g'] =
    manager = $("#maingridItem").ligerGrid({
        columns: [
        { display: '监测类型', name: 'MONITOR_NAME', width: 250, align: 'left', isSort: false },
        { display: '监测项目', name: 'ITEM_NAME', width: 250, align: 'left', isSort: false },
        { display: '平行偏差范围', name: 'TWIN_VALUE', width: 250, align: 'left', isSort: false },
        { display: '加标上限', name: 'ADD_MAX', width: 150, align: 'left', isSort: false },
        { display: '加标下限', name: 'ADD_MIN', width: 150, align: 'left', isSort: false }
        ], width: '100%', height: '100%', pageSizeOptions: [10, 15, 20, 50],
        url: 'ItemQCSet.aspx?Action=GetItem',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'modify', text: '修改', click: itemclick_OfToolbar, icon: 'modify' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionID = parm.data.ID;
            actionItemName = parm.data.ITEM_NAME;
            actionTwinValue = parm.data.TWIN_VALUE;
            actionAddMin = parm.data.ADD_MAX;
            actionAddMax = parm.data.ADD_MIN;
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetail({
                ItemID: data.ID,
                ITEM_NAME: data.ITEM_NAME,
                TWIN_VALUE: data.TWIN_VALUE,
                ADD_MAX: data.ADD_MAX,
                ADD_MIN: data.ADD_MIN
            });
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
});

//质控设置grid 的toolbar click
function itemclick_OfToolbar(item) {
    switch (item.id) {
        case 'modify':
            var selected = manager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetail({
                ItemID: selected.ID,
                ITEM_NAME: selected.ITEM_NAME,
                TWIN_VALUE: selected.TWIN_VALUE,
                ADD_MAX: selected.ADD_MAX,
                ADD_MIN: selected.ADD_MIN
            });

            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

//质控设置grid 的右键菜单
function itemclick_OfMenu(item) {
    switch (item.id) {
        case 'menumodify':
            showDetail({
                ItemID: actionID,
                ITEM_NAME: actionItemName,
                TWIN_VALUE: actionTwinValue,
                ADD_MAX: actionAddMin,
                ADD_MIN: actionAddMax
            }, false);

            break;
        default:
            break;
    }
}

//质控设置grid 的弹出编辑对话框
var detailWin = null, curentData = null;
function showDetail(data) {
    curentData = data;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测项目", name: "ItemID", newline: true, type: "select", comboboxName: "ItemID1", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "ItemID", url: "../MonitorType/Select.ashx?view=T_BASE_ITEM_INFO&idfield=ID&textfield=ITEM_NAME&where=is_del|0"} },
                      { display: "平行偏差范围", name: "TWIN_VALUE", newline: false, type: "text" },
                      { display: "加标上限", name: "ADD_MAX", newline: true, type: "text" },
                      { display: "加标下限", name: "ADD_MIN", newline: false, type: "text" }
                    ]
        });
        //表单验证
        $("#TWIN_VALUE").attr("validate", "[{maxlength:16,msg:'平行偏差范围最大长度为16'}]");
        $("#ADD_MAX").attr("validate", "[{maxlength:64,msg:'加标上限最大长度为64'}]");
        $("#ADD_MIN").attr("validate", "[{maxlength:64,msg:'加标下限最大长度为64'}]");


        detailWin = $.ligerDialog.open({
            target: $("#detailEdit"),
            width: 600, height: 200, top: 90, title: "项目信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#ItemID").val(curentData.ItemID);
        $("#ItemID1").ligerGetComboBoxManager().setValue(curentData.ItemID);
        $("#ItemID1").ligerGetComboBoxManager().setDisabled();
        $("#TWIN_VALUE").val(curentData.TWIN_VALUE);
        $("#ADD_MAX").val(curentData.ADD_MAX);
        $("#ADD_MIN").val(curentData.ADD_MIN);
    }

    function save() {
        //表单验证
        if (!$("#editForm").validate())
            return false;
        var strData = "{";
        strData += "'strItemID':'" + $("#ItemID").val() + "',";
        strData += "'strTWIN_VALUE':'" + $("#TWIN_VALUE").val() + "',";
        strData += "'strADD_MAX':'" + $("#ADD_MAX").val() + "',";
        strData += "'strADD_MIN':'" + $("#ADD_MIN").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "ItemQCSet.aspx/EditItem",
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
                    $.ligerDialog.warn('保存项目数据失败！');
                }
            }
        });
    }
}

//质控设置grid 的弹出编辑对话框 清空
function clearDialogValue() {
    $("#ItemID").val("");
    $("#ItemID1").val("");
    $("#TWIN_VALUE").val("");
    $("#ADD_MIN").val("");
    $("#ADD_MAX").val("");
}

//质控设置grid 的弹出查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "SrhMONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhMONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
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

        manager.set('url', "ItemQCSet.aspx?Action=GetItem&SrhMONITOR_ID=" + SrhMONITOR_ID + "&SrhITEM_NAME=" + SrhITEM_NAME);
    }
}

//质控设置grid 的弹出查询对话框 清空
function clearSearchDialogValue() {
    $("#SrhMONITOR_ID").val("");
    $("#SrhMONITOR_TYPE_ID").val("");
    $("#SrhITEM_NAME").val("");
}