// Create by 潘德军 2012.11.15  "出差代理管理"功能

var objGrid = null;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//点位信息管理功能
$(document).ready(function () {
    strIsAdmin = request('IsAdmin');
    if (!strIsAdmin)
        strIsAdmin = "";

    //监测点位grid的菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { line: true },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objGrid = $("#ProxyGrid").ligerGrid({
        title: '工作代理',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: '100%',
        url: 'UserProxyList.aspx?type=getProxy&IsAdmin=' + strIsAdmin,
        columns: [
                 { display: '被代理人', name: 'USER_ID', align: 'left', width: 150, isSort: false, render: function (record) {
                     return getUserName(record.USER_ID);
                 }
                 },
                 { display: '代理人', name: 'PROXY_USER_ID', align: 'left', width: 150, isSort: false, render: function (record) {
                     return getUserName(record.PROXY_USER_ID);
                 }
                 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' }
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

    //增加数据
    function createData() {
        showDetail(null, true);
    }
    //修改数据
    function updateData() {
        var selected = objGrid.getSelectedRow();
        if (!selected) { $.ligerDialog.warn('请先选择要编辑的记录！'); ; return }

        showDetail({
            ID: selected.ID,
            USER_ID: selected.USER_ID,
            PROXY_USER_ID: selected.PROXY_USER_ID
        }, false);
    }

    //删除数据
    function deleteData() {
        var rows = objGrid.getCheckedRows();
        var strDelID = "";
        $(rows).each(function () {
            strDelID += (strDelID.length > 0 ? "," : "") + this.ID;
        });

        if (strDelID.length == 0) {
            $.ligerDialog.warn('请先选择要删除的记录！');
        }
        else {
            jQuery.ligerDialog.confirm('确定删除吗?', function (confirm) {
                if (confirm) {
                    $.ajax({
                        cache: false,
                        type: "POST",
                        url: "UserProxyList.aspx/deleteData",
                        data: "{'strDelIDs':'" + strDelID + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data, textStatus) {
                            if (data.d == "1") {
                                objGrid.loadData();
                            }
                            else {
                                $.ligerDialog.warn('删除数据失败！');
                            }
                        }
                    });
                }
            });
        }
    }
});

//获取用户信息
function getUserName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "UserProxyList.aspx/getUserName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//弹出编辑框及save函数
var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editForm");

        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { name: "ID", type: "hidden" },
                      { display: "被代理人", name: "UserID", newline: true, type: "select", comboboxName: "UserID1", group: "基本信息", groupicon: groupicon, options: { valueFieldID: "UserID", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_USER&idfield=ID&textfield=REAL_NAME&where=IS_DEL|0-IS_HIDE|0&Order=ORDER_ID"} },
                      { display: "代理人", name: "ProxyID", newline: true, type: "select", comboboxName: "ProxyID1", options: { valueFieldID: "ProxyID", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_USER&idfield=ID&textfield=REAL_NAME&where=IS_DEL|0-IS_HIDE|0&Order=ORDER_ID"} }
                    ]
        });

        if (strIsAdmin.length == 0) {
            $("#UserID").val($("#hidUserID").val());
            $("#UserID1").ligerGetComboBoxManager().setValue($("#hidUserID").val());
            $("#UserID1").ligerGetComboBoxManager().setDisabled();
        }

        detailWin = $.ligerDialog.open({
            target: $("#detailProxy"),
            width: 350, height: 200, top: 90, title: "代理信息",
            buttons: [
                  { text: '确定', onclick: function () { save(); } },
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#ID").val(curentData.ID);
        if (strIsAdmin.length > 0) {
            $("#UserID").val(getUserName(curentData.USER_ID));
            $("#UserID1").ligerGetComboBoxManager().setValue(curentData.USER_ID);
        }
        $("#ProxyID").val(getUserName(curentData.PROXY_USER_ID));
        $("#ProxyID1").ligerGetComboBoxManager().setValue(curentData.PROXY_USER_ID);
    }


    function save() {
        var strData = "{" + (currentIsAddNew ? "" : "'strID':'" + $("#ID").val() + "',") + "'strUSER_ID':'" + $("#UserID").val() + "','strProxyID':'" + $("#ProxyID").val() + "'}";

        $.ajax({
            cache: false,
            type: "POST",
            url: "UserProxyList.aspx/" + (currentIsAddNew ? "AddData" : "EditData"),
            data: strData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    detailWin.hidden();
                    objGrid.loadData();
                    clearDialogValue();
                }
                else {
                    $.ligerDialog.warn('保存数据失败！');
                }
            }
        });
    }
}

//监测类别grid 弹出编辑框 清空
function clearDialogValue() {
    $("#ID").val("");
    $("#UserID").val("");
    $("#ProxyID").val("");
    $("#UserID1").val("");
    $("#ProxyID1").val("");
}

