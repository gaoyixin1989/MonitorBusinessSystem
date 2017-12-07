var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var vAreaItem = null, vMonthItem = null, vQuarterItem = null, vContratTypeItem = null;
var maingrid=null
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
    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '污染源企业', name: 'COMPANY_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '采样日期', name: 'SAMPLE_FINISH_DATE', width: 100, minWidth: 60 },
            { display: '合同号', name: 'CONTRACT_CODE', width: 100, minWidth: 60 },
            { display: '报告号', name: 'REPORT_CODE', width: 100, minWidth: 60 },
            { display: '委托类型', name: 'CONTRACT_TYPENAME', width: 100, minWidth: 60 },
            { display: '超标污染物', name: 'ITEM_NAME', width: 100, minWidth: 60 },
            { display: '查看明细', name: 'VIEWDETIAL', width: 100, minWidth: 60, render: function (item) {
                return "<a href='javascript:ViewDetial(\"" + item.ID + "\");'>查看</a>";
            }
            }
            ],
        title: '污染物超标统计',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: "ContractPolSourceReport.aspx?action=GetPolSourceList",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
                ]
        },
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }

    });
    $("#pageloading").hide();
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隐藏checkAll
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
                     { display: "污染源企业", name: "SEA_COMPANYNAME", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "采样日期", name: "SEA_SAMPLEDATE", newline: true, type: "date" },
                     { display: "区域", name: "SEA_AREA", newline: false, type: "select", comboboxName: "SEA_AREA_BOX", options: { valueFieldID: "SEA_AREA_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vAreaItem} },
                     { display: "合同号", name: "SEA_CONTRACT_CODE", newline: true, type: "text" },
                     { display: "报告号", name: "SEA_REPORT_CODE", newline: false, type: "text" },
                     { display: "委托类型", name: "SEA_CONTRACT_TYPE", newline: true, type: "select", comboboxName: "SEA_CONTRACT_TYPE_BOX", options: { valueFieldID: "SEA_CONTRACT_TYPE_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vContratTypeItem} },
                     { display: "污染源", name: "SEA_POLSOURCE", newline: false, type: "text" }
                    ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 600, height: 240, top: 90, title: "污染源超标统计查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            var SEA_SAMPLEDATE = $("#SEA_SAMPLEDATE").val();
            var SEA_AREA = $("#SEA_AREA_OP").val();
            var SEA_CONTRACT_TYPE = $("#SEA_CONTRACT_TYPE_OP").val();
            var SEA_CONTRACT_CODE = encodeURI($("#SEA_CONTRACT_CODE").val());
            var SEA_REPORT_CODE = encodeURI($("#SEA_REPORT_CODE").val());
            var SEA_POLSOURCE = encodeURI($("#SEA_POLSOURCE_OP").val());
            var SEA_COMPANYNAME = encodeURI($("#SEA_COMPANYNAME").val());
            var strUrl = "ContractPolSourceReport.aspx?action=GetPolSourceList&strContractCode=" + SEA_CONTRACT_CODE + "&strSampleDate=" + SEA_SAMPLEDATE + "&strArea=" + SEA_AREA + "&strContractType=" + SEA_CONTRACT_TYPE + "&strCompanyName=" + SEA_COMPANYNAME + "&strReportCode=" + SEA_REPORT_CODE + "&strPolSource=" + SEA_POLSOURCE;
            maingrid.set('url', strUrl);
            clearSearchDialogValue();
            detailWinSrh.hide();
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_SAMPLEDATE").val("");
        $("#SEA_COMPANYNAME").val("");
        $("#SEA_CONTRACT_CODE").val("");
        $("#SEA_REPORT_CODE").val("");

        $("#SEA_AREA_BOX").ligerGetComboBoxManager().setValue("");
        //$("#SEA_POLSOURCE_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_CONTRACT_TYPE_BOX").ligerGetComboBoxManager().setValue("");
    }
})

function ViewDetial(strTaskid) {
    if (strTaskid != "") {
        $.ligerDialog.open({ title: '污染源超标物明细查看', top: 40, width: 700, height: 420, buttons:
    [{ text: '返回', onclick: function (item, dialog) { dialog.close(); }
    }], url: 'ContractPolSourceReportDetail.aspx?strTask_id=' + strTaskid
        });
    } else {
        return;
    }

}