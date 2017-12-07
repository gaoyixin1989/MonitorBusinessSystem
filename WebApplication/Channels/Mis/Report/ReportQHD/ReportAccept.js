// Create by 邵世卓 2012.11.28  "报告办理"功能
var firstManager;
var selectId = "";
var selectedRow = "";
var arrUserList = null;
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    //用户数据集
    $.ajax({
        type: "GET",
        async: false,
        url: "../SelectUser.aspx?type=getUserList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null)
                arrUserList = data;
        }
    });

    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
    //报告领取grid
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: '任务单号', name: 'TICKET_NUM', width: 100 },
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, render: function (record) {
                    return GetContractType(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '委托书编号', name: 'CONTRACT_CODE', minWidth: 150 },
                { display: '项目名称', name: 'PROJECT_NAME', minWidth: 300 },
                { display: '委托单位', name: 'CLIENT_COMPANY_ID', minWidth: 140, render: function (record) {
                    return GetClientName(record.CLIENT_COMPANY_ID);
                }
                },
                { display: '受检单位', name: 'TESTED_COMPANY_ID', minWidth: 140, render: function (record) {
                    return GetClientName(record.TESTED_COMPANY_ID);
                }
                },
                 { display: '报告编制人', name: 'REPORT_SCHEDULER', width: 120, isSort: false,
                     editor: { type: 'select',
                         ext:
                    function (rowdata) {
                        return {
                            onBeforeOpen: f_selectReportEditor,
                            render: function () {
                                selectedRow = rowdata;
                                for (var i = 0; i < arrUserList.length; i++) {
                                    if (arrUserList[i]['ID'] == rowdata.REPORT_SCHEDULER)
                                        return arrUserList[i]['REAL_NAME'];
                                }
                            }
                        };
                    }
                     }, render: function (item) {
                         for (var i = 0; i < arrUserList.length; i++) {
                             if (arrUserList[i]['ID'] == item.REPORT_SCHEDULER)
                                 return arrUserList[i]['REAL_NAME'];
                         }
                         return item.REPORT_SCHEDULER;
                     }
                 },
                 { display: '要求完成时间', name: 'RPT_ASK_DATE', align: 'left', width: 100, minWidth: 60,
                     render: function (record, rowindex, value) {
                         var strRPT_ASK_DATE = value;
                         var strRPT_ASK_DATETemp = "";
                         if (strRPT_ASK_DATE != "")
                             strRPT_ASK_DATETemp = strRPT_ASK_DATE;
                         else
                             strRPT_ASK_DATETemp = "请选择";
                         return "<a href=\"javascript:getDate('RPT_ASK_DATE'," + rowindex + ")\">" + strRPT_ASK_DATETemp + "</a> ";
                     }
                 }
                ],
        title: "报告分配",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'ReportAccept.aspx?action=getReportInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        enabledEdit: true,
        onAfterEdit: AfterEdit,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                 { id: 'send', text: '发送', click: itemclick_OfToolbar_UnderItem, icon: 'modify' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectId = rowdata.ID;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    function AfterEdit(e) {
        var strReportId = e.record.report_id;
        var strRPT_ASK_DATE = Todate(e.record.RPT_ASK_DATE);
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "Report_Assay.aspx/saveRptAskDate",
            data: "{'strReportId':'" + strReportId + "','strRPT_ASK_DATE':'" + strRPT_ASK_DATE + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, textStatus) {
                if (data.d == 1) {
                    firstManager.cancelEdit(e);
                }
            }
        });
    }
});

function Todate(num) {
    //Fri Oct 31 18:00:00 UTC+0800 2008  
    num = num + "";
    var date = "";
    var month = new Array();
    month["Jan"] = 1; month["Feb"] = 2; month["Mar"] = 3; month["Apr"] = 4; month["May"] = 5; month["Jan"] = 6;
    month["Jul"] = 7; month["Aug"] = 8; month["Sep"] = 9; month["Oct"] = 10; month["Nov"] = 11; month["Dec"] = 12;

    str = num.split(" ");
    date = str[5] + "-" + month[str[1]] + "-" + str[2];
    return date;
}

// Create by 邵世卓 2012.11.28  "查询、查看"功能

//评价标准grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            firstManager.set('url', "ReportAccept.aspx?action=getReportInfo");
            break;
        case "send":
            Send();
            break;
        default:
            break;
    }
}

