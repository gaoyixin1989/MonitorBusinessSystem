// Create by weilin 2014.01.07  "报告办理"任务分配功能
var firstManager;
var secondManager;
var selectId = "";
var selectTabId = "0";
var firstFlowName = "";
var acceptanceCode = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {

    //构建发送人表单
    $("#sendForm").ligerForm({
        inputWidth: 160, labelWidth: 90, space: 40, labelAlign: 'right',
        fields: [
                      { display: "报告办理人", name: "ddlUser", newline: true, type: "select", comboboxName: "ddlUserBox", options: { valueFieldID: "hidUser", valueField: "ID", textField: "REAL_NAME", resize: false, url: "ReportDistribution.aspx?type=GetReportUsers"} }
                 ]
    });

    firstManager = $("#divList").ligerGrid({
        columns: [
                { display: '项目名称', name: 'PROJECT_NAME', width: 300 },
                { display: '任务单号', name: 'TICKET_NUM', width: 150 },
                { display: '委托书编号', name: 'CONTRACT_CODE', minWidth: 150 },
                { display: '委托年份', name: 'CONTRACT_YEAR', width: 80 },
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, render: function (record) {
                    return GetContractType(record.CONTRACT_TYPE, "Contract_Type");
                }
                },


                { display: '受检单位', name: 'TESTED_COMPANY_ID', minWidth: 300, render: function (record) {
                    return GetClientName(record.TESTED_COMPANY_ID);
                }
                }
                ],
        title: "",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'ReportDistribution.aspx?action=getReportInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        sortName: 'CREATE_DATE',
        sortOrder: 'DESC',
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem_First, icon: 'search' },
                 { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem_First, icon: 'search' },
                { line: true },
                { id: 'Do', text: '任务分配', click: itemclick_OfToolbar_UnderItem_First, icon: 'add' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectId = rowdata.ID;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            itemclick_OfToolbar_UnderItem_First({ id: 'Do' });
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            menuFirst.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //评价标准grid 的Toolbar click事件
    var sendhDialog = null;
    function itemclick_OfToolbar_UnderItem_First(item) {
        switch (item.id) {
            case 'srh':
                showDetailSrh();
                break;
            case 'srhAll':
                firstManager.set('url', "ReportDistribution.aspx?action=getReportInfo");
                break;
            case 'Do':
                var objSelected = firstManager.getSelectedRow();
                if (objSelected == null) {
                    $.ligerDialog.warn("请选择一行数据！");
                    return;
                }

                if (sendhDialog) {
                    sendhDialog.show();
                } else {
                    sendhDialog = $.ligerDialog.open({
                        target: $("#sendDiv"),
                        width: 300, height: 200, top: 90, title: "选择报告办理人",
                        buttons: [
                            { text: '确定', onclick: function (item, dialog) {
                                if ($("#hidUser").val() == "") {
                                    $.ligerDialog.warn('请选择报告办理人');
                                    return;
                                }
                                objSelected = firstManager.getSelectedRow();
                                
                                $.ajax({
                                    cache: false,
                                    async: false, //设置是否为异步加载,此处必须
                                    type: "POST",
                                    url: "ReportDistribution.aspx/SendToNext",
                                    data: "{'strTaskID':'" + objSelected.ID + "','strUserID':'" + $("#hidUser").val() + "'}",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (data) {
                                        if (data != null) {
                                            if (data.d == "true") {
                                                firstManager.set('url', "ReportDistribution.aspx?action=getReportInfo");
                                                $.ligerDialog.success('任务分配成功')
                                                dialog.hide();
                                            }
                                            else {
                                                $.ligerDialog.warn('任务分配失败！');
                                            }
                                        }
                                        else {
                                            $.ligerDialog.warn('获取数据失败！');
                                        }
                                    },
                                    error: function () {
                                        $.ligerDialog.warn('Ajax加载监测类别数据失败！');
                                    }
                                });

                            }
                            },
                            { text: '取消', onclick: function (item, dialog) { dialog.hide(); } }
                            ]
                    });
                }
                break;
            default:
                break;
        }
    }

});

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
            inputWidth: 150, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                     { display: "任务单号", name: "SRH_TASK_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "委托书编号", name: "SRH_CONTRACT_CODE", newline: false, type: "text" },
                     { display: "项目名称", name: "SRH_PROJECT_NAME", newline: true, type: "text" },
                     { display: "委托类型", name: "SRH_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "DROP_CONTRAC_TYPE", options: { valueFieldID: "CONTRAC_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ReportList.aspx?type=getContractType"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 520, height: 170, top: 90, title: "查询报告信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }
    function search() {
        var SRH_TASK_CODE = $("#SRH_TASK_CODE").val();
        var SRH_CONTRACT_CODE = $("#SRH_CONTRACT_CODE").val();
        var SRH_PROJECT_NAME = $("#SRH_PROJECT_NAME").val();
        var SRH_CONTRACT_TYPE = $("#CONTRAC_TYPE").val();

        firstManager.set('url', "ReportDistribution.aspx?action=getReportInfo&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME) + "&srhTaskCode=" + encodeURI(SRH_TASK_CODE));
    }
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SRH_CONTRACT_CODE").val("");
    $("#SRH_COMPANY_ID").val("");
    $("#CONTRAC_TYPE").val();
    $("#SRH_TASK_CODE").val();
}

//获取字典名称
function GetContractType(code, type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ReportList.aspx/GetDataDictName",
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
        url: "ReportList.aspx/GetClientName",
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

