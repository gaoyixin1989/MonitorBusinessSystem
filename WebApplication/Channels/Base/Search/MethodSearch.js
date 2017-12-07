//// Create by 邵世卓 2012.11.28  "方法依据查询"功能
var MethodManager;
var AnalysisManager;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //方法依据grid
    window['g'] =
        MethodManager = $("#firstgrid").ligerGrid({
            dataAction: 'server',
            usePager: true,
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            onRClickToSelect: true,
            sortName: "METHOD_CODE",
            width: '100%',
            pageSizeOptions: [5, 10, 15, 20],
            whenRClickToSelect: true,
            height: gridHeight,
             title:"方法依据",
            url: 'MethodSearch.aspx?type=getMethodInfo',
            columns: [
                    { display: '方法依据代码', name: 'METHOD_CODE', align: 'left', width: 200, minWidth: 60 },
                    { display: '方法依据名称', name: 'METHOD_NAME', minWidth: 250 },
                    { display: '监测类别', name: 'MONITOR_ID', minWidth: 140, render: function (record) {
                        return getMonitorTypeNameEx(record.MONITOR_ID);
                    }
                    }
                    ],
            toolbar: { items: [
                                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                                { line: true },
                                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                                { line: true },
                                { id: 'load', text: '附件下载', click: itemclick_OfToolbar_UnderItem, icon: 'bookpen' }
                              ]
            },
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);
                var selectedItem = MethodManager.getSelectedRow();
                AnalysisManager.set('url', "MethodSearch.aspx?type=getAnalysisInfo&appId=" + selectedItem.ID);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        }
        );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //分析方法grid
    AnalysisManager = $("#secondgrid").ligerGrid({
        columns: [
                    { display: '方法依据代码', name: 'APPARATUS_ID', align: 'left', width: 200, minWidth: 60, render: function (record) {
                        return getMonitorCode(record.METHOD_ID);
                    }
                    },
                    { display: '方法依据名称', name: 'APPARATUS_NAME', minWidth: 250, render: function (record) {
                        return getMonitorName(record.METHOD_ID);
                    }
                    },
                    { display: '分析方法名称', name: 'ANALYSIS_NAME', minWidth: 140 }
                    ], width: '100%', pageSizeOptions: [5, 10, 15, 20], height: gridHeight,heightDiff:-10,
                dataAction: 'server',
         title:"分析方法",
        usePager: true,
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        onRClickToSelect: true,
        whenRClickToSelect: true,
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

////获取监测类别名称
function getMonitorTypeNameEx(strMonitorTypeName) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "MethodSearch.aspx/getMonitorTypeName",
        data: "{'strValue':'" + strMonitorTypeName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取方法依据代码
function getMonitorCode(strMethodId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "MethodSearch.aspx/getMonitorCode",
        data: "{'strValue':'" + strMethodId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//获取方法依据名称
function getMonitorName(strMethodId) {
    var strValue = "";
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "MethodSearch.aspx/getMonitorName",
        data: "{'strValue':'" + strMethodId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, textStatus) {
            strValue = data.d;
        }
    });
    return strValue;
}

//// Create by 邵世卓 2012.11.28  "方法依据查询、查看、下载"功能

//方法依据grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            MethodManager.set('url', "MethodSearch.aspx?type=getMethodInfo");
            break;
        case 'load':
            downLoadFile();
            break;
        default:
            break;
    }
}

//方法依据grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchMethodForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                        { display: "方法依据代码", name: "SEA_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                        { display: "方法依据名称", name: "SEA_NAME", newline: true, type: "text" },
                        { display: "监测类别", name: "MONITOR_ID_NAME", newline: true, type: "select", comboboxName: "MONITOR_ID_NAME_BOX", options: { valueFieldID: "MONITOR_ID_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "MethodSearch.aspx?type=getMonitorType"} }
                        ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "方法依据查询",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SEA_CODE = encodeURI($("#SEA_CODE").val());
        var SEA_NAME = encodeURI($("#SEA_NAME").val());
        var SEA_MONITOR_ID = encodeURI($("#MONITOR_ID_OP").val());

        MethodManager.set('url', "MethodSearch.aspx?type=getMethodInfo&srhCode=" + SEA_CODE + "&srhYear= " + SEA_NAME + "&srhMonitorId=" + SEA_MONITOR_ID);
    }
}

//方法依据grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SEA_CODE").val("");
    $("#SEA_NAME").val("");
}

///附件下载
function downLoadFile() {
    if (MethodManager.getSelectedRow() == null) {
        $.ligerDialog.warn('下载附件之前请先选择一条记录');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=Method&id=' + MethodManager.getSelectedRow().ID
    });
}


