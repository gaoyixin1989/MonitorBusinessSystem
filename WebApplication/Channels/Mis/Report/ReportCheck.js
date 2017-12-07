﻿//Create by SSZ报告编制
//2012-12-5
var strContractId; //委托书ID
var strContractType; //委托类型
var contractManager; //合同信息
var clientManager; //委托单位
var testedManager; //受检单位
var sampleManager; //样品信息
var itemManager; //项目信息
var groupicon = "../../../Controls/ligerui/lib/ligerUI/skins/icons/communication.gif";

$(function () {
    //$("#layout1").ligerLayout({ topHeight: 0, allowLeftCollapse: false, allowRightCollapse: false, height: '97%' });

    //监测类别
    $("#dropItemType").ligerComboBox({ url: "ReportSchedule.aspx?type=getMonitorType", valueFieldID: "MonitorID", valueField: "ID", textField: "MONITOR_TYPE_NAME", isMultiSelect: false,
        onSelected: function (newvalue) {
            sampleManager.set('url', "ReportSchedule.aspx?type=getSampleInfo&task_id=" + $("#ID").val() + "&item_type=" + newvalue + '&strContractType=' + strContractType);
        }
    });
    //委托书 编制
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "ReportSchedule.aspx?type=getContractInfo&task_id=" + $("#ID").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            SetContractInfo(data);
        }
    });
    //委托单位信息
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "ReportSchedule.aspx?type=getClientInfo&task_id=" + $("#ID").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            SetClientInfo(data);
        }
    });
    //受检单位信息
    $.ajax({
        cache: false,
        async: false,
        type: "POST",
        url: "ReportSchedule.aspx?type=getTestedInfo&task_id=" + $("#ID").val(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            SetTestedInfo(data);
        }
    });
    //样品信息
    //自送样样品信息
    strContractId = $("#ContractID").val();
    strContractType = $("#CONTRACT_TYPE").val();

    if (strContractType == "04") {
        sampleManager = $("#sampleDiv").ligerGrid({
            columns: [
         { display: '样品号', name: 'SAMPLE_CODE', width: 100, align: 'left', isSort: false },
         { display: '样品名称', name: 'SAMPLE_NAME', width: 60, align: 'left', isSort: false },
         { display: '样品类型', name: 'SAMPLE_TYPE', width: 50, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: '220px',
            title: "样品信息",
            url: 'ReportSchedule.aspx?type=getSampleInfo&task_id=' + $("#ID").val() + '&strContractType=' + strContractType,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            whenRClickToSelect: true,
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);

                //点击的时候加载项目数据
                itemManager.set('url', "ReportSchedule.aspx?type=getItemInfo&sample_id=" + rowdata.ID);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
    }
//    else if (strContractType == "05")//验收委托
//    {
//        //$("#btn_Ok").hide();
//        //$("#btnAccept").show();
//        //$("#divPointItem").hide();
//    }
    else {
        sampleManager = $("#sampleDiv").ligerGrid({
            columns: [
        { display: '点位', name: 'POINT_NAME', width: 80, align: 'left', isSort: false },
        { display: '样品号', name: 'SAMPLE_CODE', width: 100, align: 'left', isSort: false }
        ], width: '100%', pageSizeOptions: [5, 8, 10], height: '220px',
            title: "样品信息",
            url: 'ReportSchedule.aspx?type=getSampleInfo&task_id=' + $("#ID").val() + '&strContractType=' + strContractType,
            dataAction: 'server', //服务器排序
            usePager: true,       //服务器分页
            pageSize: 5,
            alternatingRow: false,
            checkbox: true,
            whenRClickToSelect: true,
            onCheckRow: function (checked, rowdata, rowindex) {
                for (var rowid in this.records)
                    this.unselect(rowid);
                this.select(rowindex);

                //点击的时候加载项目数据
                itemManager.set('url', "ReportSchedule.aspx?type=getItemInfo&sample_id=" + rowdata.ID);
            },
            onBeforeCheckAllRow: function (checked, grid, element) { return false; }
        });
    }
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //项目信息
    itemManager = $("#itemDiv").ligerGrid({
        columns: [
        { display: '监测项目', name: 'ITEM_NAME', width: 150, align: 'left', isSort: false },
        { display: '监测结果', name: 'ITEM_RESULT', width: 80, align: 'left', isSort: false, render: function (record) {
            if (record.ITEM_RESULT.indexOf("red") >= 0)
                return "<span style='color:red'>" + record.ITEM_RESULT.replace("red", "") + "</span>";
            else
                return record.ITEM_RESULT;
        }
        },
        { display: '评价标准', name: 'STANDARD_VALUE', width: 80, align: 'left', isSort: false },
        { display: '质控手段', name: 'QC', width: 80, align: 'left', isSort: false, render: function (record) {
            return getQcType(record.QC);
        }
        },
        { display: '分析负责人', name: 'HEAD_USER', width: 80, align: 'left', isSort: false },
        { display: '分析方法', name: 'METHOD_NAME', width: 150, align: 'left', isSort: false },
        { display: '仪器', name: 'APPARATUS_NAME', width: 150, align: 'left', isSort: false }
        ], width: '90%', pageSizeOptions: [5, 8, 10], height: '220px',
        title: "项目信息",
        dataAction: 'server', //服务器排序
        usePager: true,       //服务器分页
        pageSize: 5,
        alternatingRow: false,
        checkbox: true,
        whenRClickToSelect: true,
        onCheckRow: function (checked, rowdata, rowindex) {
            for (var rowid in this.records)
                this.unselect(rowid);
            this.select(rowindex);
        },
        onBeforeCheckAllRow: function (checked, grid, element) { return false; }
    }
    );
    $(".l-grid-hd-cell-btn-checkbox").css("display", "none"); //隱藏checkAll
    //报告存在性判断
    if (strContractType != "05") {
        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: "ReportSchedule.aspx?action=ReportStatus&task_id=" + $("#ID").val(),
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var reportCount = data.split('|');
                if (reportCount[0].length > 0)//存在报告
                {
                    $("#btn_Turn").show();
                    $("#btn_Ok").attr("value", "报告查看");
                }
                else {
                    $.ligerDialog.warn("报告丢失，请退回重新编制");
                }
            }
        });
    }
});

