// Create by 邵世卓 2012.11.28  "项目查询"功能
var firstManager;
var secondManager;
var reportId; //报告ID
var groupicon = "../../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(document).ready(function () {
    var topHeight = $(window).height() / 2;
    var gridHeight = $(window).height() / 2;

    $("#layout1").ligerLayout({ height: '100%', topHeight: topHeight });

    //菜单
    menu1 = $.ligerMenu({ width: 120, items:
            [
            { id: 'sample', text: '样品信息', click: clickForTask, icon: 'process' },
            { id: 'task', text: '任务追踪', click: clickForTask, icon: 'role' },
             { id: 'report', text: '报告下载', click: clickForTask, icon: 'pager' }
            ]
    });
    //监测任务grid
    firstManager = $("#firstgrid").ligerGrid({
        columns: [
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left', isSort: false },
        { display: '任务单号', name: 'TICKET_NUM', width: 150, align: 'left', isSort: false },
        { display: '任务年度', name: 'CONTRACT_YEAR', width: 60, align: 'left', isSort: false },
        { display: '任务类型', name: 'CONTRACT_TYPE', width: 150, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.ID);
        }
        },
        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameByTask(data.TESTED_COMPANY_ID);
        }
        },
        { display: '任务状态', name: 'TASK_STATUS', width: 80, align: 'left', isSort: false, render: function (data) {
            return getTaskStatusName(data.TASK_STATUS);
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight,
        title: "任务信息",
        url: 'TotalSearch.aspx?action=GetTaskInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                { id: 'srhAll', text: '所有记录', click: clickForTask, icon: 'refresh' },
                { line: true },
                { id: 'srh', text: '查询', click: clickForTask, icon: 'search' },
                { line: true },
                { id: 'sample', text: '样品信息', click: clickForTask, icon: 'process' },
                { line: true },
                { id: 'task', text: '任务追踪', click: clickForTask, icon: 'role' },
                { line: true },
                { id: 'report', text: '报告下载', click: clickForTask, icon: 'pager' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showSampleInfo();
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu1.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectedTaskItem = firstManager.getSelectedRow();
            //点击的时候加载点位数据
            secondManager.set('url', "TotalSearch.aspx?action=GetContractInfo&contract_id=" + rowdata.CONTRACT_ID);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll

    //菜单
    menu = $.ligerMenu({ width: 120, items:
            [
            { id: 'contract', text: '详细', click: itemclick_OfToolbar_UnderItem, icon: 'archives' },
            { id: 'point', text: '点位', click: itemclick_OfToolbar_UnderItem, icon: 'database' },
             { id: 'task', text: '流程', click: itemclick_OfToolbar_UnderItem, icon: 'role' }
            ]
    });
    //委托书grid
    secondManager = $("#secondgrid").ligerGrid({
        columns: [
        { display: '委托年度', name: 'CONTRACT_YEAR', width: 60, align: 'left', isSort: false },
        { display: '项目名称', name: 'PROJECT_NAME', width: 350, align: 'left', isSort: false },
        { display: '委托书编号', name: 'CONTRACT_CODE', width: 150, align: 'left', isSort: false },
        { display: '委托类型', name: 'CONTRACT_TYPE', width: 100, align: 'left', isSort: false, render: function (data) {
            return getContractTypeForSearch(data.CONTRACT_TYPE);
        }
        },
        { display: '委托客户', name: 'CLIENT_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameForSearch(data.CLIENT_COMPANY_ID);
        }
        },
        { display: '受检企业', name: 'TESTED_COMPANY_ID', width: 200, align: 'left', isSort: false, render: function (data) {
            return getCompanyNameForSearch(data.TESTED_COMPANY_ID);
        }
        },
        { display: '办理状态', name: 'CONTRACT_SATAUS', width: 100, align: 'left', isSort: false, render: function (data) {
            if (data.CONTRACT_STATUS == '0') {
                return "<a style='color:Red'>未提交</a>";
            }
            else if (data.CONTRACT_STATUS == "1") {
                return "流转中";
            }
            else if (data.CONTRACT_STATUS == '9') {
                return "已办结";
            }
            return data.CONTRACT_STATUS;
        }
        }
        ], width: '100%', pageSizeOptions: [5, 10, 15], height: gridHeight, heightDiff: -10,
        title: "委托书信息",
        url: 'TotalSearch.aspx?Action=GetContractInfo',
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 8,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        toolbar: { items: [
                 { id: 'srhAll', text: '所有记录', click: itemclick_OfToolbar_UnderItem, icon: 'refresh' },
                { line: true },
                { id: 'contract', text: '详细', click: itemclick_OfToolbar_UnderItem, icon: 'archives' },
                { line: true },
                { id: 'point', text: '点位', click: itemclick_OfToolbar_UnderItem, icon: 'database' },
                { line: true },
                { id: 'task', text: '流程', click: itemclick_OfToolbar_UnderItem, icon: 'role' }
                ]
        },
        onDblClickRow: function (data, rowindex, rowobj) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);

            showContractInfo();
        },
        onContextmenu: function (parm, e) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(parm.rowindex);

            menu.show({ top: e.pageY, left: e.pageX });
            return false;
        },
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
            selectedConItem = secondManager.getSelectedRow();
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    });
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
});
// Create by 邵世卓 2012.11.28  "项目查询、查看"功能
//委托书grid 的Toolbar click事件
function itemclick_OfToolbar_UnderItem(item) {
    switch (item.id) {
        case 'srhAll':
            secondManager.set('url', "TotalSearch.aspx?Action=GetContractInfo");
            break;
        case 'contract':
            showContractInfo();
            break;
        case 'point':
            showPointInfo();
            break;
        case 'task':
            showContractFlow();
            break;
        default:
            break;
    }
}
//监测任务grid 的Toolbar click事件
function clickForTask(item) {
    switch (item.id) {
        case 'srhAll':
            firstManager.set('url', "TotalSearch.aspx?action=GetTaskInfo");
            break;
        case 'srh':
            showDetailSrh();
            break;
        case 'sample':
            showSampleInfo();
            break;
        case 'task':
            showTaskInfo();
            break;
        case 'report':
            showReport();
            break;
    }
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
            inputWidth: 150, labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
                      { display: "任务单号", name: "srhTicketNum", newline: true, type: "text", group: "查询信息", groupicon: groupicon },
                      { display: "任务年度", name: "srhYear", newline: false, type: "text", width: 120 },
                      { display: "项目名称", name: "srhProjectName", newline: true, width: 350, type: "text" }
                    ]
        });

        detailWinSrh = $.ligerDialog.open({
            target: $("#detailSrh"),
            width: 500, height: 180, left: 100, top: 90, title: "查询任务信息",
            buttons: [
                  { text: '确定', onclick: function () { search(); clearSearchDialogValue(); detailWinSrh.hide(); } },
                  { text: '取消', onclick: function () { clearSearchDialogValue(); detailWinSrh.hide(); } }
                  ]
        });
    }

    function search() {
        var srhTicketNum = encodeURI($("#srhTicketNum").val());
        var srhProjectName = encodeURI($("#srhProjectName").val());
        var srhYear = $("#srhYear").val();

        firstManager.set('url', "TotalSearch.aspx?Action=GetTaskInfo&srhTicketNum=" + srhTicketNum + "&srhProjectName=" + srhProjectName + "&srhYear=" + srhYear);
    }
}

