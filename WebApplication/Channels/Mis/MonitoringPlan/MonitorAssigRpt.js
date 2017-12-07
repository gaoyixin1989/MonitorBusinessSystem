
var objGrid = null;
var url = "MonitorAssigRpt.aspx";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    //构建查询表单
    $("#divDetail").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "年度", name: "ddlYear", newline: true, type: "select", group: "查询信息", groupicon: groupicon, comboboxName: "ddlYearBox", options: { valueFieldID: "hidYear", valueField: "value", textField: "value", resize: false, url: url + "?type=GetYear"} },
                      { display: "月份", name: "ddlMonth", newline: false, type: "select", comboboxName: "ddlMonthBox", options: { valueFieldID: "hidMonth", valueField: "value", textField: "value", resize: false, url: url + "?type=GetMonth"} }
                 ]
    });
    $.ligerui.get("ddlYearBox").selectValue(new Date().getFullYear());
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
        columns: [{ display: '年度', name: 'YEAR', align: 'left', width: 150, minWidth: 60 },
                       { display: '月度', name: 'MONTH', align: 'left', width: 150, minWidth: 60 },
                       { display: '委托类别', name: 'TYPE', align: 'left', width: 150, minWidth: 60 },
                       { display: '数量', name: 'COUNT', align: 'left', width: 250, minWidth: 60 }
         ],
        toolbar: { items: [
                  { text: '查询', click: ShowSearch, icon: 'search' }
                ]
        }
    });
    getData(); //一开始获取一次数据
});
//获取数据
function getData() {
    var year = $("#hidYear").val();
    var month = $("#hidMonth").val();
    objGrid.set('url', url + '?type=getGridInfo&year=' + year + '&month=' + month);
}
//查看
var searchDialog = null;
function ShowSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
    searchDialog = $.ligerDialog.open({
        target: $("#divSearchForm"),
        width: 650, height: 200, top: 90, title: "查询",
        buttons: [
                  { text: '确定', onclick: function () { getData(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } }
                  ]
    });
    }
}