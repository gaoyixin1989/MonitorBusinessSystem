// Create by 邵世卓 2012.11.28  "印章查询"功能
var SelectID;
var FirstManager;
var SecondManager;

var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: 970 });
    //部门 grid
    FirstManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: '部门名称', name: 'DICT_TEXT', align: 'left', width: 200, minWidth: 60 }
                ],
        title: "部门选择",
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: 'SignatureList.aspx?Action=getDeptInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            SecondManager.set('url', 'SignatureList.aspx?Action=getSignInfo&deptId=' + rowdata.DICT_CODE);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    //$(".l-panel-bar").css("display", "none"); // 隐藏分页控件
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //用户印章grid
    SecondManager = $("#secondgrid").ligerGrid({
        columns: [
                { display: '用户名', name: 'USER_ID', align: 'left', width: 100, minWidth: 60 },
                { display: '用户姓名', name: 'REAL_NAME', align: 'left', width: 100, minWidth: 60 },
                 { display: '文件类型', name: 'MARK_TYPE', align: 'left', width: 100, minWidth: 60 }
                ],
        title: "用户印章",
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: 'SignatureList.aspx?Action=getSignInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'add', text: '上传印章文件', click: addData, icon: 'add' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            SelectID = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

//上传印章
function addData() {
    if (SecondManager.getSelectedRow() == null) {
        $.ligerDialog.warn('上传文件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '印章上传', width: 500, height: 200,
        buttons: [
            {
                text:
                    '上传', onclick: function (item, dialog) {
                        $("iframe")[0].contentWindow.upLoadFile();
                        SecondManager.loadData();
                    }
            },
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: 'SignatureEdit.aspx?id=' + SecondManager.getSelectedRow().ID + '&user_id=' + SecondManager.getSelectedRow().USER_ID + '&real_name=' + encodeURI(SecondManager.getSelectedRow().REAL_NAME)
    });
}

///删除
function deleteData() {
    if (SelectID == null) {
        $.ligerDialog.warn('请选择一条记录进行删除');
        return;
    }

    $.ligerDialog.confirm('确认删除印章信息 ' + FirstManager.getSelectedRow().MARK_NAME + " 吗？", function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                type: "POST",
                url: "SignatureList.aspx/deleteSign",
                data: "{'strValue':'" + SelectID + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        SecondManager.loadData();
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