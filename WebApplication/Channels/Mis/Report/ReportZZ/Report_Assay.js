// Create by 潘德军 2012.11.28  "报告分配"功能
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

    //报告分配grid
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, render: function (record) {
                    return GetContractType(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '委托书编号', name: 'CONTRACT_CODE', minWidth: 150 },
                { display: '项目名称', name: 'PROJECT_NAME', minWidth: 300 },
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
                  { display: '要求完成时间', name: 'RPT_ASK_DATE', type: 'date', format: 'yyyy年MM月dd', width: 100, editor: { type: 'date'} }
                ],
        title: "报告分配",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'Report_Assay.aspx?action=getReportInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        enabledEdit: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: showAll, icon: 'search' },
                { line: true },
                { id: 'srh', text: '查询', click: showDetailSrh, icon: 'search' },
                { line: true },
                 { id: 'send', text: '发送', click: Send, icon: 'modify' },
                 { id: 'over', text: '结束', click: finishTask, icon: 'settings' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectId = rowdata.ID;
        },
        onAfterEdit: AfterEdit,
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

function showAll() {
    firstManager.set('url', "Report_Assay.aspx?action=getReportInfo");
}

//grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
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

        firstManager.set('url', "Report_Assay.aspx?action=getReportInfo&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME));
    }
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SRH_CONTRACT_CODE").val("");
    $("#SRH_PROJECT_NAME").val("");
    $("#CONTRAC_TYPE").val();
    $("#DROP_CONTRAC_TYPE").ligerGetComboBoxManager().setValue("");
}

//获取字典名称
function GetContractType(code, type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "Report_Assay.aspx/GetDataDictName",
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
        url: "Report_Assay.aspx/GetClientName",
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
        url: "Report_Assay.aspx?type=changeReporter&report_id=" + selectedRow.report_id + "&reporter=" + reporter
    });
}

function finishTask() {
    var selectedRow = firstManager.getSelectedRow();
    if (selectedRow == null) {
        $.ligerDialog.warn('请选择一条记录！'); return;
    }
    $.ligerDialog.confirm("是否确定无需编制报告，要结束该任务？", function (yes) {
        if (yes == true) {
            $.ajax({
                type: "post",
                async: false,
                contentType: "application/json; charset=utf-8",
                url: "Report_Assay.aspx?type=finishTask&taskid=" + selectedRow.ID
            });
            $.ligerDialog.success("发送成功！");
            firstManager.loadData();
        }
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
                url: "Report_Assay.aspx?type=sendReporter&report_id=" + selectedRow.report_id
            });
            $.ligerDialog.success("发送成功！");
            firstManager.loadData();
        }
    });
}