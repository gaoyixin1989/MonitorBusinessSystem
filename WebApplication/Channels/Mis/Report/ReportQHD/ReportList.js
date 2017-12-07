// Create by 邵世卓 2012.11.28  "报告办理"功能
var firstManager;
var selectId;
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });
    //右键菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'Do', text: '办理', click: itemclick_OfToolbar_UnderItem, icon: 'modify' }
            ]
    });
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
                }
                ],
        title: "报告办理",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'ReportList.aspx?action=getReportInfo',
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
                { id: 'Do', text: '办理', click: itemclick_OfToolbar_UnderItem, icon: 'add' }
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectId = rowdata.ID;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            itemclick_OfToolbar_UnderItem({ id: 'Do' });
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
            firstManager.set('url', "ReportList.aspx?action=getReportInfo");
            break;
        case 'Do':
            DoReport();
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

        firstManager.set('url', "ReportList.aspx?action=getReportInfo&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME));
    }

    $("#SRH_COMPANY_ID").autocomplete("../../Contract/MethodHander.ashx?action=GetCompanyInfo",
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
//报告办理
function DoReport() {
    var rowSelected = firstManager.getSelectedRow();
    if (rowSelected == null) {
        $.ligerDialog.warn('请选择一条记录！'); return;
    }
    var strQuestrow = jQuery.param(rowSelected);
    var tabid = "ReportSchedule"
    var surl = '../Sys/WF/WFStartPage.aspx?action=|id=' + rowSelected.ID + '&WF_ID=RPT';
    top.f_overTab('报告编制', surl);
}

