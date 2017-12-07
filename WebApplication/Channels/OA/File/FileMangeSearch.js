var objGrid = null;
var url = "FileMangeSearch.aspx";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    //保存类型
    var arrSaveType;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileMangeSearch.aspx?action=getDictJson&dict_type=save_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            arrSaveType = data;
        }
    });
    //保存类型（查询）
    var arrSaveTypeForSearch;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileMangeSearch.aspx?action=getDictJsonForSearch&dict_type=save_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            arrSaveTypeForSearch = data;
        }
    });
    //创建查询表单结构
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                     { display: "档案编号", name: "SEA_DOCUMENT_CODE", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                        { display: "保存类型", name: "SEA_SAVE_TYPE", labelWidth: 60, width: 100, newline: false, type: "select", comboboxName: "SAVE_TYPE_ID", options: { valueFieldID: "SEA_SAVE_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", data: arrSaveTypeForSearch} },
                        { display: "档案名称", name: "SEA_DOCUMENT_NAME", newline: true, type: "text", width: 330 },
                        { display: "主题词/关键字", name: "SEA_P_KEY", newline: true, type: "text", width: 330 },
                        { display: "存放位置", name: "SEA_DOCUMENT_LOCATION", newline: true, type: "text", width: 330 }
                    ]
    });
    //构建统计表格
    objGrid = $("#grid").ligerGrid({
        title: '档案管理审核',
        url: url + "?action=getDocumentInfo", //(服务端分页)
        dataAction: 'server',
        usePager: true,
        pageSize: 30,
        pageSizeOptions: [30, 40, 50, 60],
        alternatingRow: true,
        checkbox: true,
        enabledEdit: true,
        width: '100%',
        height: '100%',
        columns: [
                { display: '档案编号', name: 'DOCUMENT_CODE', align: 'left', width: 150, minWidth: 60 },
                { display: '档案名称', name: 'DOCUMENT_NAME', align: 'left', width: 150, minWidth: 60 },
                { display: '版本号', name: 'VERSION', align: 'left', width: 50, minWidth: 50, minWidth: 60 },
                { display: '保存类型', name: 'SAVE_TYPE', align: 'left', width: 100, minWidth: 60, render: function (data) {
                    for (var i = 0; i < arrSaveType.length; i++) {
                        if (arrSaveType[i].DICT_CODE == data.SAVE_TYPE)
                            return arrSaveType[i].DICT_TEXT;
                    }
                } 
                },
                { display: '保存年份', name: 'SAVE_YEAR', align: 'left', width: 100, minWidth: 60 },
                { display: '借阅状态', name: 'BORROW_STATUS', align: 'left', width: 100, minWidth: 60, render: function (data) {
                    var status = BorrowStatus(data.ID);
                    if (status == "1")
                        return "已借出";
                    else
                        return "未借出";
                } 
                },
                { display: '使用状态', name: 'IS_OVER', align: 'left', width: 100, minWidth: 60 , render: function (data) {
                    if (data.IS_OVER == "1")
                        return "已废止";
                    else
                        return "正常";
                }
            },
                  { display: '档案目录', name: 'Catalog', align: 'left', width: 120,minWidth: 60 }
                ],
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' },
                { line: true },
                { id: 'destroy', text: '销毁', click: destroyFile, icon: 'gridwarning' }
                ]
        }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
});
//借阅状态
function BorrowStatus(document_id) {
    var BorrowStatus;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileMangeSearch.aspx?action=getBorrowStatus&document_id=" + document_id,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            BorrowStatus = data;
        }
    });
    return BorrowStatus;
}
//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        //弹出窗口
        searchDialog = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 250, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } }
                  ]
        });
    }
}
//查询表单清除
function clearSearchDialogValue() {
    $("#SEA_DOCUMENT_CODE").val("");
    $("#SEA_SAVE_TYPE").val("");
    $("#SEA_DOCUMENT_NAME").val("");
    $("#SEA_P_KEY").val("");
    $("#SEA_DOCUMENT_LOCATION").val("");
}
//查询
function search() {
    var SEA_DOCUMENT_CODE = encodeURI($("#SEA_DOCUMENT_CODE").val());
    var SEA_SAVE_TYPE = $("#SEA_SAVE_TYPE").val();
    var SEA_DOCUMENT_NAME = encodeURI($("#SEA_DOCUMENT_NAME").val());
    var SEA_P_KEY = encodeURI($("#SEA_P_KEY").val());
    var SEA_DOCUMENT_LOCATION = encodeURI($("#SEA_DOCUMENT_LOCATION").val());
    objGrid.set('url', "FileMangeSearch.aspx?action=getDocumentInfo&document_code=" + SEA_DOCUMENT_CODE + "&save_type=" + SEA_SAVE_TYPE + "&document_name=" + SEA_DOCUMENT_NAME + "&p_key=" + SEA_P_KEY + "&document_location=" + SEA_DOCUMENT_LOCATION);
}
//销毁
function destroyFile() { }

