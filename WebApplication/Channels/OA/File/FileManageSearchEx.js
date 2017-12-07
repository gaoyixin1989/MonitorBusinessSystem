//档案审核（郑州）
var maingrid = null;
var maingrid1 = null;
var url = "FileManageSearchEx.aspx";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    $("#navtab1").ligerTab({ contextmenu: false, onBeforeSelectTabItem: function (tabid) {
    },
        //在点击选项卡之后触发   点击其他的选项卡后，刷新该选项卡，防止CSS样式被串
        onAfterSelectTabItem: function (tabid) {
            navtab = $("#navtab1").ligerGetTabManager();
            if (tabid == "home") {
                gridName = "0";
            }
            if (tabid == "tabitem1") {
                gridName = "1";
            }
            navtab.reload(navtab.getSelectedTabItemID());
            if (tabid != 'home') {
                isFisrt = false;
                GetPartAcceptList();
            }
            else {
                isFisrt = true;
            }
        }
    });
    maingrid = $("#maingrid").ligerGrid({
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
                { display: '使用状态', name: 'IS_DEL', align: 'left', width: 100, minWidth: 60, render: function (data) {
                    if (data.IS_DEL == "1")
                        return "销毁未审核";
                    else if (data.IS_DEL == "0")
                        return "正常";
                }
                },
                  { display: '档案目录', name: 'DIRECTORY_NAME', align: 'left', width: 120, minWidth: 60 }
            ],
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10],
        url: url + "?action=getDocumentInfo", //(服务端分页)
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showSearch, icon: 'search' },
                                { line: true },
             { id: 'destroy', text: '销毁', click: destroyFile, icon: 'gridwarning' }
                ]
        }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll

    function GetPartAcceptList() {
        maingrid1 = $("#maingrid1").ligerGrid({
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
                { display: '使用状态', name: 'IS_DEL', align: 'left', width: 100, minWidth: 60, render: function (data) {
                    if (data.IS_DEL == "1")
                        return "销毁未审核";
                    else if (data.IS_DEL == "0")
                        return "正常";
                }
                },
                  { display: '档案目录', name: 'DIRECTORY_NAME', align: 'left', width: 120, minWidth: 60 },
                 { display: '颁布时间/修订时间', name: 'UPDATE_DATE', align: 'left', width: 150, minWidth: 60 }
            ],
            width: '100%', height: '100%',
            pageSizeOptions: [5, 10],
            url: url + "?action=getDocumentInfo", //(服务端分页)
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            rownumbers: true,
            toolbar: { items: [
                   { id: 'srh', text: '查询', click: showAcceptDetailSrh, icon: 'search' },
                   { line: true },
                   { id: 'modifyTime', text: '颁布时间/修订时间修改', click: ModifyTime, icon: 'modify' }
                ]
            },
            whenRClickToSelect: true,
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
        $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
    }
    //保存类型
    var arrSaveType;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageSearchEx.aspx?action=getDictJson&dict_type=save_type",
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
        url: "FileManageSearchEx.aspx?action=getDictJsonForSearch&dict_type=save_type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            arrSaveTypeForSearch = data;
        }
    });
    //弹出查询窗口(销毁查询)
    var searchDialog = null;
    function showSearch() {
        if (searchDialog) {
            searchDialog.show();
        } else {
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
            //弹出窗口
            searchDialog = $.ligerDialog.open({
                target: $("#searchDiv"),
                width: 650, height: 250, top: 90, title: "查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); searchDialog.hide(); } }
                  ]
            });
        }
    }
    //设置grid 的弹出查询对话框（新增时间）
    var detailAcceptWinSrh = null;
    function showAcceptDetailSrh() {
        if (detailAcceptWinSrh) {
            detailAcceptWinSrh.show();
        }
        else {
            //创建表单结构
            var mainform1 = $("#SrhForm1");
            mainform1.ligerForm({
                inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
                fields: [
                    { display: "档案编号", name: "SEA_DOCUMENT_CODES", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                     { display: "保存类型", name: "SEA_SAVE_TYPES", labelWidth: 60, width: 100, newline: false, type: "select", comboboxName: "SAVE_TYPE_IDS", options: { valueFieldID: "SEA_SAVE_TYPES", valueField: "DICT_CODE", textField: "DICT_TEXT", data: arrSaveTypeForSearch} },
                     { display: "档案名称", name: "SEA_DOCUMENT_NAMES", newline: true, type: "text", width: 330 },
                     { display: "主题词/关键字", name: "SEA_P_KEYS", newline: true, type: "text", width: 330 },
                     { display: "存放位置", name: "SEA_DOCUMENT_LOCATIONS", newline: true, type: "text", width: 330 }
                    ]
            });
            detailAcceptWinSrh = $.ligerDialog.open({
                target: $("#detailSrh1"),
                width: 660, height: 230, top: 90, title: "查询",
                buttons: [
                  { text: '确定', onclick: function () { searchEx(); clearSearchDialogValue(); detailAcceptWinSrh.hide(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailAcceptWinSrh.hide(); } }
                  ]
            });
        }
    }

});

