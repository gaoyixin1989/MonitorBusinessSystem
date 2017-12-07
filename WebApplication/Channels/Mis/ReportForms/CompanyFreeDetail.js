//企业缴费信息记录明细
//创建人：胡方扬 
//创建时间:2013-02-01
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var maingrid = null, vMonthItem = null, vQuarterItem = null;
var SEA_YEAR = "", SEA_MONTH = "", SEA_QUARTER = "", SEA_COMPANYNAME = "";
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
$(document).ready(function () {
    window['g1'] = maingrid = $("#maingrid").ligerGrid({
        columns: [
            { display: '企业名称', name: 'COMPANY_NAME', align: 'left', width: 160, minWidth: 60 },
            { display: '金额', name: 'PART_CODE', width: 100, minWidth: 60, render: function (item) {
                var Total = FloatAdd(item.PAYED, item.NOPAY);
                if (Total != '0') {
                    return "￥"+Total;
                } else {
                    return "￥0";
                }

            }
            },
            { display: '未缴金额', name: 'NOPAY', width: 100, minWidth: 60, render: function (item) {
                return "￥" + item.NOPAY;
            } 
            },
            { display: '已缴金额', name: 'PAYED', width: 100, minWidth: 60, render: function (item) {
                return "￥" + item.PAYED;
            } 
            },
            { display: '查看明细', name: 'VIEWDETIAL', width: 100, minWidth: 60, render: function (item) {
                return "<a href='javascript:ViewDetial(\"" + item.ID + "\");'>查看</a>";
            }
            }
            ],
        title: '企业收费明细',
        width: '100%', height: '100%',
        pageSizeOptions: [5, 10, 15, 20],
        url: "CompanyFreeDetail.aspx?action=GetContractFreeDetial",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        rownumbers: true,
        toolbar: { items: [
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' }
//                { line: true },
//                { id: 'excel', text: '导出', click: exportExcel, icon: 'excel' }
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
                     { display: "缴费年度", name: "SEA_YEAR", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "月度", name: "SEA_MONTH", newline: false, type: "select", comboboxName: "SEA_MONTH_BOX", options: { valueFieldID: "SEA_MONTH_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vMonthItem} },
                     { display: "季度", name: "SEA_QUARTER", newline: true, type: "select", comboboxName: "SEA_QUARTER_BOX", options: { valueFieldID: "SEA_QUARTER_OP", valueField: "DICT_CODE", textField: "DICT_TEXT", data: vQuarterItem} },
                     { display: "受检单位", name: "SEA_TEST_COMPANYNAME", newline: true, type: "text" }
                     ]
            });

            detailWinSrh = $.ligerDialog.open({
                target: $("#detailSrh"),
                width: 600, height: 240, top: 90, title: "企业收费明细查询",
                buttons: [
                  { text: '确定', onclick: function () { search(); } },
                  { text: '返回', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
            });
        }

        function search() {
            SEA_YEAR = $("#SEA_YEAR").val();
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
            SEA_MONTH = $("#SEA_MONTH_BOX").val();
            SEA_QUARTER = $("#SEA_QUARTER_OP").val();
            SEA_COMPANYNAME = encodeURI($("#SEA_TEST_COMPANYNAME").val());
            maingrid.set('url', "CompanyFreeDetail.aspx?action=GetContractFreeDetial&strYear=" + SEA_YEAR + "&strMonth=" + SEA_MONTH + "&strQuarter=" + SEA_QUARTER + "&strCompanyName=" + SEA_COMPANYNAME);
            clearSearchDialogValue();
            detailWinSrh.hide();
        }
    }

    function clearSearchDialogValue() {
        $("#SEA_YEAR").val("");
        $("#SEA_COMPANYNAME").val("");
        $("#SEA_MONTH_BOX").ligerGetComboBoxManager().setValue("");
        $("#SEA_QUARTER_BOX").ligerGetComboBoxManager().setValue("");
    }

    function exportExcel() {
        $.ligerDialog.confirm('您确定要导出记录明细表吗？', function (yes) {
            if (yes == true) {
                var strPartCollarId = "";
                var spit = "";
                var rowSelected = maingrid.getSelectedRows();
                if (rowSelected.length > 1) {
                    for (var i = 0; i < rowSelected.length; i++) {
                        strPartCollarId += spit + rowSelected[i].ID;
                        spit = ",";
                    }
                    $("#hidExportDate").val(strPartCollarId);
                    $("#btnExport").click();
                } else {//如果没有选择则导出全部的
                    $("#btnExport").click();
                }
                maingrid.loadData();
            }
        });
    }
})

function ViewDetial(strCompanyId) {
    if (strCompanyId != "") {
        $.ligerDialog.open({ title: '企业费用明细查看', top: 40, width: 700, height: 420, buttons:
    [{ text: '返回', onclick: function (item, dialog) { dialog.close(); }
        }], url: 'CompanyFreeDetailList.aspx?strCompanyId=' + strCompanyId + '&strYear=' + SEA_YEAR + '&strMonth=' + SEA_MONTH + '&strQuarter=' + SEA_QUARTER
        });
    } else {
    return;
    }
}
function FloatAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2))
    return (arg1 * m + arg2 * m) / m
}