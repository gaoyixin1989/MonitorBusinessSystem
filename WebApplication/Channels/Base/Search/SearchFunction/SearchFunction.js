// Create by 潘德军 2013.7.1  "综合查询--委托书查询"功能 公用函数

////////////委托书grid
//委托书信息
var WinContract = null;
function showContract() {
    var dataContract; //委托书信息
    var dataClient; //委托单位信息
    var dataTested; //受检单位信息
    var strContractFee; //监测费用
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    //获取所有委托书信息
    $.ajax({
        async: false,
        type: "POST",
        url: webServiceUrl + "?action=getContract&contract_id=" + selectedConItem.ID,
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
        url: webServiceUrl + "?type=getClientCompanyInfo&id=" + selectedConItem.CLIENT_COMPANY_ID,
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
        url: webServiceUrl + "?type=getClientCompanyInfo&id=" + selectedConItem.TESTED_COMPANY_ID,
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
        url: webServiceUrl + "?type=getContractFee&contract_id=" + selectedConItem.ID,
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
            { display: "委托类型", name: "CONTRACT_TYPE", newline: false, width: 100, type: "select", comboboxName: "dropContractType", options: { valueFieldID: "CONTRACT_TYPE", valueField: "DICT_CODE", textField: "DICT_TEXT", url: AcceptanceCreateUrl+"?type=GetDict&dict_type=Contract_Type"} },
            { display: "合同编号", name: "CONTRACT_CODE", width: 150, newline: true, type: "text" },
            { display: "费用", name: "CONTRACT_FEE", width: 50, newline: false, type: "text" },
            { display: "委托年度", name: "CONTRACT_YEAR", newline: false, width: 100, type: "text" },
            { display: "监测类型", name: "TEST_TYPES", space: 140, width: 150, newline: true, type: "text" },
            { display: "签订日期", name: "ASKING_DATE", newline: false, width: 100, type: "text" },
            { display: "监测目的", name: "TEST_PURPOSE", width: 480, newline: true, type: "text" },
            { display: "备注", name: "REMARK", newline: true, width: 480, type: "text" },

            { display: "委托单位", name: "CLIENT_COMPANY", newline: true, width: 290, type: "text", group: "委托单位信息", groupicon: groupicon },
            { display: "所在区域", name: "AREA1", newline: false, width: 100, type: "select", comboboxName: "dropArea1", options: { valueFieldID: "AREA1", valueField: "DICT_CODE", textField: "DICT_TEXT", url: AcceptanceCreateUrl + "?type=GetDict&dict_type=administrative_area"} },
            { display: "联系人", name: "CONTACT_NAME1", space: 110, newline: true, type: "text" },
            { display: "联系电话", name: "PHONE1", newline: false, width: 100, type: "text" },
            { display: "通讯地址", name: "CONTACT_ADDRESS", newline: true, width: 480, type: "text" },

            { display: "受检单位", name: "TESTED_COMPANY", width: 290, newline: true, type: "text", group: "受检单位信息", groupicon: groupicon },
            { display: "所在区域", name: "AREA2", newline: false, width: 100, type: "select", comboboxName: "dropArea2", options: { valueFieldID: "AREA2", valueField: "DICT_CODE", textField: "DICT_TEXT", url: AcceptanceCreateUrl + "?type=GetDict&dict_type=administrative_area"} },
            { display: "联系人", name: "CONTACT_NAME2", space: 110, newline: true, type: "text" },
            { display: "联系电话", name: "PHONE2", newline: false, width: 100, type: "text" },
            { display: "监测地址", name: "MONITOR_ADDRESS", newline: true, width: 480, type: "label" }
            ]
        });

        WinContract = $.ligerDialog.open({
            target: $("#detailContract"),
            width: 700, left: 80, height: 480, top: 30, title: "委托书信息",
            buttons: [
                  { text: '关闭', onclick: function () { WinContract.hide(); } }
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

//委托书点位信息
function showPoint() {
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    if (selectedConItem.SAMPLE_SOURCE != "抽样") {
        $.ligerDialog.warn('自送样无点位信息！');
        return;
    }
    //    var surl = '../Channels/Base/Search/TotalSearchForPoint.aspx?id=' + selectedConItem.ID;
    //    top.f_addTab('TotalSearchForPoint' + selectedConItem.ID, '委托书点位信息', surl);
    var surl = pathUrl+'TotalSearchForPoint.aspx?id=' + selectedConItem.ID;
    $(document).ready(function () { $.ligerDialog.open({ title: '委托书点位信息', url: surl, width: 800, height: 600, modal: false }); });
}

//委托书流程追踪
function showContractStep() {
    var selectedConItem;
    if (gridName == "0")
        selectedConItem = contractGrid.getSelectedRow();
    else
        selectedConItem = contractGrid1.getSelectedRow();
    if (!selectedConItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var flow_id;
    var winHeight;
    $.ajax({
        type: "POST",
        async: false,
        url: webServiceUrl + "?type=getFlowInfo&business_id=" + selectedConItem.ID + "&contract_type=" + selectedConItem.CONTRACT_TYPE,
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
    var strUrl = (wfStepUrl + "?ID=" + flow_id);
//    $(document).ready(function () { $.ligerDialog.open({ title: '委托书执行情况', url: strUrl, width: 375, height: winHeight, modal: false }); });
    var strHeight = $(window).height();
    $(document).ready(function () { $.ligerDialog.open({ title: '委托书执行情况', url: strUrl, width: 400, height: strHeight, modal: false }); });
}

////////////////监测任务grid
//监测任务样品信息
function showSample() {
    var selectedTaskItem = taskGrid.getSelectedRow(); 

    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
//    var surl = '../Channels/Base/Search/TotalSearchForSample.aspx?task_id=' + selectedTaskItem.ID + "&contract_type=" + selectedTaskItem.CONTRACT_TYPE;
//    top.f_addTab('TotalSearchForSample' + selectedTaskItem.ID, '样品信息', surl);
    var surl = pathUrl + 'TotalSearchForSample.aspx?step_type=' + request("step_type") + '&task_id=' + selectedTaskItem.ID + '&contract_type=' + selectedTaskItem.CONTRACT_TYPE;
    $(document).ready(function () { $.ligerDialog.open({ title: '样品信息', url: surl, width: 800, height: 600, modal: false }); });
}

//监测任务流程追踪
function showTaskStep() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }

    var flow_id;
    var winHeight;
    $.ajax({
        type: "POST",
        async: false,
        url: webServiceUrl + "?type=getFlowInfo&business_id=" + selectedTaskItem.ID,
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
    var strUrl = (wfStepUrl+"?ID=" + flow_id);
//    $(document).ready(function () { $.ligerDialog.open({ title: '报告执行情况', url: strUrl, width: 375, height: winHeight, modal: false }); });
    var strHeight = $(window).height();
    $(document).ready(function () { $.ligerDialog.open({ title: '报告执行情况', url: strUrl, width: 400, height: strHeight, modal: false }); });
}

function showTaskStep_QHD() {
    showTaskStep();
//    var selectedTaskItem = taskGrid.getSelectedRow();
//    if (!selectedTaskItem) {
//        $.ligerDialog.warn('请先选择一条数据！');
//        return;
//    }

//    var strUrl = ("TaskFlow_Rpt.aspx?ID=" + selectedTaskItem.ID);
//    //    $(document).ready(function () { $.ligerDialog.open({ title: '报告执行情况', url: strUrl, width: 375, height: winHeight, modal: false }); });
//    var strHeight = $(window).height();
//    $(document).ready(function () { $.ligerDialog.open({ title: '报告执行情况', url: strUrl, width: 300, height: 220, modal: false }); });
}

//任务状态
function getTaskStatusName(TASK_STATUS, QC_STATUS) {
    var strReturn;

    switch (TASK_STATUS) {
        case "01":
            if (QC_STATUS == undefined || QC_STATUS == '') {
                strReturn = "监测任务未下达";
            }
            else if (QC_STATUS != '8')
                strReturn = "待采样分析中";
            else
                strReturn = "监测分析中，报告未出具";
            break;
        case "09":
        case "099":
            strReturn = "报告编制中";
            break;
        case "10":
            strReturn = "报告复核中";
            break;
        case "10.1":
            strReturn = "报告审核中";
            break;
        case "10.2":
            strReturn = "报告签发中";
            break;
        case "10.3":
            strReturn = "报告打印中";
            break;
        case "11":
            strReturn = "报告已办结";
            break;
    }
    return strReturn;
}

///////////////////////监测子任务grid
//监测任务结果信息
function showResult() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    var selectedSubTaskItem = subTaskGrid.getSelectedRow();
    if (!selectedSubTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
//    var surl = '../Channels/Base/Search/TotalSearchForSample.aspx?task_id=' + selectedTaskItem.ID + "&contract_type=" + selectedTaskItem.CONTRACT_TYPE + "&item_type=" + selectedSubTaskItem.MONITOR_ID;
//    top.f_addTab('TotalSearchForSample' + selectedTaskItem.ID, '结果信息', surl);
    var surl = pathUrl + 'TotalSearchForSample.aspx?step_type=' + request("step_type") + '&task_id=' + selectedTaskItem.ID + '&contract_type=' + selectedTaskItem.CONTRACT_TYPE + '&item_type=' + selectedSubTaskItem.MONITOR_ID;
    $(document).ready(function () { $.ligerDialog.open({ title: '结果信息', url: surl, width: 800, height: 600, modal: false }); });
}

//监测任务流程追踪
function showSubTaskStep(step_type) {
    var selectedTaskItem = taskGrid.getSelectedRow();
    var selectedSubTaskItem = subTaskGrid.getSelectedRow();
    if (!selectedSubTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    var strHeight = $(window).height();

    var strUrl = (pathUrl + "TaskFlow_Sample.aspx?step_type=" + request("step_type") + "&task_id=" + selectedTaskItem.ID + "&Monitor_ID=" + selectedSubTaskItem.MONITOR_ID);
    $(document).ready(function () { $.ligerDialog.open({ title: '监测执行情况', url: strUrl, width: 400, height: strHeight, modal: false }); });
}

////////////////报告生成
function showReport() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条数据！');
        return;
    }
    //获取报告ID
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: webServiceUrl + "?type=getReportID&task_id=" + selectedTaskItem.ID,
        contentType: "application/json; charset=utf-8",
        dataType: "text",
        success: function (data) {
            reportId = data;
        }
    });
    ReportClick();
}

//报告生成
function ReportClick() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    var taskId = selectedTaskItem.ID; //监测任务ID
    var url;
    if (reportId != null && reportId != "") {
        url = "?FILE_ID=" + reportId + "&EDIT_TYPE=1&FILE_TYPE=.doc&PROTECT=1&ReportWf=" + $("#ReportStatus").val();
    }
    else {
        $.ligerDialog.warn("报告未编制");
    }
    if (url != "" && url != null) {
        var sheight = screen.height - 70;
        var swidth = screen.width - 10;
        var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=no,resizable=yes";
        var tmp = window.open(FileEditUrl + url, '', winoption);
        return tmp;
    }
}

////////////////报告下载
function downloadReport() {
    var selectedTaskItem = taskGrid.getSelectedRow();
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
}

///////////////////数据汇总表
var detailExcelWin = null;
function excelExportForTask() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    if (!selectedTaskItem) {
        $.ligerDialog.warn('请先选择一条任务数据！');
        return;
    }
    detailExcelWin=$.ligerDialog.open({ url: "../SearchFunction/print.aspx?task_id=" + selectedTaskItem.ID });
}

function excelExportForSubTask() {
    var selectedTaskItem = taskGrid.getSelectedRow();
    var selectedsubTaskItem = subTaskGrid.getSelectedRow();
    if (!selectedsubTaskItem) {
        $.ligerDialog.warn('请先选择一条监测类别数据！');
        return;
    }
    detailExcelWin = $.ligerDialog.open({ url: "../SearchFunction/print.aspx?task_id=" + selectedTaskItem.ID + "&subtask_id=" + selectedsubTaskItem.ID });
}
//获取任务的报告编制人姓名
function getReportUserName(TaskID, ContractID) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: webServiceUrl + "/getReportUserName",
        data: "{'strTaskID':'" + TaskID + "','strContractID':'" + ContractID + "'}",
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

//////////////通用
//获取姓名
function getUserName(value) {
    var strReturn;
    if (value.length == 0)
        return "";
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: webServiceUrl+"/getUserName",
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

//获取监测类型名称
function getMonitorName(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: webServiceUrl + "/getMonitorName",
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

//获取委托类型名称
function getContractTypeForSearch(type) {
    var strReturn;
    $.ajax({
        async: false,
        type: "POST",
        url: webServiceUrl + "/getContractType",
        data: "{'strValue':'" + type + "'}",
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
        url: webServiceUrl + "/getCompanyName",
        data: "{'strValue':'" + client_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}

//获取企业名称getCompanyNameByTask
function getTaskCompanyNameForSearch(client_id) {
    var strReturn = "";
    $.ajax({
        type: "POST",
        async: false,
        url: webServiceUrl + "/getCompanyNameByTask",
        data: "{'strValue':'" + client_id + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            strReturn = data.d;
        }
    });
    return strReturn;
}