var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var vAreaItem = null, vMonthItem = null, vQuarterItem = null, vContratTypeItem = null, vMonitorItems = null;
var strPointId="",strItemId="";
$(document).ready(function () {
    //区域
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=administrative_area",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vAreaItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    //月份
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=month",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vMonthItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetMonitorType",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vMonitorItems = data.Rows;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Contract_Type",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vContratTypeItem = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    $("#SEA_CONTRACT_TYPE").ligerComboBox({
        valueFieldID: "SEA_CONTRACTTYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem, width: 200
    })
    $("#SEA_MONITORTYPE").ligerComboBox({
        valueFieldID: "SEA_MONITORTYPE_OP", valueField: "ID", textField: "MONITOR_TYPE_NAME", data: vMonitorItems, width: 200
    })

    $("#btnSubmit").bind("click", function () {

        search();
    })
    function search() {
        var SEA_YEAR = $("#SEA_YEAR").val();
        if (SEA_YEAR != "") {
            //isNaN验证是否为数字
            if (isNaN(SEA_YEAR)) {
                $.ligerDialog.warn('请输入有效的年份！'); return;
            }
            else {
                if (SEA_YEAR.length != 4) {
                    $.ligerDialog.warn('请输入有效的年份！'); return;
                }
            }
        }
        var SEA_COMPANYNAME = encodeURI($("#SEA_COMPANY").val());
        var SEA_CONTRACT_TYPE = $("#SEA_CONTRACTTYPE_OP").val();
        var SEA_MONITOR = $("#SEA_MONITORTYPE_OP").val();
        var SEA_POINTNAME = $("#SEA_POINTNAME").val();
        var SEA_ITEMNAME = $("#SEA_PSOURCE").val();
        if (SEA_COMPANYNAME == "") {
            $.ligerDialog.warn('请输入企业名称！'); return;
        }
        if (SEA_MONITOR == "") {
            $.ligerDialog.warn('请选择监测类别！'); return
        }
        if (SEA_POINTNAME == "") {
            $.ligerDialog.warn('请输入点位名称！'); return
        }
        if (SEA_ITEMNAME == "") {
            $.ligerDialog.warn('请输入污染源名称！'); return
        }
        self.location.href = "PollutantSourceReport.aspx?strYear=" + SEA_YEAR + "&strCompanyName=" + SEA_COMPANYNAME + "&strPointName=" + SEA_POINTNAME + "&strContractType=" + SEA_CONTRACT_TYPE + "&strItemName=" + SEA_ITEMNAME + "&strMonitor=" + SEA_MONITOR;
    }
})

