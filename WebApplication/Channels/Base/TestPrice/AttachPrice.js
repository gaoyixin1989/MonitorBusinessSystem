// Create by 潘德军 2012.11.16  "附加费用设置"功能

var objGrid = null;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

//grid
$(document).ready(function () {
    $("#layout1").ligerLayout({ height: '100%' });

    //监测点位grid的菜单
    var objmenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { line: true },
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
        pageSizeOptions: [10, 15, 20,50],
        height: '100%',
        url: 'AttachPrice.aspx?type=getFee',
        columns: [
                 { display: '附加费用项目', name: 'ATT_FEE_ITEM', align: 'left',  width: 200 },
                 { display: '费用单价', name: 'PRICE', align: 'left',  width: 100 },
                 { display: '费用描述', name: 'INFO', align: 'left', isSort: false, width: 400 }
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
        $.ligerDialog.open({ title: '附加费用项目增加', top: 0, width: 350, height: 250, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'AttachPriceEdit.aspx' 
        });
    }
    //修改数据
    function updateData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑！');
            return;
        }
        $.ligerDialog.open({ title: '附加费用项目编辑', top: 0, width: 350, height: 250, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'AttachPriceEdit.aspx?strid=' + objGrid.getSelectedRow().ID
        });
    }

    //save函数
    function f_SaveDate(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "AttachPrice.aspx/SaveData",
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
                    $.ligerDialog.warn('数据保存失败！');
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
        $.ligerDialog.confirm('确认附加费用项目信息吗？', function (yes) {
            if (yes == true) {
                var strValue = objGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "AttachPrice.aspx/deleteData",
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