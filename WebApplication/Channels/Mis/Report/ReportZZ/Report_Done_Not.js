// Create by 潘德军 2013.7.9  "报告办理"功能

var firstManager;
var selectId;
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
var arrUserList = null;
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
    //右键菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'Do', text: '确认', click: AcceptReport, icon: 'modify' }
            ]
    });

    //报告办理grid
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
               { display: '报告编制人', name: 'REPORT_SCHEDULER', width: 120, isSort: false, editor: { type: 'select', ext:
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
               }
                ],
        title: "报告办理",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'Report_Done_Not.aspx?action=getReportInfo',
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
                { id: 'Do', text: '确认', click: AcceptReport, icon: 'add' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectId = rowdata.ID;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            AcceptReport();
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});

function showAll() {
    firstManager.set('url', "Report_Done_Not.aspx?action=getReportInfo");
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
                     { display: "委托类型", name: "SRH_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "DROP_CONTRAC_TYPE", options: { valueFieldID: "CONTRAC_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ReportList.aspx?type=getContractType"} },
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

        firstManager.set('url', "Report_Done_Not.aspx?action=getReportInfo&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME));
    }
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SRH_CONTRACT_CODE").val("");
    $("#SRH_COMPANY_ID").val("");
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
        url: "Report_Done.aspx/GetDataDictName",
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
        url: "Report_Done.aspx/GetClientName",
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
//报告办理
function AcceptReport() {
    var rowSelected = firstManager.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一条记录！'); return;
    }
    var strLocalUserId = $("#strLocalUserID").val();
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "Report_Done_Not.aspx?action=getLocalUserId",
        contentType: "application/text; charset=utf-8",
        dataType: "text",
        success: function (data) {
            strLocalUserId = data;
        }
    });

    $.ligerDialog.confirm('是否认领此任务?\r\n', function (yes) {
        if (yes == true) {
            $.ajax({
                cache: false,
                type: "POST",
                url: "Report_Done_Not.aspx/AcceptReport",
                data: "{'strValue':'" + rowSelected.report_id + "','strLocalUserId':'"+strLocalUserId+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.d == "1") {
                        $.ligerDialog.success('数据操成功！');
                        firstManager.loadData();
                    }
                    else {
                        $.ligerDialog.warn('操作失败！');
                    }
                }
            });
        }
    });
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