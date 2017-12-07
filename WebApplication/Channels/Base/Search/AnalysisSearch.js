// Create by 邵世卓 2012.11.28  "项目查询"功能
var StandardManager;

var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    $("#layout1").ligerLayout({  height: '100%'});

    //评价标准grid
    window['g'] =
    StandardManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: 'ID', name: 'ID', align: 'left', width: 100, minWidth: 60, hide: 'true' },
                { display: '标准代码', name: 'STANDARD_CODE', align: 'left', width: 100, minWidth: 60 },
                { display: '标准名称', name: 'STANDARD_NAME', minWidth: 120 },
                { display: '标准类型', name: 'STANDARD_TYPE', minWidth: 140, render: function (item) {
                    return GetStandard(item.STANDARD_TYPE);
                }
                },
                { display: '监测类型', name: 'MONITOR_ID', minWidth: 140, render: function (item) {
                    return GetMonitor(item.MONITOR_ID);
                }
                }
                ],
        title: "评价标准",
        width: '100%',
        height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: 'AnalysisSearch.aspx?action=GetEvaluData',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                { id: 'set', text: '阀值查看', click: itemclick_OfToolbar_UnderItem, icon: 'database_wrench' },
                { line: true },
                { id: 'load', text: '附件下载', click: itemclick_OfToolbar_UnderItem, icon: 'bookpen' }
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
});

//获取标准类型数据下拉列表
function GetStandard(id) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "AnalysisSearch.aspx/GetStandard",
        data: "{'strValue':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });
    return strReturn;
}

//获取监测值类型下拉列表 
function GetMonitor(id) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "AnalysisSearch.aspx/GetMonitor",
        data: "{'strValue':'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function () {
            $.ligerDialog.warn('Ajax加载监测类别数据失败！');
        }
    });
    return strReturn;
}

// Create by 邵世卓 2012.11.28  "评价标准查询、查看"功能

//评价标准grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'view':
            var selected = StandardManager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要查看的记录！'); ; return }

            showDetail({
                STANDARD_ID: selected.ID,
                STANDARD_CODE: selected.STANDARD_CODE,
                STANDARD_NAME: selected.STANDARD_NAME,
                STANDARD_TYPE_NAME: GetStandard(selected.STANDARD_TYPE),
                MONITOR_NAME: GetMonitor(selected.MONITOR_ID)
            }, false);
            break;
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            StandardManager.set('url', "AnalysisSearch.aspx?action=GetEvaluData");
            break;
        case 'load':
            downLoadFile();
            break;
        case "set":
            var selected = StandardManager.getSelectedRow();
            if (!selected) { $.ligerDialog.warn('请先选择要进行配置的记录！'); return; }
            var rownu = StandardManager.getCheckedRows().length
            if (rownu > 1) { $.ligerDialog.warn('【阀值查看】操作只能选择一条记录'); return; }
            var strstandartId = selected.ID;

            $.ligerDialog.open({ url: 'EvaluationTapView.aspx?standartId=' + strstandartId + '', height: 580, width: 700, top: 0, title: '阀值查看' });
            break;
        default:
            break;
    }
}

//评价标准grid 的编辑对话框及save函数
var detailWin = null, curentData = null, currentIsAddNew;
function showDetail(data, isAddNew) {
    curentData = data;
    currentIsAddNew = isAddNew;
    if (detailWin) {
        detailWin.show();
    }
    else {
        //创建表单结构
        var mainform = $("#editFirstForm");
        mainform.ligerForm({
            inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "标准编码", name: "STANDARD_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                      { display: "标准名称", name: "STANDARD_NAME", newline: false, type: "text" },
                      { display: "标准类型", name: "STANDARD_TYPE_NAME", newline: true, type: "text" },
                       { display: "监测类型", name: "MONITOR_NAME", newline: false, type: "text"}]
        });

        detailWin = $.ligerDialog.open({
            target: $("#detailFirst"),
            width: 650, height: 200, top: 90, title: "评价标准信息",
            buttons: [
                  { text: '取消', onclick: function () { clearDialogValue(); detailWin.hide(); } }
                  ]
        });
    }
    if (curentData) {
        $("#STANDARD_CODE").val(curentData.STANDARD_CODE);
        $("#STANDARD_NAME").val(curentData.STANDARD_NAME);
        $("#STANDARD_TYPE_NAME").val(curentData.STANDARD_TYPE_NAME);
        $("#MONITOR_NAME").val(curentData.MONITOR_NAME);
    }
    else {
        $("#STANDARD_CODE").val("");
        $("#STANDARD_NAME").val("");
        $("#STANDARD_TYPE_NAME").val("");
        $("#MONITOR_NAME").val("");
    }
}

//评价标准grid 的编辑对话框元素的值 清除
function clearDialogValue() {
    $("#STANDARD_CODE").val("");
    $("#STANDARD_NAME").val("");
    $("#STANDARD_TYPE_NAME").val("");
    $("#MONITOR_NAME").val("");
}

//评价标准grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchFirstForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                     { display: "标准编码", name: "SEA_STANDARD_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "标准名称", name: "SEA_STANDARD_NAME", newline: false, type: "text" },
                     { display: "标准类型", name: "STANDARD_ID", newline: true, type: "select", comboboxName: "STANDARD_TYPE_ID", options: { valueFieldID: "STANDARD_ID", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "AnalysisSearch.aspx?type=getStandardType"} },
                     { display: "监测类型", name: "MONITOR_ID", newline: false, type: "select", comboboxName: "MONITOR_TYPE_ID", options: { valueFieldID: "MONITOR_ID", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "AnalysisSearch.aspx?type=getMonitorType"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 660, height: 170, top: 90, title: "查询评价标准",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_STANDARD_CODE = encodeURI($("#SEA_STANDARD_CODE").val());
        var SEA_STANDARD_NAME = encodeURI($("#SEA_STANDARD_NAME").val());
        var SEA_STANDARD_TYPE_OP = $("#STANDARD_ID").val();
        var SEA_MONITOR_ID_OP = $("#MONITOR_ID").val();

        StandardManager.set('url', "AnalysisSearch.aspx?action=GetEvaluData&srhStandard_Code=" + SEA_STANDARD_CODE + "&srhStandard_Name=" + SEA_STANDARD_NAME + "&srhStandard_Type=" + SEA_STANDARD_TYPE_OP + "&srhMonitor_Id=" + SEA_MONITOR_ID_OP);
    }
}

//评价标准grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SEA_STANDARD_CODE").val("");
    $("#SEA_STANDARD_NAME").val("");
}

///附件下载
function downLoadFile() {
    if (StandardManager.getSelectedRow() == null) {
        $.ligerDialog.warn('下载附件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=Evaluation&id=' + StandardManager.getSelectedRow().ID
    });
}


