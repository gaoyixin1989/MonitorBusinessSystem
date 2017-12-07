var objGrid = null;
var url = "SampleTimeSearch.aspx";
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var SeasonJson = [{ "VALUE": "0", "OverTime": "--全部--" }, { "VALUE": "1", "OverTime": "是" }, { "VALUE": "2", "OverTime": "否"}];
$(document).ready(function () {
    //创建查询表单结构
    $("#searchForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "实际开始时间", name: "ddlStartTime", newline: true, type: "date", group: "查询信息", groupicon: groupicon },
                      { display: "实际结束时间", name: "ddlEndTime", newline: false, type: "date" },
                      { display: "分析负责人", name: "ddlHEAD_USERID", newline: true, type: "text" },
                      { display: "任务单号", name: "ddlTICKET_NUM", newline: false, type: "text" },
                      { display: "是否超期完成", name: "ddlOverTime", newline: true, type: "select", comboboxName: "ddlOverTimeBox", options: { valueFieldID: "hidOverTime", valueField: "VALUE", textField: "OverTime", resize: false, data: SeasonJson} }
                    ]
    });
    $.ligerui.get("ddlOverTimeBox").selectValue("0"); //是否超期完成
    //构建统计表格
    objGrid = $("#grid").ligerGrid({
        title: '分析及时率统计报表',
        url: url + "?action=GetData", //(服务端分页)
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
                { display: '任务单号', name: 'TICKET_NUM', align: 'left', width: 100, minWidth: 60 },
                { display: '监测类别', name: 'MONITOR_TYPE_NAME', minWidth: 120 },
                { display: '分析负责人', name: 'REAL_NAME', minWidth: 140 },
                { display: '要求完成时间', name: 'SAMPLE_ASK_DATE', minWidth: 140 },
                { display: '实际完成时间', name: 'SAMPLE_FINISH_DATE', minWidth: 140 },
                { display: '是否超期完成', name: 'Is_OverTime', minWidth: 140 }
                ],
        toolbar: { items: [
                { text: '查询', click: showSearch, icon: 'search' }
                ]
        }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
});

//弹出查询窗口
var searchDialog = null;
function showSearch() {
    if (searchDialog) {
        searchDialog.show();
    } else {
        //弹出窗口
        searchDialog = $.ligerDialog.open({
            target: $("#searchDiv"),
            width: 650, height: 200, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { GetSearchData(); clearSearchDialogValue(); searchDialog.hide(); } },
                  { text: '取消', onclick: function () { searchDialog.hide(); } }
                  ]
        });
    }
}
//查询统计数据
function GetSearchData() {
    var StartTime = $("#ddlStartTime").val();
    var EndTime = $("#ddlEndTime").val();
    var HEAD_USERID = encodeURI($("#ddlHEAD_USERID").val()); //获取的值是中文时，为乱码，加上encodeURI（）；
    var TICKET_NUM = encodeURI($("#ddlTICKET_NUM").val());
    var OverTime = $("#hidOverTime").val();
    if (StartTime != "") {
        if (EndTime == "") {
            $.ligerDialog.warn('结束时间不能为空!');
        }
    }
    if (EndTime != "") {
        if (StartTime == "") {
            $.ligerDialog.warn('开始时间不能为空!');
        }
    }
    if (StartTime != "" && EndTime != "") {
        if (StartTime > EndTime) {
            $.ligerDialog.warn('开始时间不能超出结束时间');
        }
    }
    objGrid.set('url', url + "?action=GetData&StartTime=" + StartTime + "&EndTime=" + EndTime + "&HEAD_USERID=" + HEAD_USERID + "&TICKET_NUM=" + TICKET_NUM + "&OverTime=" + OverTime); //客户端分页
}
//查询后，清除条件数据
function clearSearchDialogValue() {
    $("#ddlStartTime").val("");
    $("#ddlEndTime").val("");
    $("#ddlHEAD_USERID").val("");
    $("#ddlTICKET_NUM").val("");
    $("#hidOverTime").val("0");
}