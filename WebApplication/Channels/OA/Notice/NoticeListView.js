// Create by 熊卫华 2013.02.25  "公告管理"功能

var objGrid = null;
var strId = "";
var url = "NoticeList.aspx";
var updateUrl = "NoticeEditView.aspx";

var strAddInfo = "公告管理信息增加";
var strUpdateInfo = "公告管理信息编辑";

var gridWidth = 830;
var gridHeight = 400;

$(document).ready(function () {
    //菜单
    objGrid = $("#grid").ligerGrid({
        title: '公告管理',
        dataAction: 'server',
        usePager: true,
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        sortName: "ID",
        width: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        height: "100%",
        url: url + '?type=getDataInfo',
        columns: [
                    { display: '公告标题', name: 'TITLE', align: 'left', width: 300, minWidth: 60 },
                    { display: '发布时间', name: 'RELEASE_TIME', minWidth: 50 },
                    { display: '发布人', name: 'RELIEASER', minWidth: 50 },
                    { display: '是否置顶', name: 'REMARK1', minWidth: 50, render: function (item) {
                        if (item.REMARK1 == '1') {
                            return '<a style=" color:Red">是</a>';
                        }
                        return '否';
                    }
                    }
                    ],
        toolbar: { items: [
                    { text: '查看公告', click: updateData, icon: 'modify' },
                    { line: true },
                    { text: '查询', click: showDetailSrh, icon: 'search' }
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

            updateData();
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            strId = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none");

    //修改数据
    function updateData() {
        if (!objGrid.getSelectedRow()) {
            $.ligerDialog.warn('请选择一条记录进行编辑');
            return;
        }
        $.ligerDialog.open({ title: strUpdateInfo, width: gridWidth, height: gridHeight, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            dialog.close();
        }
        }], url: updateUrl + '?id=' + objGrid.getSelectedRow().ID
        });
    }

    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构
            var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
            var divmainform = $("#Seachdiv");

            divmainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                        { display: "公告标题", name: "txt_TITLE", newline: true, type: "text", group: "查询信息", groupicon: groupicon
                        }
                        ]
            });
            detailWinSrh = $.ligerDialog.open({
                target: $("#Seachdetail"),
                top: 90, width: 350, height: 150, title: "公告管理信息查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }
    }
    //查询信息清除功能
    function clearSearchDialogValue() {
        $("#txt_TITLE").val("");
    }
});