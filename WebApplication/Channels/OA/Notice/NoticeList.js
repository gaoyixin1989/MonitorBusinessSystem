// Create by 熊卫华 2013.02.25  "公告管理"功能

var objGrid = null;
var strId = "";
var url = "NoticeList.aspx";
var updateUrl = "NoticeEdit.aspx";

var strAddInfo = "公告管理信息增加";
var strUpdateInfo = "公告管理信息编辑";

var gridWidth = 830;
var gridHeight = 400;

var menu = null;

$(document).ready(function () {
    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'menumodify', text: '修改', click: updateData, icon: 'modify' },
            { id: 'menudel', text: '删除', click: deleteData, icon: 'delete' }
            ]
    });
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
                    { display: '附件', name: 'UPLOAD', width: 100, render: function (item) {
                        return "<a href=\"javascript:uploadNoticeFile('" + item.ID + "')\">上传附件</a> ";
                    }
                    },
                    { display: '是否置顶', name: 'REMARK1', width: 80, render: function (item) {
                        if (item.REMARK1 == '1') {
                            return '<a style=" color:Red">是</a>';
                        }
                        return '否';
                    }
                    }
                    ],
        toolbar: { items: [
                    { text: '增加', click: createData, icon: 'add' },
                    { line: true },
                    { text: '修改', click: updateData, icon: 'modify' },
                    { line: true },
                    { text: '删除', click: deleteData, icon: 'delete' },
                    { line: true },
                    { text: '查询', click: showDetailSrh, icon: 'search' },
                    { line: true },
                    { text: '置顶', click: topOne, icon: 'search'}//黄进军 添加20141112
                    ]
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
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

    //增加数据
    function createData() {
        $.ligerDialog.open({ title: strAddInfo, width: gridWidth, height: gridHeight, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据保存成功')
            }
            else {
                $.ligerDialog.warn('数据保存失败');
            }
            objGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: updateUrl
        });
    }
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
            if ($("iframe")[0].contentWindow.DataSave()) {
                dialog.close();
                $.ligerDialog.success('数据更新成功')
            }
            else {
                $.ligerDialog.warn('数据更新失败');
            }
            objGrid.loadData();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: updateUrl + '?id=' + objGrid.getSelectedRow().ID
        });
    }
    //删除数据
    function deleteData() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录进行删除');
            return;
        }
        $.ligerDialog.confirm('确认删除已经选择的信息吗？', function (yes) {
            if (yes == true) {
                var strValue = objGrid.getSelectedRow().ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: url + "/deleteDataInfo",
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

    //huangjinjun add 20160223 查询功能
    function search() {
        var strTitle = encodeURI($("#txt_TITLE").val());
        objGrid.set('url', "NoticeList.aspx?type=getNoticeList&strTitle=" + strTitle);
    }

    //查询信息清除功能
    function clearSearchDialogValue() {
        $("#txt_TITLE").val("");
    }

    //置顶 黄进军 添加20141112
    function topOne() {
        if (objGrid.getSelectedRow() == null) {
            $.ligerDialog.warn('请选择一条记录来置顶！');
            return;
        }
        $.ajax({
            cache: false,
            type: "POST",
            url: "NoticeList.aspx/SetTopOne",
            data: "{'id':'" + objGrid.getSelectedRow().ID + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == "1") {
                    objGrid.loadData();
                    $.ligerDialog.success('置顶成功')
                }
                else {
                    $.ligerDialog.warn('操作失败');
                }
            }
        });
    }
});

function uploadNoticeFile(id) {
    $.ligerDialog.open({ title: '附件上传', width: 800, height: 350, isHidden: false,
        buttons: [
             { text: '直接下载', onclick: function (item, dialog) {
                 dialog.frame.aa(); //调用下载按钮
             }
             },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../ATT/AttMoreFileUpLoad.aspx?filetype=OA_NOTICE&id=' + id
    });
}