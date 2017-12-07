var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var vAreaItem = null, vMonthItem = null, vQuarterItem = null;
$(document).ready(function () {
    $("#navtab1").ligerTab({ contextmenu: false });
    $("#topmenu").ligerMenuBar({ items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
            ]
    });
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

    //季度
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../Contract/MethodHander.ashx?action=GetDict&type=Quarter",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                vQuarterItem = data;
                //定义了sort的比较函数
                vQuarterItem = vQuarterItem.sort(function (a, b) {
                    return a["ID"] - b["ID"]; //升序排列
                    //return b["ID"] - a["ID"];//降序排列
                });
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        },
        error: function (msg) {
            $.ligerDialog.warn('Ajax加载数据失败！' + msg);
        }
    });

    //设置grid 的弹出查询对话框
    var detailWinSrh = null;
    function showDetailSrh() {
        if (detailWinSrh) {
            detailWinSrh.show();
        }
        else {
            //创建表单结构

            var mainform = $("#SrhForm");
            mainform.ligerForm({
                inputWidth: 170, labelWidth: 90, space: 40,
                fields: [
                     { display: "要求完成年度", name: "SEA_YEAR", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "月度", name: "SEA_MONTH", newline: false, type: "select", comboboxName: "SEA_MONTH_BOX", options: { valueFieldID: "SEA_MONTH_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vMonthItem} },
                     { display: "季度", name: "SEA_QUARTER", newline: true, type: "select", comboboxName: "SEA_QUARTER_BOX", options: { valueFieldID: "SEA_QUARTER_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vQuarterItem} },
                     { display: "合同号", name: "SEA_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "执行人", name: "SEA_USERNAME", newline: true, type: "text" },
                     { display: "执行科室", name: "SEA_DEPT", newline: false, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 600, height: 240, top: 90, title: "监测任务完成情况查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

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
            var SEA_MONTH = $("#SEA_MONTH_BOX").val();
            var SEA_QUARTER = $("#SEA_QUARTER_OP").val();
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE").val());
            var SEA_DEPT = encodeURI($("#SEA_DEPT").val());
            var SEA_USERNAME = encodeURI($("#SEA_USERNAME").val());
            self.location.href = "AnalysesFinishReport.aspx?action=GetTaskListData&strYear=" + SEA_YEAR + "&strContractCode=" + SEA_CONTRACT_CODE + "&strMonth=" + SEA_MONTH + "&strQuarter=" + SEA_QUARTER + "&strUserName=" + SEA_USERNAME + "&strDept=" + SEA_DEPT;
            clearSearchDialogValue();
            detailWinSrh.hide();
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_YEAR").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_DEPT").val("");
        $("#SEA_USERNAME").val("");

        $("#SEA_MONTH_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_QUARTER_BOX").ligerGetComboBoxManager().setValue("");
    }
})