//grid 的查询对话框
var detailWinSrh = null;
var srhCompanyId = "";
function showDetailSrh() {
    //清空企业ID
    srhCompanyId = "";
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchFirstForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                     { display: "委托书编号", name: "SRH_CONTRACT_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "委托类型", name: "SRH_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "DROP_CONTRAC_TYPE", options: { valueFieldID: "CONTRAC_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ReportAccept.aspx?type=getContractType"} },
                     { display: "项目名称", name: "SRH_PROJECT_NAME", newline: true, width: 300, type: "text" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 600, height: 170, top: 90, title: "查询报告信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }
    function search() {
        var SRH_CONTRACT_CODE = $("#SRH_CONTRACT_CODE").val();
        var SRH_PROJECT_NAME = $("#SRH_PROJECT_NAME").val();
        var SRH_CONTRACT_TYPE = $("#CONTRAC_TYPE").val();

        firstManager.set('url', "ReportAccept.aspx?action=getReportInfo&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME));
    }

    $("#SRH_COMPANY_ID").autocomplete("../Contract/MethodHander.ashx?action=GetCompanyInfo",
    {
        max: 12,     // 列表里的条目数 
        minChars: 0,     // 自动完成激活之前填入的最小字符 
        matchContains: true,     // 包含匹配，就是data参数里的数据，是否只要包含文本框里的数据就显示 
        autoFill: false,     // 自动填充 
        width: 250,
        max: 20,
        dataType: "json",
        scrollHeight: 150,
        parse: function (data) {
            return $.map(eval(data), function (row) {
                return {
                    data: row,
                    value: row.ID + " <" + row.COMPANY_NAME + ">",
                    result: row.COMPANY_NAME
                }
            });
        },
        formatItem: function (item) {
            return item.COMPANY_NAME;
        }
    });

    $("#SRH_COMPANY_ID").result(function (event, data, formatted) {
        srhCompanyId = data["ID"]; //获取选择的ID
    });
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SRH_CONTRACT_CODE").val("");
    $("#SRH_COMPANY_ID").val("");
    $("#CONTRAC_TYPE").val();
    srhCompanyId = "";
}

//获取字典名称
function GetContractType(code, type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ReportAccept.aspx/GetDataDictName",
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
        url: "ReportAccept.aspx/GetClientName",
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
//报告编制人
function f_selectReportEditor() {
    $.ligerDialog.open({
        title: '选择报告编制人',
        width: 410,
        height: 250,
        url: '../SelectUser.aspx?task_id=' + selectedRow.ID,
        buttons: [{ text: '确定', onclick: f_selectReportEditorOK },
        { text: '取消', onclick: f_selectReportEditorCancel}]
    });
    return false;
}
//确定报告编制人选择
function f_selectReportEditorOK(item, dialog) {
    var data = dialog.frame.selectRow()[0];
    if (!data) {
        $.ligerDialog.warn('请选择报告编制人!');
        return;
    }
    changeReportScheduler(data.ID);
    firstManager.updateCell('REPORT_SCHEDULER', data.ID, selectedRow);
    firstManager.endEdit();
    dialog.close();

}
//取消报告编制人选择
function f_selectReportEditorCancel(item, dialog) {
    dialog.close();
}
//更改报告编制人
function changeReportScheduler(reporter) {
    if (reporter == null || reporter == "") {
        $.ligerDialog.warn("请选择报告编制人！");
        return;
    }
    $.ajax({
        type: "post",
        async: false,
        contentType: "application/json; charset=utf-8",
        url: "ReportAccept.aspx?type=changeReporter&report_id=" + selectedRow.report_id + "&reporter=" + reporter
    });
}
//发配任务
function Send() {
    var selectedRow = firstManager.getSelectedRow();
    if (selectedRow == null) {
        $.ligerDialog.warn('请选择一条记录！'); return;
    }
    
    if (selectedRow.REPORT_SCHEDULER == null || selectedRow.REPORT_SCHEDULER == "") {
        $.ligerDialog.warn('请选择报告编制人！'); return;
    }
    $.ligerDialog.confirm("是否确定发送？", function (yes) {
        if (yes == true) {
            $.ajax({
                type: "post",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "ReportAccept.aspx?type=sendReporter&report_id=" + selectedRow.report_id + "&rptaskdate" + selectedRow.RPT_ASK_DATE
            });
            $.ligerDialog.success("发送成功！");
            firstManager.loadData();
        }
    });
}
//弹出选择分析完成时间
function getDate(strColumnName, iRow) {
    $.ligerDialog.open({ title: "录入要求完成时间", width: 400, height: 300, isHidden: false, buttons:
        [
        { text:
        '确定', onclick: function (item, dialog) {
            firstManager.updateCell(strColumnName, $("iframe")[0].contentWindow.GetDate(), iRow);
            dialog.close();
        }
        }, { text:
        '关闭', onclick: function (item, dialog) { dialog.close(); }
        }], url: "../../../Mis/Monitor/Result/FinishDateSelected.aspx"
    });
}