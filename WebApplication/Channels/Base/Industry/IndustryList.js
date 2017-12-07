// Create by 潘德军 2012.11.21  "行业信息管理"功能

var objMainGrid, objSubGrid;
var strIndustryId = "";

//行业信息管理功能
$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({  height: '100%', topHeight: topHeight });

    //菜单
    var objMainMenu = $.ligerMenu({ width: 120, items:
            [
            { text: '修改', click: updateData, icon: 'modify' },
            { line: true },
            { text: '切换常用设置', click: isShowData, icon: 'modify' },
            { line: true },
            { text: '删除', click: deleteData, icon: 'delete' }
            ]
    });

    objMainGrid = $("#maingrid").ligerGrid({
        title: '行业类别',
        dataAction: 'server',
        usePager: true,
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 8, 10],
        height: gridHeight,
        url: 'IndustryList.aspx?type=getData',
        columns: [
                 { display: '行业代码', name: 'INDUSTRY_CODE', align: 'left', isSort: false, width: 200 },
                 { display: '行业名称', name: 'INDUSTRY_NAME', align: 'left', isSort: false, width: 200 },
                 { display: '常用设置', name: 'IS_SHOW', align: 'left', isSort: false, width: 200, render: function (record) {
                     return record.IS_SHOW==1?"是":"";
                 } 
                 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { line: true },
                { text: '修改', click: updateData, icon: 'modify' },
                { line: true },
                { text: '切换常用设置', click: isShowData, icon: 'modify' },
                { line: true },
                { text: '删除', click: deleteData, icon: 'delete' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            objMainMenu.show({ top: e.pageY, left: e.pageX });
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

            objSubGrid.set('url', "IndustryList.aspx?type=GetSubData&selIndustryID=" + objMainGrid.getSelectedRow().ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: '行业信息增加', top: 0, width: 350, height: 200, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'IndustryEdit.aspx'
        });
    }
    //修改数据
    function updateData() {
        if (objMainGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: '行业信息编辑', top: 0, width: 350, height: 200, buttons:
        [{ text: '确定', onclick: f_SaveDate },
         { text: '关闭', onclick: function (item, dialog) { dialog.close(); }
         }], url: 'IndustryEdit.aspx?strid=' + objMainGrid.getSelectedRow().ID
        });
    }

    //切换显示设置
    function isShowData() {
        if (objMainGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录');
            return;
        }
        
        var strValue = objMainGrid.getSelectedRow().ID;
        $.ajax({
            cache: false,
            type: "POST",
            url: "IndustryList.aspx/isShowData",
            data: "{'strValue':'" + strValue + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objMainGrid.loadData();
                    $.ligerDialog.success('操作成功')
                }
                else {
                    $.ligerDialog.warn('操作失败');
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
            url: "IndustryList.aspx/SaveData",
            data: strdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    $.ligerDialog.success('数据保存成功');
                    dialog.close();
                    objMainGrid.loadData();
                }
                else {
                    $.ligerDialog.warn('数据保存失败');
                }
            }
        });
    }

    //删除数据
    function deleteData() {
        if (objMainGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除行业 ' + objMainGrid.getSelectedRow().INDUSTRY_NAME + " 吗？", function (yes) {
            if (yes == true) {
                var strValue = objMainGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "IndustryList.aspx/deleteData",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objMainGrid.loadData();
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