//查询(销毁查询)
function search() {
    var SEA_DOCUMENT_CODE = encodeURI($("#SEA_DOCUMENT_CODE").val());
    var SEA_SAVE_TYPE = $("#SEA_SAVE_TYPE").val();
    var SEA_DOCUMENT_NAME = encodeURI($("#SEA_DOCUMENT_NAME").val());
    var SEA_P_KEY = encodeURI($("#SEA_P_KEY").val());
    var SEA_DOCUMENT_LOCATION = encodeURI($("#SEA_DOCUMENT_LOCATION").val());
    maingrid.set('url', "FileManageSearchEx.aspx?action=getDocumentInfo&document_code=" + SEA_DOCUMENT_CODE + "&save_type=" + SEA_SAVE_TYPE + "&document_name=" + SEA_DOCUMENT_NAME + "&p_key=" + SEA_P_KEY + "&document_location=" + SEA_DOCUMENT_LOCATION);
}
//查询(新增时间查询)
function searchEx() {
    var SEA_DOCUMENT_CODE = encodeURI($("#SEA_DOCUMENT_CODES").val());
    var SEA_SAVE_TYPE = $("#SEA_SAVE_TYPES").val();
    var SEA_DOCUMENT_NAME = encodeURI($("#SEA_DOCUMENT_NAMES").val());
    var SEA_P_KEY = encodeURI($("#SEA_P_KEYS").val());
    var SEA_DOCUMENT_LOCATION = encodeURI($("#SEA_DOCUMENT_LOCATIONS").val());
    maingrid1.set('url', "FileManageSearchEx.aspx?action=getDocumentInfo&document_code=" + SEA_DOCUMENT_CODE + "&save_type=" + SEA_SAVE_TYPE + "&document_name=" + SEA_DOCUMENT_NAME + "&p_key=" + SEA_P_KEY + "&document_location=" + SEA_DOCUMENT_LOCATION);
}
//查询表单清除
function clearSearchDialogValue() {
    $("#SEA_DOCUMENT_CODE").val("");
    $("#SEA_SAVE_TYPE").val("");
    $("#SEA_DOCUMENT_NAME").val("");
    $("#SEA_P_KEY").val("");
    $("#SEA_DOCUMENT_LOCATION").val("");
}
//借阅状态
function BorrowStatus(document_id) {
    var BorrowStatus;
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageSearchEx.aspx?action=getBorrowStatus&document_id=" + document_id,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            BorrowStatus = data;
        }
    });
    return BorrowStatus;
}
//销毁
function destroyFile() {
    if (!maingrid.getSelectedRow()) {
        $.ligerDialog.warn("请选择档案文件");
        return;
    }
    $.ligerDialog.confirm("确定销毁档案'" + maingrid.getSelectedRow().DOCUMENT_NAME + "'吗？", function (yes) {
        if (yes == true) {
            $.ajax({
                type: "POST",
                async: false,
                url: "FileManageSearchEx.aspx?action=destroyDocumentInfo&document_id=" + maingrid.getSelectedRow().ID,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data == "1")
                        $.ligerDialog.warn("销毁成功");
                    else
                        $.ligerDialog.warn("销毁失败");
                }
            });
            maingrid.loadData();
        }
    });
}
//颁布时间/修订时间修改
var ModifyTimes = null;
function ModifyTime() {
    if (ModifyTimes) {
        ModifyTimes.show();
    }
    else {
        //创建表单结构
        var mainform1 = $("#ModifyTimeDiv");
        mainform1.ligerForm({
            inputWidth: 160, labelWidth: 150, space: 40,
            fields: [
                     { display: "颁布时间/修订时间修改", name: "UPDATE_DATES", newline: true, type: "date", group: "时间修改", groupicon: groupicon }
                    ]
        });
        $("#UPDATE_DATES").val(new Date().getFullYear()+"-"+(new Date().getMonth()+1)+"-"+new Date().getDate());
        detailAcceptWinSrh = $.ligerDialog.open({
            target: $("#ModifyTime"),
            width: 600, height: 150, top: 90, title: "时间修改",
            buttons: [
                  { text: '确定', onclick: function () { ModifyDate();  detailAcceptWinSrh.hide(); } },
                  { text: '取消', onclick: function () { detailAcceptWinSrh.hide(); } }
                  ]
        });
    }
}
function ModifyDate() {
    var UPDATE_DATE = encodeURI($("#UPDATE_DATE").val());
    if (!maingrid1.getSelectedRow()) {
        $.ligerDialog.warn('请选择一条记录进行编辑');
        return;
    }
    if (UPDATE_DATE == "") {
        $.ligerDialog.warn('时间不能为空！');
        return;
    }
    $.ajax({
        type: "POST",
        async: false,
        url: "FileManageSearchEx.aspx?action=ModifyTime&UPDATE_DATE=" + UPDATE_DATE + "&ID=" + maingrid1.getSelectedRow().ID,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            maingrid1.loadData();
        }
    });
}