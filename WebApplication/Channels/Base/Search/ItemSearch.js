// Create by 邵世卓 2012.11.28  "项目查询"功能
var firstManager;
var secondManager;
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({height: '100%', topHeight: topHeight });

    //监测项目grid
    window['g'] =
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
        { display: '监测类型', name: 'MONITOR_NAME', width: 100, align: 'left', isSort: false },
        { display: '监测项目', name: 'ITEM_NAME', width: 250, align: 'left', isSort: false },
        { display: '实验室认可', name: 'LAB_CERTIFICATE', width: 100, align: 'left', isSort: false },
        { display: '计量认可', name: 'MEASURE_CERTIFICATE', width: 100, align: 'left', isSort: false },
        { display: '序号', name: 'ORDER_NUM', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: gridHeight,
        title:"监测项目",
        url: 'ItemSearch.aspx?Action=GetItems',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                 { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            var selectedItem = firstManager.getSelectedRow();
            secondManager.set('url', "ItemSearch.aspx?Action=GetMethods&selItemID=" + selectedItem.ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //分析方法grid
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_NAME', width: 150, align: 'left', isSort: false },
        { display: '方法依据', name: 'METHOD', width: 150, align: 'left', isSort: false },
        { display: '分析方法', name: 'ANALYSIS_METHOD', width: 250, align: 'left', isSort: false },
        { display: '仪器', name: 'INSTRUMENT', width: 100, align: 'left', isSort: false },
        { display: '最低检出限', name: 'LOWER_CHECKOUT', width: 80, align: 'left', isSort: false },
        { display: '单位', name: 'UNIT', width: 80, align: 'left', isSort: false },
        { display: '常用分析方法', name: 'IS_DEFAULT', width: 80, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: gridHeight,heightDiff:-10,
         title:'分析方法',
        url: 'ItemSearch.aspx?Action=GetMethods',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
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


// Create by 邵世卓 2012.11.28  "项目查询、查看"功能

//监测项目grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            firstManager.set('url', "ItemSearch.aspx?Action=GetItems");
            break;
        default:
            break;
    }
}

//监测项目grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchItemForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "监测类型", name: "SrhMONITOR_ID", newline: true, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", resize: false, group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhMONITOR_ID", url: "../MonitorType/Select.ashx?view=T_BASE_MONITOR_TYPE_INFO&idfield=ID&textfield=MONITOR_TYPE_NAME&where=is_del|0"} },
                      { display: "监测项目", name: "SrhITEM_NAME", newline: true, type: "text", validate: { required: true, minlength: 3} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 350, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhMONITOR_ID = $("#SrhMONITOR_ID").val();
        var SrhITEM_NAME = escape($("#SrhITEM_NAME").val());

        firstManager.set('url', "ItemSearch.aspx?Action=GetItems&SrhMONITOR_ID=" + SrhMONITOR_ID + "&SrhITEM_NAME=" + SrhITEM_NAME);
    }
}

//监测项目grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SrhITEM_NAME").val("");
}




