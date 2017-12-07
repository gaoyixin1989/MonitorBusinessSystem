// Create by 邵世卓 2012.11.28  "报告领取"功能
var firstManager;

var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
    //右键菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'get', text: '报告领取', click: itemclick_OfToolbar_UnderItem, icon: 'modify' }
            ]
    });
    //报告领取grid
    window['g'] =
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
                { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, render: function (record) {
                    return GetContractType(record.CONTRACT_TYPE, "Contract_Type");
                }
                },
                { display: '委托书编号', name: 'CONTRACT_CODE', minWidth: 150 },
                { display: '报告单号', name: 'REPORT_CODE', minWidth: 150 },
                { display: '项目名称', name: 'PROJECT_NAME', minWidth: 300 },
                { display: '委托单位', name: 'CLIENT_COMPANY_ID', minWidth: 140, render: function (record) {
                    return GetClientName(record.CLIENT_COMPANY_ID);
                }
                },
                { display: '是否缴费', name: 'IS_FEE', minWidth: 50, width: 80 },
                { display: '领取方式', name: 'RPT_WAY', minWidth: 50, width: 80, render: function (record) {
                    return GetRptWay(record.CONTRACT_ID);
                }
                },
                { display: '领取状态', name: 'IF_GET', minWidth: 100, width: 100, render: function (items) {
                    if (items.IF_GET == '未领取') {
                        return '<a style="color:Red">未领取</a>'
                    }
                    return items.IF_GET;
                }
                }
                ],
        title: "报告领取",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'ReportManager.aspx?action=getReportInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 15,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem, icon: 'search' },
                { line: true },
                { id: 'get', text: '报告领取', click: itemclick_OfToolbar_UnderItem, icon: 'print' },
                { line: true },
                { id: 'print', text: '报告打印', click: itemclick_OfToolbar_UnderItem, icon: 'print' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
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

// Create by 邵世卓 2012.11.28  "查询、查看"功能

//评价标准grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'srh':
            showDetailSrh();
            break;
        case 'srhAll':
            firstManager.set('url', "ReportManager.aspx?action=getReportInfo");
            break;
        case 'get':
            ReportGet();
            break;
        case 'print':
            ReportPrint();
            break;
        default:
            break;
    }
}

//评价标准grid 的查询对话框
var detailWinSrh = null;
function showDetailSrh() {
    if (detailWinSrh) {
        detailWinSrh.show();
    }
    else {
        //创建表单结构
        var mainform = $("#searchFirstForm");
        mainform.ligerForm({
            inputWidth: 170, labelWidth: 90, space: 40, labelAlign: 'right',
            fields: [
                     { display: "委托书编号", name: "SRH_CONTRACT_CODE", newline: true, type: "text", group: "基本信息", groupicon: groupicon },
                     { display: "报告单号", name: "SRH_REPORT_CODE", newline: false, type: "text" },
                     { display: "委托单位", name: "SRH_COMPANY_ID", newline: true, type: "text" },
                     { display: "委托类型", name: "SRH_CONTRACT_TYPE", newline: false, type: "select", comboboxName: "DROP_CONTRAC_TYPE", options: { valueFieldID: "CONTRAC_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "ReportManager.aspx?type=getContractType"} }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 660, height: 170, top: 90, title: "查询报告领取",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var SRH_CONTRACT_CODE = $("#SRH_CONTRACT_CODE").val();
        var SRH_REPORT_CODE = $("#SRH_REPORT_CODE").val();
        var SRH_COMPANY_ID = srhCompanyId;
        var SRH_CONTRACT_TYPE = $("#CONTRAC_TYPE").val();

        firstManager.set('url', "ReportManager.aspx?action=getReportInfo&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhClientName=" + SRH_COMPANY_ID + "&srhReportCode=" + SRH_REPORT_CODE);
    }

    var srhCompanyId = "";
    $("#SRH_COMPANY_ID").autocomplete("../../../Contract/MethodHander.ashx?action=GetCompanyInfo",
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

//评价标准grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#SRH_CONTRACT_CODE").val("");
    $("#SRH_REPORT_CODE").val("");
    $("#SRH_COMPANY_ID").val("");
    $("#CONTRAC_TYPE").val();
    srhCompanyId = "";
}

//报告领取状态
function GetRptWay(contract_id) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ReportManager.aspx/GetReportWay",
        data: "{'strValue':'" + contract_id + "'}",
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

//获取字典名称
function GetContractType(code, type) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ReportManager.aspx/GetDataDictName",
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
        url: "ReportManager.aspx/GetClientName",
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
//报告收卷
function ReportOver() {
    if (!firstManager.getSelectedRow()) {
        $.ligerDialog.warn('请选择一条记录');
        return;
    }
    var strReturn;
    var strSelectRowId = firstManager.getSelectedRow().ID; //所选报告ID
    var strSelectRowGetStatus = firstManager.getSelectedRow().IF_GET; //所选报告领取状态
    if (strSelectRowGetStatus.indexOf("已领取") >= 0) {
        $.ligerDialog.warn("报告已领取，不可更改收卷状态！");
    }
    else if (strSelectRowGetStatus.indexOf("未领取") >= 0) {
        if (strSelectRowGetStatus.indexOf("未收卷") >= 0) {
            strReturn = "2";
        }
        else if (strSelectRowGetStatus.indexOf("已收卷") >= 0) {
            strReturn = "0";
        }
    }
    $.post("ReportManager.aspx?action=EditStatus&id=" + strSelectRowId + "&status=" + strReturn, function (data) {
        firstManager.loadData();
    });
}
//报告领取
function ReportGet() {
    if (!firstManager.getSelectedRow()) {
        $.ligerDialog.warn('请选择一条记录');
        return;
    }
    var strReturn;
    var strSelectRowId = firstManager.getSelectedRow().ID; //所选报告ID
    var strSelectRowGetStatus = firstManager.getSelectedRow().IF_GET; //所选报告领取状态
    if (strSelectRowGetStatus.indexOf("已领取") >= 0) {
        strReturn = "0";
    }
    else if (strSelectRowGetStatus.indexOf("未领取") >= 0) {
        strReturn = "1";
    }
    $.post("ReportManager.aspx?action=EditStatus&id=" + strSelectRowId + "&status=" + strReturn, function (data) {
        firstManager.loadData();
    });
}
//报告打印
function ReportPrint() {
    if (!firstManager.getSelectedRow()) {
        $.ligerDialog.warn('请选择一条记录');
        return;
    }
    //alert(firstManager.getSelectedRow().ID + "|" + firstManager.getSelectedRow().TASK_ID);
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=ReportQHD&id=' + firstManager.getSelectedRow().TASK_ID
    });
}