//初始委托书信息模块 非编制
function SetContractInfo(data) {
    contractManager = $("#contractDiv");
    contractManager.ligerForm({
        inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
        fields: [
        { display: "项目名称", name: "PROJECT_NAME", width: 590, newline: true, type: "text" },
        { display: "任务单号", name: "TICKET_NUM", newline: true, type: "text" },
        { display: "报告单号", name: "REPORT_CODE", newline: false, type: "text" },
        
        { display: "委托书编号", name: "CONTRACT_CODE", newline: true, type: "text" },
        { display: "签订日期", name: "CONSIGN_DATE", newline: false, space: 10, type: "text" },
        { display: "委托类型", name: "CONTRACT_TYPE", width: 100, newline: false, type: "select", comboboxName: "DROP_CONTRACT_TYPE", options: { valueFieldID: "CONTRACT_TYPE", url: "../../Base/MonitorType/Select.ashx?view=T_SYS_DICT&idfield=DICT_CODE&textfield=DICT_TEXT&where=DICT_TYPE|Contract_Type" }
        },
        { display: "监测目的", name: "TEST_PURPOSE", width: 590, newline: true, type: "text" },
        { display: "备注", name: "REMARK", width: 590, newline: true, type: "text" },
        { display: "监测结论", name: "PROJECT_CONCLUSION", width: 590, newline: true, type: "text" }
        ]
    });
    //赋值
    if (data) {
        $("#REPORT_CODE").val(data.REMARK1);
        $("#PROJECT_NAME").val(data.PROJECT_NAME);
        $("#CONSIGN_DATE").val(data.CONSIGN_DATE);
        $("#CONTRACT_CODE").val(data.CONTRACT_CODE);
        $("#CONTRACT_TYPE").val(data.CONTRACT_TYPE);
        $("#TEST_PURPOSE").val(data.TEST_PURPOSE);
        $("#REMARK").val(data.REMARK);
        $("#PROJECT_CONCLUSION").val(data.REMARK2);
        $("#TICKET_NUM").val(data.TICKET_NUM);
    }
    $("#REPORT_CODE").ligerGetTextBoxManager().setDisabled();
    $("#PROJECT_CONCLUSION").ligerGetTextBoxManager().setDisabled();
    $("#PROJECT_NAME").ligerGetTextBoxManager().setDisabled();
    $("#CONSIGN_DATE").ligerGetTextBoxManager().setDisabled();
    $("#CONTRACT_CODE").ligerGetTextBoxManager().setDisabled();
    $("#DROP_CONTRACT_TYPE").ligerGetComboBoxManager().setDisabled();
    $("#TEST_PURPOSE").ligerGetTextBoxManager().setDisabled();
    $("#REMARK").ligerGetTextBoxManager().setDisabled();
    $("#TICKET_NUM").ligerGetTextBoxManager().setDisabled();
}
//初始委托单位信息
function SetClientInfo(data) {
    clientManager = $("#clientDiv");
    clientManager.ligerForm({
        inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
        fields: [
        { display: "委托单位", name: "CLIENT_COMPANY", newline: true, width: 200, type: "text" },
        { display: "联系人", name: "CLIENT_LINKER", newline: false, width: 100, type: "text" },
        { display: "联系电话", name: "CLIENT_PHONE", width: 130, newline: false, type: "text" },
        { display: "联系地址", name: "CLIENT_ADDRESS", newline: true, width: 590, type: "text" }
        ]
    });
    //赋值
    if (data) {
        $("#CLIENT_COMPANY").val(data.COMPANY_NAME);
        $("#CLIENT_LINKER").val(data.CONTACT_NAME);
        $("#CLIENT_PHONE").val(data.LINK_PHONE);
        $("#CLIENT_ADDRESS").val(data.CONTACT_ADDRESS);
    }
    $("#CLIENT_COMPANY").ligerGetTextBoxManager().setDisabled();
    $("#CLIENT_LINKER").ligerGetTextBoxManager().setDisabled();
    $("#CLIENT_PHONE").ligerGetTextBoxManager().setDisabled();
    $("#CLIENT_ADDRESS").ligerGetTextBoxManager().setDisabled();
}
//初始受检单位信息
function SetTestedInfo(data) {
    testedManager = $("#testedDiv");
    testedManager.ligerForm({
        inputWidth: 160, labelWidth: 80, space: 0, labelAlign: 'right',
        fields: [
        { display: "受检单位", name: "TESTED_COMPANY", width: 200, newline: true, type: "text" },
        { display: "联系人", name: "TESTED_LINKER", newline: false, width: 100, type: "text" },
        { display: "联系电话", name: "TESTED_PHONE", width: 130, newline: false, type: "text" },
        { display: "监测地址", name: "TESTED_ADDRESS", width: 590, newline: true, type: "text" }
        ]
    });
    //赋值
    if (data) {
        $("#TESTED_COMPANY").val(data.COMPANY_NAME);
        $("#TESTED_LINKER").val(data.CONTACT_NAME);
        $("#TESTED_PHONE").val(data.LINK_PHONE);
        $("#TESTED_ADDRESS").val(data.CONTACT_ADDRESS);
    }
    $("#TESTED_COMPANY").ligerGetTextBoxManager().setDisabled();
    $("#TESTED_LINKER").ligerGetTextBoxManager().setDisabled();
    $("#TESTED_PHONE").ligerGetTextBoxManager().setDisabled();
    $("#TESTED_ADDRESS").ligerGetTextBoxManager().setDisabled();
}