//grid 的查询对话框元素的值 清除
function clearSearchDialogValue() {
    $("#srhTicketNum").val("");
    $("#srhProjectName").val("");
    $("#srhYear").val();
}

///////////////委托书查看
var WinContract = null;
function showContractInfo() {
    var dataContract; //委托书信息
    var dataClient; //委托单位信息
    var dataTested; //受检单位信息
    var strContractFee; //监测费用
    var selectedConItem = secondManager.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    //获取所有委托书信息
    $.ajax({
        async: false,
        type: "POST",
        url: "TotalSearch.aspx?action=getContract&contract_id=" + selectedConItem.ID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            dataContract = data;
        }
    });
    //获得委托单位信息
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearch.aspx?type=getClientCompanyInfo&id=" + selectedConItem.CLIENT_COMPANY_ID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                dataClient = data;
            }
        }
    });
    //获得受检单位信息
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearch.aspx?type=getTestedCompanyInfo&id=" + selectedConItem.TESTED_COMPANY_ID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                dataTested = data;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    //监测费用获取
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearch.aspx?type=getContractFee&contract_id=" + selectedConItem.ID,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strContractFee = data;
            }
        }
    });
    if (WinContract) {
        WinContract.show();
    }
    else {
        //创建表单结构
        $("#divContract").ligerForm({
            labelWidth: 90, space: 0, labelAlign: 'right',
            fields: [
            { display: "项目名称", name: "PROJECT_NAME", newline: true, width: 290, type: "text", group: "委托书信息", groupicon: groupicon },
            { display: "委托类型", name: "CONTRACT_TYPE", newline: false, width: 100, type: "select", comboboxName: "dropContractType", options: { valueFieldID: "CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetDict&dict_type=Contract_Type"} },
            { display: "合同编号", name: "CONTRACT_CODE", width: 150, newline: true, type: "text" },
            { display: "费用", name: "CONTRACT_FEE", width: 50, newline: false, type: "text" },
            { display: "委托年度", name: "CONTRACT_YEAR", newline: false, width: 100, type: "text" },
            { display: "监测类型", name: "TEST_TYPES", space: 140, width: 150, newline: true, type: "text" },
            { display: "签订日期", name: "ASKING_DATE", newline: false, width: 100, type: "text" },
            { display: "监测目的", name: "TEST_PURPOSE", width: 480, newline: true, type: "text" },
            { display: "备注", name: "REMARK", newline: true, width: 480, type: "text" },

            { display: "委托单位", name: "CLIENT_COMPANY", newline: true, width: 290, type: "text", group: "委托单位信息", groupicon: groupicon },
            { display: "所在区域", name: "AREA1", newline: false, width: 100, type: "select", comboboxName: "dropArea1", options: { valueFieldID: "AREA1", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetDict&dict_type=administrative_area"} },
            { display: "联系人", name: "CONTACT_NAME1", space: 110, newline: true, type: "text" },
            { display: "联系电话", name: "PHONE1", newline: false, width: 100, type: "text" },
            { display: "通讯地址", name: "CONTACT_ADDRESS", newline: true, width: 480, type: "text" },

            { display: "受检单位", name: "TESTED_COMPANY", width: 290, newline: true, type: "text", group: "受检单位信息", groupicon: groupicon },
            { display: "所在区域", name: "AREA2", newline: false, width: 100, type: "select", comboboxName: "dropArea2", options: { valueFieldID: "AREA2", valueField: "DICT_CODE", textField: "DICT_TEXT", url: "../../Mis/Contract/Acceptance/AcceptanceCreate.aspx?type=GetDict&dict_type=administrative_area"} },
            { display: "联系人", name: "CONTACT_NAME2", space: 110, newline: true, type: "text" },
            { display: "联系电话", name: "PHONE2", newline: false, width: 100, type: "text" },
            { display: "监测地址", name: "MONITOR_ADDRESS", newline: true, width: 480, type: "label" }
            ]
        });

        WinContract = $.ligerDialog.open({
            target: $("#detailContract"),
            width: 700, left: 80, height: 480, top: 30, title: "委托书信息",
            buttons: [
                  { text: '取消', onclick: function () { WinContract.hide(); } }
                  ]
        });
    }
    if (dataContract) {
        //委托书
        $("#PROJECT_NAME").val(dataContract.PROJECT_NAME);
        $("#CONTRACT_TYPE").val(dataContract.CONTRACT_TYPE);
        $("#dropContractType").ligerGetComboBoxManager().setDisabled();
        $("#CONTRACT_CODE").val(dataContract.CONTRACT_CODE);
        $("#CONTRACT_FEE").val(strContractFee);
        $("#CONTRACT_YEAR").val(dataContract.CONTRACT_YEAR);
        $("#TEST_TYPES").val(dataContract.TEST_TYPES);
        $("#ASKING_DATE").val(dataContract.ASKING_DATE);
        $("#TEST_PURPOSE").val(dataContract.TEST_PURPOSE);
        $("#REMARK").val(dataContract.REMARK1);
        //委托单位
        $("#CLIENT_COMPANY").val(dataClient.COMPANY_NAME);
        $("#AREA1").val(dataClient.AREA);
        $("#dropArea1").ligerGetComboBoxManager().setDisabled();
        $("#CONTACT_NAME1").val(dataClient.CONTACT_NAME);
        $("#PHONE1").val(dataClient.PHONE);
        $("#CONTACT_ADDRESS").val(dataClient.CONTACT_ADDRESS);
        //受检单位
        $("#TESTED_COMPANY").val(dataTested.COMPANY_NAME);
        $("#AREA2").val(dataTested.AREA);
        $("#dropArea2").ligerGetComboBoxManager().setDisabled();
        $("#CONTACT_NAME2").val(dataTested.CONTACT_NAME);
        $("#PHONE2").val(dataTested.PHONE);
        $("#MONITOR_ADDRESS").val(dataTested.MONITOR_ADDRESS);
    }
}
////////////////报告下载
function showReport() {
    var selectedTaskItem = firstManager.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../../OA/ATT/AttFileDownLoad.aspx?filetype=ReportQHD&id=' + selectedTaskItem.ID
    });
    //    //获取报告ID
    //    $.ajax({
    //        cache: false,
    //        async: false,
    //        type: "POST",
    //        url: "TotalSearch.aspx?type=getReportID&task_id=" + selectedTaskItem.ID,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (data) {
    //            reportId = data;
    //        }
    //    });
    //    ReportClick();
}

