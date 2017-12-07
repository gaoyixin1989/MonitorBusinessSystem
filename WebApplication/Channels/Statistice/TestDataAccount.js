var groupicon = "../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null

$(document).ready(function () {
    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '污染源企业', name: 'COMPANY_NAME', align: 'left', width: 300, minWidth: 60 },
            { display: '委托类别', name: 'CONTRACT_TYPE', align: 'left', width: 120, minWidth: 60 },
            { display: '监测类别', name: 'MONITOR_NAME', width: 100, minWidth: 60 },
            { display: '监测点位', name: 'POINT_NAME', width: 200, minWidth: 60 },
            { display: '样品', name: 'QC_TYPE', width: 100, minWidth: 60 },
            { display: '采样日期', name: 'SAMPLE_FINISH_DATE', width: 100, minWidth: 60 },
            { display: '监测项目', name: 'ITEM_NAME', width: 200, minWidth: 60 },
            { display: '监测结果', name: 'ITEM_RESULT', width: 200, minWidth: 60 }
            ],
        title: '监测数据统计表',
        width: '100%', height: '100%',
        pageSizeOptions: [ 10, 15, 20,50],
        url: "TestDataAccount.aspx?action=GetDataList",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 20,
        alternatingRow: false,
        checkbox: false,
        rownumbers: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                { id: 'Exp', text: '导出', click: ExpExcel, icon: 'modify' }
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
                      { display: "受检单位", name: "SrhCOMPANY_NAME", newline: false, type: "text" },
                      { display: "开始时间", name: "SrhSampleDate_Begin", newline: true, type: "date", format: "yyyy-MM-dd", showTime: "false" },
                      { display: "结束时间", name: "SrhSampleDate_End", newline: false, type: "date", format: "yyyy-MM-dd", showTime: "false" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 600, height: 200, top: 90, title: "查询监测项目",
            buttons: [
                  { text: '确定', onclick: function () { search();  detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearDialogSrh(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SrhCONTRACT_TYPE = $("#SrhCONTRACT_TYPE").val();
        var SrhCOMPANY_NAME = escape($("#SrhCOMPANY_NAME").val());
        var SrhSampleDate_Begin = $("#SrhSampleDate_Begin").val();
        var SrhSampleDate_End = $("#SrhSampleDate_End").val();

        clearDialogSrh();

        $("#hdSrh").val(SrhCOMPANY_NAME + "," + SrhCONTRACT_TYPE + "," + SrhSampleDate_Begin + "," + SrhSampleDate_End);

        maingrid.set('url', "TestDataAccount.aspx?action=GetDataList&SrhCONTRACT_TYPE=" + SrhCONTRACT_TYPE + "&SrhCOMPANY_NAME=" + SrhCOMPANY_NAME + "&SrhSampleDate_Begin=" + SrhSampleDate_Begin + "&SrhSampleDate_End=" + SrhSampleDate_End);
    }

    function clearDialogSrh() {
        $("#SrhCONTRACT_TYPE").val("");
        $("#SrhCOMPANY_NAME").val("");
        $("#SrhSampleDate_Begin").val("");
        $("#SrhSampleDate_End").val("");
        $("#SrhCONTRACT_TYPE_ID").ligerGetComboBoxManager().setValue();
    }
}