//获取质控手段
function getQcType(value) {
    var strReturn;
    $.ajax({
        cache: false,
        async: false, //设置是否为异步加载,此处必须
        type: "POST",
        url: "ReportSchedule.aspx/getQcType",
        data: "{'strValue':'" + value + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                strReturn = data.d;
            }
            else {
                $.ligerDialog.warn('获取数据失败！');
            }
        }
    });
    return strReturn;
}

//获取DictText
function getDictText(type, code) {
    var strReturn;
    $.get("ReportSchedule.aspx?type=getDictName&strType=" + type + "&strCode=" + code, function (data) {
        if (data != null) {
            strReturn = data;
        }
        else {
            $.ligerDialog.warn('获取数据失败！');
        }
    });
    return strReturn;
}

//切换模板
function TurnTemplate() {
    $.ligerDialog.confirm('确定切换报告模板吗?', function (status) {
        if (status == true)
            ChangeTemplate();
        else
            $.ligerDialog.close();
    });
}
//切换模板
function ChangeTemplate() {
    $.post('ReportSchedule.aspx?action=changeTem', function (data) {

    });
}
//报告生成
function ReportClick() {
    var taskId = $("#ID").val(); //监测任务ID
    var reportId = $("#reportId").val(); //报告ID
    var temId = $("#ReportTypeID").val(); //所选模板ID
    var url;
    if (reportId != null && reportId != "") {
        url = "?FILE_ID=" + reportId + "&EDIT_TYPE=1&FILE_TYPE=.doc&ReportWf=" + $("#ReportStatus").val();
    }
    else {
        if (temId != null && temId != "")
            url = "?File_Type=.doc&Protect=0&EDIT_TYPE=1&Template_Id=" + temId + "&Task_Id=" + taskId;
        else {
            $.ligerDialog.alert("请选择模板！");
            return false;
        }
    }
    if (url != "" && url != null) {
        var sheight = screen.height - 70;
        var swidth = screen.width - 10;
        var winoption = "left=0,top=0,height=" + sheight + ",width=" + swidth + ",toolbar=no,menubar=no,location=no,status=no,scrollbars=no,resizable=yes";
        var ah = screen.availHeight - 30;
        var aw = screen.availWidth - 10;
        var tmp = window.showModalDialog("../../Rpt/Template/FileEdit.aspx" + url, window, 'dialogTop:0px;dialogLeft:0px;dialogWidth:' + aw + 'px;dialogHeight:' + ah + 'px;status:no;'); 
//        var tmp = window.open("../../Rpt/Template/FileEdit.aspx" + url, '', winoption);
        return tmp;
    }
}
function SendSave() {
    //防止多次点击按钮产生多个任务
    $("#divReportSubmit")[0].style.display = "none";
}
///附件下载
function downLoad() {
    $.ligerDialog.open({ title: '附件下载', width: 500, height: 270,
        buttons: [
            {
                text:
                 '关闭', onclick: function (item, dialog) { dialog.close(); }
            }], url: '../../OA/ATT/AttFileDownLoad.aspx?filetype=AcceptanceContract&id=' + strContractId
    });
}
//退回虚拟事件
function BackSend() {
    //防止多次点击按钮产生多个任务
    $("#btnType").val("back");
    $("#divReportSubmit")[0].style.display = "none";
}