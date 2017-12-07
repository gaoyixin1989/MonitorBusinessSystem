// Create by 苏成斌 2012.11.29  "短消息列表(发件箱)"功能

var objGrid = null;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//grid
$(document).ready(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    //监测点位grid的菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objGrid = $("#maingrid").ligerGrid({
        title: '',
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
        url: 'MessageSendList.aspx?type=getMessage',
        columns: [
                 { display: '消息标题', name: 'MESSAGE_TITLE', align: 'left', width: 300 },
                 { display: '接收类别', name: 'ACCEPT_TYPE', align: 'left', width: 90 },
                 { display: '接收人', name: 'ACCEPT_REALNAMES', align: 'left', width: 150 },
                 { display: '接收部门', name: 'ACCEPT_DEPTNAMES', align: 'left', width: 170 }
                ],
        toolbar: { items: [
                { text: '新建消息', click: createData, icon: 'add' },
                { line: true },
                { text: '查看消息', click: CheckData, icon: 'add' },
                { line: true },
                { text: '删除消息', click: deleteData, icon: 'delete' }
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

            CheckData();
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
        $.ligerDialog.open({ title: '消息增加', top: 0, width: 580, height: 480, buttons:
        [{ text: '发送', onclick: SendMsg },
         { text: '保存', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'MessageInfo.aspx'
        });
    }

    function SendMsg(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "MessageSendList.aspx/SendMsg",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('发送成功！');
                    dialog.close();
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('发送失败！');
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
            url: "MessageSendList.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('操作成功！');

                    dialog.close();
                    objGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('操作失败！');
                }
            }
        });
    }

    //修改数据
    function CheckData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条消息进行编辑！');
            return;
        }
        $.ligerDialog.open({ title: '查看消息', top: 0, width: 580, height: 420, buttons:
        [{ text: '重新发送', onclick: SendMsg },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'MessageInfo.aspx?strid=' + objGrid.getSelectedRow().ID
        });
    }

    //删除数据
    function deleteData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条消息进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除消息吗？', function (yes) {
            if (yes == true) {
                var strValue = objGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "MessageSendList.aspx/deleteData",
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