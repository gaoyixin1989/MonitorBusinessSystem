// Create by 魏林 2013.07.16  "收文管理功能"功能

var objGrid = null;
var strUrl = "SWList.aspx";
var strHandleUrl = "SWHandle.aspx";
var gridName = '0';
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    $("#layout").ligerLayout({ height: "100%" });

    //列表
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
        url: strUrl + '?action=getGridInfo&strStatus=0',
        columns: [
                { display: '收文编号', name: 'SW_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '原文编号', name: 'FROM_CODE', align: 'left', width: 150, minWidth: 60},
                { display: '来文机关', name: 'SW_FROM', align: 'left', width: 150, minWidth: 60 },
                { display: '收文标题', name: 'SW_TITLE', align: 'left', width: 250, minWidth: 60 },
                { display: '收到日期', name: 'SW_SIGN_DATE', align: 'left', width: 150, minWidth: 60 },
                { display: '审批状态', name: 'SW_STATUS', align: 'left', width: 150, minWidth: 60, render: function (item) {
                    switch (item.SW_STATUS) {
                        case "0":
                            return "<a style='color:Red'>未提交</a>";
                            break;
                        case "1":
                            return "<a style='color:Red'>办公室主任阅示</a>";
                            break;
                        case "2":
                            return "<a style='color:Red'>办公室主任阅示</a>";
                            break;
                        case "3":
                            return "<a style='color:Red'>分管领导阅办</a>";
                            break;
                        case "4":
                            return "<a style='color:Red'>科室办结</a>";
                            break;
                        case "5":
                            return "<a style='color:Red'>办公室归档</a>";
                            break;
                        case "6":
                            return "<a style='color:Red'>已办结</a>";
                            break;
                    }
                }
                }
                ],
        toolbar: { items: [
                { text: '增加', id:'add', click: createData, icon: 'add' },
                { text: '修改', click: updateData, icon: 'modify' },
                { text: '删除', click: deleteData, icon: 'delete' },
                { text: '查询', click: ShowSearch, icon: 'search' },
                { text: '查看', click: viewData, icon: 'add' },
                { text: '打印', click: printData, icon: 'print' }
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
//新增
function createData() {
    var surl = '../Channels/OA/SW/ZZ/' + strHandleUrl + '?action=Add';
    top.f_overTab('增加收文登记单', surl);
}
//修改
function updateData() {
    if (!objGrid.getSelectedRow()) {
        $.ligerDialog.warn('请选择一条记录进行编辑');
        return;
    }
    if (objGrid.getSelectedRow().SW_STATUS != "0") {
        $.ligerDialog.warn('选择的收文已经提交，无法修改');
        return;
    }

    var surl = '../Channels/OA/SW/ZZ/' + strHandleUrl + '?action=Update&ID=' + objGrid.getSelectedRow().ID;
    top.f_overTab('修改收文登记单', surl);
}
//删除
function deleteData() {
    if (objGrid.getSelectedRow() == null) {
        $.ligerDialog.warn('请选择一条记录进行删除');
        return;
    }
    $.ligerDialog.confirm("确认删除登记单信息吗？", function (yes) {
        if (yes == true) {
            var strValue = objGrid.getSelectedRow().ID;
            $.ajax({
                cache: false,
                type: "POST",
                url: strUrl + "/deleteGridInfo",
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
//查看
function viewData() {
    var rowSelected = null;

    rowSelected = objGrid.getSelectedRow();
    
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一条记录进行查看');
        return;
    }
    var surl = '../Channels/OA/SW/ZZ/' + strHandleUrl + '?action=ViewPer&ID=' + rowSelected.ID;
    top.f_overTab('查看收文登记单', surl);
}
//打印、导出
function printData() {
    var rowSelected = null;

    rowSelected = objGrid.getSelectedRow();

    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一条记录进行打印');
        return;
    }
    $("#txtSWID").val(rowSelected.ID);
    $("#btnPrintSW").click();
}

//弹出查询对话框
var searchDialog = null;
var statusJson = [{ 'value': '0,1,2,3,4,5,6', 'text': '--全部--' },
                  { 'value': '0', 'text': '未提交' },
                  { 'value': '1,2,3,4,5', 'text': '在办' },
                  { 'value': '6', 'text': '已办'}];
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
                      { display: "状态", name: "SWSTATUS", newline: true, type: "select", options: { valueFieldID: "hidSWSTATUS", valueField: "value", textField: 'text', initValue: '0', data: statusJson }, group: "查询信息", groupicon: groupicon }, 
                      { display: "原文编号", name: "FROMCODE", newline: true, type: "text" },
                      { display: "收文编号", name: "SWCODE", newline: false, type: "text" },
                      { display: "来文机关", name: "SWFROM", newline: true, type: "text" },
                      { display: "标题", name: "SWTITLE", newline: false, type: "text" },
                      { display: "收到日期", name: "SIGNDATE", newline: true, type: "date" },
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
    //$("#hidSWSTATUS").val("0");
}
