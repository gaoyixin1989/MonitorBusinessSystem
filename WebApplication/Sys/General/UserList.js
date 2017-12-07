// Create by 潘德军 2012.11.19  "用户管理"功能

var objGrid = null;
var detailWinSrh = null;
var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//用户管理功能
$(document).ready(function () {
    //grid的菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objGrid = $("#mainGrid").ligerGrid({
        title: '用户列表',
        dataAction: 'server',
        usePager: true,
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        height: '100%',
        url: 'UserList.aspx?type=getData',
        columns: [
                 { display: '用户登录名', name: 'USER_NAME', align: 'left', width: 100 },
                 { display: '用户姓名', name: 'REAL_NAME', align: 'left', width: 100 },
                 { display: '职位', name: 'Post', align: 'left', width: 300, render: function (record) {
                     return getPostName(record.ID);
                 }
                 },
                 { display: '部门', name: 'Dept', align: 'left', width: 250, render: function (record) {
                     return getDeptName(record.ID);
                 }
                 },
                { display: '办公电话', name: 'PHONE_OFFICE', align: 'left', isSort: false, width: 150 },
                { display: '手机号', name: 'PHONE_MOBILE', align: 'left', isSort: false, width: 150 },
                { display: '是否停用', name: 'IS_USE', align: 'left', isSort: false, width: 100 },
                { display: '序号', name: 'ORDER_ID', align: 'left', width: 100 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' },
                { line: true },
                { id: 'srh', text: '查询', click: searchData, icon: 'search' }
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

    //查询数据
    function searchData() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var srhform = $("#searchForm");
            srhform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                      { display: "部门", name: "SrhDept_ID", newline: true, type: "select", comboboxName: "SrhDept_ID1", group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhDept_ID", url: "../../Channels/Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|dept&Order=ORDER_ID"} },
                      { display: "用户姓名", name: "REAL_NAME", newline: true, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 350, height: 200, top: 90, title: "查询用户",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SrhDept_ID1 = $("#SrhDept_ID").val();
            var Real_Name = encodeURI($("#REAL_NAME").val());
            objGrid.set('url', "UserList.aspx?type=getData&strSrhDept_ID=" + SrhDept_ID1 + "&Real_Name=" + Real_Name);
        }
    }

    //监测项目grid 的查询对话框元素的值 清除
    function clearSearchDialogValue() {
        $("#SrhDept_ID").val("");
        $("#SrhDept_ID1").val("");
        $("#REAL_NAME").val("");
    }

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '用户信息增加', top: 0, width: 640, height: 520, buttons:
        [{ text: '确定', onclick: f_AddDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'UserEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }

        $.ligerDialog.open({ title: '用户信息编辑', top: 0, width: 640, height: 520, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'UserEdit.aspx?strid=' + objGrid.getSelectedRow().ID
        });
    }

    //save函数
    function f_AddDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "UserList.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('数据保存成功！新用户密码默认为“1”,请用户自行修改登录密码。');
                    dialog.close();
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "UserList.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('数据保存成功！');
                    dialog.close();
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    //删除数据
    function deleteData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除用户信息 ' + objGrid.getSelectedRow().USER_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "UserList.aspx/deleteData",
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
});

//获取职位信息
function getPostName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "UserList.aspx/getPostName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取部门信息
function getDeptName(strUserID) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "UserList.aspx/getDeptName",
        data: "{'strValue':'" + strUserID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

