// Create by 潘德军 2013.08.07  "收文管理功能"功能 (查看所有人的收文)

var objGrid = null;
var strUrl = "SwReadList.aspx";
var strHandleUrl = "SWHandle.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout").ligerLayout({ height: "100%" });

    //数据列表
    objGrid = $("#grid").ligerGrid({
        dataAction: 'server',
        usePager: true,
        pageSize: 20,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        width: '100%',
        pageSizeOptions: [10, 15, 20],
        height: '100%',
        url: strUrl + '?action=getGridInfo&strStatus=6',
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '原文编号', name: 'FROM_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '来文机关', name: 'SW_FROM', align: 'left', width: 150, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 250, minWidth: 60 },
                { display: '传阅日期', name: 'READ_DATE', align: 'left', width: 150, minWidth: 60 },
                { display: '是否已阅', name: 'IS_OK', align: 'left', width: 150, minWidth: 60, render: function (item) {
                    switch (item.IS_OK) {
                        case "0":
                            return "<a style='color:Red'>否</a>";
                            break;
                        case "1":
                            return "是";
                            break;
                    }
                }
                }
                ],
        toolbar: { items: [
                { text: '查看', click: viewData, icon: 'add' },
                { text: '查询', click: ShowSearch, icon: 'search' }
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

            viewData();
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

function viewData() {
    var row = objGrid.getSelectedRow();
    if (row == null) {
        $.ligerDialog.warn('请选择一条记录!');
        return;
    }
    else {
        $.ligerDialog.open({ title: '附件下载', width: 500, height: 270, isHidden: false,
            buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=SWFile&id=' + row.ID
        });
    }
}

//弹出查询对话框
var searchDialog = null;
var statusJson = [ { 'value': '6', 'text': '已办结'}];
function ShowSearch() {
    if (searchDialog) {
        searchDialog.show();
    }
    else {
        //创建表单结构
        var divDetail = $("#divDetail");
        divDetail.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "状态", name: "SWSTATUS", newline: true, type: "select", options: { valueFieldID: "hidSWSTATUS", valueField: "value", textField: 'text', initValue: '6', data: statusJson }, group: "查询信息", groupicon: groupicon },
                      { display: "原文编号", name: "FROMCODE", newline: true, type: "text" },
                      { display: "收文编号", name: "SWCODE", newline: false, type: "text" },
                      { display: "来文机关", name: "SWFROM", newline: true, type: "text" },
                      { display: "标题", name: "SWTITLE", newline: false, type: "text" },
                      { display: "传阅日期", name: "SIGNDATE", newline: true, type: "date" },
                      { display: "主题词", name: "SUBJECTWORD", newline: false, type: "text" }
                    ]
        });

        searchDialog = $.ligerDialog.open({
            target: $("#divSearchForm"),
            width: 650, height: 220, top: 90, title: "查询任务",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); searchDialog.hide(); } }
                  ]
        });
    }


}

function search() {
    var FROMCODE = escape($("#FROMCODE").val());
    var SWCODE = escape($("#SWCODE").val());
    var SWFROM = escape($("#SWFROM").val());
    var SWTITLE = escape($("#SWTITLE").val());
    var SIGNDATE = $("#SIGNDATE").val();
    var SUBJECTWORD = escape($("#SUBJECTWORD").val());
    var hidSWSTATUS = $("#hidSWSTATUS").val();

    objGrid.set('url', strUrl + '?action=getGridInfo&strStatus=' + hidSWSTATUS + '&FROMCODE=' + FROMCODE + '&SWCODE=' + SWCODE + '&SWFROM=' + SWFROM + '&SWTITLE=' + SWTITLE + '&SIGNDATE=' + SIGNDATE + '&SUBJECTWORD=' + SUBJECTWORD);

}

function clearSearchDialogValue() {
    $("#FROMCODE").val("");
    $("#SWCODE").val("");
    $("#SWFROM").val("");
    $("#SWTITLE").val("");
    $("#SIGNDATE").val("");
    $("#SUBJECTWORD").val("");
    $("#hidSWSTATUS").val("0");
}
