// Create by 邵世卓 2012.11.28  "报告办理"功能
var firstManager;
var secondManager;
var selectId = "";
var selectTabId = "0";
var firstFlowName = "";
var acceptanceCode = "";
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";
$(document).ready(function () {
    $("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: '100%' });

    // 默认固定的验收委托类型
    $.ajax({
        type: "POST",
        async: false,
        url: "ReportList.aspx/GetDataDictName",
        data: "{'strValue':'acceptance_code','strType':'dict_system_base'}",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                acceptanceCode = data.d;
            }
        }
    });
    //////////////////////////////////////////////////////未确认任务grid/////////////////////////////////////////////////////////////

    //未确认任务右键菜单
    menuFirst = $.ligerMenu({ width: 120, items:
            [
            { id: 'Do', text: '任务办理', click: itemclick_OfToolbar_UnderItem_First, icon: 'modify' }
            ]
    });
    firstManager = $("#firstgrid").ligerGrid({
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
                },
                { display: '监测类型', name: 'REMARK1', width: 120 }
                ],
        title: "",
        width: '100%',
        height: '100%',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'ReportList.aspx?action=getReportInfo&tabType=' + selectTabId,
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
                { id: 'Do', text: '任务办理', click: itemclick_OfToolbar_UnderItem_First, icon: 'add' }
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
    function itemclick_OfToolbar_UnderItem_First(item) {
        switch (item.id) {
            case 'srh':
                showDetailSrh();
                break;
            case 'srhAll':
                firstManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=" + selectTabId);
                break;
            case 'Do':
                if (acceptanceCode == firstManager.getSelectedRow().CONTRACT_TYPE)//如果是验收监测
                {
                    
                    if (!AcceptanceFilter()) {
                        ComfirmTask(selectId);
                        var surl = "../Sys/WF/WFStartPage.aspx?action=|task_id=" + selectId + "&WF_ID=RPT";
                        top.f_overTab("任务办理", surl);
                    }
                    else {
                        $.ligerDialog.warn("项目负责人才可以办理该任务！");
                    }
                }
                else {
                    ComfirmTask(selectId);
                    //firstManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=" + selectTabId);
                    var surl = "../Sys/WF/WFStartPage.aspx?action=|task_id=" + selectId + "&WF_ID=RPT";
                    top.f_overTab("任务办理", surl);
                }
                break;
            default:
                break;
        }
    }
    //任务确认
    function ComfirmTask(task_id) {
        if (firstManager.getSelectedRow() == null) {
            $.ligerDialog.warn("请选择一行数据！");
            return;
        }
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ReportList.aspx/ComfirmTask",
            data: "{'strValue':'" + task_id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //$.ligerDialog.warn("成功办理！");
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载监测类别数据失败！');
            }
        });
    }
    /////////////////////////////////////////// 未确认任务 结束 /////////////////////////////////////////////////////////////////////////////////


    ////////////////////////////////////////////////////// 已确认任务grid /////////////////////////////////////////////////////////////

    //已确认任务右键菜单
    menuSecond = $.ligerMenu({ width: 120, items:
            [
            { id: 'Do', text: '任务重新办理', click: itemclick_OfToolbar_UnderItem_Second, icon: 'modify' }
            ]
    });
    secondManager = $("#secondgrid").ligerGrid({
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
                },
                { display: '监测类型', name: 'REMARK1', width: 120 }
                ],
        title: "",
        width: '100%',
        height: '100%',
        sortName: 'CREATE_DATE',
        sortOrder: 'DESC',
        pageSizeOptions: [10, 15, 20, 50],
        url: 'ReportList.aspx?action=getReportInfo&tabType=' + selectTabId,
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 10,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem_Second, icon: 'search' },
                { line: true },
                { id: 'srh', text: '查询', click: itemclick_OfToolbar_UnderItem_Second, icon: 'search' },
                { line: true }
            //{ id: 'Do', text: '任务重新办理', click: itemclick_OfToolbar_UnderItem_Second, icon: 'add' }//huangjinjun update
                ]
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectId = rowdata.ID;
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            itemclick_OfToolbar_UnderItem_Second({ id: 'Do' });
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);
            menuSecond.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll


    //评价标准grid 的Toolbar click事件
    function itemclick_OfToolbar_UnderItem_Second(item) {
        switch (item.id) {
            case 'srh':
                showDetailSrh();
                break;
            case 'srhAll':
                secondManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=" + selectTabId);
                break;
            case 'Do':
                if (secondManager.getSelectedRow() == "" || secondManager.getSelectedRow() == null) {
                    $.ligerDialog.warn("请选择一条任务！");
                    return;
                }

                if (acceptanceCode == secondManager.getSelectedRow().CONTRACT_TYPE)//如果是验收监测
                {
                    if (AcceptanceFilter()) {
                        var surl = "../Sys/WF/WFStartPage.aspx?action=|task_id=" + selectId + "&WF_ID=RPT";
                        top.f_overTab("任务办理", surl);
                    }
                    else {
                        $.ligerDialog.warn("项目负责人才可以办理该任务！");
                    }
                }
                else {
                    //firstManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=" + selectTabId);
                    var surl = "../Sys/WF/WFStartPage.aspx?action=|task_id=" + selectId + "&WF_ID=RPT";
                    top.f_overTab("任务办理", surl);
                }
                break;
            default:
                break;
        }
    }
    //判断是否可办理验收监测任务 默认只有项目负责人可办理 定制功能
    function AcceptanceFilter() {
        var returnBool = false;
        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ReportList.aspx/AcceptanceFilter",
            data: "{'strValue':'" + selectId + "'}",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.d == "true") {
                    returnBool = true; //可办理
                }
            }
        });

        return returnBool;
    }

    //任务办理
    function ComleteTask(task_id) {

        $.ajax({
            cache: false,
            async: false, //设置是否为异步加载,此处必须
            type: "POST",
            url: "ReportList.aspx/ComleteTask",
            data: "{'strValue':'" + task_id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //$.ligerDialog.warn("办理成功！");
            },
            error: function () {
                $.ligerDialog.warn('Ajax加载监测类别数据失败！');
            }
        });
    }
    /////////////////////////////////////////// 已确认任务 结束 /////////////////////////////////////////////////////////////////////////////////




    //////////////////////////////////////////////////////////////////////////////////////Tab标签/////////////////////////////////
    $("#navtab1").ligerTab({ onAfterSelectTabItem: function (tabid) {

        tab = $("#navtab1").ligerGetTabManager();
        selecttabindex = tab.getSelectedTabItemID();
        changeTab(selecttabindex);
    }
    });

    //Tab标签切换事件
    function changeTab(tabid) {
        switch (tabid) {
            // 未确认                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
            case "tabitem1":
                selectTabId = "0";
                firstManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=0");
                break;
            case "tabitem2":
                selectTabId = "1";
                secondManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=1");
                break;
        }
    }
});

// Create by 邵世卓 2012.11.28  "查询、查看"功能

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

        switch (selectTabId) {
            case "0":
                firstManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=" + selectTabId + "&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME) + "&srhTaskCode=" + encodeURI(SRH_TASK_CODE));
                break;
            case "1":
                secondManager.set('url', "ReportList.aspx?action=getReportInfo&tabType=" + selectTabId + "&srhContractType=" + SRH_CONTRACT_TYPE + "&srhContractCode=" + SRH_CONTRACT_CODE + "&srhProjectName=" + encodeURI(SRH_PROJECT_NAME) + "&srhTaskCode=" + encodeURI(SRH_TASK_CODE));
                break;
        }
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