//获取委托类型名称
function getContractTypeForSearch(taskID) {
    var strReturn;
    $.ajax({
        async: false,
        type: "POST",
        url: "TotalSearch.aspx/getContractType",
        data: "{'strValue':'" + taskID + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}

//获取企业名称
function getCompanyNameForSearch(client_id) {
    var strReturn = "";
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearch.aspx/getCompanyName",
        data: "{'strValue':'" + client_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}
//获取任务企业名称
function getCompanyNameByTask(client_id) {
    var strReturn = "";
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearch.aspx/getCompanyNameByTask",
        data: "{'strValue':'" + client_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}
//获取质控手段
function getQcType(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "../../../Mis/Report/ReportSchedule.aspx/getQcType",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}
//获取姓名
function getUserName(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearch.aspx/getUserName",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}
//任务状态
function getTaskStatusName(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "TotalSearch.aspx/getTaskStatusName",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
        }
    });
    return strReturn;
}
//报告生成
function ReportClick() {
    var selectedTaskItem = firstManager.getSelectedRow();
    var taskId = selectedTaskItem.ID; //监测任务ID
    var url;
    if (reportId != null && reportId != "") {
        url = "?FILE_ID=" + reportId + "&EDIT_TYPE=1&FILE_TYPE=.doc&ReportWf=" + $("#ReportStatus").val();
    }
    else {
        $.ligerDialog.warn("报告未编制");
    }
    if (url != "" && url != null) {
        var sheight = screen.height - 70;
        var swidth = screen.width - 10;
        var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=no,resizable=yes";
        var tmp = window.open("../../../Rpt/Template/FileEdit.aspx" + url, '', winoption);
        return tmp;
    }
}
//委托书点位信息
function showPointInfo() {
    var selectedConItem = secondManager.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var surl = '../Channels/Base/Search/SearchQHD/TotalSearchForPoint.aspx?id=' + selectedConItem.ID;
    top.f_addTab('TotalSearchForPoint' + selectedConItem.ID, '委托书点位信息', surl);
}
//监测任务样品信息
function showSampleInfo() {
    var selectedTaskItem = firstManager.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var surl = '../Channels/Base/Search/SearchQHD/TotalSearchForSample.aspx?task_id=' + selectedTaskItem.ID + "&contract_type=" + selectedTaskItem.CONTRACT_TYPE;
    top.f_addTab('TotalSearchForSample' + selectedTaskItem.ID, '任务样品信息', surl);
}

