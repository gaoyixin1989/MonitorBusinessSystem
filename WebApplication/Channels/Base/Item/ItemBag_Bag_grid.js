// Create by 潘德军 2012.11.05  "项目包设置"功能的项目包列表

var managerBag, managerItem;
var menuBag, menuItem;
var actionBagMonitorID, actionBagMonitorName, actionBagID, actionBagName;
var actionID;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;
    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //菜单
    menuBag = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: itemclick_OfMenu_UnderItemBag, icon: 'modify' },
            { id: 'menudel', text: '删除', click: itemclick_OfMenu_UnderItemBag, icon: 'delete' }
            ]
    });

    //grid 项目包
    window['g'] =
    managerBag = $("#maingridBag").ligerGrid({
        columns: [
        { display: '监测类型', name: 'MONITOR_NAME', width: 250, align: 'left', isSort: false },
        { display: '项目包', name: 'ITEM_NAME', width: 250, align: 'left', isSort: false }
        ], width: '100%', height: gridHeight, pageSizeOptions: [5, 8, 10],
        url: 'ItemBag.aspx?Action=GetItemBag',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '项目包',
        toolbar: { items: [
                { id: 'add', text: '增加', click: itemclick_OfToolbar_UnderItemBag, icon: 'add' },
                { line: true },
                { id: 'modify', text: '修改', click: itemclick_OfToolbar_UnderItemBag, icon: 'modify' },
                { line: true },
                { id: 'del', text: '删除', click: itemclick_OfToolbar_UnderItemBag, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItemBag, icon: 'search' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            actionBagMonitorID = parm.data.MONITOR_ID;
            actionBagMonitorName = parm.data.MONITOR_NAME;
            actionBagID = parm.data.ID;
            actionBagName = parm.data.ITEM_NAME;
            menuBag.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showDetailBag({
                BagID: data.ID,
                BagMonitorID: data.MONITOR_ID,
                BagMonitorName: data.MONITOR_NAME,
                BagName: data.ITEM_NAME
            }, false);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            var selectedBag = managerBag.getSelectedRow();
            managerItem.set('url', "ItemBag.aspx?Action=GetItems&selBagID=" + selectedBag.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//项目包grid的toolbar click事件
function itemclick_OfToolbar_UnderItemBag(item) {
    switch (item.id) {
        case 'add':
            showDetailBag(null, true);
            break;
        case 'modify':
            var selected = managerBag.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

            showDetailBag({
                BagID: selected.ID,
                BagMonitorID: selected.MONITOR_ID,
                BagMonitorName: selected.MONITOR_NAME,
                BagName: selected.ITEM_NAME
            }, false);

            break;
        case 'del':
            var rows = managerBag.getCheckedRows();
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
                        delItemBag(strDelID);
                });
            }

            break;
        case 'srh':
            showDetailSrh();
            break;
        default:
            break;
    }
}

//项目包grid的右键菜单
function itemclick_OfMenu_UnderItemBag(item) {
    switch (item.id) {
        case 'menumodify':
            showDetailBag({
                BagID: actionBagID,
                BagMonitorID: actionBagMonitorID,
                BagMonitorName: actionBagMonitorName,
                BagName: actionBagName
            }, false);

            break;
        case 'menudel':
            var strDelID = actionBagID;

            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm)
                    delItemBag(strDelID);
            });
            break;
        default:
            break;
    }
}

//项目包grid的删除方法
function delItemBag(ids) {
    $.ajax({
        cache: false,
        type: "POST",
        url: "ItemBag.aspx/deleteItemBag",
        data: "{'strDelIDs':'" + ids + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            if (data.d == "1") {
                managerBag.loadData();
            }
            else {
                $.ligerDialog.warn('删除项目包数据失败！');
            }
        }
    });
}

//项目包grid的编辑对话框及保存
var detailWinBag = null, curentDataBag = null, currentIsAddNewBag;
function showDetailBag(data, isAddNew) {
    curentDataBag = data;
    currentIsAddNewBag = isAddNew;
    if (detailWinBag) {
        detailWinBag.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editBagForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
            fields: [
                      { name: "Item_ID", type: "hidden" },
                      { display: "监测类型", name: "MONITOR_ID", newline: true, type: "select", comboboxName: "MONITOR_Type_ID", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "MONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "项目包", name: "ITEM_NAME", newline: true, type: "text", validate: { required: true, minlength: 3} }
                    ]
        });
        //add validate by ssz
        $("#ITEM_NAME").attr("validate", "[{required:true, msg:'请填写项目包名称'},{maxlength:256,msg:'项目包名称最大长度为256'}]");


        detailWinBag = $.ligerDialog.open({
            target: $("#detailBag"),
            width: 350, height: 200, top: 90, title: "项目包信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWinBag.hide(); } }
                  ]
        });
    }
    if (curentDataBag) {
        $("#Item_ID").val(curentDataBag.BagID);
        $("#MONITOR_ID").val(curentDataBag.BagMonitorID);
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setValue(curentDataBag.BagMonitorID);
        $("#MONITOR_Type_ID").ligerGetComboBoxManager().setDisabled();
        $("#ITEM_NAME").val(curentDataBag.BagName);
    }

    function save() {
        //表单验证
        if (!$("#editBagForm").validate())
            return false;
        var strData = "{";
        strData += currentIsAddNewBag ? "" : "'strID':'" + $("#Item_ID").val() + "',";
        strData += currentIsAddNewBag ? "'strMONITOR_ID':'" + $("#MONITOR_ID").val() + "'," : ""; ;
        strData += "'strITEM_NAME':'" + $("#ITEM_NAME").val() + "'";
        strData += "}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "ItemBag.aspx/" + (currentIsAddNewBag ? "AddItemBag" : "EditItemBag"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWinBag.hidden();
                    managerBag.loadData();
                    clearDialogValue();
                }
                else {
                    $.ligerDialog.warn('保存项目包数据失败！');
                }
            }
        });
    }
}

//项目包grid的对话框元素清空值
function clearDialogValue() {
    $("#Item_ID").val("");
    $("#MONITOR_ID").val("");
    $("#MONITOR_Type_ID").val("");
    $("#ITEM_NAME").val("");
}

//项目包grid的查询对话框及查询函数
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#SrhItemForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 120, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "SrhMONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhMONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "项目包", name: "SrhITEM_NAME", newline: true, type: "text", validate: { required: true, minlength: 3} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询项目包",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhMONITOR_ID = $("#SrhMONITOR_ID").val();
        var SrhITEM_NAME = escape($("#SrhITEM_NAME").val());

        managerBag.set('url', "ItemBag.aspx?Action=GetItemBag&SrhMONITOR_ID=" + SrhMONITOR_ID + "&SrhITEM_NAME=" + SrhITEM_NAME);
    }
}

////项目包grid的查询对话框元素的值清空
function clearSearchDialogValue() {
    $("#SrhMONITOR_ID").val("");
    $("#SrhMONITOR_TYPE_ID").val("");
    $("#SrhITEM_NAME").val("");
}