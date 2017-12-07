// Create by 潘德军 2012.11.05  "项目包设置"功能的指定项目包的项目列表

$(document).ready(function () {
    var gridHeight = $(window).height() / 2;

    //grid 指定项目包的项目，子项目
    managerItem = $("#maingridItem").ligerGrid({
        columns: [
        { display: '项目包', name: 'ItemBagName', width: 200, align: 'left', isSort: false },
        { display: '监测项目', name: 'ItemName', width: 150, align: 'left', isSort: false }
        ], width: '100%', height: gridHeight, pageSizeOptions: [5, 8, 10], heightDiff: -10,
        url: 'ItemBag.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        title: '监测项目',
        toolbar: { items: [
                { id: 'set', text: '设置监测项目', click: SetData_Item, icon: 'add' }
                ]
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

    //设置监测项目
    function SetData_Item() {
        var selectedBag = managerBag.getSelectedRow();
        if (!selectedBag) {
            $.ligerDialog.warn('请先选择监测项目包！');
            return;
        }

        $.ligerDialog.open({ title: '设置监测项目', top: 0, width: 460, height: 380, buttons:
        [{ text: '确定', onclick: f_SaveDateItem },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'ItemBagItemEdit.aspx?selBagID=' + selectedBag.ID
        });
    }

    //save函数
    function f_SaveDateItem(item, dialog) {
        var fn = dialog.frame.getSaveDate || dialog.frame.window.getSaveDate;
        var strdata = fn();

        $.ajax({
            cache: false,
            type: "POST",
            url: "ItemBag.aspx/SaveDataItem",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    var selectedBag = managerBag.getSelectedRow();
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    managerItem.set('url', "ItemBag.aspx?Action=GetItems&selBagID=" + selectedBag.ID);
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }
});