//委托书流程
function showContractFlow() {
    var selectedConItem = secondManager.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var flow_id;
    var winHeight;
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearch.aspx?type=getFlowInfo&business_id=" + selectedConItem.ID + "&contract_type=" + selectedConItem.CONTRACT_TYPE,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                var strList = data.split('|');
                if (strList.length > 1) {
                    flow_id = strList[0]; //流程ID
                    winHeight = strList[1]; //窗体高度
                }
            }
        }
    });
    var strUrl = ("../../../../Sys/WF/WFShowStepDetail.aspx?ID=" + flow_id);
    $(document).ready(function () { $.ligerDialog.open({ title: '任务追踪', url: strUrl, width: 375, height: winHeight, modal: false }); });
}
//监测任务流程追踪
function showTaskInfo() {
    var selectedTaskItem = firstManager.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var flow_id;
    var winHeight;
    $.ajax({
        type: "POST",
        async: false,
        url: "TotalSearch.aspx?type=getFlowInfo&business_id=" + selectedTaskItem.ID,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null) {
                var strList = data.split('|');
                if (strList.length > 1) {
                    flow_id = strList[0]; //流程ID
                    winHeight = strList[1]; //窗体高度
                }
            }
        }
    });
    var strUrl = ("../../../../Sys/WF/WFShowStepDetail.aspx?ID=" + flow_id);
    $(document).ready(function () { $.ligerDialog.open({ title: '任务追踪', url: strUrl, width: 375, height: winHeight, modal: false }); });
}



