// Create by 熊卫华 2013.02.02  "收文管理功能"功能

var objOneGrid = null;
var objTwoGrid = null;
var objThreeGrid = null;
var strUrl = "SWList.aspx";
var strOneGridUpdateUrl = "SWEdit.aspx";

//未提交列表
$(document).ready(function () {
    objOneGrid = $("#oneGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo&strSwStatus=0',
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 250, minWidth: 60 },
                { display: '收文类别', name: 'SW_TYPE', align: 'left', width: 250, minWidth: 60 },
                { display: '流转情况', name: 'SW_TYPE1', align: 'left', width: 250, minWidth: 60 },
                { display: '审批情况', name: 'SW_TYPE2', align: 'left', width: 250, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '增加', click: createData, icon: 'add' },
                { text: '修改', click: updateData, icon: 'modify' },
                { text: '删除', click: deleteData, icon: 'delete' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
    function createData() {
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false&WF_ID=SW';
        top.f_overTab('增加收文登记单', surl);
    }
    function updateData() {
        if (!objOneGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        var surl = '../Sys/WF/WFStartPage.aspx?action=|type=false|view=false|strtaskID=' + objOneGrid.getSelectedRow().ID + '&WF_ID=SW';
        top.f_overTab('增加收文登记单', surl);
    }
    function deleteData() {
        if (objOneGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm("确认删除监测点信息吗？", function (yes) {
            if (yes == true) {
                var strValue = objOneGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: strUrl + "/deleteOneGridInfo",
                    data: "{'strValue':'" + strValue + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data, textStatus) {
                        if (data.d == "1") {
                            objOneGrid.loadData();
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
//流转中列表
$(document).ready(function () {
    objTwoGrid = $("#twoGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo&strSwStatus=1',
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 250, minWidth: 60 },
                { display: '收文类别', name: 'SW_TYPE', align: 'left', width: 250, minWidth: 60 },
                { display: '流转情况', name: 'SW_TYPE1', align: 'left', width: 250, minWidth: 60 },
                { display: '审批情况', name: 'SW_TYPE2', align: 'left', width: 250, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '查看', icon: 'add' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});
//已办结列表
$(document).ready(function () {
    objThreeGrid = $("#threeGrid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?type=getOneGridInfo&strSwStatus=9',
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 250, minWidth: 60 },
                { display: '来文单位', name: 'SW_FROM', align: 'left', width: 250, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 250, minWidth: 60 },
                { display: '收文类别', name: 'SW_TYPE', align: 'left', width: 250, minWidth: 60 },
                { display: '流转情况', name: 'SW_TYPE1', align: 'left', width: 250, minWidth: 60 },
                { display: '审批情况', name: 'SW_TYPE2', align: 'left', width: 250, minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '查看', icon: 'add' }
                ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            return false;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");
});