var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null

$(document).ready(function () {
    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            
            { display: '委托类别', name: 'CONTRACT_TYPE', align: 'left', width: 160, minWidth: 60 },
            { display: '监测类别', name: 'MONITOR_TYPE', width: 160, minWidth: 60 },
            { display: '报告数量', name: 'ACCOUNT', width: 120, minWidth: 60 }
            ],
        title: '报告数据统计表',
        width: '100%', height: '100%',
        pageSizeOptions: [ 10, 15, 20,50],
        url: "ReportDataAccount.aspx?action=GetDataList",
        dataAction: 'server', //服务器排序
        usePager: false,       //服务器分页
        pageSize: 20,
        alternatingRow: false,
        checkbox: false,
        rownumbers: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        whenRClickToSelect: true
    });
    $("#pageloading").hide();
})

function ExpExcel() {
    $("#btnImport").click();
}

//监测项目grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#SrhForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                      { display: "委托类别", name: "SrhCONTRACT_TYPE", newline: true, type: "select", comboboxName: "SrhCONTRACT_TYPE_ID", resize: false, group: "查询信息", groupicon: groupicon, options: { valueFieldID: "SrhCONTRACT_TYPE", url: "../BASE/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|contract_type"} },
                      { display: "监测类别", name: "SrhMONITOR_TYPE", newline: false, type: "select", comboboxName: "SrhMONITOR_TYPE_ID", resize: false, options: { valueFieldID: "SrhMONITOR_TYPE", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: "ReportDataAccount.aspx?Action=getMonitorType"} },
                      { display: "开始时间", name: "SrhSampleDate_Begin", newline: true, type: "date", format: "yyyy-MM-dd", showTime: "false" },
                      { display: "结束时间", name: "SrhSampleDate_End", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 600, height: 200, top: 90, title: "查询",
            buttons: [
                  { text: '确定', onclick: function () { search();  detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearDialogSrh(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhCONTRACT_TYPE = $("#SrhCONTRACT_TYPE").val();
        var SrhMONITOR_TYPE = $("#SrhMONITOR_TYPE").val();
        var SrhSampleDate_Begin = $("#SrhSampleDate_Begin").val();
        var SrhSampleDate_End = $("#SrhSampleDate_End").val();

        clearDialogSrh();

        $("#hdSrh").val(SrhCONTRACT_TYPE + "," + SrhMONITOR_TYPE + "," + SrhSampleDate_Begin + "," + SrhSampleDate_End);
        //alert($("#hdSrh").val());
        maingrid.set('url', "ReportDataAccount.aspx?action=GetDataList&SrhCONTRACT_TYPE=" + SrhCONTRACT_TYPE + "&SrhMONITOR_TYPE=" + SrhMONITOR_TYPE + "&SrhSampleDate_Begin=" + SrhSampleDate_Begin + "&SrhSampleDate_End=" + SrhSampleDate_End);
    }

    function clearDialogSrh() {
        $("#SrhCONTRACT_TYPE").val("");
        $("#SrhMONITOR_TYPE").val("");
        $("#SrhSampleDate_Begin").val("");
        $("#SrhSampleDate_End").val("");
        $("#SrhCONTRACT_TYPE_ID").ligerGetComboBoxManager().setValue();
        $("#SrhMONITOR_TYPE_ID").ligerGetComboBoxManager().setValue();
    }
}

