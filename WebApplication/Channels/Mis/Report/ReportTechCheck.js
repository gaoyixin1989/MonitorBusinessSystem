// Create by 邵世卓 2012.11.28  "结果汇总表"功能
var firstManager;
var secondManager;
var selectId = "";
var selectedRow = "";
var gridJSON = null;
var url = "ReportTechCheck.aspx";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {

    var topHeight = $(window).height() * 0.2;
    var gridHeight = $(window).height();

    $("#layout1").ligerLayout({ height: '80%', topHeight: topHeight });

    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });

    //任务详细信息
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: url + "?type=getTaskInfo&task_id=" + $("#cphData_TASK_ID").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            SetContractInfo(data);
            selectId = $("#cphData_TASK_ID").val();
        }
    });

    //初始任务信息模块 编制
    function SetContractInfo(data) {
        firstManager = $("#divTaskInfo");
        firstManager.ligerForm({
            inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
            fields: [
        { display: "项目名称", name: "PROJECT_NAME", width: 590, newline: true, type: "text" },
        { display: "任务单号", name: "TICKET_NUM", newline: true, type: "text" },
        { display: "报告单号", name: "REPORT_CODE", newline: false, type: "text" },
        { display: "委托类型", name: "CONTRACT_TYPE", width: 100, newline: false, type: "select", comboboxName: "DROP_CONTRACT_TYPE", options: { valueFieldID: "CONTRACT_TYPE", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Contract_Type" }
        },
        { display: "委托书编号", name: "CONTRACT_CODE", newline: true, type: "text" },
        { display: "签订日期", name: "CONSIGN_DATE", newline: false, space: 10, type: "text" }
        ]
        });
        //赋值
        if (data) {
            $("#REPORT_CODE").val(data.REMARK1);
            $("#PROJECT_NAME").val(data.PROJECT_NAME);
            $("#CONSIGN_DATE").val(data.CONSIGN_DATE);
            $("#CONTRACT_CODE").val(data.CONTRACT_CODE);
            $("#CONTRACT_TYPE").val(data.CONTRACT_TYPE);
            $("#TICKET_NUM").val(data.TICKET_NUM);
        }
        $("#PROJECT_NAME").ligerGetTextBoxManager().setDisabled();
        $("#CONSIGN_DATE").ligerGetTextBoxManager().setDisabled();
        $("#CONTRACT_CODE").ligerGetTextBoxManager().setDisabled();
        $("#DROP_CONTRACT_TYPE").ligerGetComboBoxManager().setDisabled();
        $("#TICKET_NUM").ligerGetTextBoxManager().setDisabled();
    }



    //////////////////////////////////////////////////////////////////////监测结果汇总表 动态生成//////////////////////////////////////////////////////
    //构建监测结果汇总表
    secondManager = $("#secondgrid").ligerGrid({
        title: "监测结果汇总表",
        dataAction: 'server',
        usePager: true,
        alternatingRow: true,
        checkbox: true,
        width: '99%',
        height: gridHeight * 0.5,
        heightDiff: -10,
        enabledEdit: false,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: searchAllResultInfo, icon: 'search' },
                { line: true },
                { id: 'srh', text: '查询', click: searchResultInfo, icon: 'search' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        }
    });

    getData(selectId, ""); //一开始获取一次数据

});

//具体构建监测结果汇总表
function getData(taskId, param) {
    $.ajax({
        type: "POST",
        async: false,
        url: url + "?type=GetData&strTaskId=" + taskId + param,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (json) {
            if (json != null) {
                //构建表格列
                //固定的列
                var columnsArr = [];
                var defaultWidth = 150;

                //动态的列
                $.each(json.UnSureColumns, function (i, row) {
                    if (i > 3) {
                        defaultWidth = (row.columnName.length) * 12; //动态处理项目列宽
                    }
                    columnsArr.push({ display: row.columnName, name: row.columnName, width: defaultWidth, align: "center" });
                });

                secondManager.set("columns", columnsArr);
                secondManager.set("data", json);
            }
            else {
                $.ligerDialog.warn("无可查询数据！");
            }

            $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //去除第一行第一列的复选框
        }
    });
}

// Create by 邵世卓 2012.11.28  "查询、查看"功能

///////////////////////////////////////////////////////////////监测结果汇总表查询///////////////////////////////////////////////
function searchAllResultInfo()//查询所有
{
    getData(selectId, "");
}

var detailWinSrh1 = null;
function searchResultInfo() {
    if (detailWinSrh1) {
        detailWinSrh1.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchFirstForm1");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
            { display: "样品编号", name: "SRH_SAMPLE_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
            { display: "样品种类", name: "SRH_MONITOR_ID", newline: false, type: "select", comboboxName: "DROP_MONITOR_ID", options: { valueFieldID: "MONITOR_ID", valueField: "ID", textField: "MONITOR_TYPE_NAME", url: url + "?type=getMonitorType"} }
                    ]
        });

        detailWinSrh1 = $.ligerDialog.open({
            target: $("#detailSrh1"),
            width: 600, height: 150, top: 90, title: "数据汇总表查询",
            buttons: [
                  { text: '确定', onclick: function () { search1(); clearSearchDialogValue1(); detailWinSrh1.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue1(); detailWinSrh1.hide(); } }
                  ]
        });
    }
    function search1() {
        var SRH_SAMPLE_CODE = $("#SRH_SAMPLE_CODE").val();
        var SRH_MONITOR_ID = $("#MONITOR_ID").val();

        getData(selectId, "&strSampleCode=" + encodeURI(SRH_SAMPLE_CODE) + "&strMonitorId=" + SRH_MONITOR_ID);
    }

    //grid 的查询对话框元素的值 清除
    function clearSearchDialogValue1() {
        $("#SRH_SAMPLE_CODE").val("");
        $("#SRH_MONITOR_ID").val("");
    }
}

//获取字典名称
function GetContractType(code, type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: url + "/GetDataDictName",
        data: "{'strValue':'" + code + "','strType':'" + type + "'}",
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

//获取企业名称
function GetClientName(id) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: url + "/GetClientName",
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

//发送保存
function SendSave() {
    if (selectId == "") {
        $.ligerDialog().warn("任务发送错误！");
        return false;
    }
    $("#cphData_TASK_ID").val(selectId);
}

//退回事件
function BackSend() {
    if (selectId == "") {
        $.ligerDialog().warn("任务回退错误！");
        return false;
    }
    $.ajax({
        cache: false,
        async: true, //设置是否为异步加载,此处必须
        type: "POST",
        url: url + "/BackTask",
        data: "{'strValue':'" + selectId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
        }
    });
    $("#cphData_TASK_ID").val(selectId);
